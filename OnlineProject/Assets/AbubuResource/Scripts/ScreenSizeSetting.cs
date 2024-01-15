using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScreenSizeSetting : MonoBehaviour
{
    public Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    void Start()
    {
        // ��ʃT�C�Y�̎擾
        resolutions = Screen.resolutions;

        // �h���b�v�_�E���̃I�v�V������ݒ�
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        // ���p�\�ȉ�ʃT�C�Y���h���b�v�_�E���ɒǉ�
        foreach (Resolution resolution in resolutions)
        {
            string option = $"{resolution.width} x {resolution.height}";
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
    }

    public void ChangeResolution(int index)
    {
        // �h���b�v�_�E���̑I���ɉ����ĉ�ʃT�C�Y��ύX
        if (index >= 0 && index < resolutions.Length)
        {
            Resolution selectedResolution = resolutions[index];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }
    }
}
