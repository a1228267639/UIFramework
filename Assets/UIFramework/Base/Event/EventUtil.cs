/*文件说明
 * CreateTime:  2019/06/25/16:22:21
 * Projectname:  CYGL_Platform
 * FileName:  EventUtil.cs
 * Developers:  五峰杰
 * Creator:  杨智杰
 * Description:
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件管理系统
/// </summary>
public static class EventUtil
{
    /// <summary> 事件发送器 </summary>
    private static EventSender sender = new EventSender();

    /// <summary>注册事件</summary>
    /// <param name="eventType">事件类型</param>
    /// <param name="eventHandler">事件处理器</param>
    public static void Add(EventType eventType, EventHandler<EventArgs> eventHandler)
    {
        sender.Add(eventType, eventHandler);
    }

    /// <summary> 移除事件监听器 </summary>
    /// <param name="eventType">事件类型</param>
    /// <param name="eventHandler">事件处理器</param>
    public static void Remove(EventType eventType, EventHandler<EventArgs> eventHandler)
    {
        sender.Remove(eventType, eventHandler);
    }

    /// <summary> 是否已经拥有该类型的事件 </summary>
    /// <param name="eventType">事件类型</param>
    public static bool Has(EventType eventType)
    {
        return sender.Has(eventType);
    }

    /// <summary> 发送事件 </summary>
    /// <param name="eventType">事件类型</param>
    public static void Send(EventType eventType, params object[] eventArgs)
    {
        sender.Send(eventType, eventArgs);
    }

    /// <summary> 清理所有事件监听器 </summary>
    public static void Clear()
    {
        sender.Clear();
    }
}
