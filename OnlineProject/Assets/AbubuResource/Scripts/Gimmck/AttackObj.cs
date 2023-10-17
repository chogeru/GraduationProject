using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;

public class AttackObj : MonoBehaviour
{
    public int m_KillCount;
    [SerializeField]
    private Animator m_ActiveObj;
    [SerializeField]
    private GameObject m_AvtiveOBjct;
    private List<WaterEnemy> m_Boss = new List<WaterEnemy>();
    [SerializeField]
    private GameObject m_BossWall;

    private void Start()
    {
     
        m_ActiveObj =m_AvtiveOBjct.GetComponent<Animator>();
    }
    private void Update()
    {
        if (m_KillCount>=2)
        {
          
            m_ActiveObj.SetBool("IsActive", true);
            Destroy(m_BossWall);
        }
    }
}
