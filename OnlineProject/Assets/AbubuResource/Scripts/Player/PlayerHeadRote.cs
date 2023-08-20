using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadRote : MonoBehaviour
{
    [SerializeField,Header("��]�̊��x")]
    private float m_Sensitivity = 2.0f;
    [SerializeField,Header("Y���̍ő��]�p�x")]
    private float m_MaxYRotation = 15.0f;

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        // ���݂̊p�x�Ɖ�]�ʂ��v�Z
        float currentXRotation = transform.localEulerAngles.x;
        float newRotationX = currentXRotation - mouseY * m_Sensitivity;

        // Y���̉�]�𐧌�
        if (newRotationX > 180) newRotationX -= 360; // -180����180�͈̔͂ɂ���
        newRotationX = Mathf.Clamp(newRotationX, -m_MaxYRotation, m_MaxYRotation);

        // X������ɐ��������ɉ�]
        transform.localEulerAngles = new Vector3(newRotationX, transform.localEulerAngles.y, 0);
    }
}
