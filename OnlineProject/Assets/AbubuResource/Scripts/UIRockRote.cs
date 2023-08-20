using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRockRote : MonoBehaviour
{
    public Transform player; // �v���C���[�̈ʒu
    public Transform uiImage; // UI�摜��Transform
    public string enemyTag = "Enemy"; // Enemy�^�O�̖��O

    private Transform nearestEnemy; // �ł��߂�Enemy��Transform
    private bool isAligned = false; // �ʒu���킹�t���O

    private void Update()
    {
        // Tab�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // �ʒu���킹������Ă���ꍇ�A��������
            if (isAligned)
            {
                isAligned = false;
                uiImage.gameObject.SetActive(false); // UI�摜���\���ɂ���
            }
            else
            {
                // �ł��߂�Enemy��T��
                FindNearestEnemy();

                // �ł��߂�Enemy������ꍇ�AUI�摜�̈ʒu�����킹��
                if (nearestEnemy != null)
                {
                    isAligned = true;
                    uiImage.gameObject.SetActive(true); // UI�摜��\������
                    uiImage.position = nearestEnemy.position;
                }
            }
        }

        // �ʒu���킹������Ă���ꍇ�AUI�摜���ł��߂�Enemy�ɍ��킹��
        if (isAligned && nearestEnemy != null)
        {
            uiImage.position = nearestEnemy.position;
        }
    }

    // �ł��߂�Enemy��T��
    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closestDistance = Mathf.Infinity;
        nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }
    }
}
