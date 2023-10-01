using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCol : MonoBehaviour
{
    public int damageAmount = 1; // プレイヤーに与えるダメージ量
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
            // 衝突したオブジェクトがプレイヤーの場合、体力を減らす
            PlayerMove playermove = other.GetComponent<PlayerMove>();
            if (playermove != null)
            {
                playermove.TakeDamage(damageAmount);
            }
        }
    }
}
