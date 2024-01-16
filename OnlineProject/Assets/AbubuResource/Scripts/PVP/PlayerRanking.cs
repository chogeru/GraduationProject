using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerRanking : MonoBehaviour
{
    public TextMeshProUGUI rankingText;

    void Update()
    {
        UpdatePlayerRanking();
    }

    void UpdatePlayerRanking()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        List<int> playerHPValues = new List<int>();

    
        foreach (GameObject player in players)
        {
            PlayerMove playerMove = player.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                playerHPValues.Add(playerMove.GetHP());
            }
        }

        playerHPValues.Sort((a, b) => b.CompareTo(a));

        int playerRank = playerHPValues.IndexOf(GetComponent<PlayerMove>().GetHP()) + 1;

        rankingText.text = "Rank: " + playerRank + "/" + playerHPValues.Count;
    }
}
