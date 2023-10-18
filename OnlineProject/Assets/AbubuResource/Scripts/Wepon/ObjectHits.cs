using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHits : MonoBehaviour
{
    [SerializeField,Header("壁に当たった時のパーティクル")]
    private GameObject m_ParticleWallHit;
    [SerializeField, Header("植物に当たった時のパーティクル")]
    private GameObject m_ParticlePlant;
    [SerializeField, Header("敵に当たった時のパーティクル")]
    private GameObject m_ParticleEnemyHit;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 壁にぶつかった時の処理
            HitParticleWall();
            Destroy(gameObject); // 自身のオブジェクトを破壊
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HitParticleEnemy();
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Ground"))
        {
            HitParticleGrund();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plant"))
        {
            HitParticlePlant();
            Destroy(gameObject); // 自身のオブジェクトを破壊
        }
    }
    private void HitParticleWall()
    {
        GameObject particles = Instantiate(m_ParticleWallHit, transform.position, Quaternion.identity);
        // パーティクル再生終了後に破棄
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration); 
    }
    private void HitParticleGrund()
    {
        GameObject particles=Instantiate(m_ParticleWallHit,transform.position, Quaternion.identity);
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);
    }
    private void HitParticlePlant()
    {
        GameObject particles = Instantiate(m_ParticlePlant, transform.position, Quaternion.identity);
        // パーティクル再生終了後に破棄
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration); 
    }
    private void HitParticleEnemy()
    {
        GameObject particles = Instantiate(m_ParticleEnemyHit, transform.position, Quaternion.identity);
        // パーティクル再生終了後に破棄
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration); 
    }
}
