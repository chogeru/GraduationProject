using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItem : MonoBehaviour
{
    public Transform playerObject; // プレイヤーオブジェクトの参照
    [SerializeField, Header("武器が消えるときのパーティクル")]
    private GameObject m_WeponDestroyParticle;
    private bool isFloating = true;
    private bool isPlayerGet = false;
    [SerializeField, Header("Destroyまでの時間")]
    private float m_DestroyTime=10;
    private float m_Time;
    [SerializeField, Header("武器所得時のSE")]
    private AudioClip m_WeponGetSE;
    [SerializeField,Header("武器が消えるときのSE")]
    private AudioClip m_WeponDestroySE;
    [SerializeField]
    private float m_Volume=0.6f;
    private void Start()
    {
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
            m_Time += Time.deltaTime;
            if (m_Time > m_DestroyTime)
            {
                AudioSource.PlayClipAtPoint(m_WeponDestroySE, transform.position, m_Volume);
                // パーティクルを生成
                Instantiate(m_WeponDestroyParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            // 敵オブジェクトを検出して方向を向く処理
            DetectAndFaceEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(m_WeponGetSE, transform.position,m_Volume);
            isFloating = false; // 浮かせる処理を停止
            transform.SetParent(playerObject); // プレイヤーオブジェクトの子に設定

            // プレイヤーオブジェクトの方向をオブジェクトにコピー
            transform.forward = playerObject.forward;

            // プレイヤーオブジェクトの右上に位置を固定
            transform.localPosition = new Vector3(0f, 2.3f, 0f);
            isPlayerGet = true;
         //   GetComponent<Collider>().isTrigger = false;
        }
    }
    private void DetectAndFaceEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToEnemy = enemy.transform.position - transform.position;
            float distanceToEnemy = directionToEnemy.magnitude;

            if (distanceToEnemy <= 10f && distanceToEnemy < closestDistance)
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
}
