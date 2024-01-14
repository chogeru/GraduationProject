using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPReSpown : MonoBehaviour
{
    public Transform[] respawnPoints; // リスポーンポイントのTransformを格納する配列

    public void RespawnPlayer()
    {
        // ランダムなインデックスを生成してランダムなリスポーンポイントを選択
        int randomIndex = Random.Range(0, respawnPoints.Length);
        Transform selectedRespawnPoint = respawnPoints[randomIndex];

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = selectedRespawnPoint.position;
        }
        else
        {
            Debug.LogWarning("Player not found for respawn!");
        }
    }
}
