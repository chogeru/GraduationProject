using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCamera : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_WaterSE;
    [SerializeField]
    private GameObject m_WaterImage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Water"))
        {
            m_WaterSE.Play();
        }
    }
}
