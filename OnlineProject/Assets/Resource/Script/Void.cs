using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
         �N���X���L���̎��ɁA�N���X���Q�[�����ŃC���X�^���X���i���̉��j�����u�Ԃ̂P�x�����Ăяo����郁�\�b�h�B
         */
    }

    // Update is called once per frame
    void Update()
    {
        /*
         �N���X���L���̎��ɁA�P�t���[���i���j�ɂP��Ăяo����郁�\�b�h�B
         */
    }
    void Awake()
    {
        /*
         Start()������ɌĂяo����܂��BAwake()���Ăяo����鏇�Ԃ͕s��ł��邽�߁A
        ���̃��\�b�h�ɑ��̃I�u�W�F�N�g���Q�Ƃ����Ă���ƑΏۂ̃I�u�W�F�N�g������������Ă��Ȃ����߂ɏ�肭���삵�Ȃ��\��������܂��B
        ���̂��߁AAwake()�ł͎��g�Ɋւ��鏉�����������s���܂��B
        ��ɃV�[���̃��[�h�i�������j�Ɏg���܂��B�܂��A���̃I�u�W�F�N�g���Q�Ƃ����������s���C�x���g��Start()�Œ�`���܂��B
         */
    }
    void LateUpdate()
    {
        /*
         LateUpdate()��Update()�Ɠ��l�ɂP�t���[���ɂP��Ăяo����܂��BUpdate()�Ƃ̈Ⴂ�́A�P�t���[���̒��ŌĂяo�����^�C�~���O�ł��B
        Update()���l�X�ȏ����̓r���ŌĂяo�����̂ɑ΂��ALateUpdate()�͂P�t���[�����̑S�Ă̏������I������Ō�ɌĂяo����܂��B
         */
    }
    void FixedUpdate()
    {
        /*
         �Q�[�������Ԃł̈��Ԋu�ŌĂ΂�܂��B�����̎��ԂƂ͕ʂœ����Ă��邽�߁A�t���[�����[�g�Ɉˑ����܂���B
        ���̂��߁A�P�t���[�����ɂP�x���Ă΂�Ȃ�������A������Ă΂ꂽ�肷�邱�Ƃ�����܂��B�Q�[�������ԍX�V��A�������Z�����̑O�ɖ���Ă΂�܂��B
         */

    }
}
