using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIToggle))]
public class UIToggleEditor : UnityEditor.UI.ToggleEditor
{
    SerializedProperty toggleType;
    SerializedProperty ToggleSelect_Color;
    SerializedProperty ToggleDefault_Color;
    SerializedProperty childText;
    SerializedProperty mThemeColorKey;
    SerializedProperty childImg;
    SerializedProperty mLanguageKey;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UIToggle inspectorObj = (UIToggle)target;
        InspectorTools.Title("YZJ选择器扩展功能");

        InspectorTools.BeginVertical();
        serializedObject.Update();

        toggleType = serializedObject.FindProperty("toggleType");
        EditorGUILayout.PropertyField(toggleType);

        //ToggleSelect_Color = serializedObject.FindProperty("ToggleSelect_Color");
        //EditorGUILayout.PropertyField(ToggleSelect_Color);

        //ToggleDefault_Color = serializedObject.FindProperty("ToggleDefault_Color");
        //EditorGUILayout.PropertyField(ToggleDefault_Color);

        if (toggleType.enumValueIndex == 0)
        {
            childText = serializedObject.FindProperty("childText");
            EditorGUILayout.PropertyField(childText);

            childImg = serializedObject.FindProperty("childImg");
            EditorGUILayout.PropertyField(childImg);
        }


        mThemeColorKey = serializedObject.FindProperty("mThemeColorKey");
        EditorGUILayout.PropertyField(mThemeColorKey);

        mLanguageKey = serializedObject.FindProperty("mLanguageKey");
        EditorGUILayout.PropertyField(mLanguageKey);

        // inspectorObj.LanguageKey = EditorGUILayout.TextField("语言Key", inspectorObj.LanguageKey);

        // inspectorObj.ThemeColorKey = EditorGUILayout.TextField("颜色Key", inspectorObj.ThemeColorKey);


        //SetIcon(inspectorObj.gameObject, (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image);
        //绘制贴图槽
        //   inspectorObj.childText = EditorGUILayout.ObjectField("子文字", inspectorObj.childText, typeof(UIText), true) as UIText;

        //   inspectorObj.childImg = EditorGUILayout.ObjectField("子图标", inspectorObj.childImg, typeof(UIImage), true) as UIImage;

        ////   inspectorObj.toggleType = (ToggleType)EditorGUILayout.EnumPopup("显示模式", inspectorObj.toggleType);

        serializedObject.ApplyModifiedProperties();
        InspectorTools.EndVertical();
    }
    [MenuItem("GameObject/UIFramework/UI Toggle/UI Toggle1", priority = 9)]
    public static void UIToggle1()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIToggle.prefab");
    }

    [MenuItem("GameObject/UIFramework/UI Toggle/UI Toggle2", priority = 9)]
    public static void UIToggle2()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIToggle1.prefab");
    }
    [MenuItem("GameObject/UIFramework/UI Toggle/UI Toggle3", priority = 9)]
    public static void UIToggle3()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIToggle2.prefab");
    }
}
