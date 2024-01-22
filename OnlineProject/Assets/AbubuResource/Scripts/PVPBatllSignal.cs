using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPBatllSignal : MonoBehaviour
{
    MonobitEngine.MonobitView m_MonobitView = null;
    PVPBattleManager battleManager;
    GameObject m_PVPManagerObj;
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
    private void OnDestroy()
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
        {
            if (!m_MonobitView.isMine)
            {
                return;
            }
        }
        m_PVPManagerObj = GameObject.FindGameObjectWithTag("PVPManager");
        battleManager = m_PVPManagerObj.GetComponent<PVPBattleManager>();
        if (battleManager != null)
        {
            battleManager.isDie = true;
        }

    }
}
