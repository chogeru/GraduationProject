using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrun : MonoBehaviour
{
    [SerializeField, Header("プレイヤーオブジェクト")]
    private GameObject m_Player;

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        //プレイヤーと自身の座標を計算
        Vector3 PlayerTrun=m_Player.transform.position-this.transform.position;
        //Y軸を固定
        PlayerTrun.y = 0f;
        //回転値を所得
        Quaternion quaternion = Quaternion.LookRotation(PlayerTrun);
        //回転値をオブジェクトに設定
        this.transform.rotation = quaternion;
    }
}
