﻿using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

class MyWindow : EditorWindow
{
    static string[] text;
    [MenuItem("Window/My Window")]



    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MyWindow));
        text = Resources.Load<TextAsset>("t").text.Split("\n"[0]);
    }
    public Vector2 scrollPosition;


    void OnGUI()
    {

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        //鼠标放在按钮上的样式
        foreach (MouseCursor item in Enum.GetValues(typeof(MouseCursor)))
        {
            GUILayout.Button(Enum.GetName(typeof(MouseCursor), item));
            //SetIcon(inspectorObj.gameObject, (Texture2D));
            EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), item);
            GUILayout.Space(10);
        }


        //内置图标
        for (int i = 0; i < text.Length; i += 8)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < 8; j++)
            {
                int index = i + j;
                if (index < text.Length)
                {
                    if (GUILayout.Button(UnityEditor.EditorGUIUtility.FindTexture(text[index].Trim()), GUILayout.Width(50), GUILayout.Height(30)))
                    {
                        Debug.Log(text[index].Trim());
                    }
                }

            }
            GUILayout.EndHorizontal();
        }




        GUILayout.EndScrollView();
    }


}