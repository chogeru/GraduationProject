using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItem : MonoBehaviour
{
    [SerializeField,Header("�v���C���[�I�u�W�F�N�g")]
    private Transform playerObject; 
    [SerializeField, Header("���ˈʒu")]
    private Transform m_FirePoint;
    [SerializeField, Header("���킪������Ƃ��̃p�[�e�B�N��")]
    private GameObject m_WeponDestroyParticle;
    [SerializeField, Header("�e�̃p�[�e�B�N��")]
    private GameObject m_BulletParticle;
    [SerializeField, Header("�e�̃v���n�u")]
    private GameObject m_BulletPrefab;
    private bool isFloating = true;
    private bool isPlayerGet = false;
    [SerializeField, Header("�e��")]
    private float m_BulletSpeed=10;
    [SerializeField, Header("Destroy�܂ł̎���")]
    private float m_DestroyTime=10;
    private float m_Time;
    [SerializeField,Header("�e�̔���SE")]
    private AudioClip m_FireAudioSource;
    [SerializeField, Header("���폊������SE")]
    private AudioClip m_WeponGetSE;
    [SerializeField,Header("���킪������Ƃ���SE")]
    private AudioClip m_WeponDestroySE;
    [SerializeField]
    private float m_Volume=0.6f;
    [SerializeField]
    private float m_BulletCoolTime = 3;
    private float m_FireTime;
    private void Start()
    {
        m_BulletParticle.SetActive(false);
        isPlayerGet = false;
        if (isFloating)
        {
            // �I�u�W�F�N�g�𕂂����鏈��
            transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
        }
    }
    private void Update()
    {
        if (isFloating)
        {
            // �I�u�W�F�N�g����]�����鏈��
            transform.Rotate(Vector3.up * 360f * Time.deltaTime);
        }
        if (isPlayerGet)
        {
            m_FireTime += Time.deltaTime;
            if (m_BulletCoolTime<m_FireTime)
            {
                Fire();
                m_FireTime = 0;
            }
            m_Time += Time.deltaTime;
            if (m_Time > m_DestroyTime)
            {
                AudioSource.PlayClipAtPoint(m_WeponDestroySE, transform.position, m_Volume);
                // �p�[�e�B�N���𐶐�
                Instantiate(m_WeponDestroyParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            // �G�I�u�W�F�N�g�����o���ĕ�������������
            DetectAndFaceEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_BulletParticle.SetActive(true);
            AudioSource.PlayClipAtPoint(m_WeponGetSE, transform.position,m_Volume);
            isFloating = false; // �������鏈�����~
            transform.SetParent(playerObject); // �v���C���[�I�u�W�F�N�g�̎q�ɐݒ�

            // �v���C���[�I�u�W�F�N�g�̕������I�u�W�F�N�g�ɃR�s�[
            transform.forward = playerObject.forward;

            // �v���C���[�I�u�W�F�N�g�̉E��Ɉʒu���Œ�
            transform.localPosition = new Vector3(0f, 2.3f, 0f);
            isPlayerGet = true;
         //   GetComponent<Collider>().isTrigger = false;
        }
    }
    private void DetectAndFaceEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToEnemy = enemy.transform.position - transform.position;
            float distanceToEnemy = directionToEnemy.magnitude;

            if (distanceToEnemy <= 15f && distanceToEnemy < closestDistance)
            {
                closestEnemy = enemy.transform;
                closestDistance = distanceToEnemy;
            }
        }

        if (closestEnemy != null)
        {
            Vector3 directionToClosestEnemy = closestEnemy.position - transform.position;
            directionToClosestEnemy.y = 0; // Y�������̉�]�𖳌���
            Quaternion lookRotation = Quaternion.LookRotation(directionToClosestEnemy);
            transform.rotation = lookRotation;
        }
    }
    private void Fire()
    {
        AudioSource.PlayClipAtPoint(m_FireAudioSource, transform.position, m_Volume);
        // ���̃v���n�u����V�������𐶐�
        GameObject bullet = Instantiate(m_BulletPrefab, m_FirePoint.position, m_FirePoint.rotation);
     
        // ���� Rigidbody �R���|�[�l���g���擾
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            // �����܂������O�ɔ��˂���͂�������
            bulletRigidbody.velocity = m_FirePoint.forward * m_BulletSpeed;
        }
    }
}
