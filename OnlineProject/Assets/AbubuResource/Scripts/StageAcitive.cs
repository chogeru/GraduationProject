using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAcitive : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Stage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject Stage in m_Stage)
            {
                Stage.SetActive(true);
            }
        }
    }
}
