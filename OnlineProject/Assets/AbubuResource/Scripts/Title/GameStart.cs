using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    private GameObject m_GameStartUI;
    [SerializeField]
    private GameObject m_ModeSelectUI;


    private void Start()
    {
        m_GameStartUI.SetActive(true);
        m_ModeSelectUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.Z))
        {
            m_GameStartUI.SetActive(false);
            m_ModeSelectUI.SetActive(true);
        } 
    }
}
