using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItems : MonoBehaviour
{
    PlayerMove player;
    private GameObject m_Player;
    [SerializeField]
    private int m_Recovery = 20;
    [SerializeField, Header("アイテム所得時のエフェクト")]
    private GameObject m_ItemHitEffect;
    [SerializeField, Header("アイテム所得時のSE")]
    private AudioClip m_ItemGetSE;
    private float m_SEVolume = 1;
    private float m_DestroyTime;
    
    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        player = m_Player.GetComponent<PlayerMove>();
    }
    private void Update()
    {
        if(m_DestroyTime>10)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.m_Hp += m_Recovery;
            player.isRecovery = true;
            AudioSource.PlayClipAtPoint(m_ItemGetSE, transform.position, m_SEVolume);
            Instantiate(m_ItemHitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
