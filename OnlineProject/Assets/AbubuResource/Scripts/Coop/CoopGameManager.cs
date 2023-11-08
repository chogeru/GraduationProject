using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CoopGameManager : MonoBehaviour
{
    [SerializeField, Header("�����Ώ�")]
    private GameObject m_SubjugationTarget;
    [SerializeField, Header("�N���A���")]
    private GameObject m_ClearCanvas;
    [SerializeField, Header("�X�R�A���")]
    private GameObject m_ScoreScreen;
    [SerializeField]
    private ScoreManager scoreManager;
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
        scoreManager.isCountStop = true;
        m_ClearCanvas.SetActive(true);
    }
}

