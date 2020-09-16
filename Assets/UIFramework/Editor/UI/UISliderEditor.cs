using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UISlider))]
public class UISliderEditor : UnityEditor.UI.SliderEditor
{
    SerializedProperty mThemeColorKey;
    SerializedProperty mLanguageKey;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UISlider inspectorObj = (UISlider)target;
        //SetIcon(inspectorObj.gameObject, (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image);
        //绘制贴图槽
        InspectorTools.Title("YZJ滑动条扩展功能");
        InspectorTools.BeginVertical();
        //inspectorObj.LanguageKey = EditorGUILayout.TextField("语言Key", inspectorObj.LanguageKey);

        //inspectorObj.ThemeColorKey = EditorGUILayout.TextField("颜色Key", inspectorObj.ThemeColorKey);

        serializedObject.Update();
        mThemeColorKey = serializedObject.FindProperty("mThemeColorKey");
        mLanguageKey = serializedObject.FindProperty("mLanguageKey");
        EditorGUILayout.PropertyField(mLanguageKey);
        EditorGUILayout.PropertyField(mThemeColorKey);
        serializedObject.ApplyModifiedProperties();

        InspectorTools.EndVertical();
    }

    [MenuItem("GameObject/UIFramework/UI Slider", priority = 9)]
    public static void UISlider()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UISlider.prefab");
    }
}
