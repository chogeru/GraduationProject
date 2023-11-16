using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReSpawn : MonoBehaviour
{
    private Vector3 m_StartPosition;
    private Quaternion m_StartRotetion;
    [SerializeField]
    private GameObject m_ReSpawnEffect;
    // Start is called before the first frame update
    void Start()
    {
        m_StartPosition = transform.position;
        m_StartRotetion = transform.rotation;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Water"))
        {
            Respawn();
        }
    }
    void Respawn()
    {
        Instantiate(m_ReSpawnEffect, transform.position, Quaternion.identity);
        transform.position = m_StartPosition;
        transform.rotation = m_StartRotetion;
    }
}
