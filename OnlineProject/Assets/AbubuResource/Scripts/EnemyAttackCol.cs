using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCol : MonoBehaviour
{
    public int damageAmount = 1; // �v���C���[�ɗ^����_���[�W��
    [SerializeField]
    private GameObject m_AttackEffect;
    [SerializeField]
    private GameObject m_AttackSE;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(m_AttackEffect,transform.position,Quaternion.identity);
            m_AttackSE.SetActive(true);
            // �Փ˂����I�u�W�F�N�g���v���C���[�̏ꍇ�A�̗͂����炷
            PlayerMove playermove = other.GetComponent<PlayerMove>();
            if (playermove != null)
            {
                playermove.TakeDamage(damageAmount);
            }
        }
    }
}
