/*******************************************************************
* Copyright(c) 2020 YZJ
* All rights reserved.
*
* 文件名称: AnimatedShadow.cs
* 简要描述:
* 
* 创建日期: 2020/07/25 08:50:39
* 作者:     YZJ
* 说明:  
******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(UIImage))]
public class AnimatedShadow : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private bool m_IsVisible;
    public bool isVisible
    {
        get { return m_IsVisible; }
        set { m_IsVisible = value; }
    }

    private CanvasGroup m_CanvasGroup;
    public CanvasGroup canvasGroup
    {
        get
        {
            if (m_CanvasGroup == null)
            {
                m_CanvasGroup = GetComponent<CanvasGroup>();
            }
            return m_CanvasGroup;
        }
    }

    private int m_Tweener;

    public void SetShadow(bool set, bool instant = false)
    {
        isVisible = set;

        if (set)
        {
            gameObject.SetActive(true);
        }

        if (Application.isPlaying && !instant)
        {
         //   TweenManager.EndTween(m_Tweener);

          //  m_Tweener = TweenManager.TweenFloat(f => canvasGroup.alpha = f, () => canvasGroup.alpha, set ? 1f : 0f, 0.5f, 0f, () => gameObject.SetActive(set));
        }
        else
        {
            canvasGroup.alpha = set ? 1f : 0f;
            gameObject.SetActive(set);
        }
    }
}