using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoadSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MainCamera; // カメラオブジェクトへの参照を保持する変数
    private Animator m_Animator; // カメラのアニメーターコンポーネントへの参照を保持する変数
    [SerializeField]
    private GameObject m_SelectCanvas;

    private void Start()
    {
        m_Animator = m_MainCamera.GetComponent<Animator>(); // カメラのアニメーターコンポーネントを取得
    }
    // シーンの情報を格納するクラス
    [System.Serializable]
    public class SceneData
    {
        [SerializeField, Header("シーンの名前")]
        public string sceneName; // シーンの名前を保持する変数
    }

    [SerializeField]
    private SceneData[] m_SceneDataArray; // シーン情報を保持する配列

    // ボタンがクリックされたときに呼び出される関数。指定されたインデックスのシーンをロードする
    public void ButtonClickToSceneChange(int index)
    {
        if (index >= 0 && index < m_SceneDataArray.Length) // インデックスが有効な範囲内である場合
        {
            m_SelectCanvas.SetActive(false);
            m_Animator.SetBool("isSelect", true); // カメラのアニメーターでisSelectパラメータをtrueに設定してアニメーション再生を開始
         //   SceneManager.LoadScene(m_SceneDataArray[index].sceneName); // 指定されたシーンをロードする
            StartCoroutine(LoadSceneWithDelay(m_SceneDataArray[index].sceneName, 2f));
        }
        else
        {
            Debug.LogError("Invalid scene index"); // インデックスが無効な場合、エラーメッセージをログに出力
        }
    }
    private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}

