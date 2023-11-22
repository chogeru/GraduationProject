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

    [SerializeField,Header("ルーム作成用テキスト")]
    private Text m_CreateRoomNameText;
    [SerializeField, Header("ルームパスワード")]
    private Text m_PassWordText;
    [SerializeField, Header("ジョインルーム名テキスト")]
    private Text m_JoinRoomNameText;
    [SerializeField,Header("ジョインルームパスワード")]
    private Text m_JoinRoomPasWordText;

    [SerializeField, Header("名前入力UI")]
    public List<Text> playerListTexts;

    public InputField playerNameInput;
   
    public void Awake()
    {
        //最初に自動でロビー作成
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
            Debug.Log("コネクト～");
        }
        else
        {
            Debug.Log("コネクトじゃない～");
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
        // プレイヤーがゲームに参加したら、名前を設定して保存する
        string playerName = playerNameInput.text;
        MonobitNetwork.playerName = playerName;
        Debug.Log(MonobitNetwork.playerName);
    }
}
