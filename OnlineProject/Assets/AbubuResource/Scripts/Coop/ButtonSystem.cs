using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ButtonSystem : MonoBehaviour
{
    [SerializeField,Header("プレイヤー名登録画面")]
    private GameObject m_PlayerNameSettingScreen;
    [SerializeField,Header("サーバー設定画面")]
    private GameObject m_ServerSettingScreen;
    [SerializeField,Header("プレイヤー名＆サーバー設定画面のマップオブジェクト")]
    private GameObject m_ServerScreenMap;
    [SerializeField,Header("キャラクター選択モード")]
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
