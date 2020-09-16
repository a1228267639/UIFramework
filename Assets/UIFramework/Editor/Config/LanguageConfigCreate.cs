using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LanguageTable", menuName = "PrefabPath/LanguageTable")]
public class LanguageConfigCreate : ScriptableObject
{
    public List<KeyValuesNode> KeyValues = new List<KeyValuesNode>();
}

[Serializable]
public class KeyValuesNode
{
    //��
    public string Key = null;
    //ֵ
    public string Value = null;
}