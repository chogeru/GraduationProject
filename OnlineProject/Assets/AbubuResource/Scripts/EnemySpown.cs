using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpown : MonoBehaviour
{
    public string[] playerTags; // �X�|�[�����g���K�[����v���C���[�^�O�̔z��
    public GameObject[] enemyPrefabs; // �X�|�[������Enemy�̃v���n�u�̔z��
    public float spawnDistanceThreshold = 10f; // �X�|�[�����g���K�[���鋗����臒l
    public float minSpawnInterval = 3f; // �ŏ��X�|�[���Ԋu
    public float maxSpawnInterval = 8f; // �ő�X�|�[���Ԋu
    public GameObject particleEffectPrefab; // �p�[�e�B�N���G�t�F�N�g�̃v���n�u
    [SerializeField]
    private float spawnTimer = 0f;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private AudioClip m_SpownAudio;
    private float m_Volume=1f;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // �v���C���[�Ƃ̋������v�Z
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // �v���C���[����苗���ȓ��ɂ���ꍇ
        if (distanceToPlayer <= spawnDistanceThreshold)
        {
            // �^�C�}�[���X�V
            spawnTimer -= Time.deltaTime;

            // �X�|�[���Ԋu���o�߂����ꍇ
            if (spawnTimer <= 0f)
            {
                // �����_����Enemy�v���n�u��I��
                GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

                // �v���n�u���X�|�[��
                Instantiate(randomEnemyPrefab, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(m_SpownAudio, transform.position,m_Volume);
                // �p�[�e�B�N���G�t�F�N�g���Đ�
                if (particleEffectPrefab != null)
                {
                    Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
                }

                // �V�����X�|�[���Ԋu�������_���ɐݒ�
                spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }
    }
}

