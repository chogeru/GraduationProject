using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PVPPlayerPoint : MonoBehaviour
{
    PlayerMove playerMove;
    private float m_CoolTime;
    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }
    private void Update()
    {
        if(playerMove != null)
        {
            if(playerMove.m_Hp>=0)
            {

            }
        }
    }
}
