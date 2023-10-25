using AssetKits.ParticleImage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWaterEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject m_WaterEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            m_WaterEffect.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            m_WaterEffect.SetActive(false);
        }
    }
}
