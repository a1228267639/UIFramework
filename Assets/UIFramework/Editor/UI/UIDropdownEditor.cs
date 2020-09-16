using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIDropdown))]
public class UIDropdownEditor : UnityEditor.UI.DropdownEditor
{
    SerializedProperty mThemeColorKey;
    SerializedProperty mLanguageKey;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UIDropdown inspectorObj = (UIDropdown)target;
        //SetIcon(inspectorObj.gameObject, (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image);
        //绘制贴图槽

        //inspectorObj.LanguageKey = EditorGUILayout.TextField("语言Key", inspectorObj.LanguageKey);

        //inspectorObj.ThemeColorKey = EditorGUILayout.TextField("颜色Key", inspectorObj.ThemeColorKey);
        InspectorTools.Title("YZJ下拉框扩展功能");

        InspectorTools.BeginVertical();
        serializedObject.Update();
        mThemeColorKey = serializedObject.FindProperty("mThemeColorKey");
        mLanguageKey = serializedObject.FindProperty("mLanguageKey");
        EditorGUILayout.PropertyField(mLanguageKey);
        EditorGUILayout.PropertyField(mThemeColorKey);
        serializedObject.ApplyModifiedProperties();
        InspectorTools.EndVertical();
    }
    [MenuItem("GameObject/UIFramework/UI Dropdown", priority = 7)]
    public static void UIDropdown()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIDropdown.prefab");
    }
}
