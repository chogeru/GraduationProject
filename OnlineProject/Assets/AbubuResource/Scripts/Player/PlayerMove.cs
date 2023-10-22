using MalbersAnimations;
using MalbersAnimations.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]
    public float m_MoveSpeed = 10f;
    [SerializeField, Header("�W�����v��")]
    private float m_JumpForce = 10f;
    [SerializeField, Header("�ʏ�ړ����x")]
    public float m_Speed = 10;
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
    private float m_Volume = 0.5f;

    //�A�j���[�^�[
    [SerializeField]
    private Animator m_PlayerAnimator;
    [SerializeField]
    private Animator m_CameraAnimator;
    [SerializeField]
    private Animator m_TPSCameAnimator;
    //���W�b�g�{�f�B
    [SerializeField]
    private Rigidbody m_Rigidbody;
    [SerializeField]
    private Slider m_HpSlider;
    [SerializeField, Header("HP�\���p�e�L�X�g")]
    private TextMeshProUGUI m_HpText;

    public PlayerReSpown playerReSpown;

    //���n���Ă��邩�ǂ���
    [SerializeField]
    private bool isGrounded = true;
    public bool isRecovery = false;

    [SerializeField]
    private int m_MaxHp;
    [SerializeField]
    private int m_MinHp=0;
    [SerializeField]
    public int m_Hp;
    [SerializeField]
    public int m_PlayerDamage;

    [SerializeField]
    private GameObject m_TPSCamera;
    [SerializeField]
    private GameObject m_TPSZoomCamera;
    [SerializeField]
    private GameObject m_RecoveryEffect;
    [SerializeField]
    private GameObject m_RecoverySE;
    [SerializeField]
    private GameObject m_FadInCanvas;
    private void Start()
    {
        
        //Slider�𖞃^���ɂ���B
        m_HpSlider.value = 1;
        //���݂�HP���ő�HP�Ɠ����ɁB
        m_Hp = m_MaxHp;
        // m_HpText �̏�����
        m_HpText.text = m_Hp + "/" + m_MaxHp;
        m_RecoveryEffect.SetActive(false);
        m_RecoverySE.SetActive(false);
        m_MoveParticle.SetActive(false);
        m_CameraAnimator = m_TPSZoomCamera.GetComponent<Animator>();
        m_TPSCameAnimator= m_TPSCamera.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        m_Speed = Mathf.Clamp(m_Speed,0,20);
        // �v���C���[�̈ړ�
        m_HorizontalInput = Input.GetAxis("Horizontal");
        m_VerticalInput = Input.GetAxis("Vertical");

        //�A�j���[�^�[�ɃL�[���͕��̐��l����
        m_PlayerAnimator.SetFloat("���E", Input.GetAxis("Horizontal"));
        m_PlayerAnimator.SetFloat("�O��", Input.GetAxis("Vertical"));
        m_PlayerAnimator.SetFloat("�����E", Input.GetAxis("Horizontal"));
        m_PlayerAnimator.SetFloat("���O��", Input.GetAxis("Vertical"));
       
        //�L�[���͂ɂ��ړ��ʂ����߂�
        Vector3 move = CalcMoveDir(m_HorizontalInput, m_VerticalInput) * m_Speed;
        //���݂̈ړ��ʂ�����
        Vector3 current = m_Rigidbody.velocity;
        current.y = 0f;

        //���݂̈ړ��ʂƂ̍��������v���C���[�ɗ͂�������
        m_Rigidbody.AddForce(move - current, ForceMode.VelocityChange);
        
    
        //�_�b�V������
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_MoveParticle.SetActive(true);
            m_Speed = m_RunSpeed;
            m_PlayerAnimator.SetBool("Run", true);
        }
        else
        {
            m_MoveParticle.SetActive(false);
            m_Speed = m_MoveSpeed;
            m_PlayerAnimator.SetBool("Run", false);
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
                    m_PlayerAnimator.SetBool("Jump", true);
                    m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
                    isGrounded = false;
                }
            }
            else
            {
                m_PlayerAnimator.SetBool("Jump", false);
                isGrounded = true;
            }
        }
        if(Input.GetMouseButton(1))
        {
            m_TPSCamera.SetActive(false);
            m_TPSZoomCamera.SetActive(true);
            m_CameraAnimator.SetBool("isZoom", true);
        }
        else
        {
            m_TPSCamera.SetActive(true);
            m_TPSZoomCamera.SetActive(false);
            m_CameraAnimator.SetBool("isZoom", false);
            m_TPSCameAnimator.SetBool("isTPS", true);
        }
        if(isRecovery)
        {
            HpUpdate();
            isRecovery = false;
        }
    }
 
    public void TakeDamage(int damageAmount)
    {
        // �_���[�W���󂯂����̏���
        m_Hp -= damageAmount;
        m_HpSlider.value = (float)m_Hp / (float)m_MaxHp;
        // HP �e�L�X�g���X�V
        m_HpText.text = m_Hp + "/" + m_MaxHp;
        m_PlayerAnimator.SetBool("isHit", true);
        if (m_Hp <= 0)
        {
            Die();
        }
    }
    private void EndHit()
    {
        m_PlayerAnimator.SetBool("isHit", false);
    }
    private void Die()
    {
        playerReSpown.isHit = true;
        m_FadInCanvas.SetActive(true);
        m_PlayerAnimator.SetBool("isDie", true);
    }
    private void EndDie()
    {
        m_Hp = m_MaxHp;
        m_FadInCanvas.SetActive(false);
        m_PlayerAnimator.SetBool("isDie", false);
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
            HpUpdate();
            m_RecoveryEffect.SetActive(true);
            m_RecoverySE.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RecoveryItem"))
        {
            HpUpdate();
            m_RecoveryEffect.SetActive(false);
            m_RecoverySE.SetActive(false);
        }
    }
    private void HpUpdate()
    {
        //HP�o�[�̍X�V
        m_HpSlider.value=(float)m_Hp/(float)m_MaxHp;
        //HP�e�L�X�g�̍X�V
        m_HpText.text=m_Hp + "/" + m_MaxHp;
    }
}
