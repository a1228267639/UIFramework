/*******************************************************************
* Copyright(c) 2020 YZJ
* All rights reserved.
*
* 文件名称: ScaleTest.cs
* 简要描述:
* 
* 创建日期: 2020/07/24 17:31:30
* 作者:     YZJ
* 说明:  
******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class UIBtnFold : MonoBehaviour
{
    public RectTransform BtnParent;
    public UIImage MainBtn;
    public bool IsOn = false;
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(MainBtn.gameObject).onClick = ManBtn_Event;
    }

    private void ManBtn_Event(GameObject go)
    {
        if (IsOn)
        {
            Vector3 currentValue = new Vector3(0, 0, -45);
            DOTween.To(() => currentValue, x => MainBtn.transform.localEulerAngles = x, Vector3.zero, 0.25f).SetEase(Ease.OutBack);
            StartCoroutine(ChindAnim2());
        }
        else
        {
            Vector3 currentValue = new Vector3();
            DOTween.To(() => currentValue, x => MainBtn.transform.localEulerAngles = x, new Vector3(0, 0, -45), 0.25f).SetEase(Ease.OutBack);
            StartCoroutine(ChindAnim());
        }
    }

    IEnumerator ChindAnim()
    {
        foreach (Transform tran in BtnParent.transform)
        {
            tran.DOScale(1, 0.2f).SetEase(Ease.OutCubic);
            yield return new WaitForSeconds(0.05f);
        }
        IsOn = true;
    }

    IEnumerator ChindAnim2()
    {
        for (int i = BtnParent.transform.childCount - 1; i >= 0; i--)
        {
            BtnParent.transform.GetChild(i).DOScale(0, 0.2f).SetEase(Ease.OutCubic);
            yield return new WaitForSeconds(0.05f);
        }
        IsOn = false;
    }
    // Update is called once per frame
    void Update()
    {

    }
}