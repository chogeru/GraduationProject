using MonobitEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineSystem : MonobitEngine.MonoBehaviour
{
    [SerializeField,Header("マッチルームの最大人数")]
    private byte m_MaxPlayers = 8;
    [SerializeField,Header("自身のオブジェクトが生成されたかどうか")]
    private bool m_IsSpownMyChara = false;
    
    public void Awake()
    {
        MonobitNetwork.autoJoinLobby = true;
    }
}
