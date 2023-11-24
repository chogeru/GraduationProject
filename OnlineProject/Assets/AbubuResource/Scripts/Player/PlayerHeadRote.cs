using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadRote : MonoBehaviour
{
    MonobitEngine.MonobitView m_MonobitView = null;
    [SerializeField,Header("��]�̊��x")]
    private float m_Sensitivity = 2.0f;
    [SerializeField,Header("Y���̍ő��]�p�x")]
    private float m_MaxYRotation = 15.0f;

    private void Awake()
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
        {
            // ���ׂĂ̐e�I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g����������
            if (GetComponentInParent<MonobitEngine.MonobitView>() != null)
            {
                m_MonobitView = GetComponentInParent<MonobitEngine.MonobitView>();
            }
            // �e�I�u�W�F�N�g�ɑ��݂��Ȃ��ꍇ�A���ׂĂ̎q�I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g����������
            else if (GetComponentInChildren<MonobitEngine.MonobitView>() != null)
            {
                m_MonobitView = GetComponentInChildren<MonobitEngine.MonobitView>();
            }
            // �e�q�I�u�W�F�N�g�ɑ��݂��Ȃ��ꍇ�A���g�̃I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g���������Đݒ肷��
            else
            {
                m_MonobitView = GetComponent<MonobitEngine.MonobitView>();
            }
        }
    }
    void Update()
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
        {
            if (!m_MonobitView.isMine)
            {
                return;
            }
        }
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
