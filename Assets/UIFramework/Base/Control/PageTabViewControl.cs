using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using Tools;
using System;
using UnityEngine.Android;
//2020/05/11/16:10:47
//YZJ_UIFramework
//.cs
//杨智杰
//云笔

public class PageTabViewControl : BasePanel
{

    private bool m_CanScrollBetweenTabs = true;
    private bool m_LowerUnselectedTabAlpha = true;
    [SerializeField]
    private RectTransform m_Indicator;
    public RectTransform indicator
    {
        get { return m_Indicator; }
        set { m_Indicator = value; }
    }
    [SerializeField]
    private RectTransform m_PagesRect;
    public RectTransform pagesRect
    {
        get { return m_PagesRect; }
        set { m_PagesRect = value; }
    }


    [SerializeField]
    private RectTransform m_PagesContainer;

    [SerializeField]
    private RectTransform m_TabsContainer;
    public RectTransform tabsContainer
    {
        get { return m_TabsContainer; }
        set { m_TabsContainer = value; }
    }
    [SerializeField]
    private ItemTab m_TabItemTemplate;
    public ItemTab tabItemTemplate
    {
        get { return m_TabItemTemplate; }
        set { m_TabItemTemplate = value; }
    }

    [SerializeField]
    private bool m_ForceStretchTabsOnLanscape = false;
    public bool forceStretchTabsOnLanscape
    {
        get { return m_ForceStretchTabsOnLanscape; }
        set { m_ForceStretchTabsOnLanscape = value; }
    }
    [SerializeField]
    private float m_ShrinkTabsToFitThreshold = 16f;
    public float shrinkTabsToFitThreshold
    {
        get { return m_ShrinkTabsToFitThreshold; }
        set { m_ShrinkTabsToFitThreshold = value; }
    }
    [SerializeField]
    private int m_CurrentPage;
    [SerializeField]
    private PageTab[] m_Pages;
    public PageTab[] pages
    {
        get { return m_Pages; }
        set { m_Pages = value; }
    }

    [SerializeField]
    private ItemTab[] m_Tabs;
    public ItemTab[] tabs
    {
        get { return m_Tabs; }
        set { m_Tabs = value; }
    }

    private float m_TabWidth;
    private Vector2 m_PageSize;

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
    private float m_TabPadding = 16;
    public float tabPadding
    {
        get { return m_TabPadding; }
        set { m_TabPadding = value; }
    }


    private ScrollRect m_PagesScrollRect;
    public ScrollRect pagesScrollRect
    {
        get
        {
            if (m_PagesScrollRect == null)
            {
                m_PagesScrollRect = m_PagesRect.GetComponent<ScrollRect>();
            }
            return m_PagesScrollRect;
        }
    }

    private void Start()
    {

        List<PageTab> pageTabs = new List<PageTab>();
        foreach (Transform page in m_PagesContainer.transform)
        {
            pageTabs.Add(page.GetComponent<PageTab>());
        }
        m_Pages = pageTabs.ToArray();

        string[] names = new string[m_Pages.Length];

        for (int i = 0; i < m_Pages.Length; i++)
        {
            names[i] = (i + 1) + " - " + m_Pages[i].name;
        }
        m_CurrentPage = 0;

        m_PageSize = m_PagesRect.GetProperSize();

        for (int i = 0; i < m_Pages.Length; i++)
        {
            RectTransform page = m_Pages[i].rectTransform;

            //ItemTab tab = Instantiate(m_TabItemTemplate.gameObject).GetComponent<ItemTab>();

            //tab.rectTransform.SetParent(m_TabItemTemplate.transform.parent);

            page.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, i * m_PageSize.x, m_PageSize.x);
        }
        //for (int i = 0; i < itemTabs.Count; i++)
        //{
        //    itemTabs[i].rectTransform.localScale = Vector3.one;
        //    itemTabs[i].rectTransform.localEulerAngles = Vector3.zero;

        //    itemTabs[i].id = i;
        //}

        ItemTabInit();

        m_PagesContainer.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, m_PageSize.x * m_Pages.Length);


        SetPage(m_CurrentPage, false);
    }


    public void PageTabInit()
    {

    }
    /// <summary>
    /// 标题Tab 初始化
    /// </summary>
    public void ItemTabInit()
    {
        float barWidth = rectTransform.GetProperSize().x;
        m_TabWidth = GetMaxTabTextWidth() + (2 * m_TabPadding);

        m_TabsContainer.GetComponent<LayoutElement>().minWidth = barWidth;
        m_TabsContainer.GetComponent<ContentSizeFitter>().enabled = true;

        float combinedWidth = m_TabWidth * m_Pages.Length;

        if (Screen.width > Screen.height && !m_ForceStretchTabsOnLanscape)
        {
            if (Mathf.Abs(combinedWidth - barWidth) < m_ShrinkTabsToFitThreshold)
            {
                m_TabWidth = barWidth / m_Pages.Length;
            }
            else
            {
                m_TabsContainer.GetComponent<HorizontalLayoutGroup>().childForceExpandWidth = false;
                m_TabItemTemplate.GetComponent<LayoutElement>().minWidth = 160;
            }
        }
        else
        {
            if (combinedWidth - barWidth < m_ShrinkTabsToFitThreshold)
            {
                m_TabWidth = barWidth / m_Pages.Length;
            }
        }

        m_TabWidth = Mathf.Max(m_TabWidth, m_TabItemTemplate.GetComponent<LayoutElement>().minWidth);

        m_TabItemTemplate.GetComponent<LayoutElement>().preferredWidth = m_TabWidth;

        m_Indicator.anchorMin = new Vector2(0, 0);
        m_Indicator.anchorMax = new Vector2(0, 0);
        m_Indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_TabWidth);
        m_Tabs = new ItemTab[m_Pages.Length];


        for (int i = 0; i < m_Pages.Length; i++)
        {
            ItemTab tab = Instantiate(m_TabItemTemplate.gameObject).GetComponent<ItemTab>();
            tab.rectTransform.SetParent(m_TabItemTemplate.transform.parent, false);
            tab.gameObject.SetActive(true);
            tab.rectTransform.localScale = Vector3.one;
            tab.rectTransform.localEulerAngles = Vector3.zero;
            tab.id = i;

            if (!string.IsNullOrEmpty(m_Pages[i].tabName))
            {
                tab.name = m_Pages[i].tabName;
                if (tab.itemText != null)
                {
                    tab.itemText.text = tab.name.ToUpper();
                }
            }
            else
            {
                tab.name = "Tab " + i;
                if (tab.itemText != null)
                {
                    tab.itemText.enabled = false;
                }
            }

            //  tab.SetupGraphic(m_Pages[i].tabIcon.imageDataType);

            if (m_Pages[i].tabIcon != null)
            {
                if (tab.itemIcon != null)
                {
                    //  tab.itemIcon = m_Pages[i].tabIcon;
                    tab.itemIMG.sprite = tab.itemIcon;
                    //
                }
                else
                {
                    tab.itemIMG.sprite = m_Pages[i].tabIcon;
                }
            }
            else
            {
                tab.itemIMG.gameObject.SetActive(false);
            }
            m_Tabs[i] = tab;
        }

        m_TabItemTemplate.gameObject.SetActive(false);

        m_TabsContainer.anchorMin = Vector2.zero;
        m_TabsContainer.anchorMax = new Vector2(0, 1);

    }


    private float GetMaxTabTextWidth()
    {
        float longestTextWidth = 0;

        if (m_TabItemTemplate.itemText != null)
        {
            TextGenerator textGenerator = m_TabItemTemplate.itemText.cachedTextGeneratorForLayout;
            TextGenerationSettings textGenerationSettings = m_TabItemTemplate.itemText.GetGenerationSettings(new Vector2(float.MaxValue, float.MaxValue));

            for (int i = 0; i < m_Pages.Length; i++)
            {
                longestTextWidth = Mathf.Max(longestTextWidth, textGenerator.GetPreferredWidth(m_Pages[i].name.ToUpper(), textGenerationSettings));
            }
        }
        return longestTextWidth;
    }

    public void TabPagePointerUp(float delta)
    {
        if (m_CanScrollBetweenTabs)
        {
            pagesScrollRect.velocity = Vector2.zero;

            if (Mathf.Abs(delta) < 1)
            {
                SetPage(NearestPage());
            }
            else
            {
                if (delta < 0)
                {
                    SetPage(NearestPage(1));
                }
                else
                {
                    SetPage(NearestPage(-1));
                }
            }
        }
    }

    public void TabPageDrag()
    {

    }


    public void SetPage(int index)
    {
        SetPage(index, true);
    }
    private int NearestPage(int direction = 0)
    {
        float currentPosition = -m_PagesContainer.anchoredPosition.x;

        if (direction < 0)
        {
            return Mathf.FloorToInt(currentPosition / m_PageSize.x);
        }

        if (direction > 0)
        {
            return Mathf.CeilToInt(currentPosition / m_PageSize.x);
        }

        return Mathf.RoundToInt(currentPosition / m_PageSize.x);
    }
    private void TweenTabsContainer(int index, bool animate = true)
    {
        float targetPosition = -(index * m_TabWidth);

        targetPosition += rectTransform.GetProperSize().x / 2;
        targetPosition -= m_TabWidth / 2;

        targetPosition = Mathf.Clamp(targetPosition, -LayoutUtility.GetPreferredWidth(m_TabsContainer) + rectTransform.GetProperSize().x, 0);



        if (animate)
        {
            //m_TabsContainerTweener = TweenManager.TweenVector2(
            //    vector2 => m_TabsContainer.anchoredPosition = vector2, m_TabsContainer.anchoredPosition,
            //    new Vector2(targetPosition, 0), 0.5f);
            DOTween.To(() => m_TabsContainer.anchoredPosition, f => m_TabsContainer.anchoredPosition = f, new Vector2(targetPosition, 0), 0.2f).SetEase(Ease.Linear);

        }
        else
        {
            m_TabsContainer.anchoredPosition = new Vector2(targetPosition, 0);
        }
    }

    private void TweenPagesContainer(int index, bool animate = true)
    {
        for (int i = 0; i < m_Pages.Length; i++)
        {
            int smaller = Mathf.Min(m_CurrentPage, index);
            int bigger = Mathf.Max(m_CurrentPage, index);

            if (i >= smaller - 1 && i <= bigger + 1)
            {
                m_Pages[i].gameObject.SetActive(true);
            }
            else
            {
                //  m_Pages[i].DisableIfAllowed();
                //  m_Pages[i].gameObject.SetActive(false);
            }
        }

        float targetPosition = -(index * m_PageSize.x);

        targetPosition = Mathf.Clamp(targetPosition, -(m_Pages.Length * m_PageSize.x), 0);

        //  TweenManager.EndTween(m_PagesContainerTweener);

        m_CurrentPage = index;

        if (animate)
        {
            //m_PagesContainerTweener =
            //    TweenManager.TweenVector2(vector2 => m_PagesContainer.anchoredPosition = vector2,
            //        m_PagesContainer.anchoredPosition, new Vector2(targetPosition, 0), 0.5f);
            DOTween.To(() => m_PagesContainer.anchoredPosition, f => m_PagesContainer.anchoredPosition = f, new Vector2(targetPosition, 0), 0.2f).SetEase(Ease.Linear);

        }
        else
        {
            m_PagesContainer.anchoredPosition = new Vector2(targetPosition, 0);
            //  OnPagesTweenEnd();
        }
    }
    private void TweenIndicator(int targetTab, bool animate = true)
    {
        float targetPosition = targetTab * m_TabWidth;

        if (animate)
        {
            // m_IndicatorTweener = TweenManager.TweenVector2(vector2 => m_Indicator.anchoredPosition = vector2, m_Indicator.anchoredPosition, new Vector2(targetPosition, 0), 0.5f);
            DOTween.To(() => m_Indicator.anchoredPosition, f => m_Indicator.anchoredPosition = f, new Vector2(targetPosition, 0), 0.2f);
        }
        else
        {
            m_Indicator.anchoredPosition = new Vector2(targetPosition, 0);
        }
    }


    public void SetPage(int index, bool animate)
    {
        index = Mathf.Clamp(index, 0, m_Pages.Length - 1);

        TweenIndicator(index, animate);
        TweenTabsContainer(index, animate);
        TweenPagesContainer(index, animate);

        if (m_LowerUnselectedTabAlpha)
        {
            if (animate)
            {
                for (int i = 0; i < m_Tabs.Length; i++)
                {
                    int i1 = i;
                    DOTween.To(() => m_Tabs[i1].canvasGroup.alpha, f => m_Tabs[i1].canvasGroup.alpha = f, m_Pages[i1].interactable ? (i1 == index ? 1f : 0.5f) : 0.15f, 0.2f).SetEase(Ease.Linear);
                    DOTween.To(() => m_Tabs[i1].itemIMG.transform.localScale, f => m_Tabs[i1].itemIMG.transform.localScale = f, m_Pages[i1].interactable ? (i1 == index ? new Vector3(1.2f, 1.2f, 1.2f) : Vector3.one) : Vector3.one, 0.2f).SetEase(Ease.Linear);

                }
            }
            else
            {
                for (int i = 0; i < m_Tabs.Length; i++)
                {
                    m_Tabs[i].canvasGroup.alpha = m_Pages[i].interactable ? (i == index ? 1f : 0.5f) : 0.15f;
                }
            }
        }
    }
}