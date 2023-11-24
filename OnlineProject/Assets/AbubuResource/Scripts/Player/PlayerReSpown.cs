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

    //�Ō�ɒʉ߂����`�F�b�N�|�C���g�̍��W
    [SerializeField]
    private Vector3 m_LastChackPointPosition;
    //�Ō�ɒʉ߂����`�F�b�N�|�C���g�̉�]
    [SerializeField]
    private Quaternion m_LastChackPointRotation;

    private float m_Time;
    public bool isHit=false;
    void Awake()
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
        {
            // ���ׂĂ̐e�I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g����������
            if (GetComponentInParent<MonobitEngine.MonobitView>() != null)
            {
                m_MonobitView = GetComponentInParent<MonobitEngine.MonobitView>();
            }
            // �e�I�u�W�F�N�g�ɑ��݂��Ȃ��ꍇ�A���ׂĂ̎q�I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g����������
            else if (GetComponentInChildren<MonobitEngine.MonobitView>() != null)
            {
                m_MonobitView = GetComponentInChildren<MonobitEngine.MonobitView>();
            }
            // �e�q�I�u�W�F�N�g�ɑ��݂��Ȃ��ꍇ�A���g�̃I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g���������Đݒ肷��
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
            //�Ō�̃`�F�b�N�|�C���g�̈ʒu�Ɖ�]��߂�
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
