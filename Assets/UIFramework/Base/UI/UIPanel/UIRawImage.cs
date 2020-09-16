using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRawImage : RawImage, IUIInternationalised
{

    public Sprite mSprite;
    [CustomLabel("”Ô—‘Key")]
    [SerializeField]
    private string mLanguageKey;
    public string LanguageKey { get { return mLanguageKey; } set { mLanguageKey = value; } }
    [CustomLabel("—’…´Key")]
    [SerializeField]
    private string mThemeColorKey;
    public string ThemeColorKey { get { return mThemeColorKey; } set { mThemeColorKey = value; } }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (mSprite != null && texture == null)
        {
            texture = mSprite.texture;
        }
    }
}