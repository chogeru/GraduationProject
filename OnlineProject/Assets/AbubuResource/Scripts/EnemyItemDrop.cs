using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ItemDropPoint;
    [SerializeField,Header("�������Ă���A�C�e���̃��X�g")]
    private List<GameObject> m_ItemList=new List<GameObject>();
    [SerializeField]
    private GameObject m_Item;
    [SerializeField, Header("�A�C�e���̌�")]
    private int m_ItemNumber;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
