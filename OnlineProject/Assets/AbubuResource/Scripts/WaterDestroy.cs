using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDestroy : MonoBehaviour
{
    [SerializeField]
    private GameObject m_WaterFadeout;
    private GameObject m_Player;
    private bool isHit;
    private float m_Time;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");  
    }

    // Update is called once per frame
    void Update()
    {
        if(isHit)
        {
            m_Time += Time.deltaTime;
            if(m_Time>=1.2)
            {
                Destroy(m_Player);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
           m_WaterFadeout.SetActive(true);
            isHit = true;
        }
    }
}
