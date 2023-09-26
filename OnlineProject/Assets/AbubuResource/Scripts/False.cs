using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class False : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MyObj;
    private float m_Time;
   

    void Update()
    {
        m_Time += Time.deltaTime;
        if(m_Time>1)
        {
            m_Time = 0;
            m_MyObj.SetActive(false);

        }
    }
}
