using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIRawImage))]
public class UIRawImageEditor : UnityEditor.UI.RawImageEditor
{
    SerializedProperty mThemeColorKey;
    SerializedProperty mLanguageKey;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UIRawImage inspectorObj = (UIRawImage)target;
        //SetIcon(inspectorObj.gameObject, (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image);
        //绘制贴图槽
        InspectorTools.Title("YZJRaw图片扩展功能");

        InspectorTools.BeginVertical();
        inspectorObj.mSprite = (Sprite)EditorGUILayout.ObjectField("Srpite", inspectorObj.mSprite, typeof(Sprite), true);

        serializedObject.Update();
        mThemeColorKey = serializedObject.FindProperty("mThemeColorKey");
        mLanguageKey = serializedObject.FindProperty("mLanguageKey");
        EditorGUILayout.PropertyField(mLanguageKey);
        EditorGUILayout.PropertyField(mThemeColorKey);
        serializedObject.ApplyModifiedProperties();

        //inspectorObj.LanguageKey = EditorGUILayout.TextField("语言Key", inspectorObj.LanguageKey);

        //inspectorObj.ThemeColorKey = EditorGUILayout.TextField("颜色Key", inspectorObj.ThemeColorKey);

        InspectorTools.EndVertical();
    }


    [MenuItem("GameObject/UIFramework/UI RawImage", priority = 4)]
    public static void UIRawImage()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIRawImage.prefab");
    }
}
