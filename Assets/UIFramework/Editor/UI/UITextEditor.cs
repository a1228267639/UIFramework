using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIText))]
public class UITextEditor : UnityEditor.UI.TextEditor
{
    SerializedProperty mThemeColorKey;
    SerializedProperty mLanguageKey;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UIText inspectorObj = (UIText)target;
        //绘制贴图槽
        InspectorTools.Title("YZJ文本扩展功能");
        InspectorTools.BeginVertical();

        serializedObject.Update();
        mThemeColorKey = serializedObject.FindProperty("mThemeColorKey");
        mLanguageKey = serializedObject.FindProperty("mLanguageKey");
        EditorGUILayout.PropertyField(mLanguageKey);
        EditorGUILayout.PropertyField(mThemeColorKey);
        serializedObject.ApplyModifiedProperties();

        //inspectorObj.mLanguageKey = EditorGUILayout.TextField("语言Key", inspectorObj.mLanguageKey);

        //inspectorObj.mThemeColorKey = EditorGUILayout.TextField("颜色Key", inspectorObj.mThemeColorKey);
        InspectorTools.EndVertical();
    }

    //注册到Assets目录下
    [MenuItem("GameObject/UIFramework/UI Text", priority = 1)]
    public static void UIText()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIText.prefab");
    }
}
