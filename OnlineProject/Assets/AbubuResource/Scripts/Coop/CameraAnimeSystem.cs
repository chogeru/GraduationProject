using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimeSystem : MonoBehaviour
{
    [SerializeField, Header("サーバー設定画面")]
    private GameObject m_ServerSettingScreen;
    [SerializeField]
    private Animator m_CameraAnimator;
    [SerializeField,Header("キャラクター選択画面真ん中")]
    private GameObject m_CharactorSelectMiddle;
    [SerializeField, Header("キャラクター選択画面左")]
    private GameObject m_CharactorSelectLeft;
    [SerializeField, Header("キャラクター選択画面右")]
    private GameObject m_CharactorSelectRight;
    [SerializeField, Header("キャラクター選択画面隠し")]
    private GameObject m_CharactorSelectHidden;
    [SerializeField, Header("キャラクター選択画面")]
    private GameObject m_CharaSlectCanvas;
    [SerializeField,Header("カメラの移動値")]
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
