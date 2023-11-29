using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPPlayerState : MonoBehaviour
{
    PlayerMove playerMove;
    Animator animator;
    [SerializeField]
    private bool isIce = false;
    private float m_IceTime;
    private float m_SetMoveSpeed;
    [SerializeField]
    private SkinnedMeshRenderer[] childRenderers;
    [SerializeField]
    private Material[] originalMaterials;
    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        animator= GetComponent<Animator>();
        childRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        // �q�I�u�W�F�N�g��SkinnedMeshRenderer�̏����}�e���A�����擾
        List<Material> materialsList = new List<Material>();
        foreach (var renderer in childRenderers)
        {
            materialsList.AddRange(renderer.sharedMaterials);
        }
        originalMaterials = materialsList.ToArray();
        m_SetMoveSpeed = playerMove.m_MoveSpeed;

    }
    private void Update()
    {
        if (m_IceTime > 3)
        {
            isIce = false;
            playerMove.m_MoveSpeed = m_SetMoveSpeed;
            animator.speed = 1f;
        }
        if (!isIce)
        {
            // �}�e���A����������Ԃɖ߂�
            foreach (var renderer in childRenderers)
            {
                renderer.sharedMaterials = originalMaterials;
            }
        }
        if (isIce == true)
        {
            playerMove.m_MoveSpeed = 0;
            animator.speed = 0f;
            m_IceTime += Time.deltaTime;
            return;
        }
    }
    public void SetIsIce(bool value)
    {
        isIce = value;
    }
}
