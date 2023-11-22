using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PVPBattleManager : MonoBehaviour
{
    [SerializeField,Header("���݂̎���")]
    private float m_CurrentTime;
    [SerializeField,Header("�J�E���g�_�E���̎���")]
    private float m_CountDownTime;
    [SerializeField]
    private TextMeshProUGUI M_CountDownText;
    [SerializeField]
    private string m_Text;

    [SerializeField]
    private GameObject m_ScoreCanvas;
    [SerializeField]
    private GameObject m_GameEndScreen;
    private bool isEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        isEnd = false;
        m_CurrentTime=m_CountDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentTime -= Time.deltaTime;
        if(m_CurrentTime < 0)
        {
          m_ScoreCanvas.SetActive(true);
            isEnd = true;
            m_CurrentTime = 0;
        }
        M_CountDownText.text=m_Text+ m_CurrentTime.ToString("F2");//F2�ŏ����_2���܂ŕ\��

        if(isEnd)
        {
            Invoke("GameEnd", 5);
        }
    }
    public void ClerScreen()
    {
        m_GameEndScreen.SetActive(true);
    }
    private void GameEnd()
    {
        DestroyAllWithTag("Player");
        DestroyAllWithTag("Enemy");
        SceneManager.LoadScene("Title");
    }
    private void DestroyAllWithTag(string tag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in taggedObjects)
        {
            Destroy(obj);
        }
    }
}
