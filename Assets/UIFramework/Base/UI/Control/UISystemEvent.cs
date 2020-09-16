using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UISystemEvent 
{
    /// <summary>
    /// 所有UI事件库
    /// </summary>
    public static Dictionary<UIEvent, UICallBack> s_allUIEvents = new Dictionary<UIEvent, UICallBack>();
    public static Dictionary<string, Dictionary<UIEvent, UICallBack>> s_singleUIEvents = new Dictionary<string, Dictionary<UIEvent, UICallBack>>();


    /// <summary>
    /// 每个UI都会派发的事件
    /// </summary>
    /// <param name="Event">事件类型</param>
    /// <param name="callback">回调函数</param>
    public static void RegisterAllUIEvent(UIEvent UIEvent, UICallBack CallBack)
    {
        if (s_allUIEvents.ContainsKey(UIEvent))
        {
            s_allUIEvents[UIEvent] += CallBack;
        }
        else
        {
            s_allUIEvents.Add(UIEvent, CallBack);
        }
    }

    /// <summary>
    /// 删除UI事件
    /// </summary>
    /// <param name="UIEvent"></param>
    /// <param name="l_CallBack"></param>
    public static void RemoveAllUIEvent(UIEvent UIEvent, UICallBack l_CallBack)
    {
        if (s_allUIEvents.ContainsKey(UIEvent))
        {
            s_allUIEvents[UIEvent] -= l_CallBack;
        }
        else
        {
            Debug.LogError("RemoveAllUIEvent don't exits: " + UIEvent);
        }
    }

    /// <summary>
    /// 注册单个UI派发的事件
    /// </summary>
    /// <param name="Event">事件类型</param>
    /// <param name="callback"回调函数></param>
    public static void RegisterEvent(string UIName, UIEvent UIEvent, UICallBack CallBack)
    {
        if (s_singleUIEvents.ContainsKey(UIName))
        {
            if (s_singleUIEvents[UIName].ContainsKey(UIEvent))
            {
                s_singleUIEvents[UIName][UIEvent] += CallBack;
            }
            else
            {
                s_singleUIEvents[UIName].Add(UIEvent, CallBack);
            }
        }
        else
        {
            s_singleUIEvents.Add(UIName, new Dictionary<UIEvent, UICallBack>());
            s_singleUIEvents[UIName].Add(UIEvent, CallBack);
        }
    }

    public static void RemoveEvent(string UIName, UIEvent UIEvent, UICallBack CallBack)
    {
        if (s_singleUIEvents.ContainsKey(UIName))
        {
            if (s_singleUIEvents[UIName].ContainsKey(UIEvent))
            {
                s_singleUIEvents[UIName][UIEvent] -= CallBack;
            }
            else
            {
                Debug.LogError("RemoveEvent 不存在的事件！ UIName " + UIName + " UIEvent " + UIEvent);
            }

        }
        else
        {
            Debug.LogError("RemoveEvent 不存在的事件！ UIName " + UIName + " UIEvent " + UIEvent);
        }
    }

    /// <summary>
    /// 派发事件
    /// </summary>
    /// <param name="UI"></param>
    /// <param name="UIEvent"></param>
    /// <param name="objs"></param>
    public static void Dispatch(BasePanel UI, UIEvent UIEvent, params object[] objs)
    {
        if (UI == null)
        {
            Debug.LogError("Dispatch UI is null!");
            return;
        }

        if (s_allUIEvents.ContainsKey(UIEvent))
        {
            try
            {
                s_allUIEvents[UIEvent]?.Invoke(UI, objs);
            }
            catch (Exception e)
            {
                Debug.LogError("UISystemEvent Dispatch error:" + e.ToString());
            }
        }

        if (s_singleUIEvents.ContainsKey(UI.name))
        {
            if (s_singleUIEvents[UI.name].ContainsKey(UIEvent))
            {
                try
                {
                    s_singleUIEvents[UI.name][UIEvent]?.Invoke(UI, objs);
                }
                catch (Exception e)
                {
                    Debug.LogError("UISystemEvent Dispatch error:" + e.ToString());
                }
            }
        }
    }
}