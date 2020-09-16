using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SignalObjectManager : MonoBehaviour
{
    public static SignalObjectManager Instance;
    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject Spawn(string name)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            if (child.name == name)
            {
                //child.gameObject.SetActive(true);
                return child.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="obj"></param>
    public void Despawn(Transform obj)
    {
        obj.GetComponent<RectTransform>().SetParent(this.gameObject.transform);
        //obj.gameObject.SetActive(false);
    }
}
