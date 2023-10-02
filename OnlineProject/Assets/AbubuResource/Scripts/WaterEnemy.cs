using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEnemy : MonoBehaviour
{


    [SerializeField]
    private int Damage;
    private bool isAvoiding;
    [SerializeField]
    private float m_DetectionDistance = 50f;

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
    private float m_AttackTime;
    [SerializeField]
    private float m_ATCoolTime = 10f;
    [SerializeField]
    private GameObject m_DestroySE;
    [SerializeField]
    private GameObject m_DestroyEffect;

    [SerializeField]
    private AudioSource IdleBGM;
    [SerializeField]
    private AudioSource BossBGM;
    private float m_FadeDuration = 5.0f;

    private float m_InitialVolumeIdle;
    private float m_InitialVolumeBoss;
    private float m_Timer;
    private float m_MaxVolume = 0.1f;

    private bool isDie = false;

    [SerializeField]
    private Transform m_Player;
    PlayerMove m_PlayerMove;

    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    [SerializeField]
    private GameObject m_AttackSE;
    [SerializeField]
    private GameObject m_ATCol;
    void Start()
    {
        m_InitialVolumeIdle = IdleBGM.volume;
        m_InitialVolumeBoss = BossBGM.volume;

        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_PlayerMove = m_Player.GetComponent<PlayerMove>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
     

    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;

        if (isDie == false)
        {
            // 1つ目のBGMのボリュームをだんだん下げる
            float fadeOutVolume = Mathf.Lerp(m_InitialVolumeIdle, 0f, m_Timer / m_FadeDuration);
            IdleBGM.volume = Mathf.Max(0f, fadeOutVolume);

            // 2つ目のBGMのボリュームをだんだん上げる
            float fadeInVolume = Mathf.Lerp(0f, m_InitialVolumeBoss, m_Timer / m_FadeDuration);
            BossBGM.volume = Mathf.Min(m_MaxVolume, fadeInVolume);
        }
        Vector3 directionToPlayer = m_Player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= m_DetectionDistance)
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
                transform.Translate(Vector3.forward * m_MoveSpeed * Time.deltaTime);
            }
        }
        else
        {
            isAvoiding = false;
            m_Animator.SetBool("isBattle", false);
        }
        m_AttackTime += Time.deltaTime;
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
        if (isDie)
        {
            m_Animator.SetBool("isDie", true);
            m_DestroyTime += Time.deltaTime;
            m_DestroySE.SetActive(true);
            isDie = true;
            IdleBGM.volume = 0.1f;
            if (m_DestroyTime >= 1.4)
            {
                Instantiate(m_DestroyEffect, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }

        }
    }
    private IEnumerator AvoidObstacle()
    {
        Vector3 avoidanceDirection = Quaternion.Euler(0, 360, 0) * transform.forward;
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
        m_MoveSpeed = 0;
        m_ATCol.SetActive(true);
        m_Animator.SetBool("isAttack", true);
        m_AttackTime = 0;
    }
    // アニメーションイベントから呼び出される関数
    public void EndAttackAnimation()
    {
        m_MoveSpeed = m_DefoltSpeed;
        m_Animator.SetBool("isAttack", false);
        m_ATCol.SetActive(false);
        m_AttackSE.SetActive(false);
    }
    
}
