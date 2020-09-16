/*文件说明
 * CreateTime:  2019/06/25/16:21:13
 * Projectname:  CYGL_Platform
 * FileName:  EventSender.cs
 * Developers:  五峰杰
 * Creator:  杨智杰
 * Description:
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSender
{

    /// <summary> 事件字典 </summary>
    private Dictionary<EventType, EventListener> dic = new Dictionary<EventType, EventListener>();

    /// <summary> 添加事件监听器 </summary>
    /// <param name="eventType">事件类型</param>
    /// <param name="eventHandler">事件处理器</param>
    public void Add(EventType eventType, EventHandler<EventArgs> eventHandler)
    {
        EventListener invoker;
        if (!dic.TryGetValue(eventType, out invoker))
        {
            invoker = new EventListener();
            dic.Add(eventType, invoker);
        }
        invoker.handler += eventHandler;
    }

    /// <summary> 移除事件监听器 </summary>
    /// <param name="eventType">事件类型</param>
    /// <param name="eventHandler">事件处理器</param>
    public void Remove(EventType eventType, EventHandler<EventArgs> eventHandler)
    {
        EventListener invoker;
        if (dic.TryGetValue(eventType, out invoker)) invoker.handler -= eventHandler;
    }

    /// <summary> 是否已经拥有该类型的事件 </summary>
    /// <param name="eventType">事件名称</param>
    public bool Has(EventType eventType)
    {
        return dic.ContainsKey(eventType);
    }

    /// <summary> 发送事件 </summary>
    /// <param name="eventType">事件类型</param>
    public void Send(EventType eventType, params object[] eventArgs)
    {
        EventListener invoker;
        if (dic.TryGetValue(eventType, out invoker))
        {
            EventArgs evt = new EventArgs(eventType, eventArgs);
            invoker.Invoke(evt);
        }
    }

    /// <summary> 清理所有事件监听器 </summary>
    public void Clear()
    {
        foreach (EventListener value in dic.Values)
        {
            value.Clear();
        }
        dic.Clear();
    }

}
