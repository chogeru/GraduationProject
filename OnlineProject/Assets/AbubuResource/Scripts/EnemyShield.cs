using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    [SerializeField,Header("�ő�HP")]
    private int m_MaxHp;
    [SerializeField, Header("�V�[���h��HP")]
    private int m_Hp;

    [SerializeField,Header("�V�[���h�j�󎞂̃G�t�F�N�g")]
    private GameObject m_BreakEffect;
    private void Start()
    {
        m_Hp = m_MaxHp;
    }
    private void Update()
    {
        if(m_Hp<=0)
        {
            Vector3 EffectSpawnPosition = transform.position;
            EffectSpawnPosition.y += 0f; 
            Instantiate(m_BreakEffect, EffectSpawnPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            m_Hp--;
            Destroy(other.gameObject);
        }
    }
}

