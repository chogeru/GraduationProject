using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ScriptableWizardクラスを継承して新しいウィザードクラスを定義します。
public class ObjectNameChanger : ScriptableWizard
{
    // 名前のプレフィックスとサフィックスを指定するためのシリアライズされたフィールド。
    [SerializeField] string prefix;
    [SerializeField] string subfix;

    // メニューアイテムのバリデーションメソッド。選択されたトランスフォームの数が1つ以上である場合に有効になります。
    [MenuItem("UITools/オブジェクト名変更", true)]
    static bool CreateWizardValidator()
    {
        Transform[] transforms = Selection.GetTransforms(SelectionMode.ExcludePrefab);
        return transforms.Length >= 1;
    }

    // メニューアイテムが選択されたときに呼ばれるウィザード作成メソッド。
    [MenuItem("UITools/オブジェクト名変更", false)]
    static void CreateWizard()
    {
        // ウィザードを表示します。タイトル、ウィザードの型、ボタンのラベルを指定します。
        ScriptableWizard.DisplayWizard("オブジェクト名変更", typeof(ObjectNameChanger), "リネームして閉じる", "リネーム");
    }

    // ウィザードが作成ボタンで閉じられるときに呼ばれるメソッド。
    void OnWizardCreate()
    {
        ApplyPrefix();
    }

    // プレフィックスを適用するメソッド。
    void ApplyPrefix()
    {
        GameObject[] gos = Selection.gameObjects;

        // 選択された各ゲームオブジェクトに対して処理を行います。
        foreach (GameObject go in gos)
        {
            var parent = go.GetComponentInParent(typeof(Transform));
            var children = go.GetComponentsInChildren(typeof(Transform));

            // 子の名前を変更します。
            foreach (Transform child in children)
            {
                // 名前変更のアンドゥステートメントを登録します。
                Undo.RegisterCompleteObjectUndo(child.gameObject, "Added: " + (string.IsNullOrEmpty(prefix) ? "" : prefix + " ") +
                    (string.IsNullOrEmpty(subfix) ? "" : subfix));

                // ルートオブジェクトには適用しない。
                if (child == go.transform)
                    continue;

                // プレフィックスを適用。
                if (!string.IsNullOrEmpty(prefix))
                {
                    child.name = prefix + child.name;
                }

                // サフィックスを適用。
                if (!string.IsNullOrEmpty(subfix))
                {
                    child.name = child.name + subfix;
                }
            }

            // 親の名前を変更します。
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

    // "リネーム" ボタンが押されたときに呼ばれるメソッド。
    void OnWizardOtherButton()
    {
        ApplyPrefix();
    }

    // ウィザードのUIが更新されるときに呼ばれるメソッド。選択されたトランスフォームの数やエラーメッセージを更新します。
    void OnWizardUpdate()
    {
        Transform[] transforms = Selection.GetTransforms(SelectionMode.ExcludePrefab);
        helpString = "オブジェクトの選択: " + transforms.Length;
        errorString = "";
        isValid = true;

        if (transforms.Length < 1)
        {
            errorString += "選択したオブジェクトがありません。";
        }

        isValid = string.IsNullOrEmpty(errorString);
    }

    // 選択が変更されたときに呼ばれるメソッド。ウィザードのUIを更新します。
    void OnSelectionChange()
    {
        OnWizardUpdate();
    }
}