using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneLoadPrefab : MonoBehaviour
{
    [SerializeField,Header("�L�����N�^�[�I�����̃J����")]
    private GameObject m_CharaSelectCamera;
    
    public void LoadGameMap()
    {
        m_CharaSelectCamera.SetActive(false);
    }
}
