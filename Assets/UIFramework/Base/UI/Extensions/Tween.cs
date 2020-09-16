/*******************************************************************
* Copyright(c) 2020 YZJ
* All rights reserved.
*
* 文件名称: Tween.cs
* 简要描述:
* 
* 创建日期: 2020/07/28 16:00:28
* 作者:     YZJ
* 说明:  
******************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tween
{
    public static Color CubeOut(Color startValue, Color endValue, float time, float duration)
    {
        Color tempColor = startValue;
        tempColor.r = CubeOut(startValue.r, endValue.r, time, duration);
        tempColor.g = CubeOut(startValue.g, endValue.g, time, duration);
        tempColor.b = CubeOut(startValue.b, endValue.b, time, duration);
        tempColor.a = CubeOut(startValue.a, endValue.a, time, duration);
        return tempColor;
    }

    public static float CubeOut(float startValue, float endValue, float time, float duration)
    {
        float differenceValue = endValue - startValue;
        time = Mathf.Clamp(time, 0f, duration);
        time /= duration;

        if (time == 0f)
            return startValue;
        if (time == 1f)
            return endValue;

        time--;
        return differenceValue * (time * time * time + 1) + startValue;
    }

    public static float QuintOut(float startValue, float endValue, float time, float duration)
    {
        float differenceValue = endValue - startValue;
        time = Mathf.Clamp(time, 0f, duration);
        time /= duration;

        if (time == 0f)
            return startValue;
        if (time == 1f)
            return endValue;

        time--;
        return differenceValue * (time * time * time * time * time + 1) + startValue;
    }

    public static Vector3 QuintOut(Vector3 startValue, Vector3 endValue, float time, float duration)
    {
        Vector3 tempVector3 = startValue;
        tempVector3.x = QuintOut(startValue.x, endValue.x, time, duration);
        tempVector3.y = QuintOut(startValue.y, endValue.y, time, duration);
        tempVector3.z = QuintOut(startValue.z, endValue.z, time, duration);
        return tempVector3;
    }

    /// <summary>
    /// Evaluates a point on a linear curve to return a value.
    /// </summary>
    /// <param name="startValue">The value at the start of the tween.</param>
    /// <param name="endValue">The value at the end of the tween.</param>
    /// <param name="time">The current time of the tween.</param>
    /// <param name="duration">The total duration of the tween.</param>
    /// <returns>
    /// The calculated value.
    /// </returns>
    public static float Linear(float startValue, float endValue, float time, float duration)
    {
        float differenceValue = endValue - startValue;
        time = Mathf.Clamp(time, 0f, duration);
        time /= duration;

        if (time == 0f)
            return startValue;
        if (time == 1f)
            return endValue;

        return differenceValue * time + startValue;
    }
}