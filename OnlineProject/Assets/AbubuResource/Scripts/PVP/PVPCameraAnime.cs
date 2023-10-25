using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPCameraAnime : MonoBehaviour
{
    [SerializeField]
    private Animator m_CameraAnimetor;

    public void PVPSelect()
    {
        m_CameraAnimetor.SetBool("PVP画面移行",true);
        m_CameraAnimetor.SetBool("PVPから選択画面移行", false);
    }
    public void CoopSelect()
    {
        m_CameraAnimetor.SetBool("Coop画面移行", true);
        m_CameraAnimetor.SetBool("Coopから選択画面移行", false);
    }
    public void PVPtoSelectScreen()
    {
        m_CameraAnimetor.SetBool("PVPから選択画面移行", true);
        m_CameraAnimetor.SetBool("PVP画面移行", false);
    }
    public void CooptoSelectScreen()
    {
        m_CameraAnimetor.SetBool("Coopから選択画面移行", true);
        m_CameraAnimetor.SetBool("Coop画面移行", false);
    }
}
