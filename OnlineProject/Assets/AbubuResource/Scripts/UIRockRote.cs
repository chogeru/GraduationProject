using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRockRote : MonoBehaviour
{
    public Transform player; // プレイヤーの位置
    public Transform uiImage; // UI画像のTransform
    public string enemyTag = "Enemy"; // Enemyタグの名前

    private Transform nearestEnemy; // 最も近いEnemyのTransform
    private bool isAligned = false; // 位置合わせフラグ

    private void Update()
    {
        // Tabキーが押されたら
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 位置合わせがされている場合、解除する
            if (isAligned)
            {
                isAligned = false;
                uiImage.gameObject.SetActive(false); // UI画像を非表示にする
            }
            else
            {
                // 最も近いEnemyを探す
                FindNearestEnemy();

                // 最も近いEnemyがある場合、UI画像の位置を合わせる
                if (nearestEnemy != null)
                {
                    isAligned = true;
                    uiImage.gameObject.SetActive(true); // UI画像を表示する
                    uiImage.position = nearestEnemy.position;
                }
            }
        }

        // 位置合わせがされている場合、UI画像を最も近いEnemyに合わせる
        if (isAligned && nearestEnemy != null)
        {
            uiImage.position = nearestEnemy.position;
        }
    }

    // 最も近いEnemyを探す
    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closestDistance = Mathf.Infinity;
        nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }
    }
}
