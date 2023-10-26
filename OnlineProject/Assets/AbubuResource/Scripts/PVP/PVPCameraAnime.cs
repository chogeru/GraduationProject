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
        m_CameraAnimetor.SetBool("PVP��ʈڍs",true);
        m_CameraAnimetor.SetBool("PVP����I����ʈڍs", false);
        m_CoopAnimator.SetBool("isActive", false);
        m_PVPAnimator.SetBool("isActive", true);
    }
    public void CoopSelect()
    {
        m_CameraAnimetor.SetBool("Coop��ʈڍs", true);
        m_CameraAnimetor.SetBool("Coop����I����ʈڍs", false);
        m_CoopAnimator.SetBool("isActive", true);
        m_PVPAnimator.SetBool("isActive", false);
    }
    public void PVPtoSelectScreen()
    {
        m_CameraAnimetor.SetBool("PVP����I����ʈڍs", true);
        m_CameraAnimetor.SetBool("PVP��ʈڍs", false);
        m_CoopAnimator.SetBool("isActive", false);
        m_PVPAnimator.SetBool("isActive", false);
    }
    public void CooptoSelectScreen()
    {
        m_CameraAnimetor.SetBool("Coop����I����ʈڍs", true);
        m_CameraAnimetor.SetBool("Coop��ʈڍs", false);
        m_CoopAnimator.SetBool("isActive", false);
        m_PVPAnimator.SetBool("isActive", false);
    }
}
