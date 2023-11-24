using MalbersAnimations.Controller;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;
public class BossEnemy : MonoBehaviour
{
    [SerializeField]
    private int m_MaxHp;

    [SerializeField]
    private int m_Hp;

    [SerializeField]
    private int Damage;

    [SerializeField]
    private int m_Point;

    private bool isAvoiding;

    [SerializeField]
    private float m_DetectionDistance = 30f;
    [SerializeField]
    private float m_AvoidanceDistance = 2f;
    [SerializeField]
    private float m_PlayerMinDistance=8.0f;
    [SerializeField]
    private float m_RotationSpeed = 5f;
    [SerializeField]
    private float m_MoveSpeed = 5;
    private float m_DefoltSpeed = 5;
    private float m_DestroyTime;
    [SerializeField, Header("攻撃距離")]
    private float m_AttackRange = 4;
    [SerializeField]
    private float m_AttackTime;
    [SerializeField]
    private float m_ATCoolTime = 10f;
    [SerializeField]
    private GameObject m_DestroySE;
    [SerializeField]
    private GameObject m_DestroyEffect;

    [SerializeField]
    private GameObject m_ActiveFloer;

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
    private float m_HitCoolTime;
    private bool isDie = false;
    [SerializeField, Header("最後のボスかどうか")]
    private bool isLastBoss = false;
    private bool isPoint = false;

    [SerializeField]
    private Transform m_Player;
    PlayerMove m_PlayerMove;
    [SerializeField]
    CoopGameManager m_CoopGameManager;
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private Rigidbody m_Rigidbody;
    [SerializeField]
    private GameObject m_AttackSE;
    [SerializeField]
    private GameObject m_ATCol;
    [SerializeField]
    private GameObject m_Wall;
    void Start()
    {
        m_Hp = m_MaxHp;
        mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
        m_InitialVolumeIdle = IdleBGM.volume;
        m_InitialVolumeBoss = BossBGM.volume;
        m_Wall.SetActive(false);
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_PlayerMove = m_Player.GetComponent<PlayerMove>();
        m_ActiveFloer.SetActive(false);
        BossBGM.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;

        Vector3 directionToPlayer = m_Player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= m_DetectionDistance)
        {
            BattleStart();
            BossBGMFadeIN();
            if (!isAvoiding)
            {
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
          // m_BossHoGage.SetActive(false);

        }
        m_AttackTime += Time.deltaTime;
        // プレイヤーが一定の距離内にいる場合
        if (distanceToPlayer <= m_AttackRange && m_AttackTime >= m_ATCoolTime)
        {
            AttackPlayer();
        }
        if (distanceToPlayer > m_AttackRange)
        {
            AttackStop();
        }
        if (m_Hp <= 0)
        {
            BossDie();
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
    private void BattleStart()
    {
        BossBGM.gameObject.SetActive(true);
        m_BossHoGage.SetActive(true);
    }
    private void AttackPlayer()
    {
      
        m_ATCol.SetActive(true);
        m_Animator.SetBool("isAttack", true);
        m_AttackTime = 0;
    }
    private void AttackStop()
    {
        m_ATCol.SetActive(false);
        m_AttackSE.SetActive(false);
        m_AttackTime = 0;
        m_Animator.SetBool("isAttack", false);
    }
    // アニメーションイベントから呼び出される関数
    public void EndAttackAnimation()
    {
        m_Animator.SetBool("isAttack", false);
        m_ATCol.SetActive(false);
        m_AttackSE.SetActive(false);
    }
    private void BossDie()
    {
        m_Animator.SetBool("isDie", true);
        m_DestroyTime += Time.deltaTime;
        m_DestroySE.SetActive(true);

        isDie = true;
        IdleBGM.volume = 0.1f;
        if (m_CoopGameManager != null)
        {
            m_CoopGameManager.GameClear();
        }
        if (m_DestroyTime >= 1.4)
        {
            if (m_CoopGameManager != null)
            {
                m_CoopGameManager.isClear = true;
            }

            m_MoveSpeed = 0;
            m_RotationSpeed = 0;
            if (m_Wall != null)
            {
                m_Wall.SetActive(true);
            }

            if (isLastBoss && isPoint == false)
            {
                CoopScoreManager.AddScore(m_Point);
                isPoint = true;
            }
            if (isLastBoss == false)
            {
                m_BossHoGage.SetActive(false);
                Instantiate(m_DestroyEffect, transform.position, Quaternion.identity);
                CoopScoreManager.AddScore(m_Point);
                Destroy(gameObject);
            }
            if (m_ActiveFloer != null)
            {
                m_ActiveFloer.SetActive(true);
            }
        }

    }
    private void BossBGMFadeIN()
    {
        // 1つ目のBGMのボリュームをだんだん下げる
        float fadeOutVolume = Mathf.Lerp(m_InitialVolumeIdle, 0f, m_Timer / m_FadeDuration);
        IdleBGM.volume = Mathf.Max(0f, fadeOutVolume);

        // 2つ目のBGMのボリュームをだんだん上げる
        float fadeInVolume = Mathf.Lerp(0f, m_InitialVolumeBoss, m_Timer / m_FadeDuration);
        BossBGM.volume = Mathf.Min(m_MaxVolume, fadeInVolume);
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
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
           // AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            IsHit();
            m_Animator.SetBool("isHit", true);
            m_Hp -= m_PlayerMove.m_PlayerDamage;
            mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
        }
        if (other.gameObject.CompareTag("Fire"))
        {
            // AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            IsHit();
            m_Animator.SetBool("isHit", true);
            m_Hp -= m_PlayerMove.m_PlayerDamage;
            mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
        }

    }
    public void ParticleDamage()
    {
        m_Hp -= 15;
        mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
    }
    private void IsHit()
    {
        m_HitCoolTime += Time.deltaTime;
        if (m_HitCoolTime > 0.05)
        {
            m_Hp -= m_PlayerMove.m_PlayerDamage;
            m_HitCoolTime = 0;
        }
    }
}
