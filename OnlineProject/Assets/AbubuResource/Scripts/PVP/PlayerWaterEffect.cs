using AssetKits.ParticleImage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaterEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleImage m_WaterEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
         m_WaterEffect.Stop();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Water"))
        {
            m_WaterEffect.Play();
        }
    }
}
