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
            // ���ׂĂ̐e�I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g����������
            if (GetComponentInParent<MonobitEngine.MonobitView>() != null)
            {
                m_MonobitView = GetComponentInParent<MonobitEngine.MonobitView>();
            }
            // �e�I�u�W�F�N�g�ɑ��݂��Ȃ��ꍇ�A���ׂĂ̎q�I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g����������
            else if (GetComponentInChildren<MonobitEngine.MonobitView>() != null)
            {
                m_MonobitView = GetComponentInChildren<MonobitEngine.MonobitView>();
            }
            // �e�q�I�u�W�F�N�g�ɑ��݂��Ȃ��ꍇ�A���g�̃I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g���������Đݒ肷��
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
