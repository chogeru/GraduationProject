using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObj : MonoBehaviour
{
    public int m_KillCount;
    [SerializeField]
    private Animator m_ActiveObj;
    [SerializeField]
    private Animator m_BossWallAnimator;
    [SerializeField]
    private GameObject m_AvtiveOBjct;
    private List<WaterEnemy> m_Boss = new List<WaterEnemy>();
    [SerializeField]
    private GameObject m_BossWall;
    [SerializeField]
    private GameObject m_SpownWall;
    private void Start()
    {
     
        m_ActiveObj =m_AvtiveOBjct.GetComponent<Animator>();
    }
    private void Update()
    {
        if (m_KillCount>=2)
        {
          m_SpownWall.SetActive(true);
            m_ActiveObj.SetBool("IsActive", true);
            m_BossWallAnimator.SetBool("isBossDie", true);
        }
    }
}
