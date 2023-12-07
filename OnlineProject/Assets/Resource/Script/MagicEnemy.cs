using MalbersAnimations;
using MalbersAnimations.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
public class MagicEnemy : MonobitEngine.MonoBehaviour
{
    MonobitEngine.MonobitView m_MonobitView = null;

    [SerializeField]
    private int m_Hp;
    [SerializeField]
    private int m_MaxHP;

    [SerializeField]
    private int m_Point;
    [SerializeField]
    private string m_WallTag = "Wall";

    public float m_AttackInterval = 3f;  // 攻撃間隔（秒）
    private float m_RotationSpeed=300;
    private float m_DestroyTime;
    public GameObject m_AttackEffectPrefab;  // 攻撃エフェクトのプレハブ
    public GameObject m_PreAttackEffectPrefab;  // 事前エフェクトのプレハブ

    private GameObject m_PreAttackEffectInstance; // 事前エフェクトのインスタンスを格納する変数
    private List<StageWall> m_StageWall = new List<StageWall>();
    private Transform m_Player;
    [SerializeField]
    private AudioClip m_HitAudio;
    [SerializeField]
    private GameObject m_DestroyEffect;
    [SerializeField]
    private GameObject m_DestroySE;
    [SerializeField]
    private GameObject m_AttackSE;
    [SerializeField]
    private GameObject[] m_ItemPrefabs;
    private Animator m_Animator;
    PlayerMove m_PlayerMove;

    StageWall m_stageWall;

    [SerializeField]
    private AudioClip m_SetAudio;
    [SerializeField]
    private AudioClip m_AttckAudio;
    [SerializeField]
    private ParticleSystem m_FireEffect;
    [SerializeField]
    private float m_FireTime;
    private float m_FCTime;
    private float m_HitCoolTime;
    private bool isFire = false;
    void Awake()
    {
        if (MonobitNetwork.offline == false)
        {
            // すべての親オブジェクトに対して MonobitView コンポーネントを検索する
            if (GetComponentInParent<MonobitEngine.MonobitView>() != null)
            {
                m_MonobitView = GetComponentInParent<MonobitEngine.MonobitView>();
            }
            // 親オブジェクトに存在しない場合、すべての子オブジェクトに対して MonobitView コンポーネントを検索する
            else if (GetComponentInChildren<MonobitEngine.MonobitView>() != null)
            {
                m_MonobitView = GetComponentInChildren<MonobitEngine.MonobitView>();
            }
            // 親子オブジェクトに存在しない場合、自身のオブジェクトに対して MonobitView コンポーネントを検索して設定する
            else
            {
                m_MonobitView = GetComponent<MonobitEngine.MonobitView>();
            }
        }
    }
    private void Start()
    {
        m_FireEffect.Stop();
        GameObject[] stagewalls = GameObject.FindGameObjectsWithTag("StageWall");
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_PlayerMove = m_Player.GetComponent<PlayerMove>();
        m_Animator=GetComponent<Animator>();
        foreach (GameObject stagewall in stagewalls)
        {
            m_stageWall = stagewall.GetComponent<StageWall>();
            if (m_stageWall != null)
            {
                m_StageWall.Add(m_stageWall);
            }

        }
        InvokeRepeating("Attack", 0f, m_AttackInterval);
    }

    private void Update()
    {
        if (m_Hp <= 0)
        {
            m_Animator.SetBool("isDie", true);
            m_DestroyTime += Time.deltaTime;
            m_DestroySE.SetActive(true);

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
        float distanceToPlayer = Vector3.Distance(transform.position, m_Player.position);

        if (distanceToPlayer >= 100f)
        {
            Destroy(gameObject);
        }
        // すべてのプレイヤーオブジェクトを取得
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // 最も近いプレイヤーを見つける
        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance && distance <= 25f)
            {
                closestDistance = distance;
                closestPlayer = player;
                m_Animator.SetBool("isBattle", true);
            }
        }

        // 最も近いプレイヤーが存在する場合、敵の方向をそのプレイヤーの方向に向ける
        if (closestPlayer != null)
        {
            Vector3 targetDirection = closestPlayer.transform.position - transform.position;
            targetDirection.y = 0; // Y軸の回転を無効にする（敵は水平方向に向く）
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), m_RotationSpeed * Time.deltaTime);
        }
        if (isFire == true)
        {
            IsFired();
        }
        if (m_FireTime > 5)
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
        if (m_FCTime > 0.2)
        {
            m_Hp -= 1;
            m_FCTime = 0;
          
        }

    }
    private void ItemSpown()
    {
        int randam = Random.Range(0, 100);
        if (randam < 10 && m_ItemPrefabs.Length > 0)
        {
            Vector3 ItemPos = transform.position;
            ItemPos.y += 1;
            int randamIndex = Random.Range(0, m_ItemPrefabs.Length);
            Instantiate(m_ItemPrefabs[randamIndex], ItemPos, Quaternion.identity);
        }
    }
    private void Attack()
    {
        // すべてのプレイヤーオブジェクトを取得
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
      
        // 最も近いプレイヤーを見つける
        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance && distance <= 25f) 
            {
                closestDistance = distance;
                closestPlayer = player;
                m_Animator.SetBool("isBattle", true);
            }
        }
        // 最も近いプレイヤーの足元から少し上の位置に事前エフェクトを生成する
        if (closestPlayer != null)
        { 
            m_Animator.SetBool("isAttack", true);
            Vector3 preSpawnPosition = closestPlayer.transform.position;
            preSpawnPosition.y += 1.5f; // プレイヤーの位置から1.5の高さに設定

            // 事前エフェクトを生成し、そのインスタンスを変数に保存する
            m_PreAttackEffectInstance = Instantiate(m_PreAttackEffectPrefab, preSpawnPosition, Quaternion.identity);
            AudioSource.PlayClipAtPoint(m_SetAudio, preSpawnPosition);
            // 1秒後に攻撃エフェクトを生成する
            Invoke("SpawnAttackEffect", 1f);
        }
    }

    private void SpawnAttackEffect()
    {
        // 事前エフェクトの位置を取得して攻撃エフェクトを生成する
        if (m_PreAttackEffectInstance != null)
        {
            Vector3 preEffectPosition = m_PreAttackEffectInstance.transform.position;
            preEffectPosition.y = transform.position.y;  

            // エフェクトを生成する際、X軸を270度回転させる
            Quaternion rotation = Quaternion.Euler(270f, 0f, 0f);
            Instantiate(m_AttackEffectPrefab, preEffectPosition, rotation);
            AudioSource.PlayClipAtPoint(m_AttckAudio, preEffectPosition);
            // 攻撃エフェクト生成後、事前エフェクトのインスタンスを破棄する
            Destroy(m_PreAttackEffectInstance);
        }
    }
    public void EndAttackAnimation()
    {
        m_Animator.SetBool("isAttack", false);
      
        m_AttackSE.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            m_Hp -= m_PlayerMove.m_PlayerDamage;
        }
        if (collision.gameObject.CompareTag("ItemBullet"))
        {
            AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            if(MonobitEngine.MonobitNetwork.offline==false)
            {
                m_MonobitView.RPC("HitItemBullet", MonobitEngine.MonobitTargets.All, null);
            }
            else
            {
                HitItemBullet();
            }
        }
    }
    [MunRPC]
    private void HitItemBullet()
    {
        m_Hp -= 40;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            if (MonobitEngine.MonobitNetwork.offline == false)
            {
                m_MonobitView.RPC("IsHit", MonobitEngine.MonobitTargets.All, null);
            }
            else
            {
                IsHit();
            }
            m_Animator.SetBool("isHit", true);
         
        }
        if (other.gameObject.CompareTag("Fire"))
        {
            if (MonobitEngine.MonobitNetwork.offline == false)
            {
                m_MonobitView.RPC("IsHit", MonobitEngine.MonobitTargets.All, null);
            }
            else
            {
                IsHit();
            }
            isFire = true;

            m_Animator.SetBool("isHit", true);

        }
    }
    [MunRPC]
    private void IsHit()
    {
        m_HitCoolTime += Time.deltaTime;
        if (m_HitCoolTime > 0.15)
        {
            m_Hp -= m_PlayerMove.m_PlayerDamage;
            m_HitCoolTime = 0;
            AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);

        }
    }
}
