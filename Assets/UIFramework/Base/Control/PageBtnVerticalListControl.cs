using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tools;
public class PageBtnVerticalListControl : BasePanel
{

    public VerticalLayoutGroup VerticalLayout;

    public RectTransform BtnContent;


    private void OnEnable()
    {

    }

    // Use this for initialization
    void Start()
    {
        foreach (RectTransform rect in BtnContent.GetRectTransform())
        {
            rect.sizeDelta = new Vector2(BtnContent.GetRectTransform().rect.width, 215);
        }
    }

    void Init()
    {

    }
}
