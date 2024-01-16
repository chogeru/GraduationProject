using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimeSystem : MonoBehaviour
{
    [SerializeField, Header("�T�[�o�[�ݒ���")]
    private GameObject m_ServerSettingScreen;
    [SerializeField]
    private Animator m_CameraAnimator;
    [SerializeField, Header("�L�����N�^�[�I�����")]
    private GameObject m_CharaSlectCanvas;
    [SerializeField, Header("���O�ݒ���")]
    private GameObject m_NameSettingCanvas;
    #region//�L�����N�^�[�I����ʂ̃L�����N�^�[�I�u�W�F�N�g
    [SerializeField,Header("�L�����N�^�[�I����ʐ^��")]
    private GameObject m_CharactorSelectMiddle;
    [SerializeField, Header("�L�����N�^�[�I����ʍ�")]
    private GameObject m_CharactorSelectLeft;
    [SerializeField, Header("�L�����N�^�[�I����ʉE")]
    private GameObject m_CharactorSelectRight;
    [SerializeField, Header("�L�����N�^�[�I����ʉB��")]
    private GameObject m_CharactorSelectHidden;
    [SerializeField, Header("�L�����N�^�[�A�j���[�V�����p�I�u�W�F�N�g")]
    private GameObject m_CharactorAnimetionObj;
    #endregion
 
    #region//�L�����N�^�[�I����ʂ̃{�^��
    [SerializeField,Header("�L�����N�^�[�I����ʐ^��Canvas")]
    private GameObject m_SelectCanvasMiddle;
    [SerializeField, Header("�L�����N�^�[�I����ʉECanvas")]
    private GameObject m_SelectCanvasRight;
    [SerializeField, Header("�L�����N�^�[�I����ʍ�Canvas")]
    private GameObject m_SelectCanvasLeft;
    #endregion

    private void Start()
    {
        m_CharactorAnimetionObj.SetActive(false);
        m_CharaSlectCanvas.SetActive(false);
        //�L�����N�^�[�I�u�W�F�N�g�̏����\���ݒ�
        m_CharactorSelectHidden.SetActive(false);
        m_CharactorSelectMiddle.SetActive(false);
        m_CharactorSelectLeft.SetActive(false);
        m_CharactorSelectRight.SetActive(false);
        m_CameraAnimator = GetComponent<Animator>();
        //�L�����N�^�[�I����ʂ̃L�����o�X
        m_SelectCanvasMiddle.SetActive(false);
        m_SelectCanvasLeft.SetActive(false);
        m_SelectCanvasRight.SetActive(false);
    }

    public void StartButton()
    {
        m_NameSettingCanvas.SetActive(false);
        StartCoroutine(StartCanvasActive());
        m_CameraAnimator.SetBool("SelectMode", true);
        m_ServerSettingScreen.SetActive(false);
        /////////////////////////////////////////////////////
        //�\�����؂�ւ��L�����N�^�[�I�u�W�F�N�g
        m_CharactorSelectMiddle.SetActive(true);
        m_CharactorSelectHidden.SetActive(false);
        m_CharactorSelectLeft.SetActive(false);
        m_CharactorSelectRight.SetActive(false);
        /////////////////////////////////////////////////////
        m_SelectCanvasMiddle.SetActive(true);
        m_SelectCanvasLeft.SetActive(false);
        m_SelectCanvasRight.SetActive(false);
        

    }
    public void CameraLeftAnime()
    {
        m_CameraAnimator.SetBool("LeftTomiddle", false);
        m_CameraAnimator.SetBool("RightTomiddle", false);
        m_CameraAnimator.SetBool("CameraLeft", true);
        m_CameraAnimator.SetBool("CameraRight", false);
        m_CameraAnimator.SetBool("SelectMode", false);
        /////////////////////////////////////////////////////
        //�\�����؂�ւ��L�����N�^�[�I�u�W�F�N�g
        m_CharactorSelectHidden.SetActive(false);
        m_CharactorSelectMiddle.SetActive(false);
        m_CharactorSelectRight.SetActive(false);
        m_CharactorSelectLeft.SetActive(true);
        /////////////////////////////////////////////////////
        m_SelectCanvasMiddle.SetActive(false);
        m_SelectCanvasRight.SetActive(false);
        StartCoroutine(LeftButtonActive());
    }
    public void CameraRightAnime()
    {
        m_CameraAnimator.SetBool("LeftTomiddle", false);
        m_CameraAnimator.SetBool("RightTomiddle", false);
        m_CameraAnimator.SetBool("CameraRight", true);
        m_CameraAnimator.SetBool("CameraLeft", false);
        m_CameraAnimator.SetBool("SelectMode", false);
        /////////////////////////////////////////////////////
        //�\�����؂�ւ��L�����N�^�[�I�u�W�F�N�g
        m_CharactorSelectHidden.SetActive(false);
        m_CharactorSelectMiddle.SetActive(false);
        m_CharactorSelectRight.SetActive(true);
        m_CharactorSelectLeft.SetActive(false);
        /////////////////////////////////////////////////////
         m_SelectCanvasMiddle.SetActive(false);
        m_SelectCanvasLeft.SetActive(false);
        StartCoroutine(RightButtonActive());
    }
    public void LeftToMiddle()
    {
        m_CameraAnimator.SetBool("LeftTomiddle", true);
        m_CameraAnimator.SetBool("RightTomiddle",false);
        m_CameraAnimator.SetBool("CameraLeft", false);
        m_CameraAnimator.SetBool("CameraRight", false);
        m_CameraAnimator.SetBool("SelectMode", false);
        /////////////////////////////////////////////////////
        //�\�����؂�ւ��L�����N�^�[�I�u�W�F�N�g
        m_CharactorSelectHidden.SetActive(false);
        m_CharactorSelectMiddle.SetActive(true);
        m_CharactorSelectRight.SetActive(false);
        m_CharactorSelectLeft.SetActive(false);
        ////////////////////////////////////////////////////
        m_SelectCanvasLeft.SetActive(false);
        m_SelectCanvasRight.SetActive(false);
        StartCoroutine(MiddleButtonActive());
    }
    public void RightToMiddle()
    {
        m_CameraAnimator.SetBool("LeftTomiddle", false);
        m_CameraAnimator.SetBool("RightTomiddle", true);
        m_CameraAnimator.SetBool("CameraRight", false);
        m_CameraAnimator.SetBool("CameraLeft", false);
        m_CameraAnimator.SetBool("SelectMode", false);
        ////////////////////////////////////////////////////
        //�\�����؂�ւ��L�����N�^�[�I�u�W�F�N�g
        m_CharactorSelectHidden.SetActive(false);
        m_CharactorSelectMiddle.SetActive(true);
        m_CharactorSelectRight.SetActive(false);
        m_CharactorSelectLeft.SetActive(false);
        ////////////////////////////////////////////////////
        m_SelectCanvasLeft.SetActive(false);
        m_SelectCanvasRight.SetActive(false);
        StartCoroutine(MiddleButtonActive());

    }
    private IEnumerator StartCanvasActive()
    {
        yield return new WaitForSeconds(2);
        m_CharaSlectCanvas.SetActive(true);
    }
    private IEnumerator MiddleButtonActive()
    {
        yield return new WaitForSeconds(1);
        m_SelectCanvasMiddle.SetActive(true);
    }
    private IEnumerator LeftButtonActive()
    {
        yield return new WaitForSeconds(1);
        m_SelectCanvasLeft.SetActive(true);
    }
    private IEnumerator RightButtonActive()
    {
        yield return new WaitForSeconds(1);
        m_SelectCanvasRight.SetActive(true);
    }
    public void RecruitingPlaye()
    {
        m_CharactorAnimetionObj.SetActive(true);
        m_CameraAnimator.SetBool("isRecruitingPlayer", true);
    }

}
