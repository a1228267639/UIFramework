using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollRect : ScrollRect, IUIInternationalised
{
    public string LanguageKey { get; set; }
    public string ThemeColorKey { get; set; }
}
