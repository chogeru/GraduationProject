using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField,Header("移動速度")]
    private float m_MoveSpeed = 5f;
    [SerializeField,Header("ジャンプ力")]
    private float m_JumpForce = 10f;
    [SerializeField, Header("通常移動速度")]
    private float m_Speed = 5f;
    [SerializeField, Header("ダッシュ")]
    private float m_RunSpeed=2f;
    [SerializeField,Header("加速力")]
    private float m_AccelerationAmount = 2f;
    [SerializeField,Header("回転力")]
    private float m_Sensitivity = 2.0f;
    //リジットボディ
    private Rigidbody rb;
    //着地しているかどうか
    private bool isGrounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // プレイヤーの移動
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * m_MoveSpeed * Time.deltaTime;
        transform.Translate(movement);
        // マウスの移動量を取得
        float mouseX = Input.GetAxis("Mouse X");

        // オブジェクトを回転させる
        // Y軸を基準に水平方向に回転
        transform.Rotate(Vector3.up, mouseX * m_Sensitivity, Space.World);
        //ダッシュ処理
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_MoveSpeed = m_RunSpeed;
        }
        else
        {
            m_MoveSpeed = m_Speed;
        }
      
        // ジャンプ処理
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // 加速処理
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
