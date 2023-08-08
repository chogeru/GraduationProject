using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickChanger : MonoBehaviour
{
    [SerializeField,Header("�^�C�g�����")]
    private GameObject m_Title;
    [SerializeField,Header("���[�h�I�����")]
    private GameObject m_ModeSelect;
    [SerializeField,Header("�ݒ�p���")]
    private GameObject m_SettingScreen;
    [SerializeField,Header("�������")]
    private GameObject m_Tips;
    [SerializeField,Header("�����L���O���")]
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
