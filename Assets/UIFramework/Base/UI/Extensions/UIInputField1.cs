/*******************************************************************
* Copyright(c) 2020 YZJ
* All rights reserved.
*
* 文件名称: UIInputField1.cs
* 简要描述:
* 
* 创建日期: 2020/08/03 17:29:31
* 作者:     YZJ
* 说明:  
******************************************************************/
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class UIInputField1 : MonoBehaviour
{
    private UIInputField mUIInputField;
    public UIImage Search_Icon;
    // Use this for initialization
    void Start()
    {
        mUIInputField = this.GetComponent<UIInputField>();
        mUIInputField.onValueChanged.AddListener((string value) =>
       {
           if (!string.IsNullOrEmpty(value))
           {
               Search_Icon.gameObject.Hide();
           }
           else
           {
               Search_Icon.gameObject.Show();
           }
       });
    }

    // Update is called once per frame
    void Update()
    {

    }
}