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
    private int Hp;

    private float m_DestroyTime;

    [SerializeField]
    private GameObject m_DestroyEffect;
    [SerializeField]
    private GameObject m_DestroySE;
    PlayerMove m_PlayerMove;
    [SerializeField]
    private Animator m_ActiveObj;
    [SerializeField]
    private GameObject m_ActiveObject;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag(m_PlayerTag).transform;
        m_PlayerMove = m_Player.GetComponent<PlayerMove>();
        m_ActiveObj=m_ActiveObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            m_DestroyTime += Time.deltaTime;
            if (m_DestroyTime >= 1.4)
            {
                Instantiate(m_DestroyEffect, transform.position, Quaternion.identity);
                m_ActiveObj.SetBool("IsActive", true);
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hp -= m_PlayerMove.m_PlayerDamage;
        }
        if (collision.gameObject.CompareTag("ItemBullet"))
        {
            Hp -= 40;
        }
    }
}
