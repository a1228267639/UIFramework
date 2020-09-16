using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//2020/05/11/17:33:11
//YZJ_UIFramework
//.cs
//杨智杰
//云笔

public class ItemTab : Selectable, IPointerClickHandler, ISubmitHandler
{

    [SerializeField]
    private UIText m_ItemText;
    public UIText itemText
    {
        get { return m_ItemText; }
        set { m_ItemText = value; }
    }

    [SerializeField]
    private PageTabViewControl m_TabView;
    public PageTabViewControl tabView
    {
        get { return m_TabView; }
        set { m_TabView = value; }
    }
    [SerializeField]
    private UIImage m_ItemIMG;
    public UIImage itemIMG
    {
        get { return m_ItemIMG; }
        set { m_ItemIMG = value; }
    }
    [SerializeField]
    private Sprite m_ItemIcon;
    public Sprite itemIcon
    {
        get { return m_ItemIcon; }
        set { m_ItemIcon = value; }
    }
    private int m_Id;
    public int id
    {
        get { return m_Id; }
        set { m_Id = value; }
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



    private CanvasGroup m_CanvasGroup;
    public CanvasGroup canvasGroup
    {
        get
        {
            if (m_CanvasGroup == null)
            {
                m_CanvasGroup = gameObject.GetComponent<CanvasGroup>();
            }
            return m_CanvasGroup;
        }
    }





    public void OnPointerClick(PointerEventData eventData)
    {
        if (interactable)
        {
            m_TabView.SetPage(id);
        }
    }

    public void OnSubmit(BaseEventData eventData)
    {

    }
}