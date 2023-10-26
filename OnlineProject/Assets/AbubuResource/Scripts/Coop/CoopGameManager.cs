using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CoopGameManager : MonoBehaviour
{
    [SerializeField, Header("“¢”°‘ÎÛ")]
    private GameObject m_SubjugationTarget;
    [SerializeField, Header("ƒNƒŠƒA‰æ–Ê")]
    private GameObject m_ClearCanvas;
    [SerializeField]
    public bool isClear=false;
    private float m_GameEndTime;
    private void Start()
    {
        m_ClearCanvas.SetActive(false); 
    }
    private void Update()
    {
        if(isClear)
        {
            m_GameEndTime += Time.deltaTime;
        }
        if(m_GameEndTime>5)
        {
            SceneManager.LoadScene("Title");
        }
    }
    public void GameClear()
    {
        m_ClearCanvas.SetActive(true);
    }
}

