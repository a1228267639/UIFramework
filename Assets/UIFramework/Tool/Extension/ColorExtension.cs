/*文件说明
 * CreateTime:  2019/05/17/11:43:00
 * Projectname:  CXP_Project
 * FileName:  ColorExtension.cs
 * Developers:  五峰杰
 * Creator:  杨智杰
 * Description:
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tools
{
    public static class ColorExtension
    {
        public static Color SetColor(this Color color, float r, float g, float b, float a)
        {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }
        public static Color SetAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
    }
}