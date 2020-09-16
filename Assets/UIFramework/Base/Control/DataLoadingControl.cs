/*文件说明
*创建时间：2020/01/15/14:38:16
*项目：UIFramework
*文件格式：.cs
*作者：杨智杰
*公司：云笔
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLoadingControl : ObjectBase, ITextGet
{
    public CanvasGroup _canvasGroup;
    public UIImage _animator;
    public UIText _text;
    public void Show()
    {
        base.ExcuteInit();
        if (!_canvasGroup.blocksRaycasts)
        {
            this.Invoke("Hide", 15);
        }
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        this.StartIE("Rotate", Tools.Tool.WaitUpdateTimeAction(() =>
        {
            _animator.rectTransform.Rotate(0, 0, 8);
        }, 0.02f));
    }
    public void OnEnable()
    {
        this.Invoke("Hide", 10);
    }
    public void OnDisable()
    {
        this.CancelInvoke();
    }
    public void Hide()
    {
        this.CancelInvoke();
        this.StopIE("Rotate");
        base.ExcuteFree();
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    [ContextMenu("开启")]
    private void Open()
    {
        ShouToText("数据加载中...。。。。");
    }
    [ContextMenu("关闭")]
    public void Close()
    {
        Hide();
    }

    public void ShouToText(string Text)
    {
        SetText(Text);
        Show();
    }
}
