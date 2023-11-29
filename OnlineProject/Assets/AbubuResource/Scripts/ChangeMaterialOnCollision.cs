using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialOnCollision : MonoBehaviour
{
    [SerializeField]
    private Material m_IceMaterial; // ICE�}�e���A���ւ̎Q��
    [SerializeField,Header("PVP���[�h���ǂ���")]
    private bool isPVPMode=false;
    
    private void OnCollisionEnter(Collision collision)
    {
        int randomNumber = Random.Range(0, 101); 

        if (randomNumber <= 3 && collision.gameObject.CompareTag("Enemy"))
        {
            Transform enemyTransform = collision.gameObject.transform;

            // Enemy�^�O�̎q�I�u�W�F�N�g���擾���ASkinnedMeshRenderer�R���|�[�l���g���擾
            SkinnedMeshRenderer enemyRenderer = enemyTransform.GetComponentInChildren<SkinnedMeshRenderer>();

            if (enemyRenderer != null)
            {
                // ICE�}�e���A���ɕύX
                enemyRenderer.material = m_IceMaterial;
            }

            EnemyMove enemy = collision.gameObject.GetComponent<EnemyMove>();
            if (enemy != null)
            {
                enemy.SetIsIce(true);
            }
        }
        
        if (randomNumber <= 3 && collision.gameObject.CompareTag("Player")&&isPVPMode)
        {
            Transform playerTransform = collision.gameObject.transform;

            // Enemy�^�O�̎q�I�u�W�F�N�g���擾���ASkinnedMeshRenderer�R���|�[�l���g���擾
            SkinnedMeshRenderer playerRenderer = playerTransform.GetComponentInChildren<SkinnedMeshRenderer>();

            if (playerRenderer != null)
            {
                // ICE�}�e���A���ɕύX
                playerRenderer.material = m_IceMaterial;
            }

            PVPPlayerState player = collision.gameObject.GetComponent<PVPPlayerState>();
            if (player != null)
            {
                player.SetIsIce(true);

            }
        }
    }
}
