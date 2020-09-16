using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GenerateFolders : EditorWindow
{
#if UNITY_EDITOR
    [MenuItem("Tools/CreateBasicFolder #&_b")]
    private static void CreateBasicFolder()
    {
        GenerateFolder();
        Debug.Log("Folders Created");
    }

    [MenuItem("Tools/CreateALLFolder")]
    private static void CreateAllFolder()
    {
        GenerateFolder(1);
        Debug.Log("Folders Created");
    }
    //[MenuItem("Tools/设备类型/眼镜")]
    //private static void SetDeviceType0()
    //{
    //    PlayerPrefs.SetInt("EDITORTYPE", 0);
    //    SetGameWindowSize(1280, 720, "眼镜分辨率");
    //    GameObject canvas = GameObject.Find("Canvas");
    //    if (canvas != null)
    //    {
    //        canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1280, 720);
    //        canvas.gameObject.SetActive(false);
    //        canvas.gameObject.SetActive(true);
    //    }
    //}
    //[MenuItem("Tools/设备类型/手机")]
    //private static void SetDeviceType1()
    //{
    //    PlayerPrefs.SetInt("EDITORTYPE", 1);
    //    SetGameWindowSize(1080, 1920, "手机分辨率");
    //    GameObject canvas = GameObject.Find("Canvas");
    //    if (canvas != null)
    //    {
    //        canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1080, 1920);
    //        canvas.gameObject.SetActive(false);
    //        canvas.gameObject.SetActive(true);
    //    }
    //}
    [MenuItem("Log/开启Log")]
    public static void OpenLog()
    {
        SetDebugerLevle("Log_", "OpenLog");
    }
    [MenuItem("Log/关闭Log")]
    public static void CloseLog()
    {
        SetDebugerLevle("Log_", "CloseLog");
    }
    [MenuItem("Log/清除PlayerPrefs")]
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void SetGameWindowSize(int width, int height, string text)
    {
        Type gameViewType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        EditorWindow window = GetWindow(gameViewType);

        MethodInfo get_currentGameViewSize = gameViewType.GetMethod("get_currentGameViewSize", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        object gameViewSize = get_currentGameViewSize.Invoke(window, new object[] { });
        Type gameViewSizeType = gameViewSize.GetType();

        MethodInfo widthMethodInfo = gameViewSizeType.GetMethod("set_width", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        widthMethodInfo.Invoke(gameViewSize, new object[] { width });

        MethodInfo heightMethodInfo = gameViewSizeType.GetMethod("set_height", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        heightMethodInfo.Invoke(gameViewSize, new object[] { height });

        MethodInfo baseTextMethodInfo = gameViewSizeType.GetMethod("set_baseText", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        baseTextMethodInfo.Invoke(gameViewSize, new object[] { text });
    }
    private static void GenerateFolder(int flag = 0)
    {
        // 文件路径
        string prjPath = Application.dataPath + "/";
        Directory.CreateDirectory(prjPath + "Audio");
        Directory.CreateDirectory(prjPath + "Prefabs");
        Directory.CreateDirectory(prjPath + "Materials");
        Directory.CreateDirectory(prjPath + "Resources");
        Directory.CreateDirectory(prjPath + "Scripts");
        Directory.CreateDirectory(prjPath + "Plugins");
        Directory.CreateDirectory(prjPath + "Textures");
        Directory.CreateDirectory(prjPath + "Scenes");
        Directory.CreateDirectory(prjPath + "Animations");
        if (1 == flag)
        {
            Directory.CreateDirectory(prjPath + "Meshes");
            Directory.CreateDirectory(prjPath + "Shaders");
            Directory.CreateDirectory(prjPath + "GUI");
        }

        AssetDatabase.Refresh();
    }
    public static void SetDebugerLevle(string headTpye, string logType)
    {
        BuildTargetGroup targetGroup = BuildTargetGroup.Android;
        string ori = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
        string debugType = headTpye + logType;

        List<string> defineSymbols = new List<string>(ori.Split(';'));
        for (int i = 0; i < defineSymbols.Count; ++i)
        {
            Debug.Log(defineSymbols[i]);
            if (defineSymbols[i] == debugType)
            {
                Debug.LogFormat("========== debuglog {0}", logType);
                return;
            }

            if (defineSymbols[i].StartsWith(headTpye))
            {
                defineSymbols[i] = debugType;
                debugType = null;
                break;
            }
        }
        if (debugType != null)
        {
            defineSymbols.Add(debugType);
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, string.Join(";", defineSymbols.ToArray()));
        Debug.LogFormat("========== debuglog {0}", logType);
    }

#endif


}