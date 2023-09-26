using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReSpown : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ReSpownEffect;
    [SerializeField]
    private GameObject m_ReSpownSE;

    [SerializeField]
    private GameObject m_FadeIN;
    //最後に通過したチェックポイントの座標
    [SerializeField]
    private Vector3 m_LastChackPointPosition;
    //最後に通過したチェックポイントの回転
    [SerializeField]
    private Quaternion m_LastChackPointRotation;

    private float m_Time;
    private bool isHit=false;
    void Start()
    {
        m_LastChackPointPosition = transform.position;
        m_LastChackPointRotation = Quaternion.identity;
        m_FadeIN.SetActive(false);
        m_ReSpownSE.SetActive(false);
    }
    private void Update()
    {
       
        if (isHit==true)
        {
            m_Time += Time.deltaTime;
            if (m_Time >= 1)
            {  
                  
                transform.position = m_LastChackPointPosition;
                transform.rotation = m_LastChackPointRotation;
                isHit = false;
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
