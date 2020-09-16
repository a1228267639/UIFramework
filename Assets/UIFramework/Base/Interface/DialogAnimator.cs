using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//2020/05/08/15:38:18
//YZJ_UIFramework
//.cs
//杨智杰
//云笔

public abstract class DialogAnimator
{
    protected float mAnimationDuration;

    public float animationDuration
    {
        get { return mAnimationDuration; }
    }
    private CanvasGroup m_Background;
    public CanvasGroup background
    {
        get
        {
            if (m_Background == null)
            {
              //  m_Background = PrefabManager.InstantiateGameObject("Dialogs/Dialog Background", m_Dialog.rectTransform.parent).GetComponent<CanvasGroup>();

                RectTransform backgroundTransform = m_Background.transform as RectTransform;
                backgroundTransform.SetAsFirstSibling();
                backgroundTransform.anchoredPosition = Vector2.zero;
                backgroundTransform.sizeDelta = Vector2.zero;

                m_Background.GetComponent<PageDialogBackgroundControl>().dialogAnimator = this;
            }
            return m_Background;
        }
    }

}