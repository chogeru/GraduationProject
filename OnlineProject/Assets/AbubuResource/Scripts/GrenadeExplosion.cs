using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    
    private float m_ExplosionTime=0;
    [SerializeField]
    private GameObject m_ExplosionCol;
    [SerializeField]
    private GameObject m_ExplosionEffect;
    private void Start()
    {
        m_ExplosionCol.SetActive(false);
    }
    private void Update()
    {
        m_ExplosionTime += Time.deltaTime;
        if(m_ExplosionTime>2)
        {
            m_ExplosionCol.SetActive(true);
            Instantiate(m_ExplosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
