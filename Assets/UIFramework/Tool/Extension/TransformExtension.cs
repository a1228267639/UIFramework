/*文件说明
 * CreateTime:  2019/05/16/09:44:33
 * Projectname:  CXP_Project
 * FileName:  TransformExtension.cs
 * Developers:  五峰杰
 * Creator:  杨智杰
 * Description:
*/
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    public static class TransformExtension
    {
        private static WaitForFixedUpdate Update = new WaitForFixedUpdate();
        /// <summary>
        /// 递归遍历子物体，并调用函数
        /// </summary>
        /// <param name="tfParent"></param>
        /// <param name="action"></param>
        public static void ActionRecursion(this Transform tfParent, Action<Transform> action)
        {
            action(tfParent);
            foreach (Transform tfChild in tfParent)
            {
                tfChild.ActionRecursion(action);
            }
        }
        public static Canvas GetRootCanvas(this Transform transform)
        {
            if (transform == null)
            {
                return null;
            }

            Canvas[] parentCanvases = transform.GetComponentsInParent<Canvas>();

            if (parentCanvases == null || parentCanvases.Length == 0)
            {
                return null;
            }

            for (int i = 0; i < parentCanvases.Length; i++)
            {
                Canvas canvas = parentCanvases[i];
                if (canvas.isRootCanvas)
                {
                    return canvas;
                }
            }

            return null;
        }
        /// <summary>
        /// 递归遍历查找指定的名字的子物体
        /// </summary>
        /// <param name="tfParent">当前Transform</param>
        /// <param name="name">目标名</param>
        /// <param name="stringComparison">字符串比较规则</param>
        /// <returns></returns>
        public static Transform FindChildRecursion(this Transform tfParent, string name, StringComparison stringComparison = StringComparison.Ordinal)
        {
            if (tfParent.name.Equals(name, stringComparison))
            {
                return tfParent;
            }

            foreach (Transform tfChild in tfParent)
            {
                Transform tfFinal = null;
                tfFinal = tfChild.FindChildRecursion(name, stringComparison);
                if (tfFinal)
                {
                    return tfFinal;
                }
            }
            return null;
        }
        /// <summary>
        /// 递归遍历查找相应条件的子物体
        /// </summary>
        /// <param name="tfParent">当前Transform</param>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public static Transform FindChildRecursion(this Transform tfParent, Func<Transform, bool> predicate)
        {
            if (predicate(tfParent))
            {
                Debug.Log("Hit " + tfParent.name);
                return tfParent;
            }

            foreach (Transform tfChild in tfParent)
            {
                Transform tfFinal = null;
                tfFinal = tfChild.FindChildRecursion(predicate);
                if (tfFinal)
                {
                    return tfFinal;
                }
            }

            return null;
        }
        /// <summary>
        /// 获得子对象
        /// </summary>
        /// <param name="selfTransform">当前Transform</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public static Transform FindChild(this Transform selfTransform, int index)
        {
            Transform tfFinal = null;
            tfFinal = selfTransform.GetChild(index);
            if (tfFinal)
            {
                return tfFinal;
            }
            return null;
        }
        /// <summary>
        /// 显示对象
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        public static Transform Show(this Transform selfObj)
        {
            selfObj.gameObject.SetActive(true);
            return selfObj;
        }
        /// <summary>
        /// 显示对象
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        /// <param name="action">回调函数</param>
        public static Transform Show(this Transform selfObj, Action action)
        {
            if (action != null)
            {
                action();
            }
            selfObj.gameObject.SetActive(true);
            return selfObj;
        }
        /// <summary>
        /// 隐藏对象
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        public static Transform Hide(this Transform selfObj)
        {
            selfObj.gameObject.SetActive(false);
            return selfObj;
        }
        /// <summary>
        /// 隐藏对象
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        /// <param name="action">回调函数</param>
        public static Transform Hide(this Transform selfObj, Action action)
        {
            if (action != null)
            {
                action();
            }
            selfObj.gameObject.SetActive(false);
            return selfObj;
        }
        /// <summary>
        /// 设置对象欧拉角
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        /// <param name="axis">三维向量</param>
        public static Transform SetEulerAngles(this Transform selfObj, Vector3 axis)
        {
            selfObj.localEulerAngles = axis;
            return selfObj;
        }
        /// <summary>
        /// 设置对象全局坐标
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        /// <param name="axis">三维向量</param>
        /// <returns></returns>
        public static Transform SetPosition(this Transform selfObj, Vector3 axis)
        {
            selfObj.transform.position = axis;
            return selfObj;
        }
        /// <summary>
        /// 设置对象本地坐标
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        /// <param name="axis">三维向量</param>
        /// <returns></returns>
        public static Transform SetLocalPosition(this Transform selfObj, Vector3 axis)
        {
            selfObj.transform.localPosition = axis;
            return selfObj;
        }
        /// <summary>
        /// 设置对象x全局坐标值
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        /// <param name="axi_X">x数值</param>
        /// <returns></returns>
        public static Transform SetPosX(this Transform selfObj, float axi_X)
        {
            selfObj.transform.position = new Vector3(axi_X, selfObj.transform.position.y, selfObj.transform.position.z);
            return selfObj;
        }
        /// <summary>
        /// 设置对象y全局坐标值
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        /// <param name="axi_X">y数值</param>
        /// <returns></returns>
        public static Transform SetPosY(this Transform selfObj, float axi_Y)
        {
            selfObj.transform.position = new Vector3(selfObj.transform.position.x, axi_Y, selfObj.transform.position.z);
            return selfObj;
        }
        /// <summary>
        /// 设置对象z全局坐标值
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        /// <param name="axi_X">z数值</param>
        /// <returns></returns>
        public static Transform SetPosZ(this Transform selfObj, float axi_Z)
        {
            selfObj.transform.position = new Vector3(selfObj.transform.position.x, selfObj.transform.position.y, axi_Z);
            return selfObj;
        }
        /// <summary>
        /// 设置对象x本地坐标值
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        /// <param name="axi_X">x数值</param>
        /// <returns></returns>
        public static Transform SetLocalPosX(this Transform selfObj, float axi_X)
        {
            selfObj.transform.localPosition = new Vector3(axi_X, selfObj.transform.localPosition.y, selfObj.transform.localPosition.z);
            return selfObj;
        }
        /// <summary>
        /// 设置对象y本地坐标值
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        /// <param name="axi_X">y数值</param>
        /// <returns></returns>
        public static Transform SetLocalPosY(this Transform selfObj, float axi_Y)
        {
            selfObj.transform.localPosition = new Vector3(selfObj.transform.localPosition.x, axi_Y, selfObj.transform.localPosition.z);
            return selfObj;
        }
        /// <summary>
        /// 设置对象z本地坐标值
        /// </summary>
        /// <param name="selfObj">当前Transform</param>
        /// <param name="axi_X">z数值</param>
        /// <returns></returns>
        public static Transform SetLocalPosZ(this Transform selfObj, float axi_Z)
        {
            selfObj.transform.localPosition = new Vector3(selfObj.transform.localPosition.x, selfObj.transform.localPosition.y, axi_Z);
            return selfObj;
        }
        /// <summary>
        /// 返回 RectTransform
        /// </summary>
        /// <param name="selfObj"></param>
        /// <returns></returns>
        public static RectTransform GetRectTransform(this Transform selfObj)
        {
            return selfObj.GetComponent<RectTransform>();
        }
        /// <summary>
        /// 设置循环旋转X
        /// </summary>
        /// <param name="selfObj"></param>
        /// <param name="axi_X"></param>
        /// <returns></returns>
        public static IEnumerator SetLoopRotateX(this Transform selfObj, float axi_X)
        {
            while (true)
            {
                yield return Update;
                selfObj.Rotate(axi_X, 0, 0);
            }
        }
        /// <summary>
        /// 设置循环旋转Y
        /// </summary>
        /// <param name="selfObj"></param>
        /// <param name="axi_Y"></param>
        /// <returns></returns>
        public static IEnumerator SetLoopRotateY(this Transform selfObj, float axi_Y)
        {
            while (true)
            {
                yield return Update;
                selfObj.Rotate(0, axi_Y, 0);
            }
        }
        /// <summary>
        /// 设置循环旋转Z
        /// </summary>
        /// <param name="selfObj"></param>
        /// <param name="axi_Z"></param>
        /// <returns></returns>
        public static IEnumerator SetLoopRotateZ(this Transform selfObj, float axi_Z)
        {
            while (true)
            {
                yield return Update;
                selfObj.Rotate(0, 0, axi_Z);
            }
        }
        public static Rect worldRect(this RectTransform selfObj)
        {
            Rect result = new Rect();
            float xScale = selfObj.lossyScale.x;
            float yScale = selfObj.lossyScale.y;
            Vector2 size = selfObj.rect.size;

            result.width = size.x * xScale;
            result.height = size.y * yScale;
            result.x = selfObj.position.x - result.width * selfObj.pivot.x;
            result.y = Screen.height - (selfObj.position.y + result.height * (1 - selfObj.pivot.y));
            return result;
        }
    }
}