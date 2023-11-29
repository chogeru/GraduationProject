using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPBulletSystem : MonoBehaviour
{
    public int damageAmount = 1; // プレイヤーに与えるダメージ量
    private float m_HitCoolTime;
    private bool isHit=false;

    PlayerMove playerMove;

    private void Update()
    {
        if(isHit)
        {
            m_HitCoolTime += Time.deltaTime;
        }
        if(m_HitCoolTime>1)
        {
            isHit = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerMove=collision.gameObject.GetComponent<PlayerMove>();
            if(playerMove != null)
            {
                playerMove.TakeDamage(damageAmount);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerMove=other.gameObject.GetComponent<PlayerMove>();
            if(playerMove != null)
            {
                playerMove.TakeDamage(damageAmount);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&isHit==false)
        {
            playerMove = other.gameObject.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                playerMove.TakeDamage(damageAmount);
                isHit = true;
            }
        }
    }
}
