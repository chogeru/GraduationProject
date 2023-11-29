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
    private string[] m_EnemyName;
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
    private float m_Volume = 1f;


    private void Start()
    {
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (MonobitEngine.MonobitNetwork.offline)
        {
            /*
            if (!MonobitEngine.MonobitNetwork.isHost)
            {
                return;
            }
            */
        }
        // �X�|�[���|�C���g����v���C���[�܂ł̋������v�Z���܂�
        float distanceToPlayer = Vector3.Distance(transform.position, m_PlayerTransform.position);

        // �X�|�[���|�C���g����5���[�g���ȓ���"Enemy"�^�O�̃I�u�W�F�N�g�����݂��邩���m�F���܂�
        bool isEnemyNearby = CheckForEnemiesNearby();

        // �v���C���[����苗���ȓ��ɂ��āA����5���[�g���ȓ���"Enemy"�^�O�̃I�u�W�F�N�g���Ȃ��ꍇ
        if (distanceToPlayer <= m_SpawnDistanceThreshold && !isEnemyNearby)
        {
            m_SpawnTimer -= Time.deltaTime;

            // �X�|�[���̃^�C�}�[���o�߂����ꍇ
            if (m_SpawnTimer <= 0f)
            {
                // �����_���ȃX�|�[���ʒu��X����Z���Ōv�Z���A�����̓X�|�[���|�C���g�Ɠ����ɂ���
                Vector3 randomSpawnPosition = new Vector3(
                    transform.position.x + Random.onUnitSphere.x * Random.Range(0f, 10f),
                    transform.position.y,  // �������X�|�[���|�C���g�Ɠ����ɂ���
                    transform.position.z + Random.onUnitSphere.z * Random.Range(0f, 10f)
                );

                if (MonobitEngine.MonobitNetwork.offline == false)
                {
                    string randomEnemy = m_EnemyName[Random.Range(0, m_EnemyName.Length)];
                    MonobitEngine.MonobitNetwork.Instantiate(randomEnemy, randomSpawnPosition, Quaternion.identity, 0);
                }
                if (MonobitEngine.MonobitNetwork.offline == true)
                {
                    // �����_���ȓG�v���n�u��I�����܂�
                    GameObject randomEnemyPrefab = m_EnemyPrefabs[Random.Range(0, m_EnemyPrefabs.Length)];
                    // �����_���Ȉʒu�ɓG���X�|�[�������܂�
                    Instantiate(randomEnemyPrefab, randomSpawnPosition, Quaternion.identity);
                }
                AudioSource.PlayClipAtPoint(m_SpownAudio, randomSpawnPosition, m_Volume);

                // �p�[�e�B�N���G�t�F�N�g���Đ����܂�
                if (m_ParticleEffectPrefab != null)
                {
                    Instantiate(m_ParticleEffectPrefab, randomSpawnPosition, Quaternion.identity);
                }

                // �V�����X�|�[���Ԋu�������_���ɐݒ肵�܂�
                m_SpawnTimer = Random.Range(m_MinSpawnInterval, m_MaxSpawnInterval);
            }
        }
    }

    bool CheckForEnemiesNearby()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 5f, LayerMask.GetMask("Enemy")))
        {
            return true;
        }
        return false;
    }
}

