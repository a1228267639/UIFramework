using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    public enum PiovtDirection
    {
        Left,
        LeftTop,
        LeftDown,
        Right,
        RightTop,
        RightDown,
        Center,
        Top,
        Down,
    }

    public static class RectTransformExtension
    {
        /// <summary>
        /// 获取Rect 宽高
        /// </summary>
        /// <param name="tfParent"></param>
        /// <returns></returns>
        public static Vector2 GetWidthHeight(this RectTransform tfParent)
        {
            return new Vector2(tfParent.rect.width, tfParent.rect.height);
        }
        /// <summary>
        /// 设置Rect PosX
        /// </summary>
        /// <param name="tfParent"></param>
        /// <param name="posX"></param>
        public static void SetPosX(this RectTransform tfParent, float posX)
        {
            tfParent.anchoredPosition = new Vector2(posX, tfParent.anchoredPosition.y);
        }
        /// <summary>
        /// 设置Rect PosY
        /// </summary>
        /// <param name="tfParent"></param>
        /// <param name="posX"></param>
        public static void SetPosY(this RectTransform tfParent, float posY)
        {
            tfParent.anchoredPosition = new Vector2(tfParent.anchoredPosition.x, posY);
        }
        /// <summary>
        /// 设置Rect Pos
        /// </summary>
        /// <param name="tfParent"></param>
        /// <param name="pos"></param>
        public static void SetPos(this RectTransform tfParent, Vector2 pos)
        {
            tfParent.anchoredPosition = pos;
        }
        /// <summary>
        /// 设置Rect 中心点方向
        /// </summary>
        /// <param name="tfParent"></param>
        /// <param name="piovtDirection"></param>
        public static void SetPiovtDirection(this RectTransform tfParent, PiovtDirection piovtDirection)
        {
            switch (piovtDirection)
            {
                case PiovtDirection.Center:
                    tfParent.pivot = new Vector2(0.5f, 0.5f);
                    tfParent.anchorMin = new Vector2(0.5f, 0.5f);
                    tfParent.anchorMax = new Vector2(0.5f, 0.5f);
                    break;
                case PiovtDirection.Top:
                    tfParent.pivot = new Vector2(0.5f, 1);
                    tfParent.anchorMin = new Vector2(0.5f, 1);
                    tfParent.anchorMax = new Vector2(0.5f, 1);
                    break;
                case PiovtDirection.Down:
                    tfParent.pivot = new Vector2(0.5f, 0);
                    tfParent.anchorMin = new Vector2(0.5f, 0);
                    tfParent.anchorMax = new Vector2(0.5f, 0);
                    break;
                case PiovtDirection.Left:
                    tfParent.pivot = new Vector2(0, 0.5f);
                    tfParent.anchorMin = new Vector2(0, 0.5f);
                    tfParent.anchorMax = new Vector2(0, 0.5f);
                    break;
                case PiovtDirection.Right:
                    tfParent.pivot = new Vector2(1, 0.5f);
                    tfParent.anchorMin = new Vector2(1, 0.5f);
                    tfParent.anchorMax = new Vector2(1, 0.5f);
                    break;
                case PiovtDirection.LeftTop:
                    tfParent.pivot = new Vector2(0f, 1);
                    tfParent.anchorMin = new Vector2(0f, 1);
                    tfParent.anchorMax = new Vector2(0f, 1);
                    break;
                case PiovtDirection.LeftDown:
                    tfParent.pivot = new Vector2(0, 0);
                    tfParent.anchorMin = new Vector2(0, 0);
                    tfParent.anchorMax = new Vector2(0, 0);
                    break;
                case PiovtDirection.RightTop:
                    tfParent.pivot = new Vector2(1, 1);
                    tfParent.anchorMin = new Vector2(1, 1);
                    tfParent.anchorMax = new Vector2(1, 1);
                    break;
                case PiovtDirection.RightDown:
                    tfParent.pivot = new Vector2(1, 0);
                    tfParent.anchorMin = new Vector2(1, 0);
                    tfParent.anchorMax = new Vector2(1, 0);
                    break;
            }
        }
        /// <summary>
        /// 设置Rect 中心点
        /// </summary>
        /// <param name="tfParent"></param>
        /// <param name="pivot"></param>
        public static void SetPiovt(this RectTransform tfParent, Vector2 pivot)
        {
            tfParent.pivot = pivot;
        }
        /// <summary>
        /// 设置Rect 锚点
        /// </summary>
        /// <param name="tfParent"></param>
        /// <param name="pivot"></param>
        public static void SetAnchorsMin(this RectTransform tfParent, Vector2 pivot)
        {
            tfParent.anchorMin = pivot;
        }
        public static Vector3 GetPositionRegardlessOfPivot(this RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            return (corners[0] + corners[2]) / 2;
        }
        /// <summary>
        ///  设置Rect 锚点
        /// </summary>
        /// <param name="tfParent"></param>
        /// <param name="pivot"></param>
        public static void SetAnchorsMax(this RectTransform tfParent, Vector2 pivot)
        {
            tfParent.anchorMax = pivot;
        }
        //	Sometimes sizeDelta works, sometimes rect works, sometimes neither work and you need to get the layout properties.
        //	This method provides a simple way to get the size of a RectTransform, no matter what's driving it or what the anchor values are.
        public static Vector2 GetProperSize(this RectTransform rectTransform) //, bool attemptToRefreshLayout = false)
        {
            Vector2 size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);

            if (size.x == 0 && size.y == 0)
            {
                LayoutElement layoutElement = rectTransform.GetComponent<LayoutElement>();

                if (layoutElement != null)
                {
                    size.x = layoutElement.preferredWidth;
                    size.y = layoutElement.preferredHeight;
                }
            }
            if (size.x == 0 && size.y == 0)
            {
                LayoutGroup layoutGroup = rectTransform.GetComponent<LayoutGroup>();

                if (layoutGroup != null)
                {
                    size.x = layoutGroup.preferredWidth;
                    size.y = layoutGroup.preferredHeight;
                }
            }

            if (size.x == 0 && size.y == 0)
            {
                size.x = LayoutUtility.GetPreferredWidth(rectTransform);
                size.y = LayoutUtility.GetPreferredHeight(rectTransform);
            }

            return size;
        }
    }
}