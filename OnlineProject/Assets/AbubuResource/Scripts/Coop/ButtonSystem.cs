using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ButtonSystem : MonoBehaviour
{
    [SerializeField,Header("�v���C���[���o�^���")]
    private GameObject m_PlayerNameSettingScreen;
    [SerializeField,Header("�T�[�o�[�ݒ���")]
    private GameObject m_ServerSettingScreen;
    [SerializeField,Header("�v���C���[�����T�[�o�[�ݒ��ʂ̃}�b�v�I�u�W�F�N�g")]
    private GameObject m_ServerScreenMap;
    [SerializeField,Header("�L�����N�^�[�I�����[�h")]
    private GameObject m_CaractorSelectMode;



    private void Start()
    {
        m_PlayerNameSettingScreen.SetActive(true);
        m_ServerSettingScreen.SetActive(false);
    }
    public void ButtonClickToServerSettingScreen()
    {
        m_PlayerNameSettingScreen.SetActive(false);
        m_ServerSettingScreen.SetActive(true);
    }
}
