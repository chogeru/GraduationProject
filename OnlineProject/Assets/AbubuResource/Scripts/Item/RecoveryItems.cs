using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItems : MonoBehaviour
{
    PlayerMove player;
    private GameObject m_Player;

    [SerializeField, Header("アイテム所得時のエフェクト")]
    private GameObject m_ItemHitEffect;
    [SerializeField, Header("アイテム所得時のSE")]
    private AudioClip m_ItemGetSE;
    private float m_SEVolume = 1;
    
    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        player = m_Player.GetComponent<PlayerMove>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.m_Hp += 3;
            player.isRecovery = true;
            AudioSource.PlayClipAtPoint(m_ItemGetSE, transform.position, m_SEVolume);
            Instantiate(m_ItemHitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
