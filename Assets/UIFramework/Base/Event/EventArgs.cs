/*文件说明
 * CreateTime:  2019/06/25/16:18:58
 * Projectname:  CYGL_Platform
 * FileName:  EventArg.cs
 * Developers:  五峰杰
 * Creator:  杨智杰
 * Description:
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EventArgs
{

    /// <summary> 事件类型 </summary>
    public readonly EventType type;
    /// <summary> 事件参数 </summary>
    public readonly object[] args;


    /// <param name="type">事件类型 </param>
    /// <param name="args">事件参数</param>
    public EventArgs(EventType type, params object[] args)
    {
        this.type = type;
        this.args = args;
    }
}
