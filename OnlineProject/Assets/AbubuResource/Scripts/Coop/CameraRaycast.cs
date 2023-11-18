using MonobitEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonobitEngine.MunMonoBehaviour
{
    [SerializeField, Header("パーティクルプレハブ")]
    public GameObject m_ParticlePrefab; // パーティクルのプレハブをインスペクタから割り当てる
    [SerializeField, Header("確定ボタン")]
    private GameObject m_Button;
    private GameObject m_CurrentParticle; // 現在のパーティクルを保持する変数
    [SerializeField, Header("プレイヤーの高さのオフセット")]
    private float m_PlayerOffset = 2.5f;
    [SerializeField, Header("オーディオソースコンポーネントを所得")]
    private AudioSource m_AudioSource;
    public string m_LastHitObjectName; // 最後にヒットしたオブジェクトの名前を保持する変数
    [SerializeField]
    public Vector3 m_SpownPoint;
    [SerializeField]
    private GameObject playerObject = null;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //ボタンの表示切り替え
                    m_Button.SetActive(true);
                    // 当たったオブジェクトの名前を保存
                    m_LastHitObjectName = hit.collider.gameObject.name; 
                    if (m_CurrentParticle != null)
                    {
                        // 前のパーティクルを破棄
                        Destroy(m_CurrentParticle);
                    }
                    m_AudioSource.Play();
                    Vector3 spawnPosition = hit.collider.transform.position + Vector3.up * m_PlayerOffset;
                    SpawnParticles(spawnPosition);
                }
            }
        }
    }

    private void SpawnParticles(Vector3 position)
    {

        // パーティクルを生成して指定位置に配置
        m_CurrentParticle = Instantiate(m_ParticlePrefab, position, Quaternion.identity);
    }
    public void SpownPlayer()
    {
        // lastHitObjectName に保存された名前を持つプレイヤープレハブを生成
        // GameObject playerPrefabToSpawn = Resources.Load<GameObject>(m_LastHitObjectName);
        //  if (playerPrefabToSpawn != null)
        //  {
        if (playerObject == null)
        {
            // スポーンポイントにプレイヤーを生成
            playerObject=MonobitEngine.MonobitNetwork.Instantiate(
                //playerPrefabToSpawn
                m_LastHitObjectName
                , m_SpownPoint
                , Quaternion.identity
                , 0);
            //   }
        }
    }
}