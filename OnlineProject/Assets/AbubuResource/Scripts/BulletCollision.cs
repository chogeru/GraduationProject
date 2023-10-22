using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab; 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // ���������ꏊ�Ƀp�[�e�B�N���𐶐�
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            GameObject particle = Instantiate(particlePrefab, contact.point, rotation);

            // �p�[�e�B�N����0.5�b��ɔj��
            Destroy(particle, 0.5f);

            // ��������Bullet�I�u�W�F�N�g��j��
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // ���蔲�����ꏊ�Ƀp�[�e�B�N���𐶐�
            Vector3 position = transform.position;
            Quaternion rotation = Quaternion.identity; // ��]�Ȃ�
            GameObject particle = Instantiate(particlePrefab, position, rotation);

            // �p�[�e�B�N����0.5�b��ɔj��
            Destroy(particle, 0.5f);
        }
    }
}
