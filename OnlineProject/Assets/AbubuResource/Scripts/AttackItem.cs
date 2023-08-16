using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItem : MonoBehaviour
{
    public Transform playerObject; // �v���C���[�I�u�W�F�N�g�̎Q��
    [SerializeField, Header("���킪������Ƃ��̃p�[�e�B�N��")]
    private GameObject m_WeponDestroyParticle;
    private bool isFloating = true;
    private bool isPlayerGet = false;
    [SerializeField, Header("Destroy�܂ł̎���")]
    private float m_DestroyTime=10;
    private float m_Time;
    [SerializeField, Header("���폊������SE")]
    private AudioClip m_WeponGetSE;
    [SerializeField,Header("���킪������Ƃ���SE")]
    private AudioClip m_WeponDestroySE;
    [SerializeField]
    private float m_Volume=0.6f;
    private void Start()
    {
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

            if (distanceToEnemy <= 10f && distanceToEnemy < closestDistance)
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
}
