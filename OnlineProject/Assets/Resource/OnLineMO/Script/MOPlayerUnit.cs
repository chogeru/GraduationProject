using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MonobitEngine;
using System.Collections.Generic;
/// <summary>
/// プレイヤー制御部
/// </summary>
public class MOPlayerUnit : MonobitEngine.MonoBehaviour
{
    public MOParameta m_MOParameta;
    public Animator m_Animator;

    #region 転送データ
    private Vector3 m_PlayerBaseVector3;
    private Quaternion m_PlayerBaseQuaternion;
    #endregion

    #region 転送後の移動旋回分割数
    public float m_LerpPoint = 0.5f;
    #endregion

    #region プレイヤーアバター性能
    [Header("前進後退スピード 0.前進　1.後退")]
    public int[] m_MoveSpeed;
    [Header("プレイヤーアバター旋回速度")]
    public float m_RotSpeed;
    [Header("重力値")]
    public float m_Gravity = 0.5f;
    #endregion
    /*
    #region MPFTトステム用
    [Header("MPFT:現在の重力")]
    public float AddGravity;
    [Header("MPFT:ジャンプフラグ")]
    public bool JumpFlag;
    [Header("MPFT:ジャンプ入力のディレイ")]
    public float JumpDelay;
    [Header("MPFT:ジャンプパワー")]
    public Vector3 MovePower;
    [Header("MPFT:着地地点(膝)位置指定")]
    public Transform FootPoint;
    [Header("MPFT:着地地点(膝)から判定する距離")]
    public float FloorDistance = 0.5f;
    [Header("MPFT:加算ジャンプパワー")]
    public float AddJumpPower = 1.0f;
    [Header("MPFT:基礎重力")]
    public float BasicGravity = 1.0f;
    #endregion

    [Header("物理とリンク")]
    public Rigidbody m_Rigidbody;
    [Header("プレイヤーフラグ")]
    public bool m_PlayerFlag = false;
    [Header("オーディオソース")]
    public AudioSource m_AudioSource;
    */
    #region Start
    void Start()
    {/*
        if (monobitView.isMine)
        {
            m_PlayerFlag = true;
        }
        */
        if (!m_Animator)
        {
            m_Animator = this.GetComponent<Animator>();
        }
        //初期座標
        m_PlayerBaseVector3 = this.transform.position;
        //初期向き
        m_PlayerBaseQuaternion = this.transform.rotation;
        //物理をどうオブジェクトから取得する
      //  m_Rigidbody = this.GetComponent<Rigidbody>();
    }
    #endregion

    #region Updates
    void Update()
    {
        if (m_MOParameta.m_Hp > 0)
        {
            //移動・旋回
            Moving();
        }
    }
    private void LateUpdate()
    {
    }
    #endregion

    #region キャラクター移動
    public void Moving()
    {
        bool flag = false;
        // この戦車を保有している(プレイヤー)
        if (monobitView.isMine)
        {/*
            //カメラON
            //m_Camera.SetActive(true);

            //キャラクター簡易移動システム
            MPFT.MPFT_EasySytem.FE_EasyPlayerMove_TypeA(
                this.transform,
                ref AddGravity,
                ref JumpFlag,
                ref JumpDelay,
                MovePower,
                FootPoint,
                FloorDistance,
                AddJumpPower,
                BasicGravity
                );

            //アニメーション処理
            m_Animator.SetFloat("X", Input.GetAxis("Horizontal"));
            m_Animator.SetFloat("Y", Input.GetAxis("Vertical"));
            m_Animator.SetBool("Jump", JumpFlag);
            */
        }
        else
        {
            //物理回転フリーズ
          //  this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            //戦車移動をOnMonobitSerializeViewで受け取った情報を元に
            //Lerpを利用して滑らかに移動・旋回させる
            if (this.transform.position != m_PlayerBaseVector3)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, m_PlayerBaseVector3, m_LerpPoint);
                flag = true;
            }
            if (this.transform.rotation != m_PlayerBaseQuaternion)
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, m_PlayerBaseQuaternion, m_LerpPoint);
                flag = true;
            }
        }
    }
    #endregion
            
    #region 独自同期システム
    // オブジェクトの同期データについて独自情報を読み書きします。
    public void OnMonobitSerializeView(
        MonobitEngine.MonobitStream stream,
        MonobitEngine.MonobitMessageInfo info)
    {
        if (stream.isWriting)
        {
            //
            //同期データの書き込み
            //
            stream.Enqueue(this.transform.position);
            stream.Enqueue(this.transform.rotation);
            #region
            
            #region アニメーターパラメーター
            stream.Enqueue(m_Animator.GetBool("walkFlag"));
            stream.Enqueue(m_Animator.GetBool("idleFlag"));
            stream.Enqueue(m_Animator.GetBool("jumpFlag"));
            
            #endregion
        }
        else
        {
            //
            //同期データの読み込み
            //
            m_PlayerBaseVector3 = (Vector3)stream.Dequeue();
            m_PlayerBaseQuaternion = (Quaternion)stream.Dequeue();
            
            #region アニメーターパラメーター
            m_Animator.SetBool("walkFlag", (bool)stream.Dequeue());
            m_Animator.SetBool("idleFlag", (bool)stream.Dequeue());
            m_Animator.SetBool("JumpFlag", (bool)stream.Dequeue());
            #endregion
        }
    }
    #endregion
    #endregion

}
