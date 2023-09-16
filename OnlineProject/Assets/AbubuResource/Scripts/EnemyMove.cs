using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float m_RotationSpeed = 5.0f;

    private Transform m_Player;
    private bool isAvoiding = false;

    [SerializeField,Header("戦闘開始時のアイコン")]
    private GameObject m_StAicBattle;
    private float m_AiconDestroyTime;

    [SerializeField, Header("最大体力")]
    private int m_MaxHp;
    [SerializeField,Header("現在の体力")]
    private int m_Hp;
    [SerializeField,Header("攻撃力")]
    private int m_Damage;

    [SerializeField]
    private AudioClip m_HitAudio;

    PlayerMove m_PlayerMove;
    Animator m_Animator;
    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag(m_PlayerTag).transform;
        m_PlayerMove=m_Player.GetComponent<PlayerMove>();
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 directionToPlayer = m_Player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= m_DetectionDistance)
        {
            if (!isAvoiding)
            {
                m_AiconDestroyTime += Time.deltaTime;
                m_StAicBattle.SetActive(true);
                m_Animator.SetBool("isBattle", true);
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);

                if (Physics.Raycast(transform.position, transform.forward, m_AvoidanceDistance))
                {
                    // Wall is detected in front, start avoiding
                    isAvoiding = true;
                    StartCoroutine(AvoidObstacle());
                }
                else
                {
                    // Move forward
                    transform.Translate(Vector3.forward * m_MoveSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            // Stop avoiding if player is not nearby
            isAvoiding = false;
            m_Animator.SetBool("isBattle", false);
        }
        if(m_Hp<=0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator AvoidObstacle()
    {
        Vector3 avoidanceDirection = Quaternion.Euler(0, 45, 0) * transform.forward; // Rotate by 45 degrees
        Vector3 targetPosition = transform.position + avoidanceDirection * m_AvoidanceDistance;

        float startTime = Time.time;
        float duration = 1.0f; // Avoidance duration

        while (Time.time - startTime < duration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (Time.time - startTime) / duration);
            yield return null;
        }
        isAvoiding = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
           m_Hp -= m_PlayerMove.m_PlayerDamage;
        }
    }
}
