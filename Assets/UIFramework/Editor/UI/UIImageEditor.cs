using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIImage))]
public class UIImageEditor : UnityEditor.UI.ImageEditor
{
    SerializedProperty mThemeColorKey;
    SerializedProperty mLanguageKey;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UIImage inspectorObj = (UIImage)target;
       // SetIcon(inspectorObj, (Texture2D)EditorGUIUtility.IconContent("Image Icon").image);

        //绘制贴图槽 
        InspectorTools.Title("YZJ图片扩展功能");

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


    [MenuItem("GameObject/UIFramework/UI Image", priority = 0)]
    public static void UIImage()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIImage.prefab");
    }





    private static void SetIcon(MonoBehaviour gObj, Texture2D texture)
    {
        var ty = typeof(EditorGUIUtility);
        
        var mi = ty.GetMethod("SetIconForObject", BindingFlags.NonPublic | BindingFlags.Static);
        mi.Invoke(null, new object[] { gObj, texture });
    }

    private static GUIContent[] GetTextures(string baseName, string postFix, int startIndex, int count)
    {
        GUIContent[] guiContentArray = new GUIContent[count];

        var t = typeof(EditorGUIUtility);
        var mi = t.GetMethod("IconContent", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null);
        
        for (int index = 0; index < count; ++index)
        {
            guiContentArray[index] = mi.Invoke(null, new object[] { baseName + (object)(startIndex + index) + postFix }) as GUIContent;
        }

        return guiContentArray;
    }



}
