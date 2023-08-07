using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    //シリアライズ化してインスペクター上に表示させる
    [SerializeField] private int serial;
    //シリアライズフィールドの逆、インスペクター上に表示させない
    [NonSerialized] public int nonSerial;
    //インスペクター上に見出しを表示
    [Header("見出し")] public int header;
    //インスペクター上にある変数名にカーソルを合わせると[Tooltip]内に書かれた説明が出る
    [Tooltip("ツールチップだよ")] public int tooltip;
    //最小値を設定できる
    [Min(0)] public int min;
    //テキスト入力が複数行にわたってできる
    [Multiline] public string multiline;
    //Multilineの使いやすい版、スクロールバーが表示される
    [TextArea] public string textarea;
    //インスペクター上に空白を空ける
    [Space(50)] public string space;
    //[Range]を使うことにより、インスペクター上にスライダが表示される
    [Range(0, 1000)] public int playerhp;
    [Range(0, 1000)][SerializeField] private float playermp;
    //float型の最大値と最小値が設定できる（一応数値の欄にinfと入力すると、Infinityが入る）
    [Range(Mathf.Infinity, Mathf.NegativeInfinity)] public float infinity;

    [Header("GUI")]
    //グラデーション設定できるようになる
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

