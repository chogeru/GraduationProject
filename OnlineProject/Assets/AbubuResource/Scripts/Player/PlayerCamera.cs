using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    private float m_MousePosX;
    private float m_MousePosY;  
    void Update()
    {
        m_MousePosX = Input.GetAxis("Mouse X");
        m_MousePosY = Input.GetAxis("Mouse Y");
        if(Mathf.Abs(m_MousePosY)>0.001f)
        {
            transform.RotateAround(m_Player.transform.position, Vector3.up, m_MousePosY);
        }
        if(Mathf.Abs(m_MousePosY)>0.001f)
        {
            transform.RotateAround(m_Player.transform.position, transform.right, m_MousePosY);
        }
    }
}
