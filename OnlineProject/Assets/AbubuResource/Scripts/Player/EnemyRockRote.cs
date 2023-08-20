using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRockRote : MonoBehaviour
{
    [SerializeField,Header("敵を向く速度")]
    private float m_RotationSpeed = 5.0f;
    [SerializeField,Header("敵のタグ")]
    private string m_EnemyTag = "Enemy";
    [SerializeField,Header("現在のターゲット")]
    private Transform m_CurrentTarget = null;
    [SerializeField,Header("ターゲット中かどうか")]
    private bool isTargeting = false;
    [SerializeField, Header("UI")]
    private GameObject m_RockUI;
    
    void Update()
    {
        // Tabキーが押されたらターゲットを切り替える
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleTargeting();
        }
        // ターゲット中の場合、敵の方向を向く
        if (isTargeting && m_CurrentTarget != null)
        {
            // ターゲットへの方向を計算
            Vector3 directionToTarget = m_CurrentTarget.position - transform.position;

            // ターゲットへ向かう回転を計算
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // 現在の回転から目標の回転にスムーズに遷移
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);
        }
    }

    void ToggleTargeting()
    {
        isTargeting = !isTargeting;

        if (isTargeting)
        {
            m_RockUI.SetActive(true);
            // 一番近い敵を探す
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(m_EnemyTag);
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                // プレイヤーと敵の距離を計算
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                // より近い敵を見つけたらターゲットを更新
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    m_CurrentTarget = enemy.transform;
                }
            }
        }
        else
        {
            // ターゲットが外れたら、ターゲットをnullにする
            m_CurrentTarget = null;
            m_RockUI.SetActive(false);
        }
    }
}
