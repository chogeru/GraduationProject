using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    [SerializeField,Header("���̃v���n�u")]
    private GameObject m_BulletPrefab;
    [SerializeField,Header("�e���I�u�W�F�N�g��Transform")]
    private Transform m_MuzzleTransform;
    [SerializeField,Header("�e��")]
    private float m_BulletSpeed = 10f;
    [SerializeField, Header("�N�[���^�C��")]
    private float m_CoolTime = 0.1f;
    private float m_Time;
    
    [SerializeField, Header("����SE")]
    private AudioClip m_AudioGunSE;
    private float m_Volume=0.6f;
    [SerializeField, Header("�e����SE")]
    private GameObject m_ParticleGun;
    // ���˃{�^�����������郁�\�b�h
    private void Fire()
    {
        AudioSource.PlayClipAtPoint(m_AudioGunSE, transform.position,m_Volume);
        // ���̃v���n�u����V�������𐶐�
        GameObject bullet = Instantiate(m_BulletPrefab, m_MuzzleTransform.position, m_MuzzleTransform.rotation);
        GameObject particle = Instantiate(m_ParticleGun, m_MuzzleTransform.position, m_MuzzleTransform.rotation);
        // ���� Rigidbody �R���|�[�l���g���擾
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            // �����܂������O�ɔ��˂���͂�������
            bulletRigidbody.velocity = m_MuzzleTransform.forward * m_BulletSpeed;
        }
    }
    private void Update()
    {
        m_Time += Time.deltaTime;
        if(m_Time > m_CoolTime&&Input.GetMouseButton(0))
        {
            Fire();
            m_Time = 0;
        }
    }
}
