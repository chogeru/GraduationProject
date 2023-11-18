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
       
    }
    private void Update()
    {
        m_ExplosionTime += Time.deltaTime;
        if(m_ExplosionTime>2)
        {
           
            Instantiate(m_ExplosionEffect, transform.position, Quaternion.identity);
            Instantiate(m_ExplosionCol, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
