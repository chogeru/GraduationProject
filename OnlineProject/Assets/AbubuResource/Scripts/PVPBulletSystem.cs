using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPBulletSystem : MonoBehaviour
{
    public int damageAmount = 1; // プレイヤーに与えるダメージ量
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            // 衝突したオブジェクトがプレイヤーの場合、体力を減らす
            PlayerMove playermove = other.GetComponent<PlayerMove>();
            if (playermove != null)
            {
                playermove.TakeDamage(damageAmount);
            }
        }
    }
}
