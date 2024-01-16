using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.IO;
using System.Linq;
[System.Serializable]
public class ScorePoint
{
    public int score;
}

public class CoopScoreManager : MonobitEngine.MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ScoreText;
    [SerializeField]
    private TextMeshProUGUI Test;
    [SerializeField]
    public static int m_ScorePoint;
    [SerializeField, Header("時間経過で下がるポイント")]
    private int m_DownPoint;
    private float m_ScoreDownCoolTime = 1;
    public bool isCountStop;

    [SerializeField, Header("scoreリスト")]
    private List<int> topScores = new List<int>();

    [SerializeField, Header("上位3つのscore")]
    private TextMeshProUGUI topScoreText1;
    [SerializeField]
    private TextMeshProUGUI topScoreText2;
    [SerializeField]
    private TextMeshProUGUI topScoreText3;

    [SerializeField]
    private bool isRankingScene=false;

    private void Start()
    {
        isCountStop = true;
        m_ScorePoint = 0;
        LoadTopScores();
        if (isRankingScene)
        {
            DisplayTopScores();
        }
    }

    private void Update()
    {
        m_ScoreDownCoolTime += Time.deltaTime;
        if (isCountStop == false && m_ScoreDownCoolTime >= 1)
        {
            m_ScorePoint -= m_DownPoint;
            m_ScoreDownCoolTime = 0;
        }
        if (m_ScoreText!=null)
        {
            m_ScoreText.text = m_ScorePoint.ToString();
        }
        if (Test)
        {
            Test.text = m_ScorePoint.ToString();
        }
    }

    public void StartCount()
    {
        isCountStop = false;
    }

    public static void AddScore(int point)
    {
        m_ScorePoint += point;
    }
    public void SaveJson()
    {
        SaveScoreToJson();
        UpdateTopScores();
    }
    public void SaveScoreToJson()
    {
        ScorePoint scorePoint = new ScorePoint();
        scorePoint.score = m_ScorePoint;
        string json = JsonUtility.ToJson(scorePoint);

        // 保存先のファイルパス
        string filePath = Path.Combine(Application.persistentDataPath, "score.json");

        // ファイルに書き込み
        File.WriteAllText(filePath, json);
    }

    private void UpdateTopScores()
    {
        topScores.Add(m_ScorePoint);
        topScores = topScores.OrderByDescending(score => score).Take(3).ToList();
        SaveTopScores();
    }

    private void SaveTopScores()
    {
        string json = JsonUtility.ToJson(new ScorePointList { scores = topScores });
        string filePath = Path.Combine(Application.persistentDataPath, "topScores.json");
        File.WriteAllText(filePath, json);
    }

    private void LoadTopScores()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "topScores.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ScorePointList scoreList = JsonUtility.FromJson<ScorePointList>(json);
            topScores = scoreList.scores;
        }
    }
    private void DisplayTopScores()
    {
        LoadTopScores();

        if (topScores.Count >= 1)
        {
            topScoreText1.text = topScores[0].ToString();
        }
        if (topScores.Count >= 2)
        {
            topScoreText2.text = topScores[1].ToString();
        }
        if (topScores.Count >= 3)
        {
            topScoreText3.text = topScores[2].ToString();
        }
    }

    [System.Serializable]
    private class ScorePointList
    {
        public List<int> scores;
    }
}
