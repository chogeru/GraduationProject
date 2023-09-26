using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MainCamera;
    private Animator m_Animator;
    private float m_Time;
    private bool isAnime=false;
    private bool isSelect=false;
    private void Start()
    {
        m_Animator = m_MainCamera.GetComponent<Animator>();
    }
    private void Update()
    {
        if(isAnime)
        {
            m_Time += Time.deltaTime;
            if (m_Time >= 2)
            {
                isSelect = true;
            }
        }
    }
    [System.Serializable]
    public class SceneData
    {
        //シーンの名前
        [SerializeField,Header("シーンの名前")]
        public string sceneName;
        
    }

    [SerializeField]
    private SceneData[] m_SceneDataArray;

    public void ButtonClickToSceneChange(int index)
    {
        if (index >= 0 && index < m_SceneDataArray.Length)
        {
            m_Animator.SetBool("isSelect", true);
            isAnime = true;
            if (isSelect)
            {
                SceneManager.LoadScene(m_SceneDataArray[index].sceneName);
            }
        }
        else
        {
            Debug.LogError("Invalid scene index");
        }
    }
}
