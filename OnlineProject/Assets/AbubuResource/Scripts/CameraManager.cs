using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera gameCamera; // �A�N�e�B�u�ɂ������J������Inspector�Őݒ肵�܂�

    void Start()
    {
        // �V�[�����̂��ׂẴJ�������擾
        Camera[] cameras = FindObjectsOfType<Camera>();

        // �e�J�������A�N�e�B�u�ɂ���
        foreach (Camera camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }

        // Game�J�������A�N�e�B�u�ɂ���
        if (gameCamera != null)
        {
            gameCamera.gameObject.SetActive(true);
        }
    }
    private void CameraActive()
    {/*
        Camera[] camera=FindObjectsOfType<Camera>();
        foreach (Camera camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }*/
    }
}

