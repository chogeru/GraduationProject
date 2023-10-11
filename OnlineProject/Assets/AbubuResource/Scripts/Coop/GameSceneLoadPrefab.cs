using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneLoadPrefab : MonoBehaviour
{
    [SerializeField,Header("�L�����N�^�[�I�����̃J����")]
    private GameObject m_CharaSelectCamera;
    [SerializeField,Header("�I�����C�����͉��")]
    private GameObject m_OnlineCanvas;
    [SerializeField]
    private GameObject m_GameMap;
 
    public void LoadGameMap()
    {
     
        m_GameMap.SetActive(true);
        m_OnlineCanvas.SetActive(false);
        m_CharaSelectCamera.SetActive(false);
    }
}
