using DG.DemiEditor;
using DG.DOTweenEditor.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(UIButton))]
[CanEditMultipleObjects]
public class UIButtonEditor : UnityEditor.UI.ButtonEditor
{
    SerializedProperty mThemeColorKey;
    SerializedProperty mLanguageKey;
    protected override void OnEnable()
    {
        base.OnEnable();


    }

    public override void OnInspectorGUI()
    {
        UIButton inspectorObj = (UIButton)target;
        base.OnInspectorGUI();
        InspectorTools.Title("YZJ按钮扩展功能");
        InspectorTools.BeginVertical();
        serializedObject.Update();
        mThemeColorKey = serializedObject.FindProperty("mThemeColorKey");
        mLanguageKey = serializedObject.FindProperty("mLanguageKey");
        EditorGUILayout.PropertyField(mLanguageKey);
        EditorGUILayout.PropertyField(mThemeColorKey);
        serializedObject.ApplyModifiedProperties();

        inspectorObj.mUIText = (UIText)EditorGUILayout.ObjectField("按钮Text", inspectorObj.mUIText, typeof(UIText), true);

        if (inspectorObj.mUIText == null)
        {
            EditorGUILayout.HelpBox("子文本没有引用", MessageType.Warning);
        }

        inspectorObj.mUIImage = (UIImage)EditorGUILayout.ObjectField("按钮IMG", inspectorObj.mUIImage, typeof(UIImage), true);

        InspectorTools.EndVertical();
    }
    [MenuItem("GameObject/UIFramework/UI Button", priority = 3)]
    public static void UIButton()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIButton.prefab");
    }
}
