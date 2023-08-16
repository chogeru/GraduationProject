using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHits : MonoBehaviour
{
    [SerializeField,Header("�ǂɓ����������̃p�[�e�B�N��")]
    private GameObject m_ParticleWallHit;
    [SerializeField, Header("�A���ɓ����������̃p�[�e�B�N��")]
    private GameObject m_ParticlePlant;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // �ǂɂԂ��������̏���
            HitParticleWall();
            Destroy(gameObject); // ���g�̃I�u�W�F�N�g��j��
        }
        if (collision.gameObject.CompareTag("Plant"))
        {
            HitParticlePlant();
            Destroy(gameObject); // ���g�̃I�u�W�F�N�g��j��
        }
    }

    private void HitParticleWall()
    {
        if (m_ParticleWallHit != null)
        {
            GameObject particles = Instantiate(m_ParticleWallHit, transform.position, Quaternion.identity);
            Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration); // �p�[�e�B�N���Đ��I����ɔj��
        }
    }
    private void HitParticlePlant()
    {
        if (m_ParticlePlant != null)
        {
            GameObject particles = Instantiate(m_ParticlePlant, transform.position, Quaternion.identity);
            Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration); // �p�[�e�B�N���Đ��I����ɔj��
        }
    }
}
