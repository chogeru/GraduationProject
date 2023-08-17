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
    //���W�b�g�{�f�B
    private Rigidbody rb;
    //���n���Ă��邩�ǂ���
    private bool isGrounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // �v���C���[�̈ړ�
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

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
            m_MoveSpeed = m_RunSpeed;
        }
        else
        {
            m_MoveSpeed = m_Speed;
        }
      
        // �W�����v����
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            isGrounded = false;
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
