using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPReSpown : MonoBehaviour
{
    public Transform[] respawnPoints; // ���X�|�[���|�C���g��Transform���i�[����z��

    public void RespawnPlayer()
    {
        // �����_���ȃC���f�b�N�X�𐶐����ă����_���ȃ��X�|�[���|�C���g��I��
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
