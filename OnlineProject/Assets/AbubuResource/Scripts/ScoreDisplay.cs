using UnityEngine;
using TMPro;
using System.Collections.Generic;
using MonobitEngine;

public class ScoreDisplay : MonobitEngine.MonoBehaviour
{
    public TMP_Text playerNameText1;
    public TMP_Text playerNameText2;
    public TMP_Text playerNameText3;
    public TMP_Text playerNameText4;

    private void Start()
    {
        // ルームに参加している場合にプレイヤーの名前を取得して表示
        if (MonobitNetwork.inRoom)
        {
            GetPlayerNamesInRoom();
        }
    }
    private void Update()
    {
        if (MonobitNetwork.inRoom)
        {
            GetPlayerNamesInRoom();
        }
    }
    private void GetPlayerNamesInRoom()
    {
        // プレイヤーリストから名前を取得して各テキストに表示
        MonobitPlayer[] players = MonobitNetwork.playerList;

        for (int i = 0; i < 4; i++)
        {
            TMP_Text playerNameText = GetPlayerNameText(i);

            if (i < players.Length && playerNameText != null)
            {
                playerNameText.text = "Player " + (i + 1) + ": " + players[i].name;
                playerNameText.gameObject.SetActive(true);
            }
            else if (playerNameText != null)
            {
                playerNameText.gameObject.SetActive(false);
            }
        }
    }

    private TMP_Text GetPlayerNameText(int index)
    {
        switch (index)
        {
            case 0:
                return playerNameText1;
            case 1:
                return playerNameText2;
            case 2:
                return playerNameText3;
            case 3:
                return playerNameText4;
            default:
                return null;
        }
    }
}