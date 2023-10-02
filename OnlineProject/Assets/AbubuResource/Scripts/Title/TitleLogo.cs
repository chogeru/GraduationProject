using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogo : MonoBehaviour
{
    private Animator m_Animator;
    private float m_Time;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime;
        if (m_Time > 3)
        {

            m_Animator.SetBool("isFadeOut", true);

        }
        if (m_Time > 5)
        {
            Destroy(gameObject);
        }
    }
}
