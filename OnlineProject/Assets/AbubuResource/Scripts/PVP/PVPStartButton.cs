using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using MonobitEngineBase;
using UnityEngine.UIElements;

public class PVPStartButton : MonobitEngine.MonoBehaviour
{
    MonobitEngine.MonobitView m_MonobitView = null;
    [SerializeField, Header("ボタン近くで表示するキャンバス")]
    private GameObject m_ButtonCanvas;
    [SerializeField, Header("プレイヤーのトランスフォーム")]
    private Transform m_Player;
    [SerializeField, Header("プレイヤーとの距離")]
    private float m_Distance = 20;
    private Animator m_Animator;
    [SerializeField, Header("ボタンを押したときのSE")]
    private AudioClip m_ButtonSE;
    [SerializeField, Header("ボタンを押したときのパーティクル")]
    private GameObject m_PushParticle;

    [SerializeField, Header("スポナーオブジェクト")]
    private GameObject m_Sponwer;
    [SerializeField, Header("PVPバトルマネージャー")]
    private GameObject m_PVPButtleManager;
    private float m_Volume = 0.5f;
    private bool isPush = true;
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
    void Start()
    {
        //ボタンのキャンバスの表示
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
        m_Animator.SetBool("ボタンダウン", true);
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
