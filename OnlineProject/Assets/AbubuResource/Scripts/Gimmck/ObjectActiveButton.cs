using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectActiveButton : MonoBehaviour
{

    MonobitEngine.MonobitView m_MonobitView = null;

    [SerializeField,Header("アクティブにするオブジェクト")]
    private GameObject[] m_ActiveObject;
    [SerializeField, Header("非アクティブにするオブジェクト")]
    private GameObject[] m_ParticleObject;
    [SerializeField, Header("ボタン近くで表示するキャンバス")]
    private GameObject m_ButtonCanvas;
    [SerializeField,Header("プレイヤーのトランスフォーム")]
    private Transform m_Player;
    [SerializeField,Header("プレイヤーとの距離")]
    private float m_Distance=20;
    private Animator m_Animator;
    [SerializeField,Header("ボタンを押したときのSE")]
    private AudioClip m_ButtonSE;
    [SerializeField, Header("ボタンを押したときのパーティクル")]
    private GameObject m_PushParticle;
    [SerializeField, Header("他のボタンのオブジェクト")]
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
    }
    // Start is called before the first frame update
    void Start()
    {
        //ボタンのキャンバスの表示
        m_ButtonCanvas.SetActive(false);
        //プレイヤーオブジェクトの参照
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_Animator = GetComponent<Animator>();
        // m_ActiveObject 配列内の各オブジェクトを非表示にする
        foreach (GameObject obj in m_ActiveObject)
        {
            obj.SetActive(false);
        }
        // m_ParticleObject 配列内の各オブジェクトを表示にする
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

                // 最も近いプレイヤーを探す
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
                m_Animator.SetBool("ボタンダウン", true);
                m_Button.SetActive(false);
                //配列内のオブジェクトを表示
                foreach (GameObject obj in m_ActiveObject)
                {
                    obj.SetActive(true);
                }
                // m_ParticleObject 配列内の各オブジェクトを非表示にする
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
