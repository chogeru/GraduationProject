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
    [SerializeField, Header("チャット用テキスト")]
    private Text m_ChatText;
    [SerializeField,Header("ルーム作成用テキスト")]
    private Text m_RoomNameText;
    [SerializeField, Header("チャットメッセージリスト")]
    private List<string> m_Chat = new List<string>();
    [SerializeField, Header("ログイン情報")]
    private Text m_LogineText;
    [SerializeField, Header("メッセージ")]
    private Text m_CahtMessage;
    [SerializeField, Header("プレイヤー名")]
    private Text m_PlayerName;

    [SerializeField, Header("名前入力UI")]
    private GameObject m_NameUI;
    [SerializeField, Header("ルーム入出用UI")]
    private GameObject m_RoomUI;

    [SerializeField, Header("出現用プレイヤー")]
    private string m_PlayerSpwn = "";
    [SerializeField, Header("Player出現位置")]
    private Transform[] m_PlayerPos;
    [SerializeField, Header("プレイヤースポーンチェック")]
    private bool m_PlayerSpownCheck = false;
    [SerializeField, Header("ルーム名")]
    private string m_RoomName = "";
    [SerializeField, Header("マッチルームの最大人数")]
    private byte m_MaxPlayers = 8;
    [SerializeField, Header("自身のオブジェクトが生成されたかどうか")]
    private bool m_IsSpownMyChara = false;

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
        if (MonobitNetwork.isConnect)
        {
            if(!MonobitNetwork.inRoom)
            {
                if (Input.GetKey(KeyCode.I))
                {
                    MonobitEngine.MonobitNetwork.CreateRoom(m_RoomName);
                    Debug.Log("ルーム作成");
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
