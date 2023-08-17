using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    [SerializeField,Header("球のプレハブ")]
    private GameObject m_BulletPrefab;
    [SerializeField,Header("銃口オブジェクトのTransform")]
    private Transform m_MuzzleTransform;
    [SerializeField,Header("弾速")]
    private float m_BulletSpeed = 10f;
    [SerializeField, Header("クールタイム")]
    private float m_CoolTime = 0.1f;
    private float m_Time;
    
    [SerializeField, Header("発射SE")]
    private AudioClip m_AudioGunSE;
    private float m_Volume=0.6f;
    [SerializeField, Header("弾発射SE")]
    private GameObject m_ParticleGun;
    // 発射ボタンを処理するメソッド
    private void Fire()
    {
        AudioSource.PlayClipAtPoint(m_AudioGunSE, transform.position,m_Volume);
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
