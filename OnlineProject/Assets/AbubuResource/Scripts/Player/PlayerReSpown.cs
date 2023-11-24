using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReSpown : MonoBehaviour
{
    MonobitEngine.MonobitView m_MonobitView = null;

    [SerializeField]
    private GameObject m_ReSpownEffect;
    [SerializeField]
    private GameObject m_ReSpownSE;

    [SerializeField]
    private GameObject m_FadeIN;
    private PlayerMove playerMove;

    //最後に通過したチェックポイントの座標
    [SerializeField]
    private Vector3 m_LastChackPointPosition;
    //最後に通過したチェックポイントの回転
    [SerializeField]
    private Quaternion m_LastChackPointRotation;

    private float m_Time;
    public bool isHit=false;
    void Awake()
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
        {
            // すべての親オブジェクトに対して MonobitView コンポーネントを検索する
            if (GetComponentInParent<MonobitEngine.MonobitView>() != null)
            {
                m_MonobitView = GetComponentInParent<MonobitEngine.MonobitView>();
            }
            // 親オブジェクトに存在しない場合、すべての子オブジェクトに対して MonobitView コンポーネントを検索する
            else if (GetComponentInChildren<MonobitEngine.MonobitView>() != null)
            {
                m_MonobitView = GetComponentInChildren<MonobitEngine.MonobitView>();
            }
            // 親子オブジェクトに存在しない場合、自身のオブジェクトに対して MonobitView コンポーネントを検索して設定する
            else
            {
                m_MonobitView = GetComponent<MonobitEngine.MonobitView>();
            }
        }
    }
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        m_LastChackPointPosition = transform.position;
        m_LastChackPointRotation = Quaternion.identity;
        m_FadeIN.SetActive(false);
        m_ReSpownSE.SetActive(false);
    }
    private void Update()
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
        {
            if (!m_MonobitView.isMine)
            {
                return;
            }
        }
        if (isHit==true)
        {
            m_Time += Time.deltaTime;
            if (m_Time >= 1)
            {
                transform.position = m_LastChackPointPosition;
                transform.rotation = m_LastChackPointRotation;
             
                isHit = false;
                playerMove.m_Hp = 100;
                m_Time = 0;

            }
        }
        if(isHit==false)
        {
          
            m_FadeIN.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CheckPoint"))
        {
            //最後のチェックポイントの位置と回転を戻す
            m_LastChackPointPosition=other.transform.position;
            m_LastChackPointRotation=other.transform.rotation;
        }

        if (other.gameObject.CompareTag("Water"))
        {
           isHit= true;
            m_FadeIN.SetActive(true);
            m_ReSpownSE.SetActive(true);
            Instantiate(m_ReSpownEffect, transform.position, Quaternion.identity);
        }
    }
  
}
