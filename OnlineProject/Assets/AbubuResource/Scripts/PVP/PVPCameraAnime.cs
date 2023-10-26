using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPCameraAnime : MonoBehaviour
{
    [SerializeField]
    private Animator m_CameraAnimetor;
    [SerializeField]
    private GameObject m_PVPCanvas;
    [SerializeField]
    private GameObject m_CoopCanvas;

    [SerializeField]
    private Animator m_PVPAnimator;
    [SerializeField]
    private Animator m_CoopAnimator;

    public void PVPSelect()
    {
        m_CameraAnimetor.SetBool("PVP画面移行",true);
        m_CameraAnimetor.SetBool("PVPから選択画面移行", false);
        m_CoopAnimator.SetBool("isActive", false);
        m_PVPAnimator.SetBool("isActive", true);
    }
    public void CoopSelect()
    {
        m_CameraAnimetor.SetBool("Coop画面移行", true);
        m_CameraAnimetor.SetBool("Coopから選択画面移行", false);
        m_CoopAnimator.SetBool("isActive", true);
        m_PVPAnimator.SetBool("isActive", false);
    }
    public void PVPtoSelectScreen()
    {
        m_CameraAnimetor.SetBool("PVPから選択画面移行", true);
        m_CameraAnimetor.SetBool("PVP画面移行", false);
        m_CoopAnimator.SetBool("isActive", false);
        m_PVPAnimator.SetBool("isActive", false);
    }
    public void CooptoSelectScreen()
    {
        m_CameraAnimetor.SetBool("Coopから選択画面移行", true);
        m_CameraAnimetor.SetBool("Coop画面移行", false);
        m_CoopAnimator.SetBool("isActive", false);
        m_PVPAnimator.SetBool("isActive", false);
    }
}
