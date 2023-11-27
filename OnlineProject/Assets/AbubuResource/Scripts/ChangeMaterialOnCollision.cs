using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialOnCollision : MonoBehaviour
{
    [SerializeField]
    private Material m_IceMaterial; // ICEマテリアルへの参照

    private void OnCollisionEnter(Collision collision)
    {
        int randomNumber = Random.Range(0, 101); 

        if (randomNumber <= 3 && collision.gameObject.CompareTag("Enemy"))
        {
            Transform enemyTransform = collision.gameObject.transform;

            // Enemyタグの子オブジェクトを取得し、SkinnedMeshRendererコンポーネントを取得
            SkinnedMeshRenderer enemyRenderer = enemyTransform.GetComponentInChildren<SkinnedMeshRenderer>();

            if (enemyRenderer != null)
            {
                // ICEマテリアルに変更
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
