using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private float m_DetectionDistance = 30.0f;
    [SerializeField]
    private float m_AvoidanceDistance = 2.0f;
    [SerializeField]
    private float m_DestroyDistance = 100.0f;
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

    private List<StageWall> m_StageWall = new List<StageWall>();
    [SerializeField]
    private AudioClip m_HitAudio;

    StageWall m_stageWall;
    PlayerMove m_PlayerMove;
    Animator m_Animator;

    [SerializeField]
    private GameObject m_DestroyEffect;
    [SerializeField]
    private GameObject m_DestroySE;
    [SerializeField]
    private GameObject m_AttackSE;
    [SerializeField]
    private GameObject m_ATCol;

    [SerializeField, Header("ランダムに生成するプレハブ")]
    private GameObject[] m_ItemPrefabs;
    private float m_DestroyTime;
    private void Start()
    {
        Hp = m_MaxHp;
        //Sliderを満タンにする。
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
            isAlive = false; // HPが0であることを示すフラグをfalseに設定
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
                Destroy(gameObject);
            }

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
                    m_BattleAicon.SetActive(true);
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
                        transform.Translate(Vector3.forward * m_MoveSpeed * Time.deltaTime);
                    }
                }
            }
            else
            {
                isAvoiding = false;
                m_Animator.SetBool("isBattle", false);
                m_IdleAicon.SetActive(true);
                m_BattleAicon.SetActive(false);
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
    
    }

    private IEnumerator AvoidObstacle()
    {
        Vector3 avoidanceDirection = Quaternion.Euler(0, 45, 0) * transform.forward;
        Vector3 targetPosition = transform.position + avoidanceDirection * m_AvoidanceDistance;

        float startTime = Time.time;
        float duration = 1.0f;

        while (Time.time - startTime < duration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (Time.time - startTime) / duration);
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
        if(randam<10&& m_ItemPrefabs.Length>0)
        {
           Vector3 ItemPos=transform.position;
            ItemPos.y += 1;
            int randamIndex = Random.Range(0, m_ItemPrefabs.Length);
            Instantiate(m_ItemPrefabs[randamIndex], ItemPos, Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            Hp -= m_PlayerMove.m_PlayerDamage;
            mHpSlider.value = (float)Hp / (float)m_MaxHp;
        }
        if (collision.gameObject.CompareTag("ItemBullet"))
        {
            AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            Hp -= 40;
            mHpSlider.value = (float)Hp / (float)m_MaxHp;
        }
    }
}
