using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
public class AttackItem : MonobitEngine.MonoBehaviour
{
    MonobitEngine.MonobitView m_MonobitView = null;

    [SerializeField,Header("プレイヤーオブジェクト")]
    private Transform m_Player; 
    [SerializeField, Header("発射位置")]
    private Transform m_FirePoint;
    [SerializeField, Header("武器が消えるときのパーティクル")]
    private GameObject m_WeponDestroyParticle;
    [SerializeField, Header("弾のパーティクル")]
    private GameObject m_BulletParticle;
    [SerializeField, Header("弾のプレハブ")]
    private GameObject m_BulletPrefab;
    private bool isFloating = true;
    private bool isPlayerGet = false;
    [SerializeField, Header("弾速")]
    private float m_BulletSpeed=10;
    [SerializeField, Header("Destroyまでの時間")]
    private float m_DestroyTime=10;
    private float m_Time;
    [SerializeField,Header("弾の発射SE")]
    private AudioClip m_FireAudioSource;
    [SerializeField, Header("武器所得時のSE")]
    private AudioClip m_WeponGetSE;
    [SerializeField,Header("武器が消えるときのSE")]
    private AudioClip m_WeponDestroySE;
    [SerializeField]
    private float m_Volume=0.6f;
    [SerializeField]
    private float m_BulletCoolTime = 3;
    private float m_FireTime;
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

        m_Player = GameObject.FindGameObjectWithTag("Player").transform;

        m_BulletParticle.SetActive(false);
        isPlayerGet = false;
        if (isFloating)
        {
            // オブジェクトを浮かせる処理
            transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
        }
    }
    private void Update()
    {
        if (isFloating)
        {
            // オブジェクトを回転させる処理
            transform.Rotate(Vector3.up * 360f * Time.deltaTime);
        }
        if (isPlayerGet)
        {
            m_FireTime += Time.deltaTime;
            if (m_BulletCoolTime<m_FireTime)
            {
                if (MonobitEngine.MonobitNetwork.offline == false)
                {
                    m_MonobitView.RPC("RoketFire", MonobitEngine.MonobitTargets.All, null);
                }
                else
                {
                    RoketFire();
                }
                m_FireTime = 0;
            }
            m_Time += Time.deltaTime;
            if (m_Time > m_DestroyTime)
            {
                AudioSource.PlayClipAtPoint(m_WeponDestroySE, transform.position, m_Volume);
                // パーティクルを生成
                Instantiate(m_WeponDestroyParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            if(MonobitEngine.MonobitNetwork.offline==false)
            {
                m_MonobitView.RPC("DetectAndFaceEnemy",MonobitEngine.MonobitTargets.All, null);
            }
            else
            {
                // 敵オブジェクトを検出して方向を向く処理
                DetectAndFaceEnemy();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
         m_MonobitView.RPC("SetItem", MonobitEngine.MonobitTargets.All, null);
        }
    }
    [MunRPC]
    private void SetItem()
    {
        m_BulletParticle.SetActive(true);
        AudioSource.PlayClipAtPoint(m_WeponGetSE, transform.position, m_Volume);
        isFloating = false; // 浮かせる処理を停止
        transform.SetParent(m_Player); // プレイヤーオブジェクトの子に設定

        // プレイヤーオブジェクトの方向をオブジェクトにコピー
        transform.forward = m_Player.forward;

        // プレイヤーオブジェクトの右上に位置を固定
        transform.localPosition = new Vector3(0f, 2.3f, 0f);
        isPlayerGet = true;
    }
    [MunRPC]
    private void DetectAndFaceEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToEnemy = enemy.transform.position - transform.position;
            float distanceToEnemy = directionToEnemy.magnitude;

            if (distanceToEnemy <= 300f && distanceToEnemy < closestDistance)
            {
                closestEnemy = enemy.transform;
                closestDistance = distanceToEnemy;
            }
        }

        if (closestEnemy != null)
        {
            Vector3 directionToClosestEnemy = closestEnemy.position - transform.position;
            directionToClosestEnemy.y = 0; // Y軸方向の回転を無効化
            Quaternion lookRotation = Quaternion.LookRotation(directionToClosestEnemy);
            transform.rotation = lookRotation;
        }
    }
    [MunRPC]
    private void RoketFire()
    {
        AudioSource.PlayClipAtPoint(m_FireAudioSource, transform.position, m_Volume);
        // 球のプレハブから新しい球を生成
        GameObject bullet = Instantiate(m_BulletPrefab, m_FirePoint.position, m_FirePoint.rotation);
     
        // 球の Rigidbody コンポーネントを取得
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            // 球をまっすぐ前に発射する力を加える
            bulletRigidbody.velocity = m_FirePoint.forward * m_BulletSpeed;
        }
    }
}
