using UnityEditor;
using UnityEngine;

public static class InspectorTools
{

    public static void Title(string title)
    {
        GUILayout.Space(4);
        EditorGUIUtility.SetIconSize(new Vector2(20,20));
        //GUILayout.BeginHorizontal(EditorGUIUtility.IconContent("EditorSettings Icon"),new GUIStyle(GUI.skin.box), GUILayout.Height(1));
        GUILayout.Label(EditorGUIUtility.TrTextContent(title, "", "EditorSettings Icon"));
       // GUILayout.EndHorizontal();

        Color backgroundColor3 = GUI.backgroundColor;
        GUI.backgroundColor = Color.green;

        GUILayout.Space(-4);
        GUILayout.BeginHorizontal(new GUIStyle(GUI.skin.box), GUILayout.Height(1));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(-4);
        GUI.backgroundColor = backgroundColor3;
    }
    public static void BeginVertical(Color? color = null, GUIStyle style = null)
    {
        Color? backgroundColor = color == null ? Color.white : color;
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.box);
        }
        Color backgroundColor2 = GUI.backgroundColor;
        GUI.backgroundColor = (Color)backgroundColor;
        GUILayout.BeginVertical(style, (GUILayoutOption[])new GUILayoutOption[0]);
        GUI.backgroundColor = backgroundColor2;
    }
    public static void EndVertical()
    {
        GUILayout.EndVertical();
    }
}
