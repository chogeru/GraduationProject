using MalbersAnimations.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    [SerializeField, Header("球のプレハブ")]
    private GameObject m_BulletPrefab;
    [SerializeField, Header("チャージショットの弾")]
    private GameObject m_ChargeBulletPrefab;
    [SerializeField,Header("チャージ時のエフェクト")]
    private GameObject m_ChargeEffect;
    [SerializeField, Header("銃口オブジェクトのTransform")]
    private Transform m_MuzzleTransform;
    [SerializeField]
    private Transform m_ChargeMuzzle;
    [SerializeField, Header("弾速")]
    private float m_BulletSpeed = 10f;
    [SerializeField, Header("クールタイム")]
    private float m_CoolTime = 0.1f;
    private float m_Time;
    private bool isCharge=true;
    [SerializeField]
    private float m_ChargeTime = 1f;
    [SerializeField]
    private float m_ChargeCoolTime;
    [SerializeField, Header("発射SE")]
    private AudioClip m_AudioGunSE;
    private float m_Volume = 0.6f;
    [SerializeField, Header("弾発射SE")]
    private GameObject m_ParticleGun;

    private void Start()
    {
        m_ChargeEffect.SetActive(false);
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
    }
    private void ChargeFire()
    {
        AudioSource.PlayClipAtPoint(m_AudioGunSE, transform.position, m_Volume);
        // 球のプレハブから新しい球を生成
        GameObject bullet = Instantiate(m_ChargeBulletPrefab, m_ChargeMuzzle.position,m_ChargeMuzzle.rotation);
        GameObject particle = Instantiate(m_ParticleGun, m_ChargeMuzzle.position, m_ChargeMuzzle.rotation);
        // 球の Rigidbody コンポーネントを取得
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        
        if (bulletRigidbody != null)
        {
            // 球をまっすぐ前に発射する力を加える
            bulletRigidbody.velocity = m_ChargeMuzzle.forward * m_BulletSpeed;
        }
    }
    private void Update()
    {
        m_Time += Time.deltaTime;
        if(Input.GetKey(KeyCode.R))
        {
            isCharge = true;
        }
        if(Input.GetKey(KeyCode.T))
        {
            isCharge = false;
        }
        if(isCharge==false)
        { 
            if (m_Time > m_CoolTime && Input.GetMouseButton(0))
            {
                Fire();
                m_Time = 0;
            }
        }
        if(isCharge)
        {
            if(Input.GetMouseButton(0))
            {
                m_ChargeCoolTime += Time.deltaTime;
                m_ChargeEffect.SetActive(true);
            }
           
            if (m_ChargeCoolTime > m_ChargeTime && Input.GetMouseButtonUp(0))
            {
                ChargeFire();
                m_ChargeCoolTime = 0;
                m_ChargeEffect.SetActive(false);
            }
           

        }
    }
}
