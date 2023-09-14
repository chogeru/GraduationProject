using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAiconFalse : MonoBehaviour
{
    private float m_Time;
    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if(m_Time>1)
        {
            gameObject.SetActive(false);
        }
    }
}
