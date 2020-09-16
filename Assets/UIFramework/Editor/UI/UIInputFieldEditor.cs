using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIInputField))]
public class UIInputFieldEditor : UnityEditor.UI.InputFieldEditor
{
    SerializedProperty mThemeColorKey;
    SerializedProperty mLanguageKey;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UIInputField inspectorObj = (UIInputField)target;
        //SetIcon(inspectorObj.gameObject, (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image);
        //绘制贴图槽

        //inspectorObj.LanguageKey = EditorGUILayout.TextField("语言Key", inspectorObj.LanguageKey);

        //inspectorObj.ThemeColorKey = EditorGUILayout.TextField("颜色Key", inspectorObj.ThemeColorKey);

        InspectorTools.Title("YZJ输入框扩展功能");

        InspectorTools.BeginVertical();
        serializedObject.Update();
        mThemeColorKey = serializedObject.FindProperty("mThemeColorKey");
        mLanguageKey = serializedObject.FindProperty("mLanguageKey");
        EditorGUILayout.PropertyField(mLanguageKey);
        EditorGUILayout.PropertyField(mThemeColorKey);
        serializedObject.ApplyModifiedProperties();
        InspectorTools.EndVertical();


    }

    [MenuItem("GameObject/UIFramework/UI InputField/UI InputField1", priority = 5)]
    public static void UIInputField1()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIInputField.prefab");
    }
    [MenuItem("GameObject/UIFramework/UI InputField/UI InputField2", priority = 5)]
    public static void UIInputField2()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIInputField1.prefab");
    }
}
