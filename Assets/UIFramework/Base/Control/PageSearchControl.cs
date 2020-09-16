using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using UnityEngine.UI;
//2020/05/07/11:26:13
//YZJ_UIFramework
//.cs
//杨智杰
//云笔

public class PageSearchControl : BasePanel
{

    public UIInputField SearchInputField;
    public UIImage SearchIcon;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        SearchInputField.onValueChanged.AddListener((value) =>
        {
            if (string.IsNullOrEmpty(value))
            {
                SearchIcon.gameObject.Show();
            }
            else
            {
                SearchIcon.gameObject.Hide();
            }

        });

    }
}