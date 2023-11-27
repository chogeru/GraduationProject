using Monobit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EnemyMove : MonoBehaviour
{

    [SerializeField]
    private string m_PlayerTag = "Player";
    [SerializeField]
    private string m_WallTag = "Wall";
    [SerializeField]
    private float m_MoveSpeed = 5.0f;
    [SerializeField]
    private float m_DownSpeed;
    [SerializeField]
    private float m_DetectionDistance = 30.0f;
    [SerializeField]
    private float m_AvoidanceDistance = 0f;
    [SerializeField, Header("Playerとの最小距離間隔")]
    private float m_PlayerMinDistance;
    [SerializeField]
    private float m_DestroyDistance = 100.0f;
    [SerializeField]
    private float m_HpBarAnimeDistance = 10f;
    [SerializeField, Header("攻撃距離")]
    private float m_AttackRange = 4;
    [SerializeField]
    private float m_RotationSpeed = 5.0f;
    [SerializeField]
    private float m_AttackTime;
    [SerializeField]
    private float m_ATCoolTime = 10f;

    private Transform m_Player;
    private bool isAvoiding = false;
    // HPが0でないことを示すフラグ
    private bool isAlive = true;

    [SerializeField]
    private Slider mHpSlider;

    [SerializeField, Header("通常時のアイコン")]
    private GameObject m_IdleAicon;
    [SerializeField, Header("戦闘時のアイコン")]
    private GameObject m_BattleAicon;

    [SerializeField, Header("最大体力")]
    private int m_MaxHp;
    [SerializeField, Header("現在の体力")]
    private int Hp;
    [SerializeField, Header("攻撃力")]
    private int m_Damage;

    [SerializeField]
    private int m_Point;
    private List<StageWall> m_StageWall = new List<StageWall>();

    [SerializeField]
    private AudioClip m_HitAudio;

    StageWall m_stageWall;
    PlayerMove m_PlayerMove;
    Animator m_Animator;
    [SerializeField]
    Animator m_HpBarAnimator;

    [SerializeField]
    private GameObject m_DestroyEffect;
    [SerializeField]
    private GameObject m_DestroySE;
    [SerializeField]
    private GameObject m_AttackSE;
    [SerializeField]
    private GameObject m_ATCol;

    private float m_HitCoolTime;
    [SerializeField, Header("ランダムに生成するプレハブ")]
    private GameObject[] m_ItemPrefabs;
    private float m_DestroyTime;

    //
    [SerializeField]
    private ParticleSystem m_FireEffect;
    [SerializeField]
    private float m_FireTime;
    private float m_FCTime;
    private bool isFire = false;


    private bool isIce=false;
    private float m_IceTime;
    private float m_SetMoveSpeed;
    private SkinnedMeshRenderer[] childRenderers;
    private Material[] originalMaterials;
    private void Start()
    {
        childRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        // 子オブジェクトのSkinnedMeshRendererの初期マテリアルを取得
        List<Material> materialsList = new List<Material>();
        foreach (var renderer in childRenderers)
        {
            materialsList.AddRange(renderer.sharedMaterials);
        }
        originalMaterials = materialsList.ToArray();

        m_SetMoveSpeed = m_MoveSpeed;
        m_FireEffect.Stop();
        Hp = m_MaxHp;
        mHpSlider.value = 1;
        GameObject[] stagewalls = GameObject.FindGameObjectsWithTag("StageWall");
        m_Player = GameObject.FindGameObjectWithTag(m_PlayerTag).transform;
        m_PlayerMove = m_Player.GetComponent<PlayerMove>();
        m_Animator = GetComponent<Animator>();
        m_IdleAicon.SetActive(true);
        m_BattleAicon.SetActive(false);
        m_DestroySE.SetActive(false);
        m_ATCol.SetActive(false);
        foreach (GameObject stagewall in stagewalls)
        {
            m_stageWall = stagewall.GetComponent<StageWall>();
            if (m_stageWall != null)
            {
                m_StageWall.Add(m_stageWall);
            }

        }
    }

    private void Update()
    {
        
      
        if (Hp <= 0)
        {
            isAlive = false;
            m_Animator.SetBool("isDie", true);
            m_DestroyTime += Time.deltaTime;
            m_DestroySE.SetActive(true);
            m_MoveSpeed = 0;
            m_RotationSpeed = 0;
            if (m_DestroyTime >= 1.4)
            {
                Instantiate(m_DestroyEffect, transform.position, Quaternion.identity);
                ItemSpown();
                if (m_stageWall != null)
                {
                    m_stageWall.m_DieCount++;
                }
                CoopScoreManager.AddScore(m_Point);
                Destroy(gameObject);
            }

        }
        if(m_IceTime>3)
        {
            isIce = false;
            m_MoveSpeed = m_SetMoveSpeed;
            m_Animator.speed = 1f;
        }
        if (!isIce)
        {
            // マテリアルを初期状態に戻す
            foreach (var renderer in childRenderers)
            {
                renderer.sharedMaterials = originalMaterials;
            }
        }
        if (isIce == true)
        {
            m_MoveSpeed = 0;
            m_Animator.speed = 0f;
            m_IceTime += Time.deltaTime;
            return;
        }
        Vector3 directionToPlayer = m_Player.position - transform.position;

        float distanceToPlayer = directionToPlayer.magnitude;
        // HPが0の場合は何もしない
        if (!isAlive)
        {
            return;
        }
        if (distanceToPlayer <= m_DetectionDistance)
        {
            if (!isAvoiding)
            {

                m_IdleAicon.SetActive(false);
                m_Animator.SetBool("isBattle", true);
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);

                if (Physics.Raycast(transform.position, transform.forward, m_AvoidanceDistance))
                {
                    isAvoiding = true;
                    StartCoroutine(AvoidObstacle());
                }
                else
                {
                    if (distanceToPlayer >= m_PlayerMinDistance)
                    {
                        transform.Translate(Vector3.forward * m_MoveSpeed * Time.deltaTime);
                    }
                }
            }
        }
        else
        {
            isAvoiding = false;
            m_Animator.SetBool("isBattle", false);
            m_IdleAicon.SetActive(true);
        }
        if (distanceToPlayer <= m_HpBarAnimeDistance)
        {
            m_HpBarAnimator.SetBool("isActiveBar", true);
        }
        else
        {
            m_HpBarAnimator.SetBool("isActiveBar", false);
        }
        m_AttackTime += Time.deltaTime;
        // プレイヤーとの距離が100メートル以上になったら自身を破壊
        if (distanceToPlayer > m_DestroyDistance)
        {
            Instantiate(m_DestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        // プレイヤーが一定の距離内にいる場合
        if (distanceToPlayer <= m_AttackRange && m_AttackTime >= m_ATCoolTime)
        {
            AttackPlayer();
        }
        if (distanceToPlayer > m_AttackRange)
        {
            m_ATCol.SetActive(false);
            m_AttackSE.SetActive(false);
            m_AttackTime = 0;
            m_Animator.SetBool("isAttack", false);
        }
        if(isFire==true)
        {
            IsFired();
        }
        if(m_FireTime>5)
        {
            isFire = false;
            m_FireTime = 0;
            m_FireEffect.Stop();
        }
      
    }
    private void IsFired()
    {
    
        m_FCTime += Time.deltaTime;
        m_FireTime += Time.deltaTime;
        m_FireEffect.Play();
        if(m_FCTime>0.2)
        {
            Hp -= 1;
            m_FCTime = 0;
            HpSliderUpdate();
        }
        
    }
    private IEnumerator AvoidObstacle()
    {
        Vector3 avoidanceDirection = Quaternion.Euler(0, 45, 0) * transform.forward;
        Vector3 targetPosition = transform.position + avoidanceDirection * m_AvoidanceDistance;
        Vector3 currentVelocity = Vector3.zero;

        float smoothTime = 0.3f; // スムーズさを調整するためのパラメータ

        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
            yield return null;
        }
        isAvoiding = false;
    }
    private void AttackPlayer()
    {
        m_ATCol.SetActive(true);
        m_Animator.SetBool("isAttack", true);
        m_AttackTime = 0;
    }
    // アニメーションイベントから呼び出される関数
    public void EndAttackAnimation()
    {
        m_Animator.SetBool("isAttack", false);
        m_ATCol.SetActive(false);
        m_AttackSE.SetActive(false);
    }
    private void ItemSpown()
    {
        int randam = Random.Range(0, 100);
        if (randam < 30 && m_ItemPrefabs.Length > 0)
        {
            Vector3 ItemPos = transform.position;
            ItemPos.y += 1;
            int randamIndex = Random.Range(0, m_ItemPrefabs.Length);
            Instantiate(m_ItemPrefabs[randamIndex], ItemPos, Quaternion.identity);
        }
    }
    public void ParticleDamage()
    {
        Hp -= 5;
        HpSliderUpdate();

    }

    public void EndAnimationHit()
    {
        m_Animator.SetBool("isHit", false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            Hp -= m_PlayerMove.m_PlayerDamage;
          
            m_Animator.SetBool("isHit", true);
            HpSliderUpdate();
        }
        if (collision.gameObject.CompareTag("ItemBullet"))
        {
            AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            Hp -= 40;
            HpSliderUpdate();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            InWater();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            BurretIsHit();
            m_Animator.SetBool("isHit", true);
            HpSliderUpdate();
        }
        if (other.gameObject.CompareTag("Fire"))
        {
            isFire = true;
            m_FireEffect.Play();
            FireIsHit();
            m_Animator.SetBool("isHit", true);
            HpSliderUpdate();
           
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            OutWater();
        }
    }
    private void FireIsHit()
    {
        m_HitCoolTime += Time.deltaTime;
        if (m_HitCoolTime > 0.15)
        {
            Hp -= m_PlayerMove.m_PlayerDamage;
            m_HitCoolTime = 0;
            AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);

        }
    }
    private void BurretIsHit()
    {
        m_HitCoolTime += Time.deltaTime;
        if (m_HitCoolTime > 0.01)
        {
            Hp -= m_PlayerMove.m_PlayerDamage;
            m_HitCoolTime = 0;
            AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);

        }
    }
    public void SetIsIce(bool value)
    {
        isIce = value;
    }
    private void HpSliderUpdate()
    {
        mHpSlider.value = (float)Hp / (float)m_MaxHp;
    }
    private void InWater()
    {
        m_MoveSpeed -= m_DownSpeed;
    }
    private void OutWater()
    {
        m_MoveSpeed += m_DownSpeed;
    }
  
}
