using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using Unity.Collections;
using UnityEditor;

[System.Serializable]
public enum ToggleType
{
    文字,
    图标,
    默认
}
public class UIToggle : Toggle, IUIInternationalised
{
    [CustomLabel("语言Key")]
    [SerializeField]
    private string mLanguageKey;
    public string LanguageKey { get { return mLanguageKey; } set { mLanguageKey = value; } }
    [CustomLabel("颜色Key")]
    [SerializeField]
    private string mThemeColorKey;
    public string ThemeColorKey { get { return mThemeColorKey; } set { mThemeColorKey = value; } }

    [CustomLabel("Toggle类型")]
    public ToggleType toggleType;

    [CustomLabel("子文本")]
    public UIText childText;
    [CustomLabel("子图片")]
    public UIImage childImg;



    public UnityEvent unityEventOnPointerDown;

    public UnityEvent unityEventOnPointerUP;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        unityEventOnPointerDown?.Invoke();
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        unityEventOnPointerUP?.Invoke();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
}
