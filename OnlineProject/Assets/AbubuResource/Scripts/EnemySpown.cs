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
    private string[] m_EnemyName;
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
    private float m_Volume = 1f;


    private void Start()
    {
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (MonobitEngine.MonobitNetwork.offline)
        {
            /*
            if (!MonobitEngine.MonobitNetwork.isHost)
            {
                return;
            }
            */
        }
        // スポーンポイントからプレイヤーまでの距離を計算します
        float distanceToPlayer = Vector3.Distance(transform.position, m_PlayerTransform.position);

        // スポーンポイントから5メートル以内に"Enemy"タグのオブジェクトが存在するかを確認します
        bool isEnemyNearby = CheckForEnemiesNearby();

        // プレイヤーが一定距離以内にいて、かつ5メートル以内に"Enemy"タグのオブジェクトがない場合
        if (distanceToPlayer <= m_SpawnDistanceThreshold && !isEnemyNearby)
        {
            m_SpawnTimer -= Time.deltaTime;

            // スポーンのタイマーが経過した場合
            if (m_SpawnTimer <= 0f)
            {
                // ランダムなスポーン位置をX軸とZ軸で計算し、高さはスポーンポイントと同じにする
                Vector3 randomSpawnPosition = new Vector3(
                    transform.position.x + Random.onUnitSphere.x * Random.Range(0f, 10f),
                    transform.position.y,  // 高さをスポーンポイントと同じにする
                    transform.position.z + Random.onUnitSphere.z * Random.Range(0f, 10f)
                );

                if (MonobitEngine.MonobitNetwork.offline == false)
                {
                    string randomEnemy = m_EnemyName[Random.Range(0, m_EnemyName.Length)];
                    MonobitEngine.MonobitNetwork.Instantiate(randomEnemy, randomSpawnPosition, Quaternion.identity, 0);
                }
                if (MonobitEngine.MonobitNetwork.offline == true)
                {
                    // ランダムな敵プレハブを選択します
                    GameObject randomEnemyPrefab = m_EnemyPrefabs[Random.Range(0, m_EnemyPrefabs.Length)];
                    // ランダムな位置に敵をスポーンさせます
                    Instantiate(randomEnemyPrefab, randomSpawnPosition, Quaternion.identity);
                }
                AudioSource.PlayClipAtPoint(m_SpownAudio, randomSpawnPosition, m_Volume);

                // パーティクルエフェクトを再生します
                if (m_ParticleEffectPrefab != null)
                {
                    Instantiate(m_ParticleEffectPrefab, randomSpawnPosition, Quaternion.identity);
                }

                // 新しいスポーン間隔をランダムに設定します
                m_SpawnTimer = Random.Range(m_MinSpawnInterval, m_MaxSpawnInterval);
            }
        }
    }

    bool CheckForEnemiesNearby()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 5f, LayerMask.GetMask("Enemy")))
        {
            return true;
        }
        return false;
    }
}

