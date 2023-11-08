using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RecoveryItems : MonoBehaviour
{
    enum ItemType
    {
        RecoverryItem,
        PowerUpItem,
    }
    [SerializeField]
    ItemType m_CurrentType;

    PlayerMove player;
    private GameObject m_Player;
    [SerializeField]
    private int m_Recovery = 20;
    [SerializeField, Header("�A�C�e���������̃G�t�F�N�g")]
    private GameObject m_ItemHitEffect;
    [SerializeField, Header("�A�C�e����������SE")]
    private AudioClip m_ItemGetSE;
    private float m_SEVolume = 1;
    private float m_DestroyTime;
    [SerializeField,Header("�U���͏㏸�l")]
    private int m_AttackBoost;
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
            switch (m_CurrentType)
            {
                case ItemType.RecoverryItem:
                    player.m_Hp += m_Recovery;
                    player.isRecovery = true;
                    GetItem();
                    break;
                case ItemType.PowerUpItem:
                    player.m_PlayerDamage += m_AttackBoost;
                    GetItem();
                    break;
            }

         
        }
    }
    private void GetItem()
    {
        AudioSource.PlayClipAtPoint(m_ItemGetSE, transform.position, m_SEVolume);
        Instantiate(m_ItemHitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
