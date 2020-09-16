using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectBase : MonoBehaviour
{

    public Dictionary<int, object> _Pars = new Dictionary<int, object>();
    /// <summary>
    /// 初始化
    /// </summary>
    protected virtual void Init()
    {

    }
    /// <summary>
    /// 冻结
    /// </summary>
    protected virtual void Free()
    {

    }
    /// <summary>
    /// 执行初始化
    /// </summary>
    public void ExcuteInit()
    {
        Init();
    }
    /// <summary>
    /// 执行冻结
    /// </summary>
    public void ExcuteFree()
    {
        Free();
        _Pars.Clear();
    }
    /// <summary>
    /// 添加字典内容
    /// </summary>
    /// <param name="key"></param>
    /// <param name="val"></param>
    public void SetPar(int key, object val)
    {
        _Pars.Add(key, val);
    }
    /// <summary>
    /// 获取内容
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T GetPar<T>(int key)
    {
        if (!_Pars.ContainsKey(key))
            return default(T);
        return (T)_Pars[key];
    }
}