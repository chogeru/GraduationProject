using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRockRote : MonoBehaviour
{
    [SerializeField,Header("�G���������x")]
    private float m_RotationSpeed = 5.0f;
    [SerializeField,Header("�G�̃^�O")]
    private string m_EnemyTag = "Enemy";
    [SerializeField,Header("���݂̃^�[�Q�b�g")]
    private Transform m_CurrentTarget = null;
    [SerializeField,Header("�^�[�Q�b�g�����ǂ���")]
    private bool isTargeting = false;
    [SerializeField, Header("UI")]
    private GameObject m_RockUI;
    
    void Update()
    {
        // Tab�L�[�������ꂽ��^�[�Q�b�g��؂�ւ���
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleTargeting();
        }
        // �^�[�Q�b�g���̏ꍇ�A�G�̕���������
        if (isTargeting && m_CurrentTarget != null)
        {
            // �^�[�Q�b�g�ւ̕������v�Z
            Vector3 directionToTarget = m_CurrentTarget.position - transform.position;

            // �^�[�Q�b�g�֌�������]���v�Z
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // ���݂̉�]����ڕW�̉�]�ɃX���[�Y�ɑJ��
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);
        }
    }

    void ToggleTargeting()
    {
        isTargeting = !isTargeting;

        if (isTargeting)
        {
            m_RockUI.SetActive(true);
            // ��ԋ߂��G��T��
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(m_EnemyTag);
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                // �v���C���[�ƓG�̋������v�Z
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                // ���߂��G����������^�[�Q�b�g���X�V
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    m_CurrentTarget = enemy.transform;
                }
            }
        }
        else
        {
            // �^�[�Q�b�g���O�ꂽ��A�^�[�Q�b�g��null�ɂ���
            m_CurrentTarget = null;
            m_RockUI.SetActive(false);
        }
    }
}
