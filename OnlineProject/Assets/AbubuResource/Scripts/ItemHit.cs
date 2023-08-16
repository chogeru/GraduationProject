using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHit : MonoBehaviour
{
    [SerializeField,Header("パーティクル")]
    private GameObject m_ParticlePrefab;
    [SerializeField,Header("オーディオソース")]
    private AudioClip m_SoundEffect;
    [SerializeField,Header("音量")]
    private float m_Volume = 1.0f;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーオブジェクトに接触した際の処理
            AudioSource.PlayClipAtPoint(m_SoundEffect, transform.position, m_Volume);
            // パーティクルを生成
            Instantiate(m_ParticlePrefab, transform.position, Quaternion.identity);
            // 自身のオブジェクトを破棄
            Destroy(gameObject);
        }
    }
}
