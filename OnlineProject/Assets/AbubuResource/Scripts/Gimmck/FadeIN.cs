using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIN : MonoBehaviour
{
    [SerializeField, Header("�t�F�[�h�C���L�����o�X")]
    private GameObject m_FadeinCanvas;

    private void Start()
    {
        m_FadeinCanvas.SetActive(false);
    }
    public void FadeIn()
    {
        m_FadeinCanvas.SetActive(true);
    }
}
