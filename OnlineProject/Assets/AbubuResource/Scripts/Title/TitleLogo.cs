using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogo : MonoBehaviour
{
    private Animator m_Animator;
    [SerializeField]
    private GameObject m_FadeIN;

    private float m_Time;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_FadeIN.SetActive(false);
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
            m_FadeIN.SetActive(true);
            Destroy(gameObject);
        }
    }
}
