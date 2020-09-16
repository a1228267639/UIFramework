using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ValueExtension
{
    /// <summary>
    /// �ַ���תInt
    /// </summary>
    /// <param name="selfStr"></param>
    /// <returns></returns>
    public static int TryGet_Int(this string selfStr)
    {
        int outValue = 0;
        if (int.TryParse(selfStr, out outValue))
        {
            return outValue;
        }
        else
        {
            return outValue;
        }
    }
    /// <summary>
    /// �ַ���תFloat
    /// </summary>
    /// <param name="selfStr"></param>
    /// <returns></returns>
    public static float TryGet_Float(this string selfStr)
    {
        float outValue = 0;
        if (float.TryParse(selfStr, out outValue))
        {
            return outValue;
        }
        else
        {
            return outValue;
        }
    }
    /// <summary>
    /// �ַ���תDouble
    /// </summary>
    /// <param name="selfStr"></param>
    /// <returns></returns>
    public static double TryGet_Double(this string selfStr)
    {
        double outValue = 0;
        if (double.TryParse(selfStr, out outValue))
        {
            return outValue;
        }
        else
        {
            return outValue;
        }
    }

    /// <summary>
    /// ȡС�������λ
    /// </summary>
    /// <param name="selfStr"></param>
    /// <returns></returns>
    public static string ToF5(this float selfStr)
    {
        return selfStr.ToString("F5");
    }
    /// <summary>
    /// ȡС�������λ
    /// </summary>
    /// <param name="selfStr"></param>
    /// <returns></returns>
    public static string ToF4(this float selfStr)
    {
        return selfStr.ToString("F4");
    }
    /// <summary>
    /// ȡС�������λ
    /// </summary>
    /// <param name="selfStr"></param>
    /// <returns></returns>
    public static string ToF3(this float selfStr)
    {
        return selfStr.ToString("F3");
    }
    /// <summary>
    /// ȡС������λ
    /// </summary>
    /// <param name="selfStr"></param>
    /// <returns></returns>
    public static string ToF2(this float selfStr)
    {
        return selfStr.ToString("F2");
    }
    /// <summary>
    /// ȡС�����һλ
    /// </summary>
    /// <param name="selfStr"></param>
    /// <returns></returns>
    public static string ToF1(this float selfStr)
    {
        return selfStr.ToString("F1");
    }
}