using MonobitEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OnlineSystem : MonobitEngine.MonoBehaviour
{
    [SerializeField, Header("チャット用テキスト")]
    private Text m_ChatText;
    [SerializeField, Header("チャットメッセージリスト")]
    private List<string> m_Chat = new List<string>();
    [SerializeField, Header("ログイン情報")]
    private Text m_LogineText;
    [SerializeField, Header("メッセージ")]
    private Text m_CahtMessage;
    [SerializeField, Header("プレイヤー名")]
    private Text m_PlayerName;

    [SerializeField, Header("出現用プレイヤー")]
    private string m_PlayerSpwn = "";
    [SerializeField, Header("Player出現位置")]
    private Transform[] m_PlayerPos;
    [SerializeField, Header("プレイヤースポーンチェック")]
    private GameObject m_Player;
    [SerializeField,Header("ルーム名")]
    private string m_RoomName="";
    [SerializeField,Header("マッチルームの最大人数")]
    private byte m_MaxPlayers = 8;
    [SerializeField,Header("自身のオブジェクトが生成されたかどうか")]
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
                Debug.Log("ログイン済み");
            }
        }
    }
}
