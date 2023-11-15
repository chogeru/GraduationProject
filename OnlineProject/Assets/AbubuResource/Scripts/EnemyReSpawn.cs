using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReSpawn : MonoBehaviour
{
    private Vector3 m_StartPosition;
    // Start is called before the first frame update
    void Start()
    {
        m_StartPosition = transform.position;
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
        transform.position = m_StartPosition;
    }
}
