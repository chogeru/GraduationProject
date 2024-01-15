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
        // 画面サイズの取得
        resolutions = Screen.resolutions;

        // ドロップダウンのオプションを設定
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        // 利用可能な画面サイズをドロップダウンに追加
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
        // ドロップダウンの選択に応じて画面サイズを変更
        if (index >= 0 && index < resolutions.Length)
        {
            Resolution selectedResolution = resolutions[index];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }
    }
}
