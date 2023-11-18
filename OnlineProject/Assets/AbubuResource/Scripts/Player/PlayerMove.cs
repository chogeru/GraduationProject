using MalbersAnimations;
using MalbersAnimations.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    MonobitEngine.MonobitView m_MonobitView = null;
    [SerializeField, Header("移動速度")]
    public float m_MoveSpeed = 10f;
    [SerializeField, Header("ジャンプ力")]
    private float m_JumpForce = 10f;
    [SerializeField, Header("通常移動速度")]
    public float m_Speed = 10;
    [SerializeField, Header("ダッシュ")]
    private float m_RunSpeed = 2f;
    [SerializeField, Header("ダウンスピード")]
    private float m_DownSpeed;
    [SerializeField, Header("加速力")]
    private float m_AccelerationAmount = 2f;
    [SerializeField, Header("回転力")]
    private float m_Sensitivity = 2.0f;

    private float m_DieinvincibilityTime;
    private float m_HorizontalInput;
    private float m_VerticalInput;

    [SerializeField, Header("移動時のパーティクル")]
    private GameObject m_MoveParticle;
    [SerializeField, Header("ジャンプ時のSE")]
    private AudioClip m_JumpSound;
    private float m_Volume = 0.5f;

    //アニメーター
    [SerializeField]
    private Animator m_PlayerAnimator;
    [SerializeField]
    private Animator m_CameraAnimator;
    [SerializeField]
    private Animator m_TPSCameAnimator;
    //リジットボディ
    [SerializeField]
    private Rigidbody m_Rigidbody;
    [SerializeField]
    private Slider m_HpSlider;
    [SerializeField, Header("HP表示用テキスト")]
    private TextMeshProUGUI m_HpText;

    public PlayerReSpown playerReSpown;

    //着地しているかどうか
    [SerializeField]
    private bool isGrounded = true;
    public bool isRecovery = false;
    private bool isinvincibility = false;

    [SerializeField]
    public int m_MaxHp;
    [SerializeField]
    public int m_MinHp = 0;
    [SerializeField]
    public int m_Hp;
    [SerializeField]
    public int m_PlayerDamage;
    [SerializeField]
    private int m_PlayerMaxDamage;
    [SerializeField]
    private int m_DownDamage;

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

    [SerializeField]
    private AudioSource m_WaterSE;
    [SerializeField]
    private ParticleSystem m_WaterMoveEffect;
    private void Awake()
    {
        // すべての親オブジェクトに対して MonobitView コンポーネントを検索する
        if (GetComponentInParent<MonobitEngine.MonobitView>() != null)
        {
            m_MonobitView = GetComponentInParent<MonobitEngine.MonobitView>();
        }
        // 親オブジェクトに存在しない場合、すべての子オブジェクトに対して MonobitView コンポーネントを検索する
        else if (GetComponentInChildren<MonobitEngine.MonobitView>() != null)
        {
            m_MonobitView = GetComponentInChildren<MonobitEngine.MonobitView>();
        }
        // 親子オブジェクトに存在しない場合、自身のオブジェクトに対して MonobitView コンポーネントを検索して設定する
        else
        {
            m_MonobitView = GetComponent<MonobitEngine.MonobitView>();
        }
    }
    private void Start()
    {

        //Sliderを満タンにする。
        m_HpSlider.value = 1;
        //現在のHPを最大HPと同じに。
        m_Hp = m_MaxHp;
        // m_HpText の初期化
        m_HpText.text = m_Hp + "/" + m_MaxHp;
        m_RecoveryEffect.SetActive(false);
        m_RecoverySE.SetActive(false);
        m_MoveParticle.SetActive(false);
        m_CameraAnimator = m_TPSZoomCamera.GetComponent<Animator>();
        m_TPSCameAnimator = m_TPSCamera.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(!m_MonobitView.isMine)
        {
            return;
        }
        if (m_PlayerAnimator.GetBool("isDie"))
        {
            return;
        }
        m_Speed = Mathf.Clamp(m_Speed, 0, 20);
        // プレイヤーの移動
        m_HorizontalInput = Input.GetAxis("Horizontal");
        m_VerticalInput = Input.GetAxis("Vertical");

        //アニメーターにキー入力分の数値を代入
        m_PlayerAnimator.SetFloat("左右", Input.GetAxis("Horizontal"));
        m_PlayerAnimator.SetFloat("前後", Input.GetAxis("Vertical"));
        m_PlayerAnimator.SetFloat("強左右", Input.GetAxis("Horizontal"));
        m_PlayerAnimator.SetFloat("強前後", Input.GetAxis("Vertical"));

        //キー入力による移動量を求める
        Vector3 move = CalcMoveDir(m_HorizontalInput, m_VerticalInput) * m_Speed;
        //現在の移動量を所得
        Vector3 current = m_Rigidbody.velocity;
        current.y = 0f;

        //現在の移動量との差分だけプレイヤーに力を加える
        m_Rigidbody.AddForce(move - current, ForceMode.VelocityChange);


        //ダッシュ処理
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
        if(isinvincibility)
        {
            m_DieinvincibilityTime += Time.deltaTime;
            if(m_DieinvincibilityTime>=2)
            {
                isinvincibility = false;
            }
        }

    }
    private void Update()
    {
        if (!m_MonobitView.isMine)
        {
            return;
        }
        m_Hp = Mathf.Min(m_Hp, m_MaxHp);
        m_PlayerDamage=Mathf.Min(m_PlayerDamage,m_PlayerMaxDamage);
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
        if (Input.GetMouseButton(1))
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
        if(Input.GetMouseButton(0))
        {
            if (Mathf.Approximately(m_HorizontalInput, 0f) && Mathf.Approximately(m_VerticalInput, 0f))
            {
                // isAttack 関数を呼び出す
                isAttack();
            }
        }
        else
        {
            m_PlayerAnimator.SetBool("攻撃", false);
        }
        if (!Mathf.Approximately(m_HorizontalInput, 0f) || !Mathf.Approximately(m_VerticalInput, 0f))
        {
            m_PlayerAnimator.SetBool("攻撃", false);
        }
        if (isRecovery)
        {
            HpUpdate();
            isRecovery = false;
        }
    }
    private void isAttack()
    {
        m_PlayerAnimator.SetBool("攻撃", true);
    }
    private void JumpEnd()
    {
        m_PlayerAnimator.SetBool("Jump", false);
    }
    public void TakeDamage(int damageAmount)
    {
        if (isinvincibility==false)
        {
            // ダメージを受けた時の処理
            m_Hp -= damageAmount;
            m_Hp = Mathf.Max(m_Hp, m_MinHp);
            m_HpSlider.value = (float)m_Hp / (float)m_MaxHp;
            // HP テキストを更新
            m_HpText.text = m_Hp + "/" + m_MaxHp;
            m_PlayerAnimator.SetBool("isHit", true);
            if (m_Hp <= 0)
            {
                Die();
            }
        }
    }
    private void EndHit()
    {

        m_PlayerAnimator.SetBool("isHit", false);
    }
    private void Die()
    {
        CoopScoreManager.AddScore(-250);
        isinvincibility = true;
        playerReSpown.isHit = true;
        m_FadInCanvas.SetActive(true);
        m_PlayerAnimator.SetBool("isDie", true);
    }

    private void EndDie()
    {
        m_FadInCanvas.SetActive(false);
        m_PlayerAnimator.SetBool("isDie", false);
        m_Hp = m_MaxHp;
        // HP テキストを更新
        m_HpText.text = m_Hp + "/" + m_MaxHp;
        m_HpSlider.value = (float)m_Hp / (float)m_MaxHp;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            InWater();
            m_MoveSpeed -= m_DownSpeed;
            m_RunSpeed -= m_DownSpeed;
            m_PlayerDamage -= m_DownDamage;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("RecoveryItem"))
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
        if (other.gameObject.CompareTag("Water"))
        {
            OutWater();
            m_MoveSpeed += m_DownSpeed;
            m_RunSpeed += m_DownSpeed;
            m_PlayerDamage += m_DownDamage;
        }

    }
    private void HpUpdate()
    {
        //HPバーの更新
        m_HpSlider.value = (float)m_Hp / (float)m_MaxHp;
        //HPテキストの更新
        m_HpText.text = m_Hp + "/" + m_MaxHp;
    }
    private void InWater()
    {
        m_WaterMoveEffect.Play();
        m_WaterSE.Play();
    }
    private void OutWater()
    {
        m_WaterMoveEffect.Stop();
        m_WaterSE.Stop();
    }
}
