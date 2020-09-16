using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 平台管理
/// </summary>
public static class UPlatform
{



    /// <summary>
    /// unity 移动端 常亮
    /// </summary>
    public static void NeverSleep()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
