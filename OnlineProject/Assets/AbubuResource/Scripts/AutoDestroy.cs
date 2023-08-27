using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private float m_destroyTime = 2;
    private float m_Time;
    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time > m_destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
