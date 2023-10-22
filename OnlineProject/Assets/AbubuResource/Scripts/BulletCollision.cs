using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab; 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 当たった場所にパーティクルを生成
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            GameObject particle = Instantiate(particlePrefab, contact.point, rotation);

            // パーティクルを0.5秒後に破棄
            Destroy(particle, 0.5f);

            // 当たったBulletオブジェクトを破棄
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // すり抜けた場所にパーティクルを生成
            Vector3 position = transform.position;
            Quaternion rotation = Quaternion.identity; // 回転なし
            GameObject particle = Instantiate(particlePrefab, position, rotation);

            // パーティクルを0.5秒後に破棄
            Destroy(particle, 0.5f);
        }
    }
}
