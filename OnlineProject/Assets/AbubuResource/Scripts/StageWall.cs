using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWall : MonoBehaviour
{
    public int m_DieCount=0;
  
    void Update()
    {
        if(m_DieCount>=5)
        {
            Destroy(gameObject);
        }
    }
}
