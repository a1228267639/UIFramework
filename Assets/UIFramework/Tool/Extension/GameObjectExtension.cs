/*文件说明
 * CreateTime:  2019/05/15/15:37:28
 * Projectname:  CXP_Project
 * FileName:  GameObjectExtension.cs
 * Developers:  五峰杰
 * Creator:  杨智杰
 * Description:
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tools
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="selfObj">当前GameObject</param>
        public static void DestroySelf(this GameObject selfObj)
        {
            GameObject.Destroy(selfObj);
        }
        /// <summary>
        /// 显示对象 
        /// </summary>
        /// <param name="selfObj">当前GameObject</param>
        public static GameObject Show(this GameObject selfObj)
        {
            selfObj.SetActive(true);
            return selfObj;
        }
        public static T GetAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.GetComponent<T>() != null)
            {
                return gameObject.GetComponent<T>();
            }
            else
            {
                return gameObject.AddComponent<T>();
            }
        }
        /// <summary>
        /// 显示对象 
        /// </summary>
        /// <param name="selfObj">当前GameObject</param>
        /// <param name="action">回调函数</param>
        public static GameObject Show(this GameObject selfObj, Action action)
        {
            if (action != null)
            {
                action();
            }
            selfObj.SetActive(true);
            return selfObj;
        }
        /// <summary>
        /// 当前对象是否显示
        /// </summary>
        /// <param name="selfObj"></param>
        /// <returns>显示 返回True 隐藏为 False</returns>
        public static bool IsShow(this GameObject selfObj)
        {
            if (selfObj.activeSelf)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 隐藏对象
        /// </summary>
        /// <param name="selfObj">当前GameObject</param>
        public static GameObject Hide(this GameObject selfObj)
        {
            selfObj.SetActive(false);
            return selfObj;
        }
        /// <summary>
        /// 隐藏对象
        /// </summary>
        /// <param name="selfObj">当前GameObject</param>
        /// <param name="action">回调函数</param>
        public static GameObject Hide(this GameObject selfObj, Action action)
        {
            if (action != null)
            {
                action();
            }
            selfObj.SetActive(false);
            return selfObj;
        }
        /// <summary>
        /// 设置对象的父级
        /// </summary>
        /// <param name="selfObj">当前GameObject</param>
        /// <param name="parentObj">父级对象</param>
        /// <param name="isWord">是否设置世界坐标 默认为True</param>
        public static GameObject SetParent(this GameObject selfObj, GameObject parentObj, bool isWord = true)
        {
            selfObj.transform.SetParent(parentObj.transform, isWord);
            return selfObj;
        }
        /// <summary>
        /// 设置对象的父级
        /// </summary>
        /// <param name="selfObj">当前GameObject</param>
        /// <param name="parentObj">父级对象</param>
        /// <param name="isWord">是否设置世界坐标 默认为True</param>
        public static GameObject SetParent(this GameObject selfObj, Transform parentObj, bool isWord = true)
        {
            selfObj.transform.SetParent(parentObj, isWord);
            return selfObj;
        }
        /// <summary>
        /// 获得子对象
        /// </summary>
        /// <param name="selfTransform">当前GameObject</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public static Transform FindChild(this GameObject selfGameObject, int index)
        {
            Transform tfFinal = null;
            tfFinal = selfGameObject.transform.GetChild(index);
            if (tfFinal)
            {
                return tfFinal;
            }
            return null;
        }
        /// <summary>
        /// 返回 RectTransform
        /// </summary>
        /// <param name="selfObj"></param>
        /// <returns></returns>
        public static RectTransform GetRectTransform(this GameObject selfObj)
        {
            return selfObj.GetComponent<RectTransform>();
        }
        /// <summary>
        /// 激活组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfBehaviour"></param>
        /// <returns></returns>
        public static T Enable<T>(this T selfBehaviour) where T : Behaviour
        {
            selfBehaviour.enabled = true;
            return selfBehaviour;
        }
        /// <summary>
        /// 关闭组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfBehaviour"></param>
        /// <returns></returns>
        public static T Disable<T>(this T selfBehaviour) where T : Behaviour
        {
            selfBehaviour.enabled = false;
            return selfBehaviour;
        }
    }
}