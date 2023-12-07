using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectActiveButton : MonoBehaviour
{

    MonobitEngine.MonobitView m_MonobitView = null;

    [SerializeField,Header("�A�N�e�B�u�ɂ���I�u�W�F�N�g")]
    private GameObject[] m_ActiveObject;
    [SerializeField, Header("��A�N�e�B�u�ɂ���I�u�W�F�N�g")]
    private GameObject[] m_ParticleObject;
    [SerializeField, Header("�{�^���߂��ŕ\������L�����o�X")]
    private GameObject m_ButtonCanvas;
    [SerializeField,Header("�v���C���[�̃g�����X�t�H�[��")]
    private Transform m_Player;
    [SerializeField,Header("�v���C���[�Ƃ̋���")]
    private float m_Distance=20;
    private Animator m_Animator;
    [SerializeField,Header("�{�^�����������Ƃ���SE")]
    private AudioClip m_ButtonSE;
    [SerializeField, Header("�{�^�����������Ƃ��̃p�[�e�B�N��")]
    private GameObject m_PushParticle;
    [SerializeField, Header("���̃{�^���̃I�u�W�F�N�g")]
    private GameObject m_Button;
    private float m_Volume = 0.5f;
    private bool isPush=true;
    private bool isBulletHit=false;
    [SerializeField]
    private KeyCode m_KeyCode;
    private void Awake()
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
    // Start is called before the first frame update
    void Start()
    {
        //�{�^���̃L�����o�X�̕\��
        m_ButtonCanvas.SetActive(false);
        //�v���C���[�I�u�W�F�N�g�̎Q��
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_Animator = GetComponent<Animator>();
        // m_ActiveObject �z����̊e�I�u�W�F�N�g���\���ɂ���
        foreach (GameObject obj in m_ActiveObject)
        {
            obj.SetActive(false);
        }
        // m_ParticleObject �z����̊e�I�u�W�F�N�g��\���ɂ���
        foreach (GameObject obj in m_ParticleObject)
        {
            obj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
        {
            m_MonobitView.RPC("ButtonPush", MonobitEngine.MonobitTargets.All, null);
        }
        else
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            if (players.Length > 0)
            {
                float closestDistance = Mathf.Infinity;
                GameObject closestPlayer = null;

                // �ł��߂��v���C���[��T��
                foreach (GameObject player in players)
                {
                    float distanceToPlayers = Vector3.Distance(transform.position, player.transform.position);

                    if (distanceToPlayers < closestDistance)
                    {
                        closestDistance = distanceToPlayers;
                        closestPlayer = player;
                    }
                }

                if (closestPlayer != null)
                {
                    m_Player = closestPlayer.transform;
                }
            }
            ButtonPush();
        }
    }
    [MunRPC]
    private void ButtonPush()
    {
        Vector3 Position = transform.position + Vector3.up * 0.5f;
        Vector3 PlayerDirection = m_Player.transform.position - transform.position;
        float PlayerDistance = PlayerDirection.magnitude;
        if (PlayerDistance < m_Distance && isPush)
        {
            m_ButtonCanvas.SetActive(true);
            if (Input.GetKey(m_KeyCode) || isBulletHit)
            {
                AudioSource.PlayClipAtPoint(m_ButtonSE, transform.position, m_Volume);
                Instantiate(m_PushParticle, Position, Quaternion.identity);
                m_Animator.SetBool("�{�^���_�E��", true);
                m_Button.SetActive(false);
                //�z����̃I�u�W�F�N�g��\��
                foreach (GameObject obj in m_ActiveObject)
                {
                    obj.SetActive(true);
                }
                // m_ParticleObject �z����̊e�I�u�W�F�N�g���\���ɂ���
                foreach (GameObject obj in m_ParticleObject)
                {
                    obj.SetActive(false);
                }

                isPush = false;
            }
        }
        else
        {
            m_ButtonCanvas.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isBulletHit = true;
        }
    }
}
