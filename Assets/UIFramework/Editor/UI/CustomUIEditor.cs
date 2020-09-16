using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomUIEditor
{
    // Start is called before the first frame update
    [MenuItem("GameObject/UIFramework/UI Pro/DataLoading",priority = -1)]
    public static void DataLoading()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\DataLoading.prefab");
    }
    [MenuItem("GameObject/UIFramework/UI Pro/PageAlert",priority = -1)]
    public static void PageAlert()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\PageAlert.prefab");
    }
    [MenuItem("GameObject/UIFramework/UI Pro/PageBtnHorizontalList",priority = -1)]
    public static void PageBtnHorizontalList()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\PageBtnHorizontalList.prefab");
    }
    [MenuItem("GameObject/UIFramework/UI Pro/PageBtnVerticalList",priority = -1)]
    public static void PageBtnVerticalList()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\PageBtnVerticalList.prefab");
    }
    [MenuItem("GameObject/UIFramework/UI Pro/PageConfirm",priority = -1)]
    public static void PageConfirm()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\PageConfirm.prefab");
    }
    [MenuItem("GameObject/UIFramework/UI Pro/PageContent",priority = -1)]
    public static void PageContent()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\PageContent.prefab");
    }
    [MenuItem("GameObject/UIFramework/UI Pro/PageTabView",priority = -1)]
    public static void PageTabView()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\PageTabView.prefab");
    }
    [MenuItem("GameObject/UIFramework/UI Pro/PageTitle",priority = -1)]
    public static void PageTitle()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\PageTitle.prefab");
    }

    [MenuItem("GameObject/UIFramework/UI Pro/PageRadioContent",priority = -1)]
    public static void PageRadioContent()
    {
        CreateUIPaefabTools.CreateUIPaefab(@"Assets\UIFramework\Base\Prefabs\UIPanel\PageRadioContent.prefab");
    }
}
