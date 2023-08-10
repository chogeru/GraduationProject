using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadSystem : MonoBehaviour
{
    [System.Serializable]
    public class SceneData
    {
        //�V�[���̖��O
        [SerializeField,Header("�V�[���̖��O")]
        public string sceneName;
        
    }

    [SerializeField]
    private SceneData[] m_SceneDataArray;

    public void ButtonClickToSceneChange(int index)
    {
        if (index >= 0 && index < m_SceneDataArray.Length)
        {
            SceneManager.LoadScene(m_SceneDataArray[index].sceneName);
        }
        else
        {
            Debug.LogError("Invalid scene index");
        }
    }
}
