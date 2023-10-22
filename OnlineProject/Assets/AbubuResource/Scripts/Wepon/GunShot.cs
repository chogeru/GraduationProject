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

    [SerializeField, Header("球のプレハブ")]
    private GameObject m_BulletPrefab;
    [SerializeField, Header("銃口オブジェクトのTransform")]
    private Transform m_MuzzleTransform;
    [SerializeField, Header("弾速")]
    private float m_BulletSpeed = 10f;
    [SerializeField, Header("クールタイム")]
    private float m_CoolTime = 0.1f;
    private float m_Time;
    [SerializeField]
    private Animator m_Animator;
    [SerializeField, Header("リロードの時間")]
    private float m_ReloadTime;
    [SerializeField]
    private float m_ReloadCoolTime;
    [SerializeField, Header("発射SE")]
    private AudioClip m_AudioGunSE;
    private float m_Volume = 0.6f;
    [SerializeField, Header("弾発射SE")]
    private GameObject m_ParticleGun;
    [SerializeField]
    private Slider m_BulletSlider;

    [SerializeField]
    private ParticleSystem ChargeEffect; // ParticleSystem型の変数ChargeEffectをシリアライズフィールドとして宣言

    [SerializeField]
    private Slider ChargeBar; // Slider型の

    [SerializeField]
    private int m_MaxAmmo;
    [SerializeField]
    private int m_CurrentAmmo;

    [SerializeField, Header("ショットガンの発射角度")]
    private float m_ShotgunSpreadAngle = 30f;
    [SerializeField, Header("ショットガンの弾数")]
    private int m_NumBulletsInShotgun = 6;
    private float chargeTime; // チャージ時間を保持する変数
    private bool isCharging; // チャージ中かどうかを示すフラグ
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
        // 球のプレハブから新しい球を生成
        GameObject bullet = Instantiate(m_BulletPrefab, m_MuzzleTransform.position, m_MuzzleTransform.rotation);
        GameObject particle = Instantiate(m_ParticleGun, m_MuzzleTransform.position, m_MuzzleTransform.rotation);
        // 球の Rigidbody コンポーネントを取得
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            // 球をまっすぐ前に発射する力を加える
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
            // ショットガンの発射方向を計算する
            Vector3 shotgunDirection = m_MuzzleTransform.forward;
            shotgunDirection = Quaternion.Euler(0, Random.Range(-m_ShotgunSpreadAngle, m_ShotgunSpreadAngle), 0) * shotgunDirection;

            // 球のプレハブから新しい球を生成
            GameObject bullet = Instantiate(m_BulletPrefab, m_MuzzleTransform.position, Quaternion.LookRotation(shotgunDirection));
            GameObject particle = Instantiate(m_ParticleGun, m_MuzzleTransform.position, Quaternion.LookRotation(shotgunDirection));

            // 球の Rigidbody コンポーネントを取得
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                // ショットガンの弾速を適用する
                bulletRigidbody.velocity = shotgunDirection * m_BulletSpeed;
            }
        }

        m_CurrentAmmo -= m_NumBulletsInShotgun; // ショットガンでは1回の発射で複数の弾を消費する
        UpdateBulletSlider();
    }
    private void ChargeShot()
    {
        // マウスが押されている間チャージを行う
        if (Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime; // チャージ時間を加算
        }

        // マウスを離したらチャージショットを発射
        if (Input.GetMouseButtonUp(2) && isCharging)
        {
            // チャージの強度に応じて弾の速度や威力を調整
            float bulletSpeed = m_BulletSpeed + chargeTime * 10f; // チャージ時間に応じて速度を増加
            float bulletDamage = 10f + chargeTime * 5f; // チャージ時間に応じて威力を増加

            // 球のプレハブから新しい球を生成
            GameObject bullet = Instantiate(m_BulletPrefab, m_MuzzleTransform.position, m_MuzzleTransform.rotation);
            // 球の Rigidbody コンポーネントを取得
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                // チャージの強度に応じた速度を加える
                bulletRigidbody.velocity = m_MuzzleTransform.forward * bulletSpeed;
            }

            // チャージ関連の変数をリセット
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
        m_Animator.SetBool("リロード", true);
        isReload = true;
        playerMove.m_Speed = 0;
        m_ReloadCoolTime += Time.deltaTime;
        if (m_ReloadTime <= m_ReloadCoolTime)
        {
            isReload = false;
            m_ReloadCoolTime = 0;
            m_CurrentAmmo = m_MaxAmmo;
            UpdateBulletSlider();
            m_Animator.SetBool("リロード", false);
            playerMove.m_Speed = playerMove.m_MoveSpeed;
        }
    }
}
