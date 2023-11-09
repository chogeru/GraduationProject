using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrun : MonoBehaviour
{
    [SerializeField, Header("�v���C���[�I�u�W�F�N�g")]
    private GameObject m_Player;

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        //�v���C���[�Ǝ��g�̍��W���v�Z
        Vector3 PlayerTrun=m_Player.transform.position-this.transform.position;
        //Y�����Œ�
        PlayerTrun.y = 0f;
        //��]�l������
        Quaternion quaternion = Quaternion.LookRotation(PlayerTrun);
        //��]�l���I�u�W�F�N�g�ɐݒ�
        this.transform.rotation = quaternion;
    }
}
