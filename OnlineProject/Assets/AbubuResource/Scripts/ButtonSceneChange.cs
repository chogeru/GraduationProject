using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonSceneChange : MonoBehaviour
{
    [SerializeField]
    private string m_SceneNaame;
    public void BottonSceneCghange()
    {
        SceneManager.LoadScene(m_SceneNaame);
    }
}
