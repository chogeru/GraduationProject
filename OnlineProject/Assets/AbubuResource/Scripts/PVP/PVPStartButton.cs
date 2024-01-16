using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using MonobitEngineBase;
using UnityEngine.UIElements;

public class PVPStartButton : MonobitEngine.MonoBehaviour
{
    MonobitEngine.MonobitView m_MonobitView = null;
    [SerializeField, Header("�{�^���߂��ŕ\������L�����o�X")]
    private GameObject m_ButtonCanvas;
    [SerializeField, Header("�v���C���[�̃g�����X�t�H�[��")]
    private Transform m_Player;
    [SerializeField, Header("�v���C���[�Ƃ̋���")]
    private float m_Distance = 20;
    private Animator m_Animator;
    [SerializeField, Header("�{�^�����������Ƃ���SE")]
    private AudioClip m_ButtonSE;
    [SerializeField, Header("�{�^�����������Ƃ��̃p�[�e�B�N��")]
    private GameObject m_PushParticle;

    [SerializeField, Header("�X�|�i�[�I�u�W�F�N�g")]
    private GameObject m_Sponwer;
    [SerializeField, Header("PVP�o�g���}�l�[�W���[")]
    private GameObject m_PVPButtleManager;
    private float m_Volume = 0.5f;
    private bool isPush = true;
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
    void Start()
    {
        //�{�^���̃L�����o�X�̕\��
        m_ButtonCanvas.SetActive(false);
        m_Animator = GetComponent<Animator>();

    }

    void Update()
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
        ButtonPlaayerDistance();
    }
    [MunRPC]
    private void ButtonPlaayerDistance()
    {
        Vector3 Position = transform.position + Vector3.up * 0.5f;
        Vector3 PlayerDirection = m_Player.transform.position - transform.position;
        float PlayerDistance = PlayerDirection.magnitude;
        if (PlayerDistance < m_Distance && isPush)
        {
            m_ButtonCanvas.SetActive(true);
            if (Input.GetKey(m_KeyCode))
            {
                if (MonobitEngine.MonobitNetwork.offline == true)
                {
                    Push();
                }
                else
                {
                    m_MonobitView.RPC("Push", MonobitEngine.MonobitTargets.All, null);
                }
            }
        }
        else
        {
            m_ButtonCanvas.SetActive(false);
        }
    }
    [MunRPC]
    private void Push()
    {
        AudioSource.PlayClipAtPoint(m_ButtonSE, transform.position, m_Volume);
        Instantiate(m_PushParticle, transform.position, Quaternion.identity);
        m_Animator.SetBool("�{�^���_�E��", true);
        m_PVPButtleManager.SetActive(true);
        m_Sponwer.SetActive(true);
        isPush = false;
        Invoke("ButtonDestroy", 3);
    }

    private void ButtonDestroy()
    {
        GameObject myobj = transform.parent.gameObject;
        Destroy(myobj);
    }
}
