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
    private GameObject m_SettingButton;
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
}
