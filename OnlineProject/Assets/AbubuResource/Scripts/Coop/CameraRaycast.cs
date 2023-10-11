using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField, Header("�p�[�e�B�N���v���n�u")]
    public GameObject m_ParticlePrefab; // �p�[�e�B�N���̃v���n�u���C���X�y�N�^���犄�蓖�Ă�

    [SerializeField, Header("�m��{�^��")]
    private GameObject m_Button;

    private GameObject m_CurrentParticle; // ���݂̃p�[�e�B�N����ێ�����ϐ�
    [SerializeField, Header("�v���C���[�̍����̃I�t�Z�b�g")]
    private float m_PlayerOffset = 2.5f;
    [SerializeField, Header("�I�[�f�B�I�\�[�X�R���|�[�l���g������")]
    private AudioSource m_AudioSource;

    public string lastHitObjectName; // �Ō�Ƀq�b�g�����I�u�W�F�N�g�̖��O��ێ�����ϐ�
    [SerializeField]
    public Vector3 m_SpownPoint;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    m_Button.SetActive(true);
                    lastHitObjectName = hit.collider.gameObject.name; // �q�b�g�����I�u�W�F�N�g�̖��O��ۑ�
                    if (m_CurrentParticle != null)
                    {
                        // �O�̃p�[�e�B�N����j��
                        Destroy(m_CurrentParticle);
                    }
                    m_AudioSource.Play();
                    Vector3 spawnPosition = hit.collider.transform.position + Vector3.up * m_PlayerOffset;
                    SpawnParticles(spawnPosition);
                }
            }
        }
    }

    private void SpawnParticles(Vector3 position)
    {

        // �p�[�e�B�N���𐶐����Ďw��ʒu�ɔz�u
        m_CurrentParticle = Instantiate(m_ParticlePrefab, position, Quaternion.identity);
    }
    public void SpownPlayer()
    {
        // lastHitObjectName �ɕۑ����ꂽ�I�u�W�F�N�g�̖��O�����v���C���[�v���n�u�𐶐����鏈��
        GameObject playerPrefabToSpawn = Resources.Load<GameObject>(lastHitObjectName);
        if (playerPrefabToSpawn != null)
        {
            // �X�|�[���|�C���g�Ƀv���C���[�v���n�u�𐶐�
            Instantiate(playerPrefabToSpawn, m_SpownPoint, Quaternion.identity);
        }
    }
}