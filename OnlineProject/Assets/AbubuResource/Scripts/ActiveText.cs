using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveText : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ActiveObj;
    [SerializeField]
    private float m_ActiveTime;
    private float m_TIme;
    // Update is called once per frame
    void Update()
    {
        m_TIme += Time.deltaTime;
        if(m_TIme > m_ActiveTime)
        {
            m_ActiveObj.SetActive(true);
        }
    }
}
