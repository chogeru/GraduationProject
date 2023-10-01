using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SettingUI;

    private void Start()
    {
        m_SettingUI.SetActive(false);
    }
    public void OpenSettingUI()
    {
        m_SettingUI.SetActive(true);
    }
    public void CloseSettingUI()
    {
        m_SettingUI.SetActive(false);
    }
}
