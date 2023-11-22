using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneChangeButton : MonoBehaviour
{

    [SerializeField]
    public GameObject m_GameEndCanvas;
    private bool isEnd=false;
    public void EndGameScreen()
    {
        m_GameEndCanvas.SetActive(true);
        isEnd = true;
    }
    private void Update()
    {
        if(isEnd)
        {
            Invoke("TitleScene", 5f);
        }
    }
    public void TitleScene()
    {
        DestroyAllWithTag("Player");
        DestroyAllWithTag("Enemy");
        SceneManager.LoadScene("Title");
    }
    private void DestroyAllWithTag(string tag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in taggedObjects)
        {
            Destroy(obj);
        }
    }
}
