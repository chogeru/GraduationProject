using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPBulletSystem : MonoBehaviour
{
    public int damageAmount = 1; // �v���C���[�ɗ^����_���[�W��
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            // �Փ˂����I�u�W�F�N�g���v���C���[�̏ꍇ�A�̗͂����炷
            PlayerMove playermove = other.GetComponent<PlayerMove>();
            if (playermove != null)
            {
                playermove.TakeDamage(damageAmount);
            }
        }
    }
}
