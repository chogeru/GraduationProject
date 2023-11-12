using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CoopGameManager : MonoBehaviour
{
    [SerializeField, Header("討伐対象")]
    private GameObject m_SubjugationTarget;
    [SerializeField, Header("クリア画面")]
    private GameObject m_ClearCanvas;
    [SerializeField, Header("スコア画面")]
    private GameObject m_ScoreScreen;
    [SerializeField]
    private GameObject m_BGM;
    [SerializeField]
    private CoopScoreManager scoreManager;
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
          m_ScoreScreen.SetActive(true);
        }
    }
    public void GameClear()
    {
        m_BGM.SetActive(false);
        scoreManager.isCountStop = true;
        m_ClearCanvas.SetActive(true);
    }
}

