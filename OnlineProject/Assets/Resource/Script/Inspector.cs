using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    //�V���A���C�Y�����ăC���X�y�N�^�[��ɕ\��������
    [SerializeField] private int serial;
    //�V���A���C�Y�t�B�[���h�̋t�A�C���X�y�N�^�[��ɕ\�������Ȃ�
    [NonSerialized] public int nonSerial;
    //�C���X�y�N�^�[��Ɍ��o����\��
    [Header("���o��")] public int header;
    //�C���X�y�N�^�[��ɂ���ϐ����ɃJ�[�\�������킹���[Tooltip]���ɏ����ꂽ�������o��
    [Tooltip("�c�[���`�b�v����")] public int tooltip;
    //�ŏ��l��ݒ�ł���
    [Min(0)] public int min;
    //�e�L�X�g���͂������s�ɂ킽���Ăł���
    [Multiline] public string multiline;
    //Multiline�̎g���₷���ŁA�X�N���[���o�[���\�������
    [TextArea] public string textarea;
    //�C���X�y�N�^�[��ɋ󔒂��󂯂�
    [Space(50)] public string space;
    //[Range]���g�����Ƃɂ��A�C���X�y�N�^�[��ɃX���C�_���\�������
    [Range(0, 1000)] public int playerhp;
    [Range(0, 1000)][SerializeField] private float playermp;
    //float�^�̍ő�l�ƍŏ��l���ݒ�ł���i�ꉞ���l�̗���inf�Ɠ��͂���ƁAInfinity������j
    [Range(Mathf.Infinity, Mathf.NegativeInfinity)] public float infinity;

    [Header("GUI")]
    //�O���f�[�V�����ݒ�ł���悤�ɂȂ�
    public Gradient coloer;

    public Type Method = Type.Atype;
    public enum Type
    {
        Atype,
        Btype,
    }
    public bool button = false;

    [SerializeField] private AudioClip[] Sounds = null;

}

