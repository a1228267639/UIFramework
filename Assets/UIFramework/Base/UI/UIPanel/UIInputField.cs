using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputField : InputField,IUIInternationalised
{
    [CustomLabel("����Key")]
    [SerializeField]
    private string mLanguageKey;
    public string LanguageKey { get { return mLanguageKey; } set { mLanguageKey = value; } }
    [CustomLabel("��ɫKey")]
    [SerializeField]
    private string mThemeColorKey;
    public string ThemeColorKey { get { return mThemeColorKey; } set { mThemeColorKey = value; } }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

}
