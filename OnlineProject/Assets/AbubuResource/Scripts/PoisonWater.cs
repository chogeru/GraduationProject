using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWater : MonoBehaviour
{
    PlayerMove m_PlayerMove;
    private Transform m_Player;
    private int m_Damage=1;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_PlayerMove = m_Player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            m_PlayerMove.m_Hp--;
        }
    }
}
