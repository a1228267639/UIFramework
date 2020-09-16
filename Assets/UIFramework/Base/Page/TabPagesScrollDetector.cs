using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//2020/05/13/22:36:14
//YZJ_UIFramework
//.cs
//杨智杰
//云笔

public class TabPagesScrollDetector : MonoBehaviour, IDragHandler, IEndDragHandler
{

    [SerializeField]
    private PageTabViewControl m_TabView;
    public PageTabViewControl tabView
    {
        get { return m_TabView; }
        set { m_TabView = value; }
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_TabView.TabPageDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_TabView.TabPagePointerUp(eventData.delta.x);
    }
}