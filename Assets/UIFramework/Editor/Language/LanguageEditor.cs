using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UIFramework;
using UnityEditor;
using UnityEngine;


public class LanguageEditor : EditorWindow
{
    static LanguageEditor window;
    private static int select = 0;
    private static string[] names;
    private static GUIContent[] myGUIContent = new GUIContent[4];
    private static List<KeyValuesNode> LanguageKey_Dic = new List<KeyValuesNode>();
    private static List<KeyValuesNode> cacheLanguageKey_Dic = new List<KeyValuesNode>();

    //private static Dictionary<string, string> LanguageValue_Dic;
    static bool Isinit = false;
    static int nameIndex;

    static string configDataEntityPath;
    string searchValue = "";
    string searchIndexValue = "";

    private Vector2 m_DeviceModelTablePosition = Vector2.zero;
    GUIStyle titleStyle2 = new GUIStyle();
    GUIStyle titleStyle1 = new GUIStyle();
    bool IsClick1;
    bool IsClick2;
    int index;
    bool IsSave;
    static string ChinaConfigPath = @"Assets/UIFramework/Resources/Language/ChineseLauguageJSONConfig.json";
    static string JapanConfigPath = @"Assets/UIFramework/Resources/Language/JapaneseLauguageJSONConfig.json";

    static void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        if (e != null && e.button == 1 && e.type == EventType.MouseUp)
        {
            //右键单击啦，在这里显示菜单
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("菜单项1"), false, OnMenuClick, "menu_1");
            menu.AddItem(new GUIContent("菜单项2"), false, OnMenuClick, "menu_2");
            menu.AddItem(new GUIContent("菜单项3"), false, OnMenuClick, "menu_3");
            menu.ShowAsContext();
        }
    }
    static void OnMenuClick(object userData)
    {
        EditorUtility.DisplayDialog("Tip", "OnMenuClick" + userData.ToString(), "Ok");
    }


    static TextAsset configInfo = null;
    static KeyValuesInfo keyvalueInfoObj = null;
    [MenuItem("Tools/多语言编辑器")]
    [CanEditMultipleObjects]
    static void Init()
    {
        SceneView.duringSceneGui += OnSceneGUI;
        window = EditorWindow.GetWindow<LanguageEditor>();
        window.titleContent = new GUIContent("多语言编辑器", EditorGUIUtility.IconContent("EditorSettings Icon").image);
        window.maxSize = new Vector2(650, 800);
        window.minSize = new Vector2(650, 800);
        window.Show();
        configDataEntityPath = ChinaConfigPath;
        configInfo = AssetDatabase.LoadAssetAtPath<TextAsset>(ChinaConfigPath);
        keyvalueInfoObj = LitJson.JsonMapper.ToObject<KeyValuesInfo>(configInfo.text);
        names = System.Enum.GetNames(typeof(LauguageType));
        LanguageKey_Dic.Clear();
        int index = 0;
        select = 0;
        foreach (var item in keyvalueInfoObj.ConfigInfo)
        {
            KeyValuesNode keyValuesNode = new KeyValuesNode();
            keyValuesNode.Index = index += 1;
            keyValuesNode.Key = item.Key;
            keyValuesNode.Value = item.Value;
            LanguageKey_Dic.Add(keyValuesNode);
        }
    }
    private void OnDisable()
    {
        if (IsSave)
        {
            if (EditorUtility.DisplayDialog("标题", "是否保存", "确认", "取消"))
            {
                string json = LitJson.JsonMapper.ToJson(LanguageKey_Dic);
                switch (select)
                {
                    case 0:
                        //configInfo = AssetDatabase.LoadAssetAtPath<TextAsset>(@"Assets/UIFramework/Resources/Language/ChineseLauguageJSONConfig.json");
                        Debug.Log(json);
                        using (StreamWriter sw = new StreamWriter(@"Assets/UIFramework/Resources/Language/ChineseLauguageJSONConfig.json"))
                        {
                            sw.WriteLine("{\"ConfigInfo\":" + json + "}");
                        }
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        break;
                    case 1:
                        // configInfo = AssetDatabase.LoadAssetAtPath<TextAsset>(@"Assets/UIFramework/Resources/Language/JapaneseLauguageJSONConfig.json");
                        Debug.Log(json);
                        using (StreamWriter sw = new StreamWriter(@"Assets/UIFramework/Resources/Language/JapaneseLauguageJSONConfig.json"))
                        {
                            sw.WriteLine("{\"ConfigInfo\":" + json + "}");
                        }
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        break;
                }
            }
        }

    }
    private void DrawKeyItme(KeyValuesNode obj, string Title, List<KeyValuesNode> Dic, float width = 30f)
    {
        KeyValuesNode keyValuesNode = null;
        keyValuesNode = LanguageKey_Dic.Find(Ds => Ds.Index == obj.Index);
        EditorGUILayout.LabelField(new GUIContent(Title, obj.Key.ToString()), titleStyle2, GUILayout.Width(width));
        string value = EditorGUILayout.TextField(obj.Key.ToString(), GUILayout.Width(position.width / 2 - 100));
        if (value != obj.Key.ToString())
        {
            IsSave = true;
            LanguageKey_Dic.ForEach(Dc =>
            {
                if (Dc.Index == keyValuesNode.Index)
                {
                    keyValuesNode.Key = value;
                    Undo.undoRedoPerformed += () =>
                    {
                        keyValuesNode.Key = obj.Key;
                    };
                }
            });
        }
    }
    private void DrawValueItem(KeyValuesNode obj, string Title, List<KeyValuesNode> Dic, float width = 30f)
    {
        KeyValuesNode keyValuesNode = null;
        keyValuesNode = LanguageKey_Dic.Find(Ds => Ds.Index == obj.Index);
        EditorGUILayout.LabelField(new GUIContent(Title, obj.Value.ToString()), titleStyle2, GUILayout.Width(width));
        string value = EditorGUILayout.TextField(obj.Value.ToString(), GUILayout.Width(position.width / 2 - 100));

        if (value != obj.Value.ToString())
        {
            IsSave = true;
            if (keyValuesNode == null)
            {
                Debug.LogError(obj + "is  Null");
                return;
            }
            LanguageKey_Dic.ForEach(Dc =>
            {
                if (Dc.Index == keyValuesNode.Index)
                {
                    keyValuesNode.Value = value;
                }
            });

        }

        // field.SetValue(obj, value);
    }

    public void OnDeviceModelGUI()
    {

    }
    private void OnGUI()
    {
        select = GUILayout.Toolbar(select, names);
        int deleteIndex = -1;
        int moveIndex = -1;
        switch (select)
        {
            case 0:
                if (!IsClick1)
                {
                    if (IsSave)
                    {
                        if (EditorUtility.DisplayDialog("标题", "是否保存", "确认", "取消"))
                        {
                            string json = LitJson.JsonMapper.ToJson(LanguageKey_Dic);
                            Debug.Log(json);
                            using (StreamWriter sw = new StreamWriter(@"Assets/UIFramework/Resources/Language/JapaneseLauguageJSONConfig.json"))
                            {
                                sw.WriteLine("{\"ConfigInfo\":" + json + "}");
                            }
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                            IsSave = false;
                        }
                    }
                    GUIUtility.keyboardControl = 0;
                    IsClick1 = true;
                    IsClick2 = false;
                    configDataEntityPath = ChinaConfigPath;
                    configInfo = AssetDatabase.LoadAssetAtPath<TextAsset>(ChinaConfigPath);
                    keyvalueInfoObj = LitJson.JsonMapper.ToObject<KeyValuesInfo>(configInfo.text);
                    names = System.Enum.GetNames(typeof(LauguageType));
                    LanguageKey_Dic.Clear();
                    int index = 0;
                    foreach (var item in keyvalueInfoObj.ConfigInfo)
                    {
                        KeyValuesNode keyValuesNode = new KeyValuesNode();
                        keyValuesNode.Index = index += 1;
                        keyValuesNode.Key = item.Key;
                        keyValuesNode.Value = item.Value;
                        LanguageKey_Dic.Add(keyValuesNode);
                    }

                }
                break;
            case 1:
                if (!IsClick2)
                {
                    if (IsSave)
                    {
                        if (EditorUtility.DisplayDialog("标题", "是否保存", "确认", "取消"))
                        {
                            IsSave = false;
                            string json = LitJson.JsonMapper.ToJson(LanguageKey_Dic);
                            Debug.Log(json);
                            using (StreamWriter sw = new StreamWriter(@"Assets/UIFramework/Resources/Language/ChineseLauguageJSONConfig.json"))
                            {
                                sw.WriteLine("{\"ConfigInfo\":" + json + "}");
                            }
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }
                    }
                    GUIUtility.keyboardControl = 0;
                    IsClick2 = true;
                    IsClick1 = false;
                    configDataEntityPath = JapanConfigPath;
                    configInfo = AssetDatabase.LoadAssetAtPath<TextAsset>(JapanConfigPath);
                    keyvalueInfoObj = LitJson.JsonMapper.ToObject<KeyValuesInfo>(configInfo.text);
                    names = System.Enum.GetNames(typeof(LauguageType));
                    LanguageKey_Dic.Clear();
                    int index = 0;
                    foreach (var item in keyvalueInfoObj.ConfigInfo)
                    {
                        KeyValuesNode keyValuesNode = new KeyValuesNode();
                        keyValuesNode.Index = index += 1;
                        keyValuesNode.Key = item.Key;
                        keyValuesNode.Value = item.Value;
                        LanguageKey_Dic.Add(keyValuesNode);
                    }

                }
                break;
        }
        if (GUILayout.Button("保存语言配置表", GUI.skin.button))
        {
            string json = LitJson.JsonMapper.ToJson(LanguageKey_Dic);
            switch (select)
            {
                case 0:
                    //configInfo = AssetDatabase.LoadAssetAtPath<TextAsset>(@"Assets/UIFramework/Resources/Language/ChineseLauguageJSONConfig.json");
                    Debug.Log(json);
                    using (StreamWriter sw = new StreamWriter(@"Assets/UIFramework/Resources/Language/ChineseLauguageJSONConfig.json"))
                    {
                        sw.WriteLine("{\"ConfigInfo\":" + json + "}");
                    }
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    break;
                case 1:
                    // configInfo = AssetDatabase.LoadAssetAtPath<TextAsset>(@"Assets/UIFramework/Resources/Language/JapaneseLauguageJSONConfig.json");
                    Debug.Log(json);
                    using (StreamWriter sw = new StreamWriter(@"Assets/UIFramework/Resources/Language/JapaneseLauguageJSONConfig.json"))
                    {
                        sw.WriteLine("{\"ConfigInfo\":" + json + "}");
                    }
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    break;
            }
        }
        //if (GUILayout.Button("百度翻译", GUI.skin.button))
        //{
        //    string json = LitJson.JsonMapper.ToJson(LanguageKey_Dic);
        //    foreach (var item in keyvalueInfoObj.ConfigInfo)
        //    {
        //        TranslationResult result = TranslateLanguageTool.GetTranslationFromBaiduFanyi(item.Value, Language.中文, Language.英文, "英文");
        //        for (int i = 0; i < result.trans_result.Length; i++)
        //        {
        //            string str = "语言 : " + @"  {""Key"":" + item.Key + @",""Value"":" + @"""" + result.trans_result[i].dst + @"""" + "}" + "\n";
        //            Debug.Log(str);
        //        }
        //    }
        //}


        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField(new GUIContent("   定位 : ", "定位"), titleStyle2, GUILayout.Width(40f));
        //searchIndexValue = EditorGUILayout.TextField("", searchIndexValue, "SearchTextField", GUILayout.Width(position.width - 171), GUILayout.Height(EditorGUIUtility.singleLineHeight));
        //if (GUILayout.Button("", "SearchCancelButton", GUILayout.Width(18f)))
        //{
        //    searchIndexValue = "";
        //    GUIUtility.keyboardControl = 0;
        //}
        //bool DingWei = GUILayout.Button("定位", GUILayout.Width(98f), GUILayout.Height(EditorGUIUtility.singleLineHeight));
        //if (DingWei)
        //{
        //    if (string.IsNullOrEmpty(searchIndexValue.Trim()))
        //    {
        //        EditorUtility.DisplayDialog("警告", "搜索关键字是空", "yes");
        //        Debug.Log("搜索关键字是空");
        //    }
        //    else
        //    {

        //        m_DeviceModelTablePosition = new Vector2(m_DeviceModelTablePosition.x, 20 * (int.Parse(searchIndexValue) - 1));
        //    }
        //}
        //EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField(new GUIContent("   搜索 : ", "搜索"), titleStyle2, GUILayout.Width(40f));
        string searchNewValue = EditorGUILayout.TextField("", searchValue, "SearchTextField", GUILayout.Width(position.width - 25), GUILayout.Height(EditorGUIUtility.singleLineHeight));
        if (!string.IsNullOrEmpty(searchValue.Trim()))
        {
            LanguageKey_Dic = LanguageKey_Dic.Where((user) => { return user.Key.ToUpper().Contains(searchValue.ToUpper()) || user.Value.ToUpper().Contains(searchValue.ToUpper()); }).ToList();
            LanguageKey_Dic.ForEach(item => cacheLanguageKey_Dic.Add(item));
        }


        if (searchValue != searchNewValue)
        {
            searchValue = searchNewValue;
            LanguageKey_Dic.Clear();
            index = 0;
            foreach (var item in keyvalueInfoObj.ConfigInfo)
            {
                KeyValuesNode keyValuesNode = new KeyValuesNode();
                keyValuesNode.Index = index += 1;
                keyValuesNode.Key = item.Key;
                keyValuesNode.Value = item.Value;
                LanguageKey_Dic.Add(keyValuesNode);
                foreach (var newItem in cacheLanguageKey_Dic)
                {
                    if (newItem.Index == keyValuesNode.Index)
                    {
                        LanguageKey_Dic.RemoveAt(LanguageKey_Dic.Count - 1);
                        LanguageKey_Dic.Add(newItem);
                        break;
                    }
                }

            }
        }
        //EditorGUILayout.Space(10);
        if (GUILayout.Button("", "SearchCancelButton", GUILayout.Width(25f)))
        {
            if (!string.IsNullOrEmpty(searchValue.Trim()))
            {
                searchValue = "";
                LanguageKey_Dic.Clear();
                index = 0;
                foreach (var item in keyvalueInfoObj.ConfigInfo)
                {
                    KeyValuesNode keyValuesNode = new KeyValuesNode();
                    keyValuesNode.Index = index += 1;
                    keyValuesNode.Key = item.Key;
                    keyValuesNode.Value = item.Value;
                    LanguageKey_Dic.Add(keyValuesNode);
                    foreach (var newItem in cacheLanguageKey_Dic)
                    {
                        if (newItem.Index == keyValuesNode.Index)
                        {
                            LanguageKey_Dic.RemoveAt(LanguageKey_Dic.Count - 1);
                            LanguageKey_Dic.Add(newItem);
                            break;
                        }
                    }
                }

            }
            GUIUtility.keyboardControl = 0;
        }
        //bool search = GUILayout.Button("搜索", GUILayout.Width(47), GUILayout.Height(EditorGUIUtility.singleLineHeight));
        //   bool Refresh = GUILayout.Button("刷新", GUILayout.Width(98), GUILayout.Height(EditorGUIUtility.singleLineHeight));
        //if (search)
        //{
        //    if (string.IsNullOrEmpty(searchValue.Trim()))
        //    {
        //        EditorUtility.DisplayDialog("警告", "搜索关键字是空", "yes");
        //        Debug.Log("搜索关键字是空");
        //    }
        //    else
        //    {

        //        // LanguageKey_Dic
        //        //Selection.activeObject = EditorUtility.GetPrefabParent()
        //        LanguageKey_Dic = LanguageKey_Dic.Where((user) => { return user.Key.ToUpper().Contains(searchValue.ToUpper()) || user.Value.ToUpper().Contains(searchValue.ToUpper()); }).ToList();
        //        LanguageKey_Dic.ForEach(item => cacheLanguageKey_Dic.Add(item));
        //        Debug.Log(cacheLanguageKey_Dic.Count);
        //        Debug.Log(searchValue);
        //    }
        //}
        //if (Refresh)
        //{
        //    int index = 0;
        //    int cache = 0;
        //    searchValue = "";
        //    Debug.Log(cacheLanguageKey_Dic.Count);
        //    switch (select)
        //    {
        //        case 0:
        //            configInfo = AssetDatabase.LoadAssetAtPath<TextAsset>(ChinaConfigPath);
        //            keyvalueInfoObj = LitJson.JsonMapper.ToObject<KeyValuesInfo>(configInfo.text);
        //            names = System.Enum.GetNames(typeof(LauguageType));

        //            LanguageKey_Dic.Clear();
        //            foreach (var item in keyvalueInfoObj.ConfigInfo)
        //            {
        //                KeyValuesNode keyValuesNode = new KeyValuesNode();
        //                keyValuesNode.Key = item.Key;
        //                keyValuesNode.Value = item.Value;
        //                if (cacheLanguageKey_Dic.Count > 0)
        //                {
        //                    if (cacheLanguageKey_Dic[cache].Index == index + 1)
        //                    {
        //                        Debug.Log($"{ cacheLanguageKey_Dic[cache].Value} { cacheLanguageKey_Dic[cache].Key}");
        //                        keyValuesNode.Key = cacheLanguageKey_Dic[cache].Key;
        //                        keyValuesNode.Value = cacheLanguageKey_Dic[cache].Value;
        //                        cache += 1;
        //                        if (cache >= cacheLanguageKey_Dic.Count)
        //                        {
        //                            cache = cacheLanguageKey_Dic.Count - 1;
        //                        }
        //                    }
        //                }
        //                keyValuesNode.Index = index += 1;

        //                LanguageKey_Dic.Add(keyValuesNode);
        //            }
        //            cacheLanguageKey_Dic.Clear();
        //            break;
        //        case 1:
        //            configInfo = AssetDatabase.LoadAssetAtPath<TextAsset>(JapanConfigPath);
        //            keyvalueInfoObj = LitJson.JsonMapper.ToObject<KeyValuesInfo>(configInfo.text);
        //            names = System.Enum.GetNames(typeof(LauguageType));
        //            LanguageKey_Dic.Clear();
        //            foreach (var item in keyvalueInfoObj.ConfigInfo)
        //            {
        //                KeyValuesNode keyValuesNode = new KeyValuesNode();
        //                keyValuesNode.Index = index += 1;
        //                keyValuesNode.Key = item.Key;
        //                keyValuesNode.Value = item.Value;
        //                LanguageKey_Dic.Add(keyValuesNode);
        //            }
        //            break;
        //    }
        //}
        EditorGUILayout.EndHorizontal();

        OnDeviceModelGUI();
        m_DeviceModelTablePosition = EditorGUILayout.BeginScrollView(m_DeviceModelTablePosition, GUILayout.Width(position.width));

        for (int i = 0; i < LanguageKey_Dic.Count; i++)
        {

            if (!string.IsNullOrEmpty(searchValue.Trim()))
            {
                InspectorTools.BeginVertical(new Color(140f / 255f, 255f / 255f, 0 / 255f));
            }
            else
            {
                InspectorTools.BeginVertical();
            }

            EditorGUILayout.BeginHorizontal();
            // bool moveMe = GUILayout.Button("上移", GUILayout.Width(40f), GUILayout.Height(EditorGUIUtility.singleLineHeight));
            //EditorGUILayout.LabelField(new GUIContent("ID :", "下标"), titleStyle2, GUILayout.Width(10));
            EditorGUILayout.LabelField(new GUIContent(LanguageKey_Dic[i].Index + "  :", "下标"), titleStyle2, GUILayout.Width(30f));

            DrawKeyItme(LanguageKey_Dic[i], "Key :", LanguageKey_Dic, 35f);
            DrawValueItem(LanguageKey_Dic[i], " Value :", LanguageKey_Dic, 50f);
            //GUI.backgroundColor = Color.yellow;
            bool deleteMe = GUILayout.Button(new GUIContent("", EditorGUIUtility.IconContent("winbtn_mac_close_h").image), EditorStyles.miniButton, GUILayout.Width(40f), GUILayout.Height(EditorGUIUtility.singleLineHeight));
            //GUI.backgroundColor = Color.white;
            if (deleteMe)
            {
                deleteIndex = i;
            }
            //if (moveMe)
            //{
            //    moveIndex = i;
            //}
            EditorGUILayout.EndHorizontal();
            InspectorTools.EndVertical();
            GUILayout.Space(0);
        }
        if (deleteIndex >= 0)
        {
            if (EditorUtility.DisplayDialog("标题", "是否删除", "确认", "取消"))
            {
                Debug.LogFormat("即将删除的 :Key :{0} Value :{0}", LanguageKey_Dic[deleteIndex].Key, LanguageKey_Dic[deleteIndex].Value);
                LanguageKey_Dic.RemoveAt(deleteIndex);
                string json = LitJson.JsonMapper.ToJson(LanguageKey_Dic);
                Debug.Log(json);
                switch (select)
                {
                    case 0:
                        using (StreamWriter sw = new StreamWriter(@"Assets/UIFramework/Resources/Language/ChineseLauguageJSONConfig.json"))
                        {
                            sw.WriteLine("{\"ConfigInfo\":" + json + "}");
                        }
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        break;
                    case 1:
                        using (StreamWriter sw = new StreamWriter(@"Assets/UIFramework/Resources/Language/JapaneseLauguageJSONConfig.json"))
                        {
                            sw.WriteLine("{\"ConfigInfo\":" + json + "}");
                        }
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        break;
                }
                deleteIndex = -1;
            }
        }
        //if (moveIndex > 0)
        //{
        //    var tempitem = LanguageKey_Dic[moveIndex];
        //    LanguageKey_Dic[moveIndex] = LanguageKey_Dic[moveIndex - 1];
        //    LanguageKey_Dic[moveIndex - 1] = tempitem;
        //    string json = LitJson.JsonMapper.ToJson(LanguageKey_Dic);
        //    Debug.Log(json);
        //    switch (select)
        //    {
        //        case 0:
        //            using (StreamWriter sw = new StreamWriter(@"Assets/UIFramework/Resources/Language/ChineseLauguageJSONConfig.json"))
        //            {
        //                sw.WriteLine("{\"ConfigInfo\":" + json + "}");
        //            }
        //            AssetDatabase.SaveAssets();
        //            AssetDatabase.Refresh();
        //            break;
        //        case 1:
        //            using (StreamWriter sw = new StreamWriter(@"Assets/UIFramework/Resources/Language/JapaneseLauguageJSONConfig.json"))
        //            {
        //                sw.WriteLine("{\"ConfigInfo\":" + json + "}");
        //            }
        //            AssetDatabase.SaveAssets();
        //            AssetDatabase.Refresh();
        //            break;
        //    }
        //    moveIndex = -1;
        //}

        if (string.IsNullOrEmpty(searchValue.Trim()))
        {
            if (GUILayout.Button(new GUIContent("添加", EditorGUIUtility.IconContent("d_winbtn_mac_max_h").image), GUI.skin.button))
            {
                KeyValuesNode keyValuesNode = new KeyValuesNode();
                keyValuesNode.Index = LanguageKey_Dic.Count + 1;
                keyValuesNode.Key = "";
                keyValuesNode.Value = "";
                LanguageKey_Dic.Add(keyValuesNode);
                m_DeviceModelTablePosition = new Vector2(m_DeviceModelTablePosition.x, m_DeviceModelTablePosition.y + 20);
                //configInfo = AssetDatabase.LoadAssetAtPath<TextAsset>(@"Assets/UIFramework/Resources/Language/ChineseLauguageJSONConfig.json");
                string json = LitJson.JsonMapper.ToJson(LanguageKey_Dic);
                m_DeviceModelTablePosition = new Vector2(m_DeviceModelTablePosition.x, LanguageKey_Dic.Count * 14);
                switch (select)
                {
                    case 0:
                        using (StreamWriter sw = new StreamWriter(ChinaConfigPath))
                        {
                            sw.WriteLine("{\"ConfigInfo\":" + json + "}");
                        }
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        break;
                    case 1:
                        using (StreamWriter sw = new StreamWriter(JapanConfigPath))
                        {
                            sw.WriteLine("{\"ConfigInfo\":" + json + "}");
                        }
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        break;
                }
            }
        }

        EditorGUILayout.EndScrollView();

        // EditorGUILayout.BeginHorizontal();
        //GUILayout.Label(new GUIContent("配置文件路径 :"), GUI.skin.box, GUILayout.Width(85));
        if (GUILayout.Button(new GUIContent("配置文件路径 : " + configDataEntityPath, configDataEntityPath), EditorStyles.linkLabel, GUILayout.Width(position.width - 190)))
        {
            // EditorUtility.OpenWithDefaultApp(configDataEntityPath);
            AddFileHead.InvokeCmd(AddFileHead.NotePadJJ_APP_NAME, configDataEntityPath);
        }
        // EditorGUILayout.EndHorizontal();


        GUILayout.BeginArea(new Rect(position.width - 75, position.height - 19, 200, 100));
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(" ▲ ", EditorStyles.miniButton, GUILayout.Width(30), GUILayout.Height(20)))
        {
            m_DeviceModelTablePosition = new Vector2(m_DeviceModelTablePosition.x, 0);
        }
        if (GUILayout.Button(" ▼ ", EditorStyles.miniButton, GUILayout.Width(30), GUILayout.Height(20)))
        {
            m_DeviceModelTablePosition = new Vector2(m_DeviceModelTablePosition.x, LanguageKey_Dic.Count * 14);
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.EndArea();
    }


    public static IEnumerator Process(string targetLang, string sourceText, System.Action<string> result)
    {
        yield return Process("auto", targetLang, sourceText, result);
    }


    public static IEnumerator Process(string sourceLang, string targetLang, string sourceText, System.Action<string> result)
    {
        string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
            + sourceLang + "&tl=" + targetLang + "&dt=t&q=" + WWW.EscapeURL(sourceText);

        WWW www = new WWW(url);
        yield return www;

        if (www.isDone)
        {
            if (string.IsNullOrEmpty(www.error))
            {
                LitJson.JsonData N = LitJson.JsonMapper.ToObject(www.text);
                string translatedText = N[0][0][0].ToString();

                result(translatedText);
            }
        }
    }


    [Serializable]
    internal class KeyValuesInfo
    {
        //配置信息
        public List<KeyValuesNode> ConfigInfo = null;
    }

    [Serializable]
    internal class KeyValuesNode
    {
        //下标
        public int Index = 0;
        //键
        public string Key = null;
        //值
        public string Value = null;
    }
}