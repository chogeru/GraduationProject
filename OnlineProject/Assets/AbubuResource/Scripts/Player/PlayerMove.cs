using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]
    private float m_MoveSpeed = 5f;
    [SerializeField, Header("�W�����v��")]
    private float m_JumpForce = 10f;
    [SerializeField, Header("�ʏ�ړ����x")]
    private float m_Speed = 5f;
    [SerializeField, Header("�_�b�V��")]
    private float m_RunSpeed = 2f;
    [SerializeField, Header("������")]
    private float m_AccelerationAmount = 2f;
    [SerializeField, Header("��]��")]
    private float m_Sensitivity = 2.0f;
    private float m_HorizontalInput;
    private float m_VerticalInput;
    [SerializeField, Header("�ړ����̃p�[�e�B�N��")]
    private GameObject m_MoveParticle;

    [SerializeField, Header("�W�����v����SE")]
    private AudioClip m_JumpSound;
    //����
    private float m_Volume = 0.5f;
    //�A�j���[�^�[
    [SerializeField]
    private Animator m_Animator;
    //���W�b�g�{�f�B
    [SerializeField]
    private Rigidbody rb;
    //���n���Ă��邩�ǂ���
    [SerializeField]
    private bool isGrounded = true;

    [SerializeField]
    private int m_MaxHp;
    private int m_MinHp;
    [SerializeField]
    public int m_Hp;
    [SerializeField]
    public int m_PlayerDamage;

    [SerializeField]
    private GameObject m_RecoveryEffect;
    [SerializeField]
    private GameObject m_RecoverySE;
    private void Start()
    {
        m_RecoveryEffect.SetActive(false);
        m_RecoverySE.SetActive(false);
        m_MoveParticle.SetActive(false);
        rb = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // �v���C���[�̈ړ�
        m_HorizontalInput = Input.GetAxis("Horizontal");
        m_VerticalInput = Input.GetAxis("Vertical");

        //�A�j���[�^�[�ɃL�[���͕��̐��l����
        m_Animator.SetFloat("���E", Input.GetAxis("Horizontal"));
        m_Animator.SetFloat("�O��", Input.GetAxis("Vertical"));
        m_Animator.SetFloat("�����E", Input.GetAxis("Horizontal"));
        m_Animator.SetFloat("���O��", Input.GetAxis("Vertical"));
       
        //�L�[���͂ɂ��ړ��ʂ����߂�
        Vector3 move = CalcMoveDir(m_HorizontalInput, m_VerticalInput) * m_Speed;
        //���݂̈ړ��ʂ�����
        Vector3 current = rb.velocity;
        current.y = 0f;

        //���݂̈ړ��ʂƂ̍��������v���C���[�ɗ͂�������
        rb.AddForce(move - current, ForceMode.VelocityChange);
        
    
        //�_�b�V������
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_MoveParticle.SetActive(true);
            m_MoveSpeed = m_RunSpeed;
            m_Animator.SetBool("Run", true);
        }
        else
        {
            m_MoveParticle.SetActive(false);
            m_MoveSpeed = m_Speed;
            m_Animator.SetBool("Run", false);
        }
      
       
    }
    private void Update()
    {
        m_Hp = Mathf.Clamp(m_Hp, m_MinHp, m_MaxHp);
        // �}�E�X�̈ړ��ʂ��擾
        float mouseX = Input.GetAxis("Mouse X");

        // �I�u�W�F�N�g����]������
        // Y������ɐ��������ɉ�]
        transform.Rotate(Vector3.up, mouseX * m_Sensitivity, Space.World);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.distance < 0.2f)
            {
                // �W�����v����
                if (isGrounded && Input.GetKeyDown(KeyCode.Space))
                {
                    AudioSource.PlayClipAtPoint(m_JumpSound, transform.position, m_Volume);
                    m_Animator.SetBool("Jump", true);
                    rb.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
                    isGrounded = false;
                }
            }
            else
            {
                m_Animator.SetBool("Jump", false);
                isGrounded = true;
            }
        }
    }

    private Vector3 CalcMoveDir(float moveX, float moveZ)
    {
        //�w�肳�ꂽ�ړ��͈ړ��x�N�g�������߂�
        Vector3 moveVec = new Vector3(moveX, 0f, moveZ).normalized;
        //�x�N�g���ɕϊ����āA�Ԃ�
        Vector3 moveDir = transform.rotation * moveVec;
        moveDir.y = 0f;
        return moveDir.normalized;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("RecoveryItem"))
        {
            m_RecoveryEffect.SetActive(true);
            m_RecoverySE.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RecoveryItem"))
        {
            m_RecoveryEffect.SetActive(false);
            m_RecoverySE.SetActive(false);
        }
    }
}
