using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    private GameObject m_GameStartUI;
    [SerializeField]
    private GameObject m_ModeSelectUI;
    [SerializeField]
    private GameObject m_ButtonSE;

    private Animator m_Animetor;
    private void Start()
    {
        m_ButtonSE.SetActive(false);
        m_Animetor=GetComponent<Animator>();
        m_GameStartUI.SetActive(true);
        m_ModeSelectUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.Z))
        {
            m_ButtonSE.SetActive(true);
            m_Animetor.SetBool("isWater", true);
            m_GameStartUI.SetActive(false);
            m_ModeSelectUI.SetActive(true);
        } 
    }
}
