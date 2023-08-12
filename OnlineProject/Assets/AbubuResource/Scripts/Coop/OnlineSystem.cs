using MonobitEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineSystem : MonobitEngine.MonoBehaviour
{
    [SerializeField,Header("�}�b�`���[���̍ő�l��")]
    private byte m_MaxPlayers = 8;
    [SerializeField,Header("���g�̃I�u�W�F�N�g���������ꂽ���ǂ���")]
    private bool m_IsSpownMyChara = false;
    
    public void Awake()
    {
        MonobitNetwork.autoJoinLobby = true;
    }
}
