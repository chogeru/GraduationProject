using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PVPBattleManager : MonoBehaviour
{
    [SerializeField,Header("現在の時間")]
    private float m_CurrentTime;
    [SerializeField,Header("カウントダウンの時間")]
    private float m_CountDownTime;
    [SerializeField]
    private TextMeshProUGUI M_CountDownText;
    [SerializeField]
    private string m_Text;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentTime=m_CountDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentTime -= Time.deltaTime;
        if(m_CurrentTime < 0)
        {
            SceneManager.LoadScene("Title");
            m_CurrentTime = 0;
        }
        M_CountDownText.text=m_Text+ m_CurrentTime.ToString("F2");//F2で小数点2桁まで表示
    }
}
