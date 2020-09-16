/*******************************************************************
* Copyright(c) 2020 YZJ
* All rights reserved.
*
* 文件名称: UIToggle2.cs
* 简要描述:
* 
* 创建日期: 2020/07/24 16:09:39
* 作者:     YZJ
* 说明:  
******************************************************************/
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle2 : MonoBehaviour
{
    private UIToggle mUIToggle;
    public RectTransform OnObj;
    public RectTransform OnObjBG;
    public UIImage LeftIMG;
    public UIImage RightIMG;
    Vector2 currentValue;
    // Use this for initialization
    void Awake()
    {
        mUIToggle = this.GetComponent<UIToggle>();
        mUIToggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                currentValue = OnObj.anchorMax;
                DOTween.To(() => currentValue, x => OnObj.anchorMax = x, new Vector2(1, 0.5f), 0.25f);
                DOTween.To(() => currentValue, x => OnObj.anchorMin = x, new Vector2(1, 0.5f), 0.25f);
                DOTween.To(() => currentValue, x => OnObj.pivot = x, new Vector2(1, 0.5f), 0.25f);
                DOTween.To(() => currentValue, x => OnObj.anchoredPosition = x, new Vector2(15, 0), 0.25f);
                OnObjBG.GetComponent<UIImage>().DOColor(Color.white, 0.25f);
                LeftIMG.DOColor(Color.gray, 0.25f);
                RightIMG.DOColor(Color.red, 0.25f);
                LeftIMG.transform.DOScale(new Vector3(0.8f,0.8f,0.8f), 0.5f).SetEase(Ease.OutBack);
                RightIMG.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            }
            else
            {
                currentValue = OnObj.anchorMax;
                DOTween.To(() => currentValue, x => OnObj.anchorMax = x, new Vector2(0, 0.5f), 0.25f);
                DOTween.To(() => currentValue, x => OnObj.anchorMin = x, new Vector2(0, 0.5f), 0.25f);
                DOTween.To(() => currentValue, x => OnObj.pivot = x, new Vector2(0, 0.5f), 0.25f);
                DOTween.To(() => currentValue, x => OnObj.anchoredPosition = x, new Vector2(-15, 0), 0.25f);
                OnObjBG.GetComponent<UIImage>().DOColor(Color.gray, 0.25f);
                RightIMG.DOColor(Color.gray, 0.25f);
                LeftIMG.DOColor(Color.red, 0.25f);
                LeftIMG.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
                RightIMG.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f).SetEase(Ease.OutBack);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}