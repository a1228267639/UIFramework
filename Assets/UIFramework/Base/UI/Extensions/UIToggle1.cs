/*******************************************************************
* Copyright(c) 2020 YZJ
* All rights reserved.
*
* 文件名称: UIToggle1.cs
* 简要描述:
* 
* 创建日期: 2020/07/24 16:05:19
* 作者:     YZJ
* 说明:  
******************************************************************/
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class UIToggle1 : MonoBehaviour
{
    public Color ToggleSelect_Color;
    public Color ToggleDefault_Color;
    public UIImage ToggleBG;
    private UIToggle mUIToggle;
    // Use this for initialization
    void Awake()
    {
        mUIToggle = this.GetComponent<UIToggle>();
        mUIToggle.unityEventOnPointerDown.AddListener(() =>
        {
            ToggleBG.color = mUIToggle.graphic.color;
            ToggleBG.gameObject.Show();
            ToggleBG.transform.localScale = Vector3.zero;
            ToggleBG.transform.DOScale(Vector3.one, 0.2f);

        });
        mUIToggle.unityEventOnPointerUP.AddListener(() =>
        {
            ToggleBG.transform.localScale = Vector3.one;
            ToggleBG.canvasGroup.DOFade(0, 0.1f).OnComplete(() =>
            {
                ToggleBG.color = mUIToggle.graphic.color;
                ToggleBG.gameObject.Hide();
                ToggleBG.canvasGroup.alpha = 0.2f;
            });

        });
        mUIToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                mUIToggle.graphic.rectTransform.localScale = Vector3.zero;
                mUIToggle.graphic.rectTransform.DOScale(Vector3.one, 0.25f).SetEase(Ease.Linear);
                mUIToggle.targetGraphic.DOColor(ToggleSelect_Color, 0.25f);
                mUIToggle.graphic.DOColor(ToggleSelect_Color, 0.25f);
                switch (mUIToggle.toggleType)
                {
                    case ToggleType.文字:
                        mUIToggle.childText.DOColor(ToggleSelect_Color, 0.25f);
                        break;
                    case ToggleType.图标:
                        mUIToggle.childImg.DOColor(ToggleSelect_Color, 0.25f);
                        break;
                }
            }
            else
            {
                mUIToggle.graphic.rectTransform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.Linear);
                mUIToggle.targetGraphic.DOColor(ToggleDefault_Color, 0.25f);
                mUIToggle.graphic.DOColor(ToggleDefault_Color, 0.25f);
                switch (mUIToggle.toggleType)
                {
                    case ToggleType.文字:
                        mUIToggle.childText.DOColor(ToggleDefault_Color, 0.25f);
                        break;
                    case ToggleType.图标:
                        mUIToggle.childImg.DOColor(ToggleDefault_Color, 0.25f);
                        break;
                }
            }
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}