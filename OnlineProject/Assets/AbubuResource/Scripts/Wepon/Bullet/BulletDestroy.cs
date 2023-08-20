using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    //オブジェクトが消えるまでの時間
    [SerializeField]
    private float m_DestroyTime=3;
    private float m_Time;

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if(m_Time > m_DestroyTime)
        {
            Destroy(gameObject);
        }
    }
}
