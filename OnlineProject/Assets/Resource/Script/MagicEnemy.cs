using MalbersAnimations;
using MalbersAnimations.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEnemy : MonoBehaviour
{
    [SerializeField]
    private int m_Hp;
    [SerializeField]
    private int m_MaxHP;


    public float attackInterval = 5f;  // 攻撃間隔（秒）
    private float m_DestroyTime;
    public GameObject attackEffectPrefab;  // 攻撃エフェクトのプレハブ
    public GameObject preAttackEffectPrefab;  // 事前エフェクトのプレハブ

    private GameObject preAttackEffectInstance; // 事前エフェクトのインスタンスを格納する変数
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
    private Animator m_Animator;
    PlayerMove m_PlayerMove;
    StageWall m_stageWall;

    private void Start()
    {
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
        InvokeRepeating("Attack", 0f, attackInterval);
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
                if (m_stageWall != null)
                {
                    m_stageWall.m_DieCount++;
                }
                Destroy(gameObject);
            }

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
            preSpawnPosition.y += 1.5f; // プレイヤーの位置から0.5の高さに設定

            // 事前エフェクトを生成し、そのインスタンスを変数に保存する
            preAttackEffectInstance = Instantiate(preAttackEffectPrefab, preSpawnPosition, Quaternion.identity);

            // 1秒後に攻撃エフェクトを生成する
            Invoke("SpawnAttackEffect", 1f);
        }
    }

    private void SpawnAttackEffect()
    {
        // 事前エフェクトの位置を取得して攻撃エフェクトを生成する
        if (preAttackEffectInstance != null)
        {
            Vector3 preEffectPosition = preAttackEffectInstance.transform.position;
            preEffectPosition.y = transform.position.y;  // Y座標を敵の高さに合わせる

            // エフェクトを生成する際、X軸を270度回転させる
            Quaternion rotation = Quaternion.Euler(270f, 0f, 0f);
            Instantiate(attackEffectPrefab, preEffectPosition, rotation);

            // 攻撃エフェクト生成後、事前エフェクトのインスタンスを破棄する
            Destroy(preAttackEffectInstance);
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
            m_Hp -= 40;
        }
    }
}
