using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpown : MonoBehaviour
{
    public string[] playerTags; // スポーンをトリガーするプレイヤータグの配列
    public GameObject[] enemyPrefabs; // スポーンするEnemyのプレハブの配列
    public float spawnDistanceThreshold = 10f; // スポーンをトリガーする距離の閾値
    public float minSpawnInterval = 3f; // 最小スポーン間隔
    public float maxSpawnInterval = 8f; // 最大スポーン間隔
    public GameObject particleEffectPrefab; // パーティクルエフェクトのプレハブ
    [SerializeField]
    private float spawnTimer = 0f;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private AudioClip m_SpownAudio;
    private float m_Volume=1f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // プレイヤーとの距離を計算
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // プレイヤーが一定距離以内にいる場合
        if (distanceToPlayer <= spawnDistanceThreshold)
        {
            // タイマーを更新
            spawnTimer -= Time.deltaTime;

            // スポーン間隔が経過した場合
            if (spawnTimer <= 0f)
            {
                // ランダムなEnemyプレハブを選択
                GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

                // プレハブをスポーン
                Instantiate(randomEnemyPrefab, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(m_SpownAudio, transform.position,m_Volume);
                // パーティクルエフェクトを再生
                if (particleEffectPrefab != null)
                {
                    Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
                }

                // 新しいスポーン間隔をランダムに設定
                spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }
    }
}

