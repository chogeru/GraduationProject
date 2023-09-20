using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGimmckEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject m_WaterEffect;
    [SerializeField]
    private GameObject m_Boss;
    private float m_Time;
    private void Start()
    {
        m_WaterEffect.SetActive(false);
        m_Boss.SetActive(false);
    }
    private void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time >= 2.5)
        {
            m_WaterEffect.SetActive(true);
        }
        if(m_Time >=3)
        {
            m_Boss.SetActive(true);
        }
    }
}
