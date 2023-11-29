using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWater : MonoBehaviour
{
    PlayerMove m_PlayerMove;
    private int m_Damage=20;
    private float m_PoinsonCoolTIme;
    private bool isAttck=false;

    // Update is called once per frame
    void Update()
    {
        if(isAttck)
        {
            m_PoinsonCoolTIme += Time.deltaTime;
        }
        if(m_PoinsonCoolTIme>1)
        {
            m_PoinsonCoolTIme = 0;
            isAttck = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player")&&isAttck==false)
        {
            m_PlayerMove=other.GetComponent<PlayerMove>();
            if(m_PlayerMove!=null)
            {
                m_PlayerMove.TakeDamage(m_Damage);

            }
            isAttck = true;
        }
    }
}
