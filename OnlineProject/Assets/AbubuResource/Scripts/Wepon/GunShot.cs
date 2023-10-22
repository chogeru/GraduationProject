using MalbersAnimations.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GunType
{
    Assault,
    Shotgun,
    RailGun,
}

public class GunShot : MonoBehaviour
{
    PlayerMove playerMove;

    [SerializeField, Header("���̃v���n�u")]
    private GameObject m_BulletPrefab;
    [SerializeField, Header("�e���I�u�W�F�N�g��Transform")]
    private Transform m_MuzzleTransform;
    [SerializeField, Header("�e��")]
    private float m_BulletSpeed = 10f;
    [SerializeField, Header("�N�[���^�C��")]
    private float m_CoolTime = 0.1f;
    private float m_Time;
    [SerializeField]
    private Animator m_Animator;
    [SerializeField, Header("�����[�h�̎���")]
    private float m_ReloadTime;
    [SerializeField]
    private float m_ReloadCoolTime;
    [SerializeField, Header("����SE")]
    private AudioClip m_AudioGunSE;
    private float m_Volume = 0.6f;
    [SerializeField, Header("�e����SE")]
    private GameObject m_ParticleGun;
    [SerializeField]
    private Slider m_BulletSlider;

    [SerializeField]
    private ParticleSystem ChargeEffect; // ParticleSystem�^�̕ϐ�ChargeEffect���V���A���C�Y�t�B�[���h�Ƃ��Đ錾

    [SerializeField]
    private Slider ChargeBar; // Slider�^��

    [SerializeField]
    private int m_MaxAmmo;
    [SerializeField]
    private int m_CurrentAmmo;

    [SerializeField, Header("�V���b�g�K���̔��ˊp�x")]
    private float m_ShotgunSpreadAngle = 30f;
    [SerializeField, Header("�V���b�g�K���̒e��")]
    private int m_NumBulletsInShotgun = 6;
    private float chargeTime; // �`���[�W���Ԃ�ێ�����ϐ�
    private bool isCharging; // �`���[�W�����ǂ����������t���O
    private bool isReload;
    [SerializeField]
    private GunType currentGunType = GunType.Assault;
  
    private void Start()
    {
        m_BulletSlider.value = 1;
        m_CurrentAmmo = m_MaxAmmo;

        m_Animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }
    private void Fire()
    {
        AudioSource.PlayClipAtPoint(m_AudioGunSE, transform.position, m_Volume);
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
        m_CurrentAmmo--;
        UpdateBulletSlider();
    }
    private void FireShotgun()
    {
        AudioSource.PlayClipAtPoint(m_AudioGunSE, transform.position, m_Volume);

        for (int i = 0; i < m_NumBulletsInShotgun; i++)
        {
            // �V���b�g�K���̔��˕������v�Z����
            Vector3 shotgunDirection = m_MuzzleTransform.forward;
            shotgunDirection = Quaternion.Euler(0, Random.Range(-m_ShotgunSpreadAngle, m_ShotgunSpreadAngle), 0) * shotgunDirection;

            // ���̃v���n�u����V�������𐶐�
            GameObject bullet = Instantiate(m_BulletPrefab, m_MuzzleTransform.position, Quaternion.LookRotation(shotgunDirection));
            GameObject particle = Instantiate(m_ParticleGun, m_MuzzleTransform.position, Quaternion.LookRotation(shotgunDirection));

            // ���� Rigidbody �R���|�[�l���g���擾
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                // �V���b�g�K���̒e����K�p����
                bulletRigidbody.velocity = shotgunDirection * m_BulletSpeed;
            }
        }

        m_CurrentAmmo -= m_NumBulletsInShotgun; // �V���b�g�K���ł�1��̔��˂ŕ����̒e�������
        UpdateBulletSlider();
    }
    private void ChargeShot()
    {
        // �}�E�X��������Ă���ԃ`���[�W���s��
        if (Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime; // �`���[�W���Ԃ����Z
        }

        // �}�E�X�𗣂�����`���[�W�V���b�g�𔭎�
        if (Input.GetMouseButtonUp(2) && isCharging)
        {
            // �`���[�W�̋��x�ɉ����Ēe�̑��x��З͂𒲐�
            float bulletSpeed = m_BulletSpeed + chargeTime * 10f; // �`���[�W���Ԃɉ����đ��x�𑝉�
            float bulletDamage = 10f + chargeTime * 5f; // �`���[�W���Ԃɉ����ĈЗ͂𑝉�

            // ���̃v���n�u����V�������𐶐�
            GameObject bullet = Instantiate(m_BulletPrefab, m_MuzzleTransform.position, m_MuzzleTransform.rotation);
            // ���� Rigidbody �R���|�[�l���g���擾
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                // �`���[�W�̋��x�ɉ��������x��������
                bulletRigidbody.velocity = m_MuzzleTransform.forward * bulletSpeed;
            }

            // �`���[�W�֘A�̕ϐ������Z�b�g
            chargeTime = 0f;
            isCharging = false;


            // ChargeEffect.Stop();
            // ChargeBar.Hide();
        }


        if (chargeTime > 0f && !isCharging)
        {
            isCharging = true;
          
            // ChargeEffect.Play();
            // ChargeBar.Show();
        }
    }
    private void Update()
    {
        m_Time += Time.deltaTime;
        if (isReload == false)
        {
            if (m_Time > m_CoolTime && Input.GetMouseButton(0))
            {
                switch (currentGunType)
                {
                    case GunType.Assault:
                        Fire();
                        m_Time = 0;
                        break;
                    case GunType.Shotgun:
                        FireShotgun();
                        m_Time = 0;
                        break;
                    case GunType.RailGun:
                        ChargeShot();
                        break;

                }
            }       
        }
        if (m_CurrentAmmo <= 0)
        {
            Reload();
        }
        if (Input.GetKey(KeyCode.R))
        {
            m_CurrentAmmo = 0;
        }
    }
    private void UpdateBulletSlider()
    {
        m_BulletSlider.value = (float)m_CurrentAmmo / (float)m_MaxAmmo;
    }
    private void Reload()
    {
        m_Animator.SetBool("�����[�h", true);
        isReload = true;
        playerMove.m_Speed = 0;
        m_ReloadCoolTime += Time.deltaTime;
        if (m_ReloadTime <= m_ReloadCoolTime)
        {
            isReload = false;
            m_ReloadCoolTime = 0;
            m_CurrentAmmo = m_MaxAmmo;
            UpdateBulletSlider();
            m_Animator.SetBool("�����[�h", false);
            playerMove.m_Speed = playerMove.m_MoveSpeed;
        }
    }
}
