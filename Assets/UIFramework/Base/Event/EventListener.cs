/*文件说明
 * CreateTime:  2019/06/25/16:20:02
 * Projectname:  CYGL_Platform
 * FileName:  EventListener.cs
 * Developers:  五峰杰
 * Creator:  杨智杰
 * Description:
*/


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 事件处理器委托 </summary>
public delegate void EventHandler<T>(T args);

public class EventListener
{
    /// <summary> 事件处理器集合 </summary>
    public EventHandler<EventArgs> handler;

    /// <summary> 调用所有添加的事件 </summary>
    public void Invoke(EventArgs args)
    {
        if (handler != null) handler.Invoke(args);
    }

    /// <summary> 清理所有事件委托 </summary>
    public void Clear()
    {
        handler = null;
    }
}
