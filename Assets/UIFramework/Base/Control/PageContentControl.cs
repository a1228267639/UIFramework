using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageContentControl : ObjectBase
{
    public PageTitleControl titleControl;
    private RectTransform mSelfRect;
    public Vector2 offsetMax;
    public Vector2 offsetMin;
    public void Start()
    {
        mSelfRect = GetComponent<RectTransform>();
        Init();
    }


    public new void Init()
    {
        if (titleControl != null)
        {
            mSelfRect.offsetMax = new Vector2(-offsetMax.x, -titleControl.IMGBG.rectTransform.rect.height);
            mSelfRect.offsetMin = new Vector2(-offsetMin.x, -offsetMin.y);
        }
        else
        {
            mSelfRect.offsetMax = new Vector2(-offsetMax.x, -offsetMax.y);
            mSelfRect.offsetMin = new Vector2(-offsetMin.x, -offsetMin.y);
        }
        mSelfRect.anchorMax = Vector2.one;
        mSelfRect.anchorMin = Vector2.zero;

    }
}
