using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ä£°æUISlider
/// </summary>
public class UISlider : Slider, IUIInternationalised
{
    [CustomLabel("ÓïÑÔKey")]
    [SerializeField]
    private string mLanguageKey;
    public string LanguageKey { get { return mLanguageKey; } set { mLanguageKey = value; } }
    [CustomLabel("ÑÕÉ«Key")]
    [SerializeField]
    private string mThemeColorKey;
    public string ThemeColorKey { get { return mThemeColorKey; } set { mThemeColorKey = value; } }
}
