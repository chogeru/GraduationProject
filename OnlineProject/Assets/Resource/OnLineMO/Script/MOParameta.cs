using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// キャラクターパラメータ部
/// </summary>
public class MOParameta : MonoBehaviour
{
    [Header("プレイヤーのMHp")]
    public int m_MaxHp;
    [Header("プレイヤーのHp")]
    public int m_Hp;
    [Header("プレイヤーの物理攻撃力")]
    public float m_AttackPower;
}
