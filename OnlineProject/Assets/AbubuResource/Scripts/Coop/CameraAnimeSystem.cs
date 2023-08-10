using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimeSystem : MonoBehaviour
{
    [SerializeField, Header("�T�[�o�[�ݒ���")]
    private GameObject m_ServerSettingScreen;
    [SerializeField]
    private Animator m_CameraAnimator;
    [SerializeField,Header("�L�����N�^�[�I����ʐ^��")]
    private GameObject m_CharactorSelectMiddle;
    [SerializeField, Header("�L�����N�^�[�I����ʍ�")]
    private GameObject m_CharactorSelectLeft;
    [SerializeField, Header("�L�����N�^�[�I����ʉE")]
    private GameObject m_CharactorSelectRight;
    [SerializeField, Header("�L�����N�^�[�I����ʉB��")]
    private GameObject m_CharactorSelectHidden;
    [SerializeField, Header("�L�����N�^�[�I�����")]
    private GameObject m_CharaSlectCanvas;
    [SerializeField,Header("�J�����̈ړ��l")]
    private float moveAmount = 5f; 
    private void Start()
    {
        m_CharaSlectCanvas.SetActive(false);
        m_CharactorSelectHidden.SetActive(false);
        m_CharactorSelectMiddle.SetActive(false);
        m_CharactorSelectLeft.SetActive(false);
        m_CharactorSelectRight.SetActive(false);
        m_CameraAnimator = GetComponent<Animator>();
    }

    public void StartButton()
    {
        m_CharaSlectCanvas.SetActive(true);
        m_CameraAnimator.SetBool("SelectMode", true);
        m_CharactorSelectMiddle.SetActive(true);
        m_ServerSettingScreen.SetActive(false);
        m_CharactorSelectHidden.SetActive(false);
        m_CharactorSelectLeft.SetActive(false);
        m_CharactorSelectRight.SetActive(false);
    }
    public void CameraLeftAnime()
    {
        m_CameraAnimator.SetBool("SelectMode", false);
        m_CharactorSelectHidden.SetActive(false);
        m_CharactorSelectMiddle.SetActive(false);
        m_CharactorSelectRight.SetActive(false);
        m_CharactorSelectLeft.SetActive(true);
    }
    public void CameraRightAnime()
    {
        m_CameraAnimator.SetBool("SelectMode", false);
        m_CharactorSelectHidden.SetActive(false);
        m_CharactorSelectMiddle.SetActive(false);
        m_CharactorSelectRight.SetActive(true);
        m_CharactorSelectLeft.SetActive(false);
    }
}
