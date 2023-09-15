using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpown : MonoBehaviour
{
    [SerializeField]
    private string[] playerTags; // スポーンをトリガーするプレイヤータグの配列
    [SerializeField]
    private GameObject[] m_EnemyPrefabs; // スポーンするEnemyのプレハブの配列
    [SerializeField]
    private float m_SpawnDistanceThreshold = 10f; // スポーンをトリガーする距離の閾値
    [SerializeField]
    private float m_MinSpawnInterval = 3f; // 最小スポーン間隔]
    [SerializeField]
    private float m_MaxSpawnInterval = 8f; // 最大スポーン間隔
    [SerializeField]
    private GameObject m_ParticleEffectPrefab; // パーティクルエフェクトのプレハブ
    [SerializeField]
    private float m_SpawnTimer = 0f;
    [SerializeField]
    private Transform m_PlayerTransform;
    [SerializeField]
    private AudioClip m_SpownAudio;
    private float m_Volume=1f;

    private void Start()
    {
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // プレイヤーとの距離を計算
        float distanceToPlayer = Vector3.Distance(transform.position, m_PlayerTransform.position);

        // プレイヤーが一定距離以内にいる場合
        if (distanceToPlayer <= m_SpawnDistanceThreshold)
        {
            // タイマーを更新
            m_SpawnTimer -= Time.deltaTime;

            // スポーン間隔が経過した場合
            if (m_SpawnTimer <= 0f)
            {
                // ランダムなEnemyプレハブを選択
                GameObject randomEnemyPrefab = m_EnemyPrefabs[Random.Range(0, m_EnemyPrefabs.Length)];

                // プレハブをスポーン
                Instantiate(randomEnemyPrefab, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(m_SpownAudio, transform.position,m_Volume);
                // パーティクルエフェクトを再生
                if (m_ParticleEffectPrefab != null)
                {
                    Instantiate(m_ParticleEffectPrefab, transform.position, Quaternion.identity);
                }

                // 新しいスポーン間隔をランダムに設定
                m_SpawnTimer = Random.Range(m_MinSpawnInterval, m_MaxSpawnInterval);
            }
        }
    }
}

