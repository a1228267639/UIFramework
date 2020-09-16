/*文件说明
 * CreateTime:  2019/01/15/10:59:51
 * Projectname:  XHDemo
 * FileName:  RemoveScript.cs
 * Developers:  新斛信息
 * Creator:  杨智杰
 * Description:
*/


using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RemoveScript : Editor
{

    [MenuItem("Tools/Delete Missing Scripts")]
    static void CleanupMissingScript()
    {
        GameObject[] pAllObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));

        int r;
        int j;
        for (int i = 0; i < pAllObjects.Length; i++)
        {
            if (pAllObjects[i].hideFlags == HideFlags.None)//HideFlags.None 获取Hierarchy面板所有Object
            {
                var components = pAllObjects[i].GetComponents<Component>();
                var serializedObject = new SerializedObject(pAllObjects[i]);
                var prop = serializedObject.FindProperty("m_Component");
                r = 0;

                for (j = 0; j < components.Length; j++)
                {
                    if (components[j] == null)
                    {
                        prop.DeleteArrayElementAtIndex(j - r);
                        r++;
                    }
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}