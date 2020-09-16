using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIScrollViewEditor : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    base.OnInspectorGUI();
    //    UIDropdown inspectorObj = (UIDropdown)target;
    //    //SetIcon(inspectorObj.gameObject, (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image);
    //    //绘制贴图槽

    //    inspectorObj.LanguageKey = EditorGUILayout.TextField("语言Key", inspectorObj.LanguageKey);

    //    inspectorObj.ThemeColorKey = EditorGUILayout.TextField("颜色Key", inspectorObj.ThemeColorKey);
    //}
    [MenuItem("GameObject/UIFramework/UI ScrollView/UI ScrollView", priority = 2)] //UIScrollView-Horizontal
    public static void UIScrollView()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIScrollView.prefab");
    }

    [MenuItem("GameObject/UIFramework/UI ScrollView/UI ScrollView-Horizontal", priority = 2)] //
    public static void UIScrollView_Horizontal()
    {      
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIScrollView-Horizontal.prefab");
    }

    [MenuItem("GameObject/UIFramework/UI ScrollView/UI ScrollView_Vertical", priority = 2)] //
    public static void UIScrollView_Vertical()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\UIScrollView-Vertical.prefab");
    }
}
