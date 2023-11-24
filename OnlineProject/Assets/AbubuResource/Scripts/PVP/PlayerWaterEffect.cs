using AssetKits.ParticleImage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaterEffect : MonoBehaviour
{
    MonobitEngine.MonobitView m_MonobitView = null;
    void Awake()
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
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
    [SerializeField]
    private ParticleImage m_WaterEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
        {
            if (!m_MonobitView.isMine)
            {
                return;
            }
        }

        if (other.gameObject.CompareTag("Water"))
        {
         m_WaterEffect.Stop();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
        {
            if (!m_MonobitView.isMine)
            {
                return;
            }
        }

        if (other.gameObject.CompareTag("Water"))
        {
            m_WaterEffect.Play();
        }
    }
}
