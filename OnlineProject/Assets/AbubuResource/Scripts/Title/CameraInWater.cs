using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInWater : MonoBehaviour
{
    [SerializeField]
    private GameObject m_WaterInSE;
    [SerializeField]
    private GameObject m_WaterSE;

    void Start()
    {
        m_WaterInSE.SetActive(false);
        m_WaterSE.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Water"))
        {
            m_WaterInSE.SetActive(true);
            m_WaterSE.SetActive(true);
        }
    }
}
