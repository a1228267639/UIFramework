using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//2020/05/11/17:31:40
//YZJ_UIFramework
//.cs
//杨智杰
//云笔

public class PageTab : MonoBehaviour 
{
    [SerializeField]
    private bool m_Interactable = true;
    public bool interactable
    {
        get { return m_Interactable; }
        set { m_Interactable = value; }
    }

    [SerializeField]
    private UIImage m_ItemIMG;
    public UIImage itemIMG
    {
        get { return m_ItemIMG; }
        set { m_ItemIMG = value; }
    }

    [SerializeField]
    private string m_TabName;
    public string tabName
    {
        get { return m_TabName; }
        set { m_TabName = value; }
    }

    [SerializeField]
    private Sprite m_TabIcon;
    public Sprite tabIcon
    {
        get { return m_TabIcon; }
        set { m_TabIcon = value; }
    }


    private RectTransform m_RectTransform;
    public RectTransform rectTransform
    {
        get
        {
            if (m_RectTransform == null)
            {
                m_RectTransform = (RectTransform)transform;
            }
            return m_RectTransform;
        }
    }
}