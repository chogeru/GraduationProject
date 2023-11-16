using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ImputTextGetter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_DisplayText;
   public void OnEndEdit()
    {
        string inputFiledText = GetComponent<InputField>().text;
        m_DisplayText.text = inputFiledText;
    }
}
