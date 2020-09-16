using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class BasePanel : ObjectBase
{
    protected CanvasGroup canvasGroup;
    protected new string name;

    [HideInInspector]
    public PageStatus pageStatus;

    public UIType m_UIType;

    protected virtual void Awake()
    {
        name = GetType() + ">>";
        Debug.Log(name);
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }


    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnOpen()
    {
        SetPanelActive(true);
        SetPanelInteractable(true);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    /// <summary>
    /// 界面不显示,退出这个界面，界面被关闭
    /// </summary>
    public virtual void OnClose()
    {
        SetPanelInteractable(false);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnHide()
    {
        SetPanelInteractable(false);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    /// <summary>
    /// 重新显示
    /// </summary>
    public virtual void OnShow()
    {

    }
    /// <summary>
    /// 界面刷新
    /// </summary>
    public virtual void OnRefresh()
    {
        SetPanelInteractable(true);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }


    /// <summary>
    /// 设置隐藏
    /// </summary>
    /// <param name="isActive"></param>
    protected void SetPanelActive(bool isActive)
    {
        ///(true ^ false);  // 返回 true
        ///(false ^ false);  // 返回 false
        ///(true ^ true);  // 返回 false
        if (isActive ^ gameObject.activeSelf) gameObject.SetActive(isActive);
    }
    /// <summary>
    /// 设置激活
    /// </summary>
    /// <param name="isInteractable"></param>
    protected void SetPanelInteractable(bool isInteractable)
    {
        if (canvasGroup == null)
        {
            canvasGroup = this.GetComponent<CanvasGroup>();
        }
        if (isInteractable ^ canvasGroup.interactable) canvasGroup.interactable = isInteractable;
    }

    public void Hide()
    {
        SetPanelInteractable(false);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public void Show()
    {
        SetPanelInteractable(true);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual IEnumerator EnterAnim(UIAnimCallBack animComplete, UICallBack callBack, params object[] objs)
    {
        //默认无动画
        animComplete(this, callBack, objs);

        yield break;
    }
    public virtual void OnCompleteEnterAnim()
    {
    }
    public virtual IEnumerator ExitAnim(UIAnimCallBack animComplete, UICallBack callBack, params object[] objs)
    {
        //默认无动画
        animComplete(this, callBack, objs);

        yield break;
    }

    public virtual void OnCompleteExitAnim()
    {
    }




    public enum PageStatus
    {
        Create,
        Open,
        Close,
        OpenAnim,
        CloseAnim,
        Hide,
    }
}