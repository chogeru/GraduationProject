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
    private bool avoiding = false;

    [SerializeField,Header("戦闘開始時のアイコン")]
    private GameObject m_StAicBattle;
    private float m_AiconDestroyTime;

    [SerializeField, Header("最大体力")]
    private int m_MaxHp;
    [SerializeField,Header("現在の体力")]
    private int m_Hp;
    [SerializeField,Header("攻撃力")]
    private int m_Damage;

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
            if (!avoiding)
            {
                m_AiconDestroyTime += Time.deltaTime;
                m_StAicBattle.SetActive(true);
                m_Animator.SetBool("isBattle", true);
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);

                if (Physics.Raycast(transform.position, transform.forward, m_AvoidanceDistance))
                {
                    // Wall is detected in front, start avoiding
                    avoiding = true;
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
            avoiding = false;
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
        avoiding = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
           m_Hp -= m_PlayerMove.m_PlayerDamage;
        }
    }
}
