using MonobitEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OnlineSystem : MonobitEngine.MonoBehaviour
{
    [SerializeField, Header("�`���b�g�p�e�L�X�g")]
    private Text m_ChatText;
    [SerializeField, Header("�`���b�g���b�Z�[�W���X�g")]
    private List<string> m_Chat = new List<string>();
    [SerializeField, Header("���O�C�����")]
    private Text m_LogineText;
    [SerializeField, Header("���b�Z�[�W")]
    private Text m_CahtMessage;
    [SerializeField, Header("�v���C���[��")]
    private Text m_PlayerName;

    [SerializeField, Header("�o���p�v���C���[")]
    private string m_PlayerSpwn = "";
    [SerializeField, Header("Player�o���ʒu")]
    private Transform[] m_PlayerPos;
    [SerializeField, Header("�v���C���[�X�|�[���`�F�b�N")]
    private GameObject m_Player;
    [SerializeField,Header("���[����")]
    private string m_RoomName="";
    [SerializeField,Header("�}�b�`���[���̍ő�l��")]
    private byte m_MaxPlayers = 8;
    [SerializeField,Header("���g�̃I�u�W�F�N�g���������ꂽ���ǂ���")]
    private bool m_IsSpownMyChara = false;
    
    public void Awake()
    {
        MonobitNetwork.autoJoinLobby = false;
        MonobitNetwork.ConnectServer("OnlineSarver");
    }
    private void SarverConnect()
    {
        if(MonobitNetwork.isConnect)
        {
            if(MonobitNetwork.inRoom)
            {
                Debug.Log("���O�C���ς�");
            }
        }
    }
}
