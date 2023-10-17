using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    public void StartTimeline()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Player.SetActive(false);
    }
    public void AvtivePlayer()
    {
        m_Player.SetActive(true);
    }
}

