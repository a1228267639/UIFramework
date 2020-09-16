using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class UIElement
{
    public string Name;
    public string Path;
    public string ComponentName;
    public UIElement(string name, string path, string componentName)
    {
        Name = name;
        Path = path;
        ComponentName = componentName;
    }

    public override string ToString()
    {
        string str = string.Format("Name={0} || Path={1} || ComponentName={2}", Name, Path, ComponentName);
        return str;
    }
}

public class UICodeGenerator
{
    [MenuItem("Assets/创建代码删除Mark组件")]
    public static void CreateCodeDeleteComponent()
    {
        GetPath(true);
    }

    [MenuItem("Assets/只创建代码")]
    public static void OnlyCreateCode()
    {
        GetPath(false);
    }

    public static void GetPath(bool isDeleteComponent)
    {
        var objs =
            Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets | SelectionMode.TopLevel);
        GameObject obj = objs[0] as GameObject;
        elements = new List<UIElement>();
        if (obj.transform.gameObject.GetComponent<UIMark>())
        {
            elements.Add(new UIElement(obj.transform.name, "",
                                           obj.transform.gameObject.GetComponent<UIMark>().ComponentName));
            if (isDeleteComponent)
                GameObject.DestroyImmediate(obj.transform.gameObject.GetComponent<UIMark>(), true);
        }
        GetPathAs(obj.transform, isDeleteComponent);

        foreach (var item in elements)
        {
            Debug.Log(item);
        }

        GeneratePane("Assets/" + obj.name + "_Panel.cs", obj.name + "_Panel", elements);
        //GenerateCtrl("Assets/" + obj.name + "Ctrl.cs", obj.name, elements);

    }

    public static List<UIElement> elements;

    static void GetPathAs(Transform transform, bool isDeleteComponent)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<UIMark>())
            {
                elements.Add(new UIElement(child.name, GetPath(child.transform),
                                               child.gameObject.GetComponent<UIMark>().ComponentName));
                if (isDeleteComponent)
                    GameObject.DestroyImmediate(child.gameObject.GetComponent<UIMark>(), true);
            }

            if (child.childCount != 0)
            {
                GetPathAs(child, isDeleteComponent);
            }
        }
    }


    public static void GeneratePane(string generateFilePath, string behaviourName, List<UIElement> elements)
    {
        UTF8Encoding utf8 = new UTF8Encoding(); // Create a UTF-8 encoding. 

        using (StreamWriter sw = new StreamWriter(generateFilePath, false, utf8))
        {
            sw.WriteLine("using UnityEngine;");
            sw.WriteLine("using UnityEngine.UI;");
            sw.WriteLine("using Tools;");
            sw.WriteLine();
            sw.WriteLine("/" + "/" + "模板生成的UI类");
            sw.WriteLine("public class {0} : BasePanel ", behaviourName);
            sw.WriteLine("{");
            // sw.WriteLine("\tprivate CanvasGroup canvasGroup;");

            sw.WriteLine("\t    #region UI变量");
            foreach (var item in elements)
            {
                sw.WriteLine("\tpublic " + item.ComponentName + " " + item.Name + " { get; private set; }");
            }
            sw.WriteLine("\t    #endregion");

            sw.WriteLine("\tvoid Awake()");
            sw.WriteLine("\t{");
            foreach (var item in elements)
            {
                sw.WriteLine("\t\t{0} = transform.Find(\"{1}\").GetComponent<{2}>();", item.Name, item.Path.Replace(behaviourName.Replace("_Panel", "") + "/", ""), item.ComponentName);
                sw.WriteLine();
                Debug.Log(behaviourName);
                Debug.Log(item.Path);
            }
            sw.WriteLine("\t}");

            sw.WriteLine();
            sw.WriteLine("\tprivate void Event_Init()");
            sw.WriteLine("\t{");
            sw.WriteLine("\t}");

            sw.WriteLine("\tprivate void State_Init()");
            sw.WriteLine("\t{");
            sw.WriteLine("\t}");

            sw.WriteLine("\tprivate void Text_Init()");
            sw.WriteLine("\t{");
            sw.WriteLine("\t}");

            sw.WriteLine("\tpublic  void OnEnable()");
            sw.WriteLine("\t{");
            sw.WriteLine("\tText_Init();");
            sw.WriteLine("\tState_Init();");
            sw.WriteLine("\tEvent_Init();");
            sw.WriteLine("\t}");

            sw.WriteLine("\tpublic  void OnDisable()");
            sw.WriteLine("\t{");
            sw.WriteLine("\t}");

            sw.WriteLine("\tpublic override void OnEnter()");
            sw.WriteLine("\t{");
            sw.WriteLine("\t base.OnEnter();");
            sw.WriteLine("\t}");

            sw.WriteLine("\tpublic override void OnPause()");
            sw.WriteLine("\t{");
            sw.WriteLine("\t base.OnPause();");
            sw.WriteLine("\t}");

            sw.WriteLine("\tpublic override void OnResume()");
            sw.WriteLine("\t{");
            sw.WriteLine("\t base.OnResume();");
            sw.WriteLine("\t}");

            sw.WriteLine("\tpublic override void OnExit()");
            sw.WriteLine("\t{");
            sw.WriteLine("\t base.OnExit();");
            sw.WriteLine("\t}");

            sw.WriteLine("\t}");


            sw.WriteLine("\t/// <summary>");
            sw.WriteLine("\t///  模板Key");
            sw.WriteLine("\t /// <summary>");
            sw.WriteLine("public class {0}_Key ", behaviourName);
            sw.WriteLine("\t{");
            sw.WriteLine("\t     public const string Template = \"{0}_Template\";", behaviourName);
            sw.WriteLine("\t}");
            //sw.Write(strBuilder);
            sw.Flush();
            sw.Close();
            sw.Dispose();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

    }

    public static void GenerateCtrl(string generateFilePath, string behaviourName, List<UIElement> elements)
    {
        var sw = new StreamWriter(generateFilePath, false, Encoding.UTF8);
        var strBuilder = new StringBuilder();

        List<UIElement> temp = new List<UIElement>();

        foreach (UIElement element in elements)
        {
            if (element.ComponentName.Equals("Button"))
                temp.Add(element);
        }

        sw.WriteLine("using UnityEngine;");
        sw.WriteLine("using UnityEngine.UI;");
        sw.WriteLine();
        strBuilder.AppendFormat("public class {0}Ctrl : BaseCtrl ", behaviourName);
        sw.WriteLine("{");
        sw.WriteLine();
        strBuilder.AppendFormat("\tprivate {0} panel;", behaviourName);
        sw.WriteLine();
        sw.WriteLine();
        sw.WriteLine("\tpublic override void InitPanel()");
        sw.WriteLine("\t{");
        strBuilder.AppendFormat("\t\tpanel = GetComponent<{0}>();", behaviourName);
        sw.WriteLine();
        foreach (UIElement element in temp)
        {
            strBuilder.AppendFormat("\t\tpanel.{0}.AddListenerGracefully( {1}Click );", element.Name, element.Name);
            sw.WriteLine();
        }
        sw.WriteLine("\t}");
        sw.WriteLine();
        foreach (UIElement element in temp)
        {
            strBuilder.AppendFormat("\tvoid {0}Click()", element.Name);
            sw.WriteLine();
            sw.WriteLine("\t{");
            sw.WriteLine();
            sw.WriteLine("\t}");
            sw.WriteLine();
        }

        sw.WriteLine("}");
        sw.Write(strBuilder);
        sw.Flush();
        sw.Close();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static string GetPath(Transform transform)
    {
        var sb = new System.Text.StringBuilder();
        var t = transform;
        while (true)
        {
            sb.Insert(0, t.name);
            t = t.parent;
            if (t)
            {
                sb.Insert(0, "/");
            }
            else
            {
                return sb.ToString();
            }
        }
    }
}
