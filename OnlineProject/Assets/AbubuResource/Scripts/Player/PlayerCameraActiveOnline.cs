using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraActiveOnline : MonoBehaviour
{
    MonobitEngine.MonobitView m_MonobitView = null;

    [SerializeField]
    private GameObject m_PlayerCamera;
    [SerializeField]
    private GameObject m_PlayerTPSCamera;

    private void Awake()
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

    // Start is called before the first frame update
    void Start()
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
        {
            if (!m_MonobitView.isMine)
            {
                return;
            }
        }
        m_PlayerCamera.SetActive(true);
        m_PlayerTPSCamera.SetActive(true);
    }

}
