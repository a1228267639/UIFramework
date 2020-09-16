using UnityEngine;
using UnityEditor;
using System.Collections;


public class ScriptTemplate : EditorWindow
{

    static ScriptTemplate window;
    [MenuItem("Tools/CreateConfig")]
    static void CreateTestWindows()
    {
        window = GetWindow<ScriptTemplate>(true, "模板脚本配置");
        window.minSize = new Vector2(200, 100);
        window.maxSize = new Vector2(200, 100);
        _companyValue = PlayerPrefs.GetString("COMPANY", "");
        _developerValue = PlayerPrefs.GetString("DEVELOPER", "");
    }
    GUIStyle titleStyle2 = new GUIStyle();
    GUIStyle titleStyle1 = new GUIStyle();
    private Vector2 m_DeviceModelTablePosition = Vector2.zero;
    static string _companyValue = string.Empty;
    static string _developerValue = string.Empty;
    string cur_Value;
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 20, 200, 100), GUIContent.none);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("公司:", titleStyle2, GUILayout.Width(50f));
        cur_Value = EditorGUILayout.TextField(_companyValue, GUILayout.Width(100f));
        _companyValue = cur_Value;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("开发者:", titleStyle2, GUILayout.Width(50f));
        cur_Value = EditorGUILayout.TextField(_developerValue, GUILayout.Width(100f));
        _developerValue = cur_Value;
        EditorGUILayout.EndHorizontal();

        bool OnClick = GUILayout.Button("保存", GUILayout.Width(40f), GUILayout.Height(40f), GUILayout.MaxHeight(30), GUILayout.MaxWidth(155));
        if (OnClick)
        {
            PlayerPrefs.SetString("COMPANY", _companyValue);
            PlayerPrefs.SetString("DEVELOPER", _developerValue);
            HEScriptKeywordReplace._company = _companyValue;
            HEScriptKeywordReplace._developer = _developerValue;
            PlayerPrefs.Save();
            window.Close();
        }
        GUILayout.EndArea();
    }

}

/// <summary>
/// 脚本模板赋值
/// </summary>
public class HEScriptKeywordReplace : UnityEditor.AssetModificationProcessor
{

    public static string _developer;
    public static string _company;

    public static void OnWillCreateAsset(string path)
    {
        _developer = PlayerPrefs.GetString("COMPANY", "杨智杰");
        _company = PlayerPrefs.GetString("DEVELOPER", "云笔");
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        string file = path.Substring(index);
        if (file != ".cs" && file != ".js" && file != ".boo") return;
        string fileExtension = file;

        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        file = System.IO.File.ReadAllText(path);

        file = file.Replace("#CREATIONDATE#", System.DateTime.Now.ToString("yyyy/MM/dd/HH:mm:ss"));
        file = file.Replace("#PROJECTNAME#", PlayerSettings.productName);
        file = file.Replace("#FILEEXTENSION#", fileExtension);
        file = file.Replace("#DEVELOPER#", _developer);
        file = file.Replace("#DEVELOPERS#", _company);

        System.IO.File.WriteAllText(path, file);
        AssetDatabase.Refresh();
    }
}