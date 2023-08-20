using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField,Header("�ړ����x")]
    private float m_MoveSpeed = 5f;
    [SerializeField,Header("�W�����v��")]
    private float m_JumpForce = 10f;
    [SerializeField, Header("�ʏ�ړ����x")]
    private float m_Speed = 5f;
    [SerializeField, Header("�_�b�V��")]
    private float m_RunSpeed=2f;
    [SerializeField,Header("������")]
    private float m_AccelerationAmount = 2f;
    [SerializeField,Header("��]��")]
    private float m_Sensitivity = 2.0f;

    [SerializeField, Header("�ړ����̃p�[�e�B�N��")]
    private GameObject m_MoveParticle;


    [SerializeField,Header("�W�����v����SE")]
    private AudioClip m_JumpSound;
    //����
    private float m_Volume=0.5f;
    //�A�j���[�^�[
    private Animator m_Animator;
    //���W�b�g�{�f�B
    private Rigidbody rb;
    //���n���Ă��邩�ǂ���
    private bool isGrounded = true;

    private void Start()
    {
        m_MoveParticle.SetActive(false);
        rb = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // �v���C���[�̈ړ�
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //�A�j���[�^�[�ɃL�[���͕��̐��l����
        m_Animator.SetFloat("���E", Input.GetAxis("Horizontal"));
        m_Animator.SetFloat("�O��", Input.GetAxis("Vertical"));
        m_Animator.SetFloat("�����E", Input.GetAxis("Horizontal"));
        m_Animator.SetFloat("���O��", Input.GetAxis("Vertical"));
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * m_MoveSpeed * Time.deltaTime;
        transform.Translate(movement);
        // �}�E�X�̈ړ��ʂ��擾
        float mouseX = Input.GetAxis("Mouse X");

        // �I�u�W�F�N�g����]������
        // Y������ɐ��������ɉ�]
        transform.Rotate(Vector3.up, mouseX * m_Sensitivity, Space.World);
        //�_�b�V������
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_MoveParticle.SetActive(true);
            m_MoveSpeed = m_RunSpeed;
            m_Animator.SetBool("Run",true);
        }
        else
        {
            m_MoveParticle.SetActive(false);
            m_MoveSpeed = m_Speed;
            m_Animator.SetBool("Run", false);
        }
      
        // �W�����v����
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource.PlayClipAtPoint(m_JumpSound, transform.position,m_Volume);
            m_Animator.SetBool("Jump", true);
            rb.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        else
        {
            m_Animator.SetBool("Jump", false);
        }
        // ��������
        if (Input.GetKey(KeyCode.R))
        {
            Vector3 forwardDirection = transform.forward;
            rb.AddForce(forwardDirection * m_AccelerationAmount, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
