using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEditor;
using UnityEngine;


/// <summary>
/// Mono扩展
/// </summary>
public static class MonoBehaviourExtension
{

    public static Dictionary<string, IEnumerator> CorDic = new Dictionary<string, IEnumerator>();

    /// <summary>
    /// 停止一个携程
    /// </summary>
    /// <param name="selfMono">self Mono类</param>
    /// <param name="IEname">协程 key</param>
    /// <returns></returns>
    public static MonoBehaviour StopIE(this MonoBehaviour selfMono, string IEname)
    {

        if (CorDic.ContainsKey(IEname))
        {
            selfMono.StopCoroutine(CorDic[IEname]);
            CorDic.Remove(IEname);
            return selfMono;
        }
        else
        {
            return selfMono;
        }
    }

    /// <summary>
    /// 获取组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="monoBehaviour"></param>
    /// <returns></returns>
    public static T GetAddComponent<T>(this MonoBehaviour monoBehaviour) where T : Component
    {
        if (monoBehaviour.GetComponent<T>() != null)
        {
            return monoBehaviour.GetComponent<T>();
        }
        return monoBehaviour.gameObject.AddComponent<T>();
    }

    /// <summary>
    /// 开始一个协程 
    /// </summary>
    /// <param name="selfMono">self Mono类</param>
    /// <param name="IEname">协程 key</param>
    /// <param name="selfCor">协程逻辑</param>
    /// <returns></returns>
    public static MonoBehaviour StartIE(this MonoBehaviour selfMono, string IEname, IEnumerator selfCor)
    {
        if (!CorDic.ContainsKey(IEname))
        {
            CorDic.Add(IEname, selfCor);
            if (selfMono.gameObject.IsShow())
            {
                selfMono.StartCoroutine(selfCor);
                return selfMono;
            }
            else
            {
                Debug.Log(selfMono.gameObject.name + " 没有激活对象");
            }
            return selfMono;
        }
        else
        {
            selfMono.StopIE(IEname);
            selfMono.StartIE(IEname, selfCor);
            return selfMono;
        }
    }
    /// <summary>
    /// 清空全部的协调程序
    /// </summary>
    /// <param name="selfMono">self Mono类</param>
    /// <returns></returns>
    public static MonoBehaviour Clear(this MonoBehaviour selfMono)
    {
        if (CorDic.Count > 0)
        {
            CorDic.Clear();
        }
        return selfMono;
    }
    /// <summary>
    /// 获取RectTransform
    /// </summary>
    /// <param name="selfObj"></param>
    /// <returns></returns>
    public static RectTransform GetRectTransform(this MonoBehaviour selfObj)
    {
        return selfObj.GetComponent<RectTransform>();
    }
    /// <summary>
    /// 退出应用
    /// </summary>
    public static void QuitApp(this MonoBehaviour selfeMono)
    {
        selfeMono.StartIE("QuitApp", Tools.Tool.OnClickEvent(() =>
        {
            Tools.Tool.AndroidToast("再按一次退出");
            selfeMono.StartIE("QuitApp", Tools.Tool.OnClickEvent(() =>
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }, null, KeyCode.Escape, 0.01f));
            selfeMono.StartIE("WiatClose", Tools.Tool.WaitTimeAction(() =>
            {
                QuitApp(selfeMono);
            }, 3f));
        }, null, KeyCode.Escape, 0.01f));
    }


    public static void Log(this MonoBehaviour self, object msg)
    {
#if Log_OpenLog
        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
        Debug.Log($"<color=#9A0674>消息：<color=#003399>[{msg}]</color>  对象名 :<color=#336666>[{ self.gameObject.name}]</color>类名 :<color=#003399>[{self.GetType().Name}]</color> 行数  :<color=#663300>[{st.GetFrame(0).GetFileLineNumber()}]</color></color>");
#endif
    }
    public static void LogWarning(this MonoBehaviour self, object msg)
    {
#if Log_OpenLog
        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
        Debug.LogWarning($"<color=#9A0674>消息：<color=#003399>[{msg}]</color>  对象名 :<color=#336666>[{ self.gameObject.name}]</color>类名 :<color=#003399>[{self.GetType().Name}]</color> 行数  :<color=#663300>[{st.GetFrame(0).GetFileLineNumber()}]</color></color>");
#endif
    }
    public static void LogError(this MonoBehaviour self, object msg)
    {
#if Log_OpenLog
        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
        Debug.LogError($"<color=#9A0674>消息：<color=#003399>[{msg}]</color>  对象名 :<color=#336666>[{ self.gameObject.name}]</color>类名 :<color=#003399>[{self.GetType().Name}]</color> 行数  :<color=#663300>[{st.GetFrame(0).GetFileLineNumber()}]</color></color>");
#endif
    }
}