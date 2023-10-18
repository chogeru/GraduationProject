using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private int m_Damage;
    [SerializeField]
    private GameObject m_DestroyEffect;
    [SerializeField]
    private GameObject m_AttackEffect;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(m_DestroyEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(m_AttackEffect, transform.position, Quaternion.identity);
            // �Փ˂����I�u�W�F�N�g���v���C���[�̏ꍇ�A�̗͂����炷
            PlayerMove playermove = other.GetComponent<PlayerMove>();
            if (playermove != null)
            {
                playermove.TakeDamage(m_Damage);
            }
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

    }
}
