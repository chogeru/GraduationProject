using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private float m_Time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if(m_Time>5)
        {
            Destroy(gameObject);
        }
    }
}
