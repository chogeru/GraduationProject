using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaterEnemy : MonoBehaviour
{
    [SerializeField]
    private int Damage;

    [SerializeField]
    private int m_Hp;
    [SerializeField]
    private int m_MaxHp;

    [SerializeField]
    private int m_Point;

    public int m_KillCount;
    private bool isAvoiding;
    [SerializeField]
    private float m_DetectionDistance = 50f;

    [SerializeField]
    private float m_ShotCooldown = 10f;
    private float m_CurrentCooldown = 0f;
    [SerializeField]
    private float projectileSpeed = 10f; 

    [SerializeField]
    private float m_AvoidanceDistance = 2f;
    [SerializeField]
    private float m_RotationSpeed = 5f;
    private float m_MoveSpeed = 5;
    private float m_DefoltSpeed = 5;
    private float m_DestroyTime;
    [SerializeField, Header("攻撃距離")]
    private float m_AttackRange = 50;

    [SerializeField]
    private GameObject m_DestroySE;
    [SerializeField]
    private GameObject m_DestroyEffect;
    [SerializeField]
    private GameObject m_BossHoGage;
    [SerializeField]
    private Slider mHpSlider;
    [SerializeField]
    private AudioSource IdleBGM;
    [SerializeField]
    private AudioSource BossBGM;
    private float m_FadeDuration = 5.0f;

    private float m_InitialVolumeIdle;
    private float m_InitialVolumeBoss;
    private float m_Timer;
    private float m_MaxVolume = 0.1f;


    [SerializeField]
    private Transform m_Player;

    PlayerMove m_PlayerMove;
    [SerializeField]
    AttackObj m_AttackObj;
    [SerializeField]
    private Transform m_ShotPoint;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    [SerializeField]
    private GameObject m_AttackSE;
    [SerializeField]
    private GameObject m_ATCol;
    [SerializeField]
    private GameObject m_Shot;
    [SerializeField]
    private GameObject m_ChrgeShot;

    void Start()
    {
        m_InitialVolumeIdle = IdleBGM.volume;
        m_InitialVolumeBoss = BossBGM.volume;
        BossBGM.gameObject.SetActive(false);
        m_Hp = m_MaxHp;
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_PlayerMove = m_Player.GetComponent<PlayerMove>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;

        
        Vector3 directionToPlayer = m_Player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= m_DetectionDistance)
        {
            
            m_BossHoGage.SetActive(true);
            m_Animator.SetBool("isBattle", true);
            if (m_Hp<=0)
            {
                BossBGM.gameObject.SetActive(true);
                // 1つ目のBGMのボリュームをだんだん下げる
                float fadeOutVolume = Mathf.Lerp(m_InitialVolumeIdle, 0f, m_Timer / m_FadeDuration);
                IdleBGM.volume = Mathf.Max(0f, fadeOutVolume);

                // 2つ目のBGMのボリュームをだんだん上げる
                float fadeInVolume = Mathf.Lerp(0f, m_InitialVolumeBoss, m_Timer / m_FadeDuration);
                BossBGM.volume = Mathf.Min(m_MaxVolume, fadeInVolume);
            }
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);
            // ゲームオブジェクト本体の回転は変更しない
         //   transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        }
        else
        {
            isAvoiding = false;
            m_Animator.SetBool("isBattle", false);
        }
      
       
        if(m_Hp <= 0) 
        {
            m_Animator.SetBool("isDie", true);
            m_DestroyTime += Time.deltaTime;
            m_DestroySE.SetActive(true);
            IdleBGM.volume = 0.1f;
    
            if (m_DestroyTime >= 1.4)
            {          
                m_BossHoGage.SetActive(false);
                m_AttackObj.m_KillCount++;
                Instantiate(m_DestroyEffect, transform.position, Quaternion.identity);
                CoopScoreManager.AddScore(m_Point);
                Destroy(gameObject);
            }

        }
        // プレイヤーが一定の距離内にいる場合
        if (distanceToPlayer <= m_AttackRange)
        {
            if (m_CurrentCooldown <= 0f)
            {
                ShootRandomProjectile();
                // クールダウンをリセット
                m_CurrentCooldown = m_ShotCooldown;
            }
            else
            {
                // クールダウン時間を減らす
                m_CurrentCooldown -= Time.deltaTime;
            }
        }
        if (distanceToPlayer > m_AttackRange)
        {
            m_ATCol.SetActive(false);
            m_AttackSE.SetActive(false);
            m_Animator.SetBool("isAttack", false);
        }
    }
    private void ShootRandomProjectile()
    {
        // 3つの弾のうちランダムに1つを選ぶ
        int randomProjectileIndex = Random.Range(0, 2);
       
        // ランダムな弾を生成し、プレイヤーに向かって発射する処理を実装する
        GameObject projectile = null;
        switch (randomProjectileIndex)
        {
            case 0:
                m_Animator.SetBool("isAttack", true);
                projectile = Instantiate(m_Shot, m_ShotPoint.position, Quaternion.identity);
                break;
            case 1:
                m_Animator.SetBool("isChrgeShot", true);
                projectile = Instantiate(m_ChrgeShot, m_ShotPoint.position, Quaternion.identity);
                break;
        }

        // プレイヤーの方向を向く
        if (projectile != null)
        {
            Vector3 directionToPlayer = (m_Player.position - m_ShotPoint.position).normalized;
            projectile.GetComponent<Rigidbody>().velocity = directionToPlayer * projectileSpeed;

            // m_ShotPoint をプレイヤーの方向に向ける
            Vector3 directionToPlayerWithoutY = new Vector3(directionToPlayer.x, 0f, directionToPlayer.z);
            if (directionToPlayerWithoutY != Vector3.zero)
            {
                m_ShotPoint.rotation = Quaternion.LookRotation(directionToPlayerWithoutY);
            }
        }
    }
 
    // アニメーションイベントから呼び出される関数
    public void EndAttackAnimation()
    {
        m_MoveSpeed = m_DefoltSpeed;
        m_Animator.SetBool("isChrgeShot", false);
        m_Animator.SetBool("isAttack", false);
        m_ATCol.SetActive(false);
        m_AttackSE.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //     AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            m_Hp -= m_PlayerMove.m_PlayerDamage;
            mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
        }
        if (collision.gameObject.CompareTag("ItemBullet"))
        {
            //AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            m_Hp -= 40;
            mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
        }
    }
}
