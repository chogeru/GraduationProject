using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaterEnemy : MonoBehaviour
{

    MonobitEngine.MonobitView m_MonobitView = null;

    [SerializeField]
    private string m_PlayerTag = "Player";

    [SerializeField]
    private int Damage;

    [SerializeField]
    private int m_Hp;
    [SerializeField]
    private int m_MaxHp;

    [SerializeField]
    private int m_Point;

    public int m_KillCount;
    private bool isAvoiding;
    [SerializeField]
    private float m_DetectionDistance = 50f;

    [SerializeField]
    private float m_ShotCooldown = 10f;
    private float m_CurrentCooldown = 0f;
    [SerializeField]
    private float projectileSpeed = 10f;

    [SerializeField]
    private float m_AvoidanceDistance = 2f;
    [SerializeField]
    private float m_RotationSpeed = 5f;
    private float m_MoveSpeed = 5;
    private float m_DefoltSpeed = 5;
    private float m_DestroyTime;
    [SerializeField, Header("�U������")]
    private float m_AttackRange = 50;

    [SerializeField]
    private GameObject m_DestroySE;
    [SerializeField]
    private GameObject m_DestroyEffect;
    [SerializeField]
    private GameObject m_BossHoGage;
    [SerializeField]
    private Slider mHpSlider;
    [SerializeField]
    private AudioSource IdleBGM;
    [SerializeField]
    private AudioSource BossBGM;

    private float m_FadeDuration = 5.0f;

    private float m_InitialVolumeIdle;
    private float m_InitialVolumeBoss;
    private float m_Timer;
    private float m_MaxVolume = 0.1f;
    private float m_HitCoolTime;

    [SerializeField]
    private Transform m_Player;

    PlayerMove m_PlayerMove;
    [SerializeField]
    AttackObj m_AttackObj;
    [SerializeField]
    private Transform m_ShotPoint;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    [SerializeField]
    private GameObject m_AttackSE;
    [SerializeField]
    private GameObject m_ATCol;
    [SerializeField]
    private GameObject m_Shot;
    [SerializeField]
    private GameObject m_ChrgeShot;

    [SerializeField]
    private ParticleSystem m_FireEffect;
    [SerializeField]
    private float m_FireTime;
    private float m_FCTime;
    private bool isFire = false;

    private void Awake()
    {
        if (MonobitEngine.MonobitNetwork.offline == false)
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
    }
    void Start()
    {
        m_FireEffect.Stop();

        m_InitialVolumeIdle = IdleBGM.volume;
        m_InitialVolumeBoss = BossBGM.volume;
        BossBGM.gameObject.SetActive(false);
        m_Hp = m_MaxHp;
        mHpSlider.value = 1;

        m_Player = GameObject.FindGameObjectWithTag(m_PlayerTag).transform;


        m_PlayerMove = m_Player.GetComponent<PlayerMove>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;

        GameObject[] players = GameObject.FindGameObjectsWithTag(m_PlayerTag);
        if (players.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            GameObject closestPlayer = null;

            foreach (GameObject player in players)
            {
                float distanceToPlayers = Vector3.Distance(transform.position, player.transform.position);

                if (distanceToPlayers < closestDistance)
                {
                    closestDistance = distanceToPlayers;
                    closestPlayer = player;
                }
            }
            if (closestPlayer != null)
            {
                m_Player = closestPlayer.transform;
            }
        }
        Vector3 directionToPlayer = m_Player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= m_DetectionDistance)
        {
            mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
            m_BossHoGage.SetActive(true);
            m_Animator.SetBool("isBattle", true);
            if (m_Hp <= 0)
            {
                BossBGM.gameObject.SetActive(true);
                // 1�ڂ�BGM�̃{�����[�������񂾂񉺂���
                float fadeOutVolume = Mathf.Lerp(m_InitialVolumeIdle, 0f, m_Timer / m_FadeDuration);
                IdleBGM.volume = Mathf.Max(0f, fadeOutVolume);

                // 2�ڂ�BGM�̃{�����[�������񂾂�グ��
                float fadeInVolume = Mathf.Lerp(0f, m_InitialVolumeBoss, m_Timer / m_FadeDuration);
                BossBGM.volume = Mathf.Min(m_MaxVolume, fadeInVolume);
            }
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);
            // �Q�[���I�u�W�F�N�g�{�̂̉�]�͕ύX���Ȃ�
            //   transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        }
        else
        {
            isAvoiding = false;
            m_Animator.SetBool("isBattle", false);
        }


        if (m_Hp <= 0)
        {
            if (MonobitEngine.MonobitNetwork.offline == false)
            {
                m_MonobitView.RPC("IsDie", MonobitEngine.MonobitTargets.All, null);
            }
            else
            {
                IsDie();
            }

        }
        // �v���C���[�����̋������ɂ���ꍇ
        if (distanceToPlayer <= m_AttackRange)
        {
            if (m_CurrentCooldown <= 0f)
            {
                if (MonobitEngine.MonobitNetwork.offline == false)
                {
                    m_MonobitView.RPC("ShootRandomProjectile", MonobitEngine.MonobitTargets.All, null);
                }
                else
                {
                    ShootRandomProjectile();
                }

                // �N�[���_�E�������Z�b�g
                m_CurrentCooldown = m_ShotCooldown;
            }
            else
            {
                // �N�[���_�E�����Ԃ����炷
                m_CurrentCooldown -= Time.deltaTime;
            }
        }
        if (distanceToPlayer > m_AttackRange)
        {
            m_ATCol.SetActive(false);
            m_AttackSE.SetActive(false);
            m_Animator.SetBool("isAttack", false);
        }
        if (isFire == true)
        {
            IsFired();
        }
        if (m_FireTime > 5)
        {
            isFire = false;
            m_FireTime = 0;
            m_FireEffect.Stop();
        }
    }
    private void IsFired()
    {

        m_FCTime += Time.deltaTime;
        m_FireTime += Time.deltaTime;
        m_FireEffect.Play();
        if (m_FCTime > 0.2)
        {
            m_Hp -= 1;
            m_FCTime = 0;
            mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
        }

    }
    [MunRPC]
    private void ShootRandomProjectile()
    {
        // 2�̒e�̂��������_����1��I��
        int randomProjectileIndex = Random.Range(0, 2);

        // �����_���Ȓe�𐶐����A�v���C���[�Ɍ������Ĕ��˂��鏈������������
        GameObject projectile = null;
        switch (randomProjectileIndex)
        {
            case 0:
                m_Animator.SetBool("isAttack", true);
                projectile = Instantiate(m_Shot, m_ShotPoint.position, Quaternion.identity);
                break;
            case 1:
                m_Animator.SetBool("isChrgeShot", true);
                projectile = Instantiate(m_ChrgeShot, m_ShotPoint.position, Quaternion.identity);
                break;
        }

        // �v���C���[�̕���������
        if (projectile != null)
        {
            Vector3 directionToPlayer = (m_Player.position - m_ShotPoint.position).normalized;
            projectile.GetComponent<Rigidbody>().velocity = directionToPlayer * projectileSpeed;

            // m_ShotPoint ���v���C���[�̕����Ɍ�����
            Vector3 directionToPlayerWithoutY = new Vector3(directionToPlayer.x, 0f, directionToPlayer.z);
            if (directionToPlayerWithoutY != Vector3.zero)
            {
                m_ShotPoint.rotation = Quaternion.LookRotation(directionToPlayerWithoutY);
            }
        }
    }
    [MunRPC]
    private void IsDie()
    {
        m_Animator.SetBool("isDie", true);
        m_DestroyTime += Time.deltaTime;
        m_DestroySE.SetActive(true);
        IdleBGM.volume = 0.1f;

        if (m_DestroyTime >= 1.4)
        {
            m_BossHoGage.SetActive(false);
            m_AttackObj.m_KillCount++;
            Instantiate(m_DestroyEffect, transform.position, Quaternion.identity);
            CoopScoreManager.AddScore(m_Point);
            Destroy(gameObject);
        }
    }
    // �A�j���[�V�����C�x���g����Ăяo�����֐�
    public void EndAttackAnimation()
    {
        m_MoveSpeed = m_DefoltSpeed;
        m_Animator.SetBool("isChrgeShot", false);
        m_Animator.SetBool("isAttack", false);
        m_ATCol.SetActive(false);
        m_AttackSE.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //     AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            m_Hp -= m_PlayerMove.m_PlayerDamage;
            mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
        }
        if (collision.gameObject.CompareTag("ItemBullet"))
        {
            //AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            m_Hp -= 40;
            mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            // AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            IsHit();
            m_Animator.SetBool("isHit", true);
            mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
        }
        if (other.gameObject.CompareTag("Fire"))
        {
            // AudioSource.PlayClipAtPoint(m_HitAudio, transform.position);
            IsHit();
            isFire = true;

            m_Animator.SetBool("isHit", true);
            mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
        }

    }
       public void ParticleDamage()
    {
        m_Hp -= 500;
        mHpSlider.value = (float)m_Hp / (float)m_MaxHp;
    }
    private void IsHit()
    {
        m_HitCoolTime += Time.deltaTime;
        if (m_HitCoolTime > 0.3)
        {
            m_Hp -= m_PlayerMove.m_PlayerDamage;
            m_HitCoolTime = 0;
        }
    }
}
