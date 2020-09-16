using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TableBase", menuName = "PrefabPath/TableBase")]
public class PrefabPath : ScriptableObject
{
    [Header("配置表")]
    public List<UIPanelInfo> ConfigInfo =new List<UIPanelInfo>(5);
}
