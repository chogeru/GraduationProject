using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SefePoint : MonoBehaviour
{
    private GameObject m_Player;
    PlayerMove playerMove;
    private int m_Recovery = 40;
    private bool isHit;
    private float m_Time;
    private float CoolTime=1;
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        playerMove=m_Player.GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (isHit)
        {
            m_Time += Time.deltaTime;
            if(m_Time > CoolTime)
            {
                playerMove.m_Hp+=m_Recovery;
                m_Time = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
           isHit = false;    
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isHit = true;
        }
    }
}
