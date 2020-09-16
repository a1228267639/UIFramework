using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private static GameObject m_UIManagerGo;
    private static UIStackManager s_UIStackManager; //UI栈管理器

    private static EventSystem s_EventSystem;

    static public Dictionary<string, List<BasePanel>> s_UIs = new Dictionary<string, List<BasePanel>>(); //打开的UI
    static public Dictionary<string, List<BasePanel>> s_hideUIs = new Dictionary<string, List<BasePanel>>(); //隐藏的UI


    public static UIStackManager UIStackManager
    {
        get
        {
            if (s_UIStackManager == null)
            {
                Init();
            }
            return s_UIStackManager;
        }

        set
        {

            s_UIStackManager = value;
        }
    }
    public static GameObject UIManagerGo
    {
        get
        {
            if (m_UIManagerGo == null)
            {
                Init();
            }
            return m_UIManagerGo;
        }

        set
        {
            m_UIManagerGo = value;
        }
    }

    public static EventSystem EventSystem
    {
        get
        {
            if (s_EventSystem == null)
            {
                Init();
            }
            return s_EventSystem;
        }

        set
        {
            s_EventSystem = value;
        }
    }


    public static void Init()
    {
        GameObject instance = GameObject.Find("UIManager");
        if (instance == null)
        {
            instance = Resources.Load<GameObject>("UI/UIManager");
        }
        UIManagerGo = instance;

        if (Application.isPlaying)
        {
            DontDestroyOnLoad(instance);
        }
    }

    /// <summary>
    /// 设置事件系统激活
    /// </summary>
    /// <param name="enable"></param>
    public static void SetEventSystemEnable(bool enable)
    {
        if (EventSystem != null)
        {
            EventSystem.enabled = enable;
        }
        else
        {
            Debug.LogError("EventSystem.current is null !");
        }
    }


    /// <summary>
    /// 创建UI,如果不打开则存放在Hide列表中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T CreateUIPage<T>() where T : BasePanel
    {
        return (T)CreateUIPage(typeof(T).Name);
    }





    public static BasePanel CreateUIPage(string UIName)
    {
        Debug.Log("CreateUIWindow " + UIName);

        // GameObject UItmp = GameObjectManager.CreateGameObjectByPool(UIName, UIManagerGo);
        GameObject UItmp = SignalObjectManager.Instance.Spawn(UIName);
        BasePanel UIBase = UItmp.GetComponent<BasePanel>();
        UISystemEvent.Dispatch(UIBase, UIEvent.OnInit);  //派发OnInit事件

        UIBase.pageStatus = BasePanel.PageStatus.Create;

        try
        {
            UIBase.ExcuteInit();
        }
        catch (Exception e)
        {
            Debug.LogError(UIName + "OnInit Exception: " + e.ToString());
        }

        AddHideUI(UIBase);

        // UILayerManager.SetLayer(UIWIndowBase);      //设置层级

        return UIBase;
    }
    /// <summary>
    /// 打开UI
    /// </summary>
    /// <param name="UIName">UI名</param>
    /// <param name="callback">动画播放完毕回调</param>
    /// <param name="objs">回调传参</param>`
    /// <returns>返回打开的UI</returns>
    public static BasePanel OpenUIPage(string UIName, UICallBack callback = null, params object[] objs)
    {
        BasePanel UIbase = GetHideUIPage(UIName);

        if (UIbase == null)
        {
            UIbase = CreateUIPage(UIName);
        }

        RemoveUI(UIbase);
        AddUIPage(UIbase);

        UIStackManager.OnUIOpen(UIbase);
        //UILayerManager.SetLayer(UIbase);      //设置层级

        UIbase.pageStatus = BasePanel.PageStatus.OpenAnim;

        UISystemEvent.Dispatch(UIbase, UIEvent.OnOpen);  //派发OnOpen事件
        try
        {
            UIbase.OnOpen();
        }
        catch (Exception e)
        {
            Debug.LogError(UIName + " OnOpen Exception: " + e.ToString());
        }

        UISystemEvent.Dispatch(UIbase, UIEvent.OnOpened);  //派发OnOpened事件

        //UIAnimManager.StartEnterAnim(UIbase, callback, objs); //播放动画
        return UIbase;
    }


    public static void DestroyUI(BasePanel UI)
    {
        Debug.Log("UIManager DestroyUI " + UI.name);

        if (GetIsExitsHide(UI))
        {
            RemoveUI(UI);
        }
        else if (GetIsExits(UI))
        {
            RemoveUIPage(UI);
        }

        UISystemEvent.Dispatch(UI, UIEvent.OnDestroy);  //派发OnDestroy事件
        try
        {
            //UI.Dispose();
        }
        catch (Exception e)
        {
            Debug.LogError("OnDestroy :" + e.ToString());
        }
        // GameObjectManager.DestroyGameObjectByPool(UI.gameObject);
        SignalObjectManager.Instance.Despawn(UI.transform);
    }

    public static void DestroyAllUI()
    {
        DestroyAllActiveUI();
        DestroyAllHideUI();
    }

    public static void OpenUIAsync<T>(UICallBack callback, params object[] objs) where T : BasePanel
    {
        string UIName = typeof(T).Name;
        OpenUIAsync(UIName, callback, objs);
    }
    /// <summary>
    /// 异步加载UI
    /// </summary>
    /// <param name="UIName"></param>
    /// <param name="callback"></param>
    /// <param name="objs"></param>
    public static void OpenUIAsync(string UIName, UICallBack callback, params object[] objs)
    {
        //ResourceManager.LoadAsync(UIName, (resObject) =>
        //{
        //    OpenUIWindow(UIName, callback, objs);
        //});
    }
    /// <summary>
    /// 删除所有打开的UI
    /// </summary>
    public static void DestroyAllActiveUI()
    {
        foreach (List<BasePanel> uis in s_UIs.Values)
        {
            for (int i = 0; i < uis.Count; i++)
            {
                UISystemEvent.Dispatch(uis[i], UIEvent.OnDestroy);  //派发OnDestroy事件
                try
                {
                    //uis[i].Dispose();
                }
                catch (Exception e)
                {
                    Debug.LogError("OnDestroy :" + e.ToString());
                }
                //GameObjectManager.DestroyGameObjectByPool(uis[i].gameObject);
                SignalObjectManager.Instance.Despawn(uis[i].transform);
            }
        }

        s_UIs.Clear();
    }
    static bool GetIsExits(BasePanel UI)
    {
        if (!s_UIs.ContainsKey(UI.name))
        {
            return false;
        }
        else
        {
            return s_UIs[UI.name].Contains(UI);
        }
    }
    static void AddUIPage(BasePanel UI)
    {
        if (!s_UIs.ContainsKey(UI.name))
        {
            s_UIs.Add(UI.name, new List<BasePanel>());
        }

        s_UIs[UI.name].Add(UI);

        UI.Show();
    }
    static void RemoveUIPage(BasePanel UI)
    {
        if (UI == null)
        {
            throw new Exception("UIManager: RemoveUI error UI is null: !");
        }

        if (!s_UIs.ContainsKey(UI.name))
        {
            throw new Exception("UIManager: RemoveUI error dont find UI name: ->" + UI.name + "<-  " + UI);
        }

        if (!s_UIs[UI.name].Contains(UI))
        {
            throw new Exception("UIManager: RemoveUI error dont find UI: ->" + UI.name + "<-  " + UI);
        }
        else
        {
            s_UIs[UI.name].Remove(UI);
        }
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    /// <param name="UI">目标UI</param>
    /// <param name="isPlayAnim">是否播放关闭动画</param>
    /// <param name="callback">动画播放完毕回调</param>
    /// <param name="objs">回调传参</param>
    public static void CloseUIWindow(BasePanel UI, bool isPlayAnim = true, UICallBack callback = null, params object[] objs)
    {
        RemoveUI(UI);        //移除UI引用
       // UI.RemoveAllListener();

        if (isPlayAnim)
        {
            //动画播放完毕删除UI
            if (callback != null)
            {
                callback += CloseUIWindowCallBack;
            }
            else
            {
                callback = CloseUIWindowCallBack;
            }
            UI.pageStatus = BasePanel.PageStatus.CloseAnim;
            //UIAnimManager.StartExitAnim(UI, callback, objs);
        }
        else
        {
            CloseUIWindowCallBack(UI, objs);
        }
    }

    static void CloseUIWindowCallBack(BasePanel UI, params object[] objs)
    {
        UI.pageStatus = BasePanel.PageStatus.Close;
        UISystemEvent.Dispatch(UI, UIEvent.OnClose);  //派发OnClose事件
        try
        {
            UI.OnClose();
        }
        catch (Exception e)
        {
            Debug.LogError(UI.name + " OnClose Exception: " + e.ToString());
        }

        UIStackManager.OnUIClose(UI);
        AddHideUI(UI);

        UISystemEvent.Dispatch(UI, UIEvent.OnClosed);  //派发OnClosed事件
    }

    #region  隐藏UI列表的管理 

    public static void CloseLastUI(UIType uiType = UIType.Normal)
    {
        UIStackManager.CloseLastUIWindow(uiType);
    }

    /// <summary>
    /// 删除所有隐藏的UI
    /// </summary>
    public static void DestroyAllHideUI()
    {
        foreach (List<BasePanel> uis in s_hideUIs.Values)
        {
            for (int i = 0; i < uis.Count; i++)
            {
                UISystemEvent.Dispatch(uis[i], UIEvent.OnDestroy);  //派发OnDestroy事件
                try
                {
                    // uis[i].Dispose();
                }
                catch (Exception e)
                {
                    Debug.LogError("OnDestroy :" + e.ToString());
                }
                SignalObjectManager.Instance.Despawn(uis[i].transform);
                // GameObjectManager.DestroyGameObjectByPool(uis[i].gameObject);
            }
        }

        s_hideUIs.Clear();
    }



    public static T GetHideUIPage<T>() where T : BasePanel
    {
        string UIname = typeof(T).Name;
        return (T)GetHideUIPage(UIname);
    }
    /// <summary>
    /// 获取一个隐藏的UI,如果有多个同名UI，则返回最后创建的那一个
    /// </summary>
    /// <param name="UIname">UI名</param>
    /// <returns></returns>
    public static BasePanel GetHideUIPage(string UIname)
    {
        if (!s_hideUIs.ContainsKey(UIname))
        {
            return null;
        }
        else
        {
            if (s_hideUIs[UIname].Count == 0)
            {
                return null;
            }
            else
            {
                BasePanel ui = s_hideUIs[UIname][s_hideUIs[UIname].Count - 1];
                //默认返回最后创建的那一个
                return ui;
            }
        }
    }
    static bool GetIsExitsHide(BasePanel UI)
    {
        if (!s_hideUIs.ContainsKey(UI.name))
        {
            return false;
        }
        else
        {
            return s_hideUIs[UI.name].Contains(UI);
        }
    }

    static void AddHideUI(BasePanel UI)
    {
        if (!s_hideUIs.ContainsKey(UI.name))
        {
            s_hideUIs.Add(UI.name, new List<BasePanel>());
        }

        s_hideUIs[UI.name].Add(UI);

        UI.Hide();
    }

    static void RemoveUI(BasePanel UI)
    {
        if (UI == null)
        {
            throw new Exception("UIManager: RemoveUI error l_UI is null: !");
        }

        if (!s_hideUIs.ContainsKey(UI.name))
        {
            throw new Exception("UIManager: RemoveUI error dont find: " + UI.name + "  " + UI);
        }

        if (!s_hideUIs[UI.name].Contains(UI))
        {
            throw new Exception("UIManager: RemoveUI error dont find: " + UI.name + "  " + UI);
        }
        else
        {
            s_hideUIs[UI.name].Remove(UI);
        }
    }
    #endregion
}

/// <summary>
/// UI回调
/// </summary>
/// <param name="objs"></param>
public delegate void UICallBack(BasePanel UIBase, params object[] objs);
public delegate void UIAnimCallBack(BasePanel UIbase, UICallBack callBack, params object[] objs);

public enum UIType
{
    GameUI = 0,

    Fixed = 1,
    Normal = 2,
    TopBar = 3,
    Upper = 4,
    PopUp = 5,
}

public enum UIEvent
{
    OnOpen,
    OnOpened,

    OnClose,
    OnClosed,

    OnHide,
    OnShow,

    OnInit,
    OnDestroy,

    OnRefresh,

    OnStartEnterAnim,
    OnCompleteEnterAnim,

    OnStartExitAnim,
    OnCompleteExitAnim,
}