using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadRote : MonoBehaviour
{
    MonobitEngine.MonobitView m_MonobitView = null;
    [SerializeField,Header("回転の感度")]
    private float m_Sensitivity = 2.0f;
    [SerializeField,Header("Y軸の最大回転角度")]
    private float m_MaxYRotation = 15.0f;

    private void Awake()
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
        {
            // すべての親オブジェクトに対して MonobitView コンポーネントを検索する
            if (GetComponentInParent<MonobitEngine.MonobitView>() != null)
            {
                m_MonobitView = GetComponentInParent<MonobitEngine.MonobitView>();
            }
            // 親オブジェクトに存在しない場合、すべての子オブジェクトに対して MonobitView コンポーネントを検索する
            else if (GetComponentInChildren<MonobitEngine.MonobitView>() != null)
            {
                m_MonobitView = GetComponentInChildren<MonobitEngine.MonobitView>();
            }
            // 親子オブジェクトに存在しない場合、自身のオブジェクトに対して MonobitView コンポーネントを検索して設定する
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
        // 現在の角度と回転量を計算
        float currentXRotation = transform.localEulerAngles.x;
        float newRotationX = currentXRotation - mouseY * m_Sensitivity;

        // Y軸の回転を制限
        if (newRotationX > 180) newRotationX -= 360; // -180から180の範囲にする
        newRotationX = Mathf.Clamp(newRotationX, -m_MaxYRotation, m_MaxYRotation);

        // X軸を基準に垂直方向に回転
        transform.localEulerAngles = new Vector3(newRotationX, transform.localEulerAngles.y, 0);
    }
}
