using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHits : MonoBehaviour
{
    [SerializeField,Header("�ǂɓ����������̃p�[�e�B�N��")]
    private GameObject m_ParticleWallHit;
    [SerializeField, Header("�A���ɓ����������̃p�[�e�B�N��")]
    private GameObject m_ParticlePlant;
    [SerializeField, Header("�G�ɓ����������̃p�[�e�B�N��")]
    private GameObject m_ParticleEnemyHit;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // �ǂɂԂ��������̏���
            HitParticleWall();
            Destroy(gameObject); // ���g�̃I�u�W�F�N�g��j��
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
            Destroy(gameObject); // ���g�̃I�u�W�F�N�g��j��
        }
    }
    private void HitParticleWall()
    {
        GameObject particles = Instantiate(m_ParticleWallHit, transform.position, Quaternion.identity);
        // �p�[�e�B�N���Đ��I����ɔj��
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
        // �p�[�e�B�N���Đ��I����ɔj��
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration); 
    }
    private void HitParticleEnemy()
    {
        GameObject particles = Instantiate(m_ParticleEnemyHit, transform.position, Quaternion.identity);
        // �p�[�e�B�N���Đ��I����ɔj��
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration); 
    }
}
