using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float m_MoveSpeed = 5f;
    [SerializeField, Header("ジャンプ力")]
    private float m_JumpForce = 10f;
    [SerializeField, Header("通常移動速度")]
    private float m_Speed = 5f;
    [SerializeField, Header("ダッシュ")]
    private float m_RunSpeed = 2f;
    [SerializeField, Header("加速力")]
    private float m_AccelerationAmount = 2f;
    [SerializeField, Header("回転力")]
    private float m_Sensitivity = 2.0f;
    private float m_HorizontalInput;
    private float m_VerticalInput;
    [SerializeField, Header("移動時のパーティクル")]
    private GameObject m_MoveParticle;

    [SerializeField, Header("ジャンプ時のSE")]
    private AudioClip m_JumpSound;
    //音量
    private float m_Volume = 0.5f;
    //アニメーター
    [SerializeField]
    private Animator m_Animator;
    //リジットボディ
    [SerializeField]
    private Rigidbody rb;
    //着地しているかどうか
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
        // プレイヤーの移動
        m_HorizontalInput = Input.GetAxis("Horizontal");
        m_VerticalInput = Input.GetAxis("Vertical");

        //アニメーターにキー入力分の数値を代入
        m_Animator.SetFloat("左右", Input.GetAxis("Horizontal"));
        m_Animator.SetFloat("前後", Input.GetAxis("Vertical"));
        m_Animator.SetFloat("強左右", Input.GetAxis("Horizontal"));
        m_Animator.SetFloat("強前後", Input.GetAxis("Vertical"));
       
        //キー入力による移動量を求める
        Vector3 move = CalcMoveDir(m_HorizontalInput, m_VerticalInput) * m_Speed;
        //現在の移動量を所得
        Vector3 current = rb.velocity;
        current.y = 0f;

        //現在の移動量との差分だけプレイヤーに力を加える
        rb.AddForce(move - current, ForceMode.VelocityChange);
        
    
        //ダッシュ処理
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
        // マウスの移動量を取得
        float mouseX = Input.GetAxis("Mouse X");

        // オブジェクトを回転させる
        // Y軸を基準に水平方向に回転
        transform.Rotate(Vector3.up, mouseX * m_Sensitivity, Space.World);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.distance < 0.2f)
            {
                // ジャンプ処理
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
        //指定された移動力移動ベクトルを求める
        Vector3 moveVec = new Vector3(moveX, 0f, moveZ).normalized;
        //ベクトルに変換して、返す
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
