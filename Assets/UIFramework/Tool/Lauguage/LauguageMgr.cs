/*文件说明
 * CreateTime:  2019/07/23/16:06:03
 * Projectname:  UIFramework
 * FileName:  LauguageMgr.cs
 * Developers:  五峰杰
 * Creator:  杨智杰
 * Description:
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LauguageType
{
    Chinese,
    Japanese
}

namespace UIFramework
{

    public class LauguageMgr
    {
        //本类实例
        private static LauguageMgr _Instance;

        //语言翻译的缓存集合
        private Dictionary<string, string> _DicLauguageCache;


        public static LauguageType lauguageType;
        private LauguageMgr()
        {
            _DicLauguageCache = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(PlayerPrefs.GetString("LauguageType", "")))
            {
                PlayerPrefs.SetString("LauguageType", "Chinese");
                PlayerPrefs.Save();
            }
            lauguageType = (LauguageType)System.Enum.Parse(typeof(LauguageType), PlayerPrefs.GetString("LauguageType", ""));
            //初始化语言缓存集合
            InitLauguageCache();
        }

        /// <summary>
        /// 得到本类实例
        /// </summary>
        /// <returns></returns>
        public static LauguageMgr GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new LauguageMgr();
            }
            return _Instance;
        }
        public static void Clear()
        {
            _Instance = null;
        }
        /// <summary>
        /// 到显示文本信息
        /// </summary>
        /// <param name="lauguageID">语言的ID</param>
        /// <returns></returns>
        public string ShowText(string lauguageID)
        {
            string strQueryResult = string.Empty;           //查询结果

            //参数检查
            if (string.IsNullOrEmpty(lauguageID)) return null;

            //查询处理
            if (_DicLauguageCache != null && _DicLauguageCache.Count >= 1)
            {
                _DicLauguageCache.TryGetValue(lauguageID, out strQueryResult);
                if (!string.IsNullOrEmpty(strQueryResult))
                {
                    return strQueryResult;
                }
            }

           Debug.Log(GetType() + "/ShowText()/ Query is Null!  Parameter lauguageID: " + lauguageID);
            return null;
        }

        /// <summary>
        /// 初始化语言缓存集合
        /// </summary>
        private void InitLauguageCache()
        {
            IConfigManager config = null;
            switch (lauguageType)
            {
                case LauguageType.Chinese:
                    config = new ConfigManagerByJson("Language/ChineseLauguageJSONConfig");
                    break;
                case LauguageType.Japanese:
                    config = new ConfigManagerByJson("Language/JapaneseLauguageJSONConfig");
                    break;
            }
            if (config != null)
            {
                _DicLauguageCache = config.AppSetting;
            }
        }
    }
}