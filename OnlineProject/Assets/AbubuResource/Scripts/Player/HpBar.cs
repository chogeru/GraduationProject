using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField,Header("Hp�p�̃X���C�_�[")]
    private Slider m_HpSlider;
    [SerializeField, Header("Hp�o�[�ɕ\���p�e�L�X�g")]
    private TextMeshProUGUI m_HpBarText;
    private int m_MaxBar;
    protected void StartHpBar()
    {
        //����HP�Q�[�W��ݒ�
        m_HpSlider.value = 1;
    }

    private void HpUpDate(int value)
    {
        float thisbar = Mathf.Clamp01((float)value / m_MaxBar);

        //HP�e�L�X�g�̍X�V
     //   m_HpBarText.text =  + "/" + m_MaxHp;
    }
}
