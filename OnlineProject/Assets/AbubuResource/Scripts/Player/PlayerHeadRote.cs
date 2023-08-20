using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadRote : MonoBehaviour
{
    [SerializeField,Header("‰ñ“]‚ÌŠ´“x")]
    private float m_Sensitivity = 2.0f;
    [SerializeField,Header("Y²‚ÌÅ‘å‰ñ“]Šp“x")]
    private float m_MaxYRotation = 15.0f;

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        // Œ»İ‚ÌŠp“x‚Æ‰ñ“]—Ê‚ğŒvZ
        float currentXRotation = transform.localEulerAngles.x;
        float newRotationX = currentXRotation - mouseY * m_Sensitivity;

        // Y²‚Ì‰ñ“]‚ğ§ŒÀ
        if (newRotationX > 180) newRotationX -= 360; // -180‚©‚ç180‚Ì”ÍˆÍ‚É‚·‚é
        newRotationX = Mathf.Clamp(newRotationX, -m_MaxYRotation, m_MaxYRotation);

        // X²‚ğŠî€‚É‚’¼•ûŒü‚É‰ñ“]
        transform.localEulerAngles = new Vector3(newRotationX, transform.localEulerAngles.y, 0);
    }
}
