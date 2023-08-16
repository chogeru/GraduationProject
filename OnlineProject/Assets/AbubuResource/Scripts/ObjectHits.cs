using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHits : MonoBehaviour
{
    [SerializeField,Header("壁に当たった時のパーティクル")]
    private GameObject m_ParticleWallHit;
    [SerializeField, Header("植物に当たった時のパーティクル")]
    private GameObject m_ParticlePlant;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 壁にぶつかった時の処理
            HitParticleWall();
            Destroy(gameObject); // 自身のオブジェクトを破壊
        }
        if (collision.gameObject.CompareTag("Plant"))
        {
            HitParticlePlant();
            Destroy(gameObject); // 自身のオブジェクトを破壊
        }
    }

    private void HitParticleWall()
    {
        if (m_ParticleWallHit != null)
        {
            GameObject particles = Instantiate(m_ParticleWallHit, transform.position, Quaternion.identity);
            Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration); // パーティクル再生終了後に破棄
        }
    }
    private void HitParticlePlant()
    {
        if (m_ParticlePlant != null)
        {
            GameObject particles = Instantiate(m_ParticlePlant, transform.position, Quaternion.identity);
            Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration); // パーティクル再生終了後に破棄
        }
    }
}
