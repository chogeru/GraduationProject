using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHit : MonoBehaviour
{
    [SerializeField,Header("�p�[�e�B�N��")]
    private GameObject m_ParticlePrefab;
    [SerializeField,Header("�I�[�f�B�I�\�[�X")]
    private AudioClip m_SoundEffect;
    [SerializeField,Header("����")]
    private float m_Volume = 1.0f;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �v���C���[�I�u�W�F�N�g�ɐڐG�����ۂ̏���
            AudioSource.PlayClipAtPoint(m_SoundEffect, transform.position, m_Volume);
            // �p�[�e�B�N���𐶐�
            Instantiate(m_ParticlePrefab, transform.position, Quaternion.identity);
            // ���g�̃I�u�W�F�N�g��j��
            Destroy(gameObject);
        }
    }
}
