using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class CoopScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ScoreText;
    [SerializeField]
    public static int m_ScorePoint;
    [SerializeField, Header("時間経過で下がるポイント")]
    private int m_DownPoint;
    public bool isCountStop = false;
    private void Start()
    {
        m_ScorePoint = 0;
    }
    private void Update()
    {
        if (isCountStop == false)
        {
            m_ScorePoint -= m_DownPoint;
        }
        m_ScoreText.text = m_ScorePoint.ToString();
    }
 
    public static void AddScore(int point)
    {
        m_ScorePoint += point;
    }
}
