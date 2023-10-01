using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoadSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MainCamera; // �J�����I�u�W�F�N�g�ւ̎Q�Ƃ�ێ�����ϐ�
    private Animator m_Animator; // �J�����̃A�j���[�^�[�R���|�[�l���g�ւ̎Q�Ƃ�ێ�����ϐ�
    [SerializeField]
    private GameObject m_SelectCanvas;

    private void Start()
    {
        m_Animator = m_MainCamera.GetComponent<Animator>(); // �J�����̃A�j���[�^�[�R���|�[�l���g���擾
    }
    // �V�[���̏����i�[����N���X
    [System.Serializable]
    public class SceneData
    {
        [SerializeField, Header("�V�[���̖��O")]
        public string sceneName; // �V�[���̖��O��ێ�����ϐ�
    }

    [SerializeField]
    private SceneData[] m_SceneDataArray; // �V�[������ێ�����z��

    // �{�^�����N���b�N���ꂽ�Ƃ��ɌĂяo�����֐��B�w�肳�ꂽ�C���f�b�N�X�̃V�[�������[�h����
    public void ButtonClickToSceneChange(int index)
    {
        if (index >= 0 && index < m_SceneDataArray.Length) // �C���f�b�N�X���L���Ȕ͈͓��ł���ꍇ
        {
            m_SelectCanvas.SetActive(false);
            m_Animator.SetBool("isSelect", true); // �J�����̃A�j���[�^�[��isSelect�p�����[�^��true�ɐݒ肵�ăA�j���[�V�����Đ����J�n
         //   SceneManager.LoadScene(m_SceneDataArray[index].sceneName); // �w�肳�ꂽ�V�[�������[�h����
            StartCoroutine(LoadSceneWithDelay(m_SceneDataArray[index].sceneName, 2f));
        }
        else
        {
            Debug.LogError("Invalid scene index"); // �C���f�b�N�X�������ȏꍇ�A�G���[���b�Z�[�W�����O�ɏo��
        }
    }
    private IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}

