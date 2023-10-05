using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObj : MonoBehaviour
{
    [SerializeField]
    private string m_PlayerTag = "Player";
    private Transform m_Player;
    [SerializeField, Header("ç≈ëÂëÃóÕ")]
    private int m_MaxHp;
    [SerializeField, Header("åªç›ÇÃëÃóÕ")]
    private int m_Hp;

    private float m_DestroyTime;
    [SerializeField]
    private GameObject[] m_Boss;

    [SerializeField]
    private GameObject m_DestroyEffect;
    [SerializeField]
    private GameObject m_DestroySE;
    PlayerMove m_PlayerMove;
    [SerializeField]
    private Animator m_ActiveObj;
    [SerializeField]
    private GameObject m_ActiveObject;
    [SerializeField]
    private GameObject m_BossWall;
    [SerializeField]
    private GameObject m_StageWall;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag(m_PlayerTag).transform;
        m_PlayerMove = m_Player.GetComponent<PlayerMove>();
        m_ActiveObj=m_ActiveObject.GetComponent<Animator>();
        foreach (GameObject Boss in m_Boss)
        {
            Boss.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_MaxHp>m_Hp)
        {
            foreach (GameObject Boss in m_Boss)
            {
                Boss.SetActive(true);
            }
        }
        if (m_Hp <= 0)
        {
            
            m_DestroyTime += Time.deltaTime;
            if (m_DestroyTime >= 1.4)
            {
                Instantiate(m_DestroyEffect, transform.position, Quaternion.identity);
                m_ActiveObj.SetBool("IsActive", true);
                BossDestroy();
                Destroy(gameObject);
            }
        }
    }
    private void BossDestroy()
    {
        Destroy(m_BossWall);
        m_StageWall.SetActive(true);
        foreach(GameObject Boss in m_Boss)
        {
            Destroy(Boss);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            m_Hp -= m_PlayerMove.m_PlayerDamage;
        }
        if (collision.gameObject.CompareTag("ItemBullet"))
        {
            m_Hp -= 40;
        }
    }
}
