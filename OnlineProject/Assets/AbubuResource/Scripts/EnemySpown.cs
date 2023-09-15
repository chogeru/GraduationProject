using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpown : MonoBehaviour
{
    [SerializeField]
    private string[] playerTags; // �X�|�[�����g���K�[����v���C���[�^�O�̔z��
    [SerializeField]
    private GameObject[] m_EnemyPrefabs; // �X�|�[������Enemy�̃v���n�u�̔z��
    [SerializeField]
    private float m_SpawnDistanceThreshold = 10f; // �X�|�[�����g���K�[���鋗����臒l
    [SerializeField]
    private float m_MinSpawnInterval = 3f; // �ŏ��X�|�[���Ԋu]
    [SerializeField]
    private float m_MaxSpawnInterval = 8f; // �ő�X�|�[���Ԋu
    [SerializeField]
    private GameObject m_ParticleEffectPrefab; // �p�[�e�B�N���G�t�F�N�g�̃v���n�u
    [SerializeField]
    private float m_SpawnTimer = 0f;
    [SerializeField]
    private Transform m_PlayerTransform;
    [SerializeField]
    private AudioClip m_SpownAudio;
    private float m_Volume=1f;

    private void Start()
    {
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // �v���C���[�Ƃ̋������v�Z
        float distanceToPlayer = Vector3.Distance(transform.position, m_PlayerTransform.position);

        // �v���C���[����苗���ȓ��ɂ���ꍇ
        if (distanceToPlayer <= m_SpawnDistanceThreshold)
        {
            // �^�C�}�[���X�V
            m_SpawnTimer -= Time.deltaTime;

            // �X�|�[���Ԋu���o�߂����ꍇ
            if (m_SpawnTimer <= 0f)
            {
                // �����_����Enemy�v���n�u��I��
                GameObject randomEnemyPrefab = m_EnemyPrefabs[Random.Range(0, m_EnemyPrefabs.Length)];

                // �v���n�u���X�|�[��
                Instantiate(randomEnemyPrefab, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(m_SpownAudio, transform.position,m_Volume);
                // �p�[�e�B�N���G�t�F�N�g���Đ�
                if (m_ParticleEffectPrefab != null)
                {
                    Instantiate(m_ParticleEffectPrefab, transform.position, Quaternion.identity);
                }

                // �V�����X�|�[���Ԋu�������_���ɐݒ�
                m_SpawnTimer = Random.Range(m_MinSpawnInterval, m_MaxSpawnInterval);
            }
        }
    }
}

