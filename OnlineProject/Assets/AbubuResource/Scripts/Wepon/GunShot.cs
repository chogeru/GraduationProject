using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;
public enum GunType
{
    Assault,
    Shotgun,
    RailGun,
    FlameThrower,
    Grenade,
}

public class GunShot : MonobitEngine.MonoBehaviour
{
    MonobitEngine.MonobitView m_MonobitView=null;
    PlayerMove playerMove;

    [SerializeField, Header("���̃v���n�u")]
    private GameObject m_BulletPrefab;
    [SerializeField, Header("�e���I�u�W�F�N�g��Transform")]
    private Transform[] m_MuzzleTransforms;
    [SerializeField, Header("�e��")]
    private float m_BulletSpeed = 10f;
    [SerializeField, Header("�_�E���X�s�[�h")]
    private float m_BulletDownSpeed;
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
    private GameObject m_FlameCol;

    [SerializeField]
    private Slider m_BulletSlider;

    [SerializeField]
    private ParticleSystem ChargeEffect;
    [SerializeField]
    private ParticleSystem m_FlameEffect;
    [SerializeField]
    private GameObject m_RailChargEffect;
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
    void Awake()
    {
        // ���ׂĂ̐e�I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g����������
        if (GetComponentInParent<MonobitEngine.MonobitView>() != null)
        {
            m_MonobitView = GetComponentInParent<MonobitEngine.MonobitView>();
        }
        // �e�I�u�W�F�N�g�ɑ��݂��Ȃ��ꍇ�A���ׂĂ̎q�I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g����������
        else if (GetComponentInChildren<MonobitEngine.MonobitView>() != null)
        {
            m_MonobitView = GetComponentInChildren<MonobitEngine.MonobitView>();
        }
        // �e�q�I�u�W�F�N�g�ɑ��݂��Ȃ��ꍇ�A���g�̃I�u�W�F�N�g�ɑ΂��� MonobitView �R���|�[�l���g���������Đݒ肷��
        else
        {
            m_MonobitView = GetComponent<MonobitEngine.MonobitView>();
        }
    }
    private void Start()
    {
        m_BulletSlider.value = 1;
        m_CurrentAmmo = m_MaxAmmo;
        if (currentGunType == GunType.FlameThrower)
        {
            m_FlameCol.SetActive(false);
            m_FlameEffect.Stop();
        }
        m_Animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }
    [MunRPC]
    private void Fire()
    {
        AudioSource.PlayClipAtPoint(m_AudioGunSE, transform.position, m_Volume);

        // �e�e�����狅�𔭎˂���
        foreach (Transform muzzleTransform in m_MuzzleTransforms)
        {
            GameObject bullet = Instantiate(m_BulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
            GameObject particle = Instantiate(m_ParticleGun, muzzleTransform.position, muzzleTransform.rotation);

            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = muzzleTransform.forward * m_BulletSpeed;
            }
        }

        m_CurrentAmmo--;
        UpdateBulletSlider();
    }
    [MunRPC]
    private void FireShotgun()
    {

        AudioSource.PlayClipAtPoint(m_AudioGunSE, transform.position, m_Volume);

        for (int i = 0; i < m_NumBulletsInShotgun; i++)
        {
            foreach (Transform muzzleTransform in m_MuzzleTransforms)
            {
                // �V���b�g�K���̔��˕������v�Z����
                Vector3 shotgunDirection = muzzleTransform.forward;
                shotgunDirection = Quaternion.Euler(0, Random.Range(-m_ShotgunSpreadAngle, m_ShotgunSpreadAngle), 0) * shotgunDirection;

                // ���̃v���n�u����V�������𐶐�
                GameObject bullet = Instantiate(m_BulletPrefab, muzzleTransform.position, Quaternion.LookRotation(shotgunDirection));
                GameObject particle = Instantiate(m_ParticleGun, muzzleTransform.position, Quaternion.LookRotation(shotgunDirection));

                // ���� Rigidbody �R���|�[�l���g���擾
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

                if (bulletRigidbody != null)
                {
                    // �V���b�g�K���̒e����K�p����
                    bulletRigidbody.velocity = shotgunDirection * m_BulletSpeed;
                }
            }
        }

        m_CurrentAmmo -= m_NumBulletsInShotgun; // �V���b�g�K���ł�1��̔��˂ŕ����̒e�������
        UpdateBulletSlider();
    }
    [MunRPC]
    private void ChargeShot()
    {
        // �}�E�X��������Ă���ԃ`���[�W���s��
      
            chargeTime += Time.deltaTime; // �`���[�W���Ԃ����Z
        m_RailChargEffect.SetActive(true);

        // �}�E�X�𗣂�����`���[�W�V���b�g�𔭎�
        if(chargeTime>1.3)
        { 
            foreach (Transform muzzleTransform in m_MuzzleTransforms)
            {
                // �`���[�W�̋��x�ɉ����Ēe�̑��x��З͂𒲐�
                float bulletSpeed = m_BulletSpeed + chargeTime * 10f; // �`���[�W���Ԃɉ����đ��x�𑝉�
                float bulletDamage = 10f + chargeTime * 5f; // �`���[�W���Ԃɉ����ĈЗ͂𑝉�

                // ���̃v���n�u����V�������𐶐�
                GameObject bullet = Instantiate(m_BulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
                // ���� Rigidbody �R���|�[�l���g���擾
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                m_RailChargEffect.SetActive(false);
                if (bulletRigidbody != null)
                {
                    // �`���[�W�̋��x�ɉ��������x��������
                    bulletRigidbody.velocity = muzzleTransform.forward * bulletSpeed;
                }
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
    [MunRPC]
    private void FlameFire()
    {
        m_FlameEffect.Play();
        m_FlameCol.SetActive(true);
    }
    [MunRPC]
    private void GrenadeThrow()
    {
        foreach (Transform muzzleTransform in m_MuzzleTransforms)
        {
            AudioSource.PlayClipAtPoint(m_AudioGunSE, transform.position, m_Volume);
            // ���̃v���n�u����V�������𐶐�
            GameObject bullet = Instantiate(m_BulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
            GameObject particle = Instantiate(m_ParticleGun, muzzleTransform.position, muzzleTransform.rotation);
            // ���� Rigidbody �R���|�[�l���g���擾
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                // �����܂������O�ɔ��˂���͂�������
                bulletRigidbody.velocity = muzzleTransform.forward * m_BulletSpeed;
            }
        }
        m_CurrentAmmo--;
        UpdateBulletSlider();
    }
    private void Update()
    {
        if (!m_MonobitView.isMine)
        {
            return;
        }
        m_Time += Time.deltaTime;
        if (isReload == false)
        {
            if (m_Time > m_CoolTime && Input.GetMouseButton(0))
            {
                switch (currentGunType)
                {
                    case GunType.Assault:
                        //  Fire();
                       m_MonobitView.RPC("Fire",MonobitEngine.MonobitTargets.All,null);
                        m_Time = 0;
                        break;
                    case GunType.Shotgun:
                        m_MonobitView.RPC("FireShotgun",MonobitEngine.MonobitTargets.All,null);
                        m_Time = 0;
                        break;
                    case GunType.RailGun:
                        m_MonobitView.RPC("ChargeShot", MonobitEngine.MonobitTargets.All, null);  
                        break;
                    case GunType.FlameThrower:
                        m_MonobitView.RPC("FlameFire", MonobitEngine.MonobitTargets.All, null);
                        break;
                    case GunType.Grenade:
                        m_MonobitView.RPC("GrenadeThrow", MonobitEngine.MonobitTargets.All, null);
                        m_Time = 0;
                        break;


                }
            }
        }
        if (currentGunType == GunType.FlameThrower)
        {
            if (Input.GetMouseButtonUp(0))
            {
                m_FlameCol.SetActive(false);
                m_FlameEffect.Stop();
            }
        }
        if(currentGunType==GunType.RailGun)
        {
            if (Input.GetMouseButtonUp(0))
            {
                m_RailChargEffect.SetActive(false);
            }

        }
        if (m_Animator.GetBool("Run"))
        {
            m_FlameCol.SetActive(false);
            m_FlameEffect.Stop();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            m_BulletSpeed -= m_BulletDownSpeed;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            m_BulletSpeed += m_BulletDownSpeed;
        }
    }
}
