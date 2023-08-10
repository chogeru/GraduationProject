using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickChanger : MonoBehaviour
{
    [SerializeField,Header("タイトル画面")]
    private GameObject m_Title;
    [SerializeField,Header("モード選択画面")]
    private GameObject m_ModeSelect;
    [SerializeField,Header("設定用画面")]
    private GameObject m_SettingScreen;
    [SerializeField,Header("説明画面")]
    private GameObject m_Tips;
    [SerializeField,Header("ランキング画面")]
    private GameObject m_Ranking;
    private void Start()
    {
        m_Title.SetActive(true);
        m_ModeSelect.SetActive(false);
    }
    public void OnClickToSelectScreen()
    {
        m_Title.SetActive(false);
        m_ModeSelect.SetActive(true);
    }
    public void OnClickSettingScreen()
    {
        m_ModeSelect.SetActive(false);
        m_SettingScreen.SetActive(true);
    }
    public void OnClickTipsScreen()
    {
        m_ModeSelect.SetActive(false);
        m_Tips.SetActive(true);
    }
    public void OnClickCloceSettingScreen()
    {
        m_SettingScreen.SetActive(false);
        m_ModeSelect.SetActive(true);
    }
    public void OnClickCloceTipsScreen()
    {
        m_Tips.SetActive(false);
        m_ModeSelect.SetActive(true);
    }
}
