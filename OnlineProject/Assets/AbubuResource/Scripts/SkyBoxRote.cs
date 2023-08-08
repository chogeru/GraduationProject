using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxRote : MonoBehaviour
{
    float m_CurRot = 0f;
    [SerializeField]
    float m_RotSpeed = 5f;

    void Update()
    {
        m_CurRot += m_RotSpeed * Time.deltaTime;
        m_CurRot %= 360f;
        RenderSettings.skybox.SetFloat("_Rotation", m_CurRot);
    }
}
