using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDestroy : MonoBehaviour
{
    private float m_Time;
    private void Update()
    {
        m_Time += Time.deltaTime;
        if(m_Time>1)
        {
            Destroy(gameObject);
        }
    }
}
