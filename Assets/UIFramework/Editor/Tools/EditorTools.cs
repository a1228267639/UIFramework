using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateUIPaefabTools : Editor
{

    public static void CreateUIPaefab(string path)
    {
        Debug.Log(path);
        if (Selection.transforms.Length <= 0)
        {
            // GameObject obj = Resources.Load<GameObject>(path);
            GameObject obj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);
            GameObject newObj = GameObject.Instantiate(obj);
            newObj.transform.SetParent(null);
            newObj.name = obj.name;
            newObj.transform.SetAsLastSibling();
        }
        else
        {
            //实际的项目中 资源可能放在别的路径，根据项目自行选择加载方式和路径
            //GameObject obj = Resources.Load<GameObject>(path);
            GameObject obj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);
            //选中了几个就在这几个物体的子目录下创建相应的东西
            for (int i = 0; i < Selection.transforms.Length; i++)
            {
                GameObject newObj = GameObject.Instantiate(obj);
                newObj.transform.SetParent(Selection.transforms[i], false);
                newObj.name = obj.name;
            }
        }
    }
}
