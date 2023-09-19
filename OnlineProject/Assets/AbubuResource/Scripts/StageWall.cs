using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWall : MonoBehaviour
{
    public int m_DieCount=0;
    [SerializeField]
    private int m_MaxCount;
    void Update()
    {
        if(m_DieCount>=m_MaxCount)
        {
            Destroy(gameObject);
        }
    }
}
