using MonobitEngine;
using mun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OnlineSystem : MonobitEngine.MonoBehaviour
{
    [SerializeField, Header("�`���b�g�p�e�L�X�g")]
    private Text m_ChatText;
    [SerializeField,Header("���[���쐬�p�e�L�X�g")]
    private Text m_RoomNameText;
    [SerializeField, Header("�`���b�g���b�Z�[�W���X�g")]
    private List<string> m_Chat = new List<string>();
    [SerializeField, Header("���O�C�����")]
    private Text m_LogineText;
    [SerializeField, Header("���b�Z�[�W")]
    private Text m_CahtMessage;
    [SerializeField, Header("�v���C���[��")]
    private Text m_PlayerName;

    [SerializeField, Header("���O����UI")]
    private GameObject m_NameUI;
    [SerializeField, Header("���[�����o�pUI")]
    private GameObject m_RoomUI;

    [SerializeField, Header("�o���p�v���C���[")]
    private string m_PlayerSpwn = "";
    [SerializeField, Header("Player�o���ʒu")]
    private Transform[] m_PlayerPos;
    [SerializeField, Header("�v���C���[�X�|�[���`�F�b�N")]
    private bool m_PlayerSpownCheck = false;
    [SerializeField, Header("���[����")]
    private string m_RoomName = "";
    [SerializeField, Header("�}�b�`���[���̍ő�l��")]
    private byte m_MaxPlayers = 8;
    [SerializeField, Header("���g�̃I�u�W�F�N�g���������ꂽ���ǂ���")]
    private bool m_IsSpownMyChara = false;

    public void Awake()
    {
        //�ŏ��Ɏ����Ń��r�[�쐬
        MonobitEngine.MonobitNetwork.autoJoinLobby = true;
        MonobitNetwork.ConnectServer("OnlineServer_v1.0");
    }
    private void Update()
    {
        SarverConnect();
    }
    private void SarverConnect()
    {
        if (MonobitNetwork.isConnect)
        {
            if(!MonobitNetwork.inRoom)
            {
                if (Input.GetKey(KeyCode.I))
                {
                    MonobitEngine.MonobitNetwork.CreateRoom(m_RoomName);
                    Debug.Log("���[���쐬");
                }
                if(Input.GetKey(KeyCode.R))
                {
                    MonobitEngine.MonobitNetwork.JoinRoom(m_RoomName);

                }
            }
        }
    }
    private void CreateRoom()
    {
        m_RoomNameText = GetComponent<Text>();
    }
}
