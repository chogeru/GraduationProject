using MalbersAnimations.Controller;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [SerializeField]
    private int m_MaxHp;
    private int m_MinHp;
    [SerializeField]
    private int m_Hp;

    [SerializeField]
    private int Damage;

    private bool isAvoiding;

    [SerializeField]
    private float m_DetectionDistance = 30f;
    
    [SerializeField]
    private float m_AvoidanceDistance = 2f;
    [SerializeField]
    private float m_RotationSpeed = 5f;
    private float m_MoveSpeed = 5;
    private float m_DestroyTime;

    [SerializeField]
    private GameObject m_DestroySE;
    [SerializeField]
    private GameObject m_DestroyEffect;

    [SerializeField]
    private GameObject m_ActiveFloer;

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

    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    void Start()
    {
        m_InitialVolumeIdle = IdleBGM.volume;
        m_InitialVolumeBoss = BossBGM.volume;

        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_PlayerMove = m_Player.GetComponent<PlayerMove>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        m_ActiveFloer.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;

        // 1つ目のBGMのボリュームをだんだん下げる
        float fadeOutVolume = Mathf.Lerp(m_InitialVolumeIdle, 0f, m_Timer / m_FadeDuration);
        IdleBGM.volume = Mathf.Max(0f, fadeOutVolume);

        // 2つ目のBGMのボリュームをだんだん上げる
        float fadeInVolume = Mathf.Lerp(0f, m_MaxVolume, m_Timer / m_FadeDuration);
        BossBGM.volume = Mathf.Min(m_MaxVolume, fadeInVolume);

        // フェードが完了したらリセット
        if (m_Timer >= m_FadeDuration)
        {
            m_Timer = 0f;
            // 1つ目のBGMを停止
            IdleBGM.Stop();
            // 2つ目のBGMを再生
            BossBGM.Play();
       
        }
        Vector3 directionToPlayer = m_Player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= m_DetectionDistance)
        {
          
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
                    transform.Translate(Vector3.forward * m_MoveSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            isAvoiding = false;
            m_Animator.SetBool("isBattle", false);

        }
       
        if (m_Hp <= 0)
        {
            m_Animator.SetBool("isDie", true);
            m_DestroyTime += Time.deltaTime;
            m_DestroySE.SetActive(true);
            if (m_DestroyTime >= 1.4)
            {
                Instantiate(m_DestroyEffect, transform.position, Quaternion.identity);

                Destroy(gameObject);
                m_ActiveFloer.SetActive(true);
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //     AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            m_Hp -= m_PlayerMove.m_PlayerDamage;
        }
    }
}
