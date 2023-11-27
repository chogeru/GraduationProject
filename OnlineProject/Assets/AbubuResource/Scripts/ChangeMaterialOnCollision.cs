using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialOnCollision : MonoBehaviour
{
    [SerializeField]
    private Material m_IceMaterial; // ICE�}�e���A���ւ̎Q��

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
    }
}
