using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MonobitEngine;
public class SceneChangeButton : MonobitEngine.MonoBehaviour
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
        MonobitEngine.MonobitNetwork.LeaveRoom();
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
