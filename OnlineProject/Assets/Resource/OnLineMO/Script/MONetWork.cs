using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MonobitEngine;
using System.Collections.Generic;
//using Cinemachine;

public class MONetWork : MonobitEngine.MonoBehaviour
{
    [Header("フローティングカメラ")]
    public Transform m_Camera;
    //ルーム名
    private string roomName = "";

    #region チャット関連
    [Header("チャット発現Text")]
    public Text m_ChatWord;

    [Header("チャット発現リスト")]
    public List<string> m_ChatLog = new List<string>();
    #endregion

    [Header("ログイン情報")]
    public Text m_LoginText;
    [Header("メッセージ")]
    public Text m_MessText;
    [Header("プレイヤー名")]
    public Text m_PlayerNameText;

    #region 各UI統括オブジェクト
    [Header("ロビー入室UI(表示・非表示用)")]
    public GameObject m_LobbyGUI;
    [Header("ルーム入室UI(表示・非表示用)")]
    public GameObject m_RoomGUI;
    [Header("ルーム入室後UI(表示・非表示用)")]
    public GameObject m_GameGUI;
    #endregion


    #region ゲーム系
    [Header("出現Player")]
    public string m_PlayerName = "Player";
    [Header("Player出現位置")]
    public Transform[] m_PlayerPopPoint;
    [Header("Player確認用")]
    public GameObject m_Player;

    [Header("Player蘇生転送時間")]
    public float m_DeadCounter = 5.0f;
    [Header("Player蘇生転送最大時間")]
    public float m_DeadMaxCounter = 5.0f;
    [Header("Player蘇生転送リスタートオブジェクト")]
    public GameObject m_ReStartObject;
    [Header("Player蘇生転送時間描画(秒)")]
    public Text m_ReStartByou;

    [Header("ルームNo")]
    public int m_RoomNo = 0;
    [Header("タイトルカメラ")]
    public GameObject m_TitleCamera;
    
    [Header("オーディオソース")]
    public AudioSource m_AudioSource;
    [Header("BGMリスト")]
    public AudioClip[] m_AudioClips;
    #endregion


    private void Update()
    {
        //オンライン状況・進行マネージャー
        OnlineSystem();
    }

    #region オンライン状況・進行マネージャー
    public void OnlineSystem()
    {
        // MUNサーバに接続している場合
        if (MonobitNetwork.isConnect)
        {
            // ルームに入室している場合
            if (MonobitNetwork.inRoom)
            {
                Debug.Log("ルームにいます。");

                //ロビー・ルーム入室ボタン表示・非表示
                LoginLeaveRoomButtonRender(2);

                //プレイヤー入室リスト
                PlayerListData(true);

                //現在プレイヤーが出現していない
                if (!m_Player)
                {
                    List<Transform> DummyS = new List<Transform>();
                    foreach (Transform DummyList in m_PlayerPopPoint[m_RoomNo])
                    {
                        DummyS.Add(DummyList);
                    }
                    Transform Dummy = DummyS[Random.Range(0, DummyS.Count - 1)];

                    //自身と、オンラインでつながっているプレイヤーにアバターを出現させる
                    m_Player = MonobitEngine.MonobitNetwork.Instantiate(
                        m_PlayerName,               //出現プレイヤー名
                        Dummy.position,             //出現位置
                        Dummy.rotation,             //出現向き
                        0,                          //ネットワークID
                        null,                       //付随情報
                        true,                       //シーン切り替えでもこれを維持するか?
                                                    //trueでシーンが切り替えでも継続可能
                        true,                       //所有権は自分にあるか?
                                                    //falseにすると静的オブジェクトとなる
                        false);                     //ルーム退出時にこのオブジェクトを維持するか?
                                                    //trueにすると、残り続ける
                                                    //本体だけ入力を許可する
                    m_Player.GetComponent<Muryotaisu.MuryotaisuController>().enabled = true;

                }
                else
                {
                    //プレイヤー追従
                    m_Camera.position = m_Player.transform.position;

                    /*
                    if (m_CVC)
                    {
                        m_CVC.Follow = m_Player.transform;
                        m_CVC.LookAt = m_Player.transform;
                    }
                    */


                    /*
                    if (m_Player.GetComponent<TankMaster>().m_LifePoint <= 0.0f)
                    {
                        m_ReStartObject.SetActive(true);
                        m_DeadCounter -= 1.0f * Time.deltaTime;
                        if (m_DeadCounter <= 0.0f)
                        {
                            m_DeadCounter = m_DeadMaxCounter;
                            MonobitNetwork.Destroy(m_Player);
                            m_ReStartObject.SetActive(false);
                        }
                        int Dummy = (int)m_DeadCounter + 1;
                        m_ReStartByou.text = Dummy.ToString();
                    }
                    */
                }
            }
            // ルームに入室していない場合
            else
            {
                Debug.Log("ロビーにいます。");

                //プレイヤー入室リスト
                PlayerListData(false);

                //ロビー・ルーム入室ボタン表示・非表示
                LoginLeaveRoomButtonRender(1);
            }
        }
        // MUNサーバに接続していない場合
        else
        {
            Debug.Log("ログインしていません。");

            //入室ボタンに委任

            //プレイヤー入室リスト
            PlayerListData(false);

            //ロビー・ルーム入室ボタン表示・非表示
            LoginLeaveRoomButtonRender(0);
        }
    }
    #endregion



    #region ☆表示処理

    #region プレイヤーリスト表示・非表示
    /// <summary>
    /// プレイヤーリスト表示・非表示
    /// </summary>
    /// <param name="flag"></param>
    public void PlayerListData(bool flag)
    {
        if (m_LoginText.enabled != flag)
        {
            m_LoginText.enabled = !m_LoginText.enabled;
        }
        if (flag)
        {
            // ルーム内のプレイヤー一覧の表示
            string LoginPlayerList = "";
            foreach (MonobitPlayer player in MonobitNetwork.playerList)
            {
                LoginPlayerList += player.name + "\n";
            }
            m_LoginText.text = LoginPlayerList;
        }
    }
    #endregion

    #region ロビー・ルーム退室ボタン表示・非表示
    public void LoginLeaveRoomButtonRender(int No)
    {
        switch (No)
        {
            //ロビー入室前
            case 0:
                //カーソル表示
                Cursor.visible = true;
                //カーソルのロック解除
                Cursor.lockState = CursorLockMode.None;

                //ロビーGUIを描画
                m_LobbyGUI.SetActive(true);
                //ルームGUIを描画
                m_RoomGUI.SetActive(false);
                //ゲームGUI描画
                m_GameGUI.SetActive(false);

                //ロビー音楽を流す
                if (m_AudioSource.clip != m_AudioClips[0])
                {
                    m_AudioSource.clip = m_AudioClips[0];
                    m_AudioSource.Play();
                }
                break;
            //ルーム入室前
            case 1:
                //カーソル表示
                Cursor.visible = true;
                //カーソルのロック解除
                Cursor.lockState = CursorLockMode.None;

                //ロビーGUIを描画
                m_LobbyGUI.SetActive(false);
                //ルームGUIを描画
                m_RoomGUI.SetActive(true);
                //ゲームGUI描画
                m_GameGUI.SetActive(false);

                //ルーム音楽を流す
                if (m_AudioSource.clip != m_AudioClips[0])
                {
                    m_AudioSource.clip = m_AudioClips[0];
                    m_AudioSource.Play();
                }
                break;
            //ルーム入室後
            case 2:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (Cursor.visible)
                    {
                        //カーソル非表示
                        Cursor.visible = false;
                        //カーソルのロック
                        Cursor.lockState = CursorLockMode.None;
                        //カーソルをウィンドウ内に
                        Cursor.lockState = CursorLockMode.Confined;
                    }
                    else
                    {
                        //カーソル非表示
                        Cursor.visible = true;
                        //カーソルのロック
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }

                //ロビーGUIを描画
                m_LobbyGUI.SetActive(false);
                //ルームGUIを描画
                m_RoomGUI.SetActive(false);
                //ゲームGUI描画
                m_GameGUI.SetActive(true);

                //ゲーム音楽を流す
                if (m_AudioSource.clip != m_AudioClips[1])
                {
                    m_AudioSource.clip = m_AudioClips[1];
                    m_AudioSource.Play();
                }
                break;
        }
    }
    #endregion

    #endregion



    #region ☆ロビー、及びルーム入退室処理

    #region ロビー入室実行
    public void LobbySystem()
    {
        if (m_PlayerNameText.text != "")
        {
            // MUNサーバに接続している場合
            if (!MonobitNetwork.isConnect)
            {
                // デフォルトロビーへの自動入室を許可する
                MonobitNetwork.autoJoinLobby = true;
                // プレイヤー名の入力
                MonobitNetwork.playerName = m_PlayerNameText.text;

                MonobitNetwork.ConnectServer("OnLineMO_v1.0");
                Debug.Log("ロビーに入室しました。");
            }
        }
    }
    #endregion

    #region ルーム入室実行
    public void LoginRoomSystem(int RoomNo)
    {
        // MUNサーバに接続している場合
        if (MonobitNetwork.isConnect)
        {
            // ルームに入室している場合
            if (!MonobitNetwork.inRoom)
            {
                m_RoomNo = RoomNo;
                // ルームの最大人数を10人、公開で入室可能な新規ルーム"roomName"を、通常のロビー名「lobbyName」から検索します。
                // 見つからなければ、上記設定のルームを作成し、入室します。
                MonobitEngine.RoomSettings settings = new MonobitEngine.RoomSettings();
                settings.maxPlayers = 10;
                settings.isVisible = true;
                settings.isOpen = true;
                MonobitEngine.LobbyInfo lobby = new MonobitEngine.LobbyInfo();
                lobby.Kind = LobbyKind.Default;
                lobby.Name = "onlinemolobby";
                bool Flag = false;
                Flag = MonobitEngine.MonobitNetwork.JoinOrCreateRoom("infinitechronicle" + RoomNo, settings, lobby);
                if (!Flag)
                {
                    Debug.Log("失敗");
                }
                m_ChatLog.Clear();
            }
        }
    }
    #endregion


    #region ロビー退室実行
    public void LeaveLobbySystem()
    {
        // MUNサーバに接続している場合
        if (MonobitNetwork.isConnect)
        {
            // ロビーに入室している場合
            if (MonobitNetwork.inLobby)
            {
                Debug.Log("退室ボタン実行");
                MonobitNetwork.LeaveLobby();
                m_ChatLog.Clear();
            }
        }
    }
    #endregion

    #region ルーム退室実行
    public void LeaveRoomSystem()
    {
        // MUNサーバに接続している場合
        if (MonobitNetwork.isConnect)
        {
            // ルームに入室している場合
            if (MonobitNetwork.inRoom)
            {
                MonobitNetwork.LeaveRoom();
                m_ChatLog.Clear();
            }
        }
    }
    #endregion

    #endregion



    #region ☆コールバック系

    #region (自分)ロビーを退室が実行された場合
    public void OnLeftLobby()
    {
        ///ロビーから出ても、サーバーから切断されていないので、これで切断しタイトルに戻る
        MonobitNetwork.DisconnectServer();
    }
    #endregion

    #region 他のプレイヤーがルームに入室した場合
    public void OnOtherPlayerConnected(MonobitPlayer newPlayer)
    {
        SystemCharSender(newPlayer.name + "が入室しました。");
    }
    #endregion

    #region 他のプレイヤーがルームから退室した場合
    public void OnOtherPlayerDisconnected(MonobitPlayer otherPlayer)
    {
        SystemCharSender(otherPlayer.name + "が退室しました。");
    }
    #endregion

    #region ルームマスターが変更された場合
    public void OnHostChanged(MonobitPlayer newHost)
    {
        SystemCharSender("ルームマスターが、" + newHost.name + "に引き継がれました。");
    }
    #endregion

    #region サーバーに接続された場合呼ばれる(ロビー入室していない/オフラインモード終了)
    public void OnConnectedToServer()
    {
        Debug.Log("サーバーに接続されました\nオフラインモードを終了します");
        //オフラインモードoFF
        MonobitEngine.MonobitNetwork.offline = false;
    }
    #endregion

    #region サーバーを切断した場合(オフラインモードへ移行)
    public void OnDisconnectedFromServer()
    {
        Debug.Log("サーバーからログアウトしました\nオフラインモードへ移行します");
        //オフラインモードOn
        MonobitEngine.MonobitNetwork.offline = true;
    }
    #endregion

    #region ロビーにいる時、ルーム一覧が更新されたら呼ばれる
    public void OnReceivedRoomListUpdate()
    {
        Debug.Log("ルーム一覧が更新されました。");
    }
    #endregion

    #region ルームに入室した場合呼ばれる
    public void OnJoinedRoom()
    {
        Debug.Log("ルームに入室しました。");
    }
    #endregion

    #region ルームの設定された最大人数を超過した際、ルームに入れずこれが呼ばれる
    public void OnMonobitMaxConnectionReached()
    {
        Debug.Log("ルームの最大収容人数を超過しました。");
    }
    #endregion

    #region ルームのパラメーターが変更されたら呼ばれる
    public void OnMonobitCustomRoomParametersChanged(Hashtable parametersChanged)
    {
        Debug.Log("ルームパラメーターが変更されました");
    }
    #endregion

    #region プレイヤーのパラメーターが変更されました
    public void OnMonobitPlayerParametersChanged(object[] playerAndUpdatedParameters)
    {
        Debug.Log("プレイヤーパラメーターが変更されました");
    }
    #endregion

    #region プレイヤーリストサーチリクエストで更新された場合呼ばれる
    public void OnUpdatedSearchPlayers()
    {
        Debug.Log("プレイヤーリストサーチリクエストによりリストの更新が確認された。");
    }
    #endregion

    #region ロビーステータスが更新されたら呼ばれる
    public void OnLobbyDataUpdate()
    {
        Debug.Log("ロビーステータスの更新が確認された。");
    }
    #endregion

    #endregion



    #region ☆メッセージ送信

    #region チャット文章を送信
    public void CharSender()
    {
        if (m_ChatWord)
        {
            Debug.Log($"送信実行 {m_ChatWord.text.ToString()}");
            monobitView.RPC("ChatLogSystem",
                MonobitTargets.All, //送信相手（全員）
                MonobitNetwork.playerName,
                m_ChatWord.text.ToString());
        }
    }
    #endregion

    #region システム文章をチャットに流す
    public void SystemCharSender(string senderWord)
    {
        if (m_ChatWord)
        {
            //メッセージをリストに追加する
            m_ChatLog.Add("System : " + senderWord);
            //受信したメッセージを含め10個を超えている場合
            if (m_ChatLog.Count > 10)
            {
                //古いメッセージを削除する
                m_ChatLog.RemoveAt(0);
            }
            //チャットログウィンドゥを初期化する
            m_MessText.text = "";
            //全てのチャットログの文字をチャットログウィンドゥに表示する
            foreach (string Dummy in m_ChatLog)
            {
                m_MessText.text += Dummy + "\n";
            }

        }
    }
    #endregion

    #endregion

    #region チャットログ
    [MunRPC]
    public void ChatLogSystem(string senderName, string senderWord)
    {
        //受信した名前とメッセージをリストに追加する
        m_ChatLog.Add(senderName + " : " + senderWord);
        //受信したメッセージを含め10個を超えている場合
        if (m_ChatLog.Count > 10)
        {
            //古いメッセージを削除する
            m_ChatLog.RemoveAt(0);
        }
        //チャットログウィンドゥを初期化する
        m_MessText.text = "";
        //全てのチャットログの文字をチャットログウィンドゥに表示する
        foreach (string Dummy in m_ChatLog)
        {
            m_MessText.text += Dummy + "\n";
        }
    }
    #endregion
}
