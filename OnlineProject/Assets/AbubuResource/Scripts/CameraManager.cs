using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera gameCamera; // アクティブにしたいカメラをInspectorで設定します

    void Start()
    {
        // シーン内のすべてのカメラを取得
        Camera[] cameras = FindObjectsOfType<Camera>();

        // 各カメラを非アクティブにする
        foreach (Camera camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }

        // Gameカメラをアクティブにする
        if (gameCamera != null)
        {
            gameCamera.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Gameカメラが設定されていません。");
        }
    }
}
