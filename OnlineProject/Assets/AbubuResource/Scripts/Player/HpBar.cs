using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField,Header("Hp用のスライダー")]
    private Slider m_HpSlider;
    [SerializeField, Header("Hpバーに表示用テキスト")]
    private TextMeshProUGUI m_HpBarText;
    private int m_MaxBar;
    protected void StartHpBar()
    {
        //初期HPゲージを設定
        m_HpSlider.value = 1;
    }

    private void HpUpDate(int value)
    {
        float thisbar = Mathf.Clamp01((float)value / m_MaxBar);

        //HPテキストの更新
     //   m_HpBarText.text =  + "/" + m_MaxHp;
    }
}
