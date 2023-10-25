using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPCameraAnime : MonoBehaviour
{
    [SerializeField]
    private Animator m_CameraAnimetor;

    public void PVPSelect()
    {
        m_CameraAnimetor.SetBool("PVP��ʈڍs",true);
        m_CameraAnimetor.SetBool("PVP����I����ʈڍs", false);
    }
    public void CoopSelect()
    {
        m_CameraAnimetor.SetBool("Coop��ʈڍs", true);
        m_CameraAnimetor.SetBool("Coop����I����ʈڍs", false);
    }
    public void PVPtoSelectScreen()
    {
        m_CameraAnimetor.SetBool("PVP����I����ʈڍs", true);
        m_CameraAnimetor.SetBool("PVP��ʈڍs", false);
    }
    public void CooptoSelectScreen()
    {
        m_CameraAnimetor.SetBool("Coop����I����ʈڍs", true);
        m_CameraAnimetor.SetBool("Coop��ʈڍs", false);
    }
}
