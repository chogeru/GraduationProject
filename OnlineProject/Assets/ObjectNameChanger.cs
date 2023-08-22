using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ScriptableWizard�N���X���p�����ĐV�����E�B�U�[�h�N���X���`���܂��B
public class ObjectNameChanger : ScriptableWizard
{
    // ���O�̃v���t�B�b�N�X�ƃT�t�B�b�N�X���w�肷�邽�߂̃V���A���C�Y���ꂽ�t�B�[���h�B
    [SerializeField] string prefix;
    [SerializeField] string subfix;

    // ���j���[�A�C�e���̃o���f�[�V�������\�b�h�B�I�����ꂽ�g�����X�t�H�[���̐���1�ȏ�ł���ꍇ�ɗL���ɂȂ�܂��B
    [MenuItem("UITools/�I�u�W�F�N�g���ύX", true)]
    static bool CreateWizardValidator()
    {
        Transform[] transforms = Selection.GetTransforms(SelectionMode.ExcludePrefab);
        return transforms.Length >= 1;
    }

    // ���j���[�A�C�e�����I�����ꂽ�Ƃ��ɌĂ΂��E�B�U�[�h�쐬���\�b�h�B
    [MenuItem("UITools/�I�u�W�F�N�g���ύX", false)]
    static void CreateWizard()
    {
        // �E�B�U�[�h��\�����܂��B�^�C�g���A�E�B�U�[�h�̌^�A�{�^���̃��x�����w�肵�܂��B
        ScriptableWizard.DisplayWizard("�I�u�W�F�N�g���ύX", typeof(ObjectNameChanger), "���l�[�����ĕ���", "���l�[��");
    }

    // �E�B�U�[�h���쐬�{�^���ŕ�����Ƃ��ɌĂ΂�郁�\�b�h�B
    void OnWizardCreate()
    {
        ApplyPrefix();
    }

    // �v���t�B�b�N�X��K�p���郁�\�b�h�B
    void ApplyPrefix()
    {
        GameObject[] gos = Selection.gameObjects;

        // �I�����ꂽ�e�Q�[���I�u�W�F�N�g�ɑ΂��ď������s���܂��B
        foreach (GameObject go in gos)
        {
            var parent = go.GetComponentInParent(typeof(Transform));
            var children = go.GetComponentsInChildren(typeof(Transform));

            // �q�̖��O��ύX���܂��B
            foreach (Transform child in children)
            {
                // ���O�ύX�̃A���h�D�X�e�[�g�����g��o�^���܂��B
                Undo.RegisterCompleteObjectUndo(child.gameObject, "Added: " + (string.IsNullOrEmpty(prefix) ? "" : prefix + " ") +
                    (string.IsNullOrEmpty(subfix) ? "" : subfix));

                // ���[�g�I�u�W�F�N�g�ɂ͓K�p���Ȃ��B
                if (child == go.transform)
                    continue;

                // �v���t�B�b�N�X��K�p�B
                if (!string.IsNullOrEmpty(prefix))
                {
                    child.name = prefix + child.name;
                }

                // �T�t�B�b�N�X��K�p�B
                if (!string.IsNullOrEmpty(subfix))
                {
                    child.name = child.name + subfix;
                }
            }

            // �e�̖��O��ύX���܂��B
            if (!string.IsNullOrEmpty(prefix))
            {
                parent.name = prefix + parent.name;
            }

            if (!string.IsNullOrEmpty(subfix))
            {
                parent.name = parent.name + subfix;
            }
        }
    }

    // "���l�[��" �{�^���������ꂽ�Ƃ��ɌĂ΂�郁�\�b�h�B
    void OnWizardOtherButton()
    {
        ApplyPrefix();
    }

    // �E�B�U�[�h��UI���X�V�����Ƃ��ɌĂ΂�郁�\�b�h�B�I�����ꂽ�g�����X�t�H�[���̐���G���[���b�Z�[�W���X�V���܂��B
    void OnWizardUpdate()
    {
        Transform[] transforms = Selection.GetTransforms(SelectionMode.ExcludePrefab);
        helpString = "�I�u�W�F�N�g�̑I��: " + transforms.Length;
        errorString = "";
        isValid = true;

        if (transforms.Length < 1)
        {
            errorString += "�I�������I�u�W�F�N�g������܂���B";
        }

        isValid = string.IsNullOrEmpty(errorString);
    }

    // �I�����ύX���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h�B�E�B�U�[�h��UI���X�V���܂��B
    void OnSelectionChange()
    {
        OnWizardUpdate();
    }
}