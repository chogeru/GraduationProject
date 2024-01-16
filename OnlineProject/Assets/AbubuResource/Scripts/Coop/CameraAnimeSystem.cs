using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimeSystem : MonoBehaviour
{
    [SerializeField, Header("サーバー設定画面")]
    private GameObject m_ServerSettingScreen;
    [SerializeField]
    private Animator m_CameraAnimator;
    [SerializeField, Header("キャラクター選択画面")]
    private GameObject m_CharaSlectCanvas;
    [SerializeField, Header("名前設定画面")]
    private GameObject m_NameSettingCanvas;
    #region//キャラクター選択画面のキャラクターオブジェクト
    [SerializeField,Header("キャラクター選択画面真ん中")]
    private GameObject m_CharactorSelectMiddle;
    [SerializeField, Header("キャラクター選択画面左")]
    private GameObject m_CharactorSelectLeft;
    [SerializeField, Header("キャラクター選択画面右")]
    private GameObject m_CharactorSelectRight;
    [SerializeField, Header("キャラクター選択画面隠し")]
    private GameObject m_CharactorSelectHidden;
    [SerializeField, Header("キャラクターアニメーション用オブジェクト")]
    private GameObject m_CharactorAnimetionObj;
    #endregion
 
    #region//キャラクター選択画面のボタン
    [SerializeField,Header("キャラクター選択画面真ん中Canvas")]
    private GameObject m_SelectCanvasMiddle;
    [SerializeField, Header("キャラクター選択画面右Canvas")]
    private GameObject m_SelectCanvasRight;
    [SerializeField, Header("キャラクター選択画面左Canvas")]
    private GameObject m_SelectCanvasLeft;
    #endregion

    private void Start()
    {
        m_CharactorAnimetionObj.SetActive(false);
        m_CharaSlectCanvas.SetActive(false);
        //キャラクターオブジェクトの初期表示設定
        m_CharactorSelectHidden.SetActive(false);
        m_CharactorSelectMiddle.SetActive(false);
        m_CharactorSelectLeft.SetActive(false);
        m_CharactorSelectRight.SetActive(false);
        m_CameraAnimator = GetComponent<Animator>();
        //キャラクター選択画面のキャンバス
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
        //表示時切り替えキャラクターオブジェクト
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
        //表示時切り替えキャラクターオブジェクト
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
        //表示時切り替えキャラクターオブジェクト
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
        //表示時切り替えキャラクターオブジェクト
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
        //表示時切り替えキャラクターオブジェクト
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
