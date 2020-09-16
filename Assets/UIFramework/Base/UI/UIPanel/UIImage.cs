using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIImage : Image, IUIInternationalised
{
    [CustomLabel("”Ô—‘Key")]
    [SerializeField]
    private string mLanguageKey;
    public string LanguageKey { get { return mLanguageKey; } set { mLanguageKey = value; } }
    [CustomLabel("—’…´Key")]
    [SerializeField]
    private string mThemeColorKey;
    public string ThemeColorKey { get { return mThemeColorKey; } set { mThemeColorKey = value; } }


    public CanvasGroup canvasGroup
    {
        get
        {
            return this.GetAddComponent<CanvasGroup>();
        }
    }
}
