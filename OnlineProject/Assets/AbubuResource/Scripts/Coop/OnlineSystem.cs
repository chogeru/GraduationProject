using MonobitEngine;
using mun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OnlineSystem : MonobitEngine.MonoBehaviour
{

    [SerializeField,Header("���[���쐬�p�e�L�X�g")]
    private Text m_CreateRoomNameText;
    [SerializeField, Header("���[���p�X���[�h")]
    private Text m_PassWordText;
    [SerializeField, Header("�W���C�����[�����e�L�X�g")]
    private Text m_JoinRoomNameText;
    [SerializeField,Header("�W���C�����[���p�X���[�h")]
    private Text m_JoinRoomPasWordText;

    [SerializeField, Header("���O����UI")]
    public List<Text> playerListTexts;

    public InputField playerNameInput;
   
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
        if (MonobitEngine.MonobitNetwork.isConnect)
        {
            Debug.Log("�R�l�N�g�`");
        }
        else
        {
            Debug.Log("�R�l�N�g����Ȃ��`");
        }
    }
 
    public void CreateRoom()
    {
        string roomName = m_CreateRoomNameText.text;
        string roomPassword=m_PassWordText.text;
        MonobitEngine.RoomSettings roomsetting = new MonobitEngine.RoomSettings();
        roomsetting.maxPlayers = 4;
        roomsetting.isVisible = true;
        roomsetting.isOpen = true;

        MonobitEngine.MonobitNetwork.CreateRoom(roomName+roomPassword);
        Debug.Log(roomName+roomPassword);

    }
    public void JoinRoom()
    {
        string JoinRoomName=m_JoinRoomNameText.text;
        string JoinRoomPasword=m_JoinRoomPasWordText.text;

        MonobitEngine.MonobitNetwork.JoinRoom(JoinRoomName+JoinRoomPasword);
        Debug.Log(JoinRoomName+JoinRoomPasword);
        
    }
    public void OfflineMode()
    {
        if (!MonobitNetwork.inRoom)
        {
            MonobitNetwork.LeaveRoom();
            MonobitEngine.MonobitNetwork.DisconnectServer();

        }
    }
    public void OnDisconnectedFromServer()
    { 
        SceneManager.LoadScene("Title");

    }
    public void SetPlayerName()
    {
        // �v���C���[���Q�[���ɎQ��������A���O��ݒ肵�ĕۑ�����
        string playerName = playerNameInput.text;
        MonobitNetwork.playerName = playerName;
        Debug.Log(MonobitNetwork.playerName);
    }
}
