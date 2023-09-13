using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneLoadPrefab : MonoBehaviour
{
    [SerializeField,Header("キャラクター選択時のカメラ")]
    private GameObject m_CharaSelectCamera;
    
    public void LoadGameMap()
    {
        m_CharaSelectCamera.SetActive(false);
    }
}
