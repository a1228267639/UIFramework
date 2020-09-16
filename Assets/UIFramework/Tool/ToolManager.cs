/*文件说明
 * CreateTime:  2019/05/15/11:16:25
 * Projectname:  CXP_Project
 * FileName:  ToolManager.cs
 * Developers:  五峰杰
 * Creator:  杨智杰
 * Description: 工具类
*/
using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
namespace Tools
{
    public static class Tool
    {
        private static string key
        {
            get
            {
                return "YBZN8888";
            }
        }
        private static string iv
        {
            get
            {
                return "8888NZBY";
            }
        }
        /// <summary>
        /// 判断路径是否有文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool Exists(string filePath)
        {
            if (File.Exists(filePath))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///  HTMl 颜色  转换为 Unity Color 
        /// </summary>
        /// <param name="htmlColor"></param>
        /// <returns></returns>
        public static Color HTMLColor(string htmlColor)
        {
            Color defColor = Color.white;
            if (ColorUtility.TryParseHtmlString(htmlColor, out defColor))
            {
                return defColor;
            }
            return defColor;
        }
        /// <summary>
        ///  Unity Color 转换为HTMl RGB颜色
        /// </summary>
        /// <param name="defColor"></param>
        /// <returns></returns>
        public static string ColorHTML_RGB(Color defColor)
        {
            return ColorUtility.ToHtmlStringRGB(defColor);
        }
        /// <summary>
        ///   Unity Color 转换为HTMl RGBA颜色
        /// </summary>
        /// <param name="defColor"></param>
        /// <returns></returns>
        public static string ColorHTML_RGBA(Color defColor)
        {
            return ColorUtility.ToHtmlStringRGBA(defColor);
        }

        /// <summary>
        /// 先等待  后执行事件
        /// </summary>
        /// <param name="action"></param>
        /// <param name="_delaySeconds"></param>
        /// <returns></returns>
        public static IEnumerator WaitTimeAction(Action action, float waitSeconds)
        {
            yield return new WaitForSeconds(waitSeconds);
            if (action != null)
            {
                action();
            }
        }
        /// <summary>
        /// 一个循环的Update事件
        /// </summary>
        /// <param name="action"></param>
        /// <param name="_delaySeconds"></param>
        /// <returns></returns>
        public static IEnumerator WaitUpdateTimeAction(Action action)
        {
            while (true)
            {
                if (action != null)
                {
                    action();
                }
                yield return 0;
            }
        }
        /// <summary>
        /// 先执行 后等待时间
        /// </summary>
        /// <param name="action"></param>
        /// <param name="waitSeconds"></param>
        /// <returns></returns>
        public static IEnumerator WaitUpdateTimeAction(Action action, float waitSeconds)
        {
            while (true)
            {
                action?.Invoke();
                yield return new WaitForSeconds(waitSeconds);
            }
        }
        /// <summary>
        ///  隐藏对象数组
        /// </summary>
        /// <param name="selfObj">当前GameObject</param>
        /// <param name="gameObjects">隐藏的对象</param>
        public static void OtherHide(params GameObject[] gameObjects)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].Hide();
            }
        }
        /// <summary>
        ///  显示对象数组
        /// </summary>
        /// <param name="selfObj">当前GameObject</param>
        /// <param name="gameObjects">隐藏的对象</param>
        public static void OtherShow(params GameObject[] gameObjects)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].Show();
            }
        }
        /// <summary>
        /// 随机一个float 
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static float Random(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        /// <summary>
        /// 随机一个int
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static int Random(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        /// <summary>
        /// 设置帧率
        /// </summary>
        /// <param name="frame"></param>
        public static void SetFrameRate(int frame = 30)
        {
            Application.targetFrameRate = frame;
        }
        /// <summary>
        /// 等比缩放
        /// </summary>
        /// <param name="ShowTexture"></param>
        /// <param name="ShowIMG"></param>
        /// <param name="W"></param>
        /// <param name="H"></param>
        public static void SetTexture(Texture2D ShowTexture, UnityEngine.UI.Image ShowIMG, float W = 640, float H = 480)
        {
            int w = ShowTexture.width;
            int h = ShowTexture.height;
            Sprite sprite = Sprite.Create(ShowTexture, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f));
            float ratio;
            if (w == h)
            {
                ratio = (float)H / (float)h; //保留小数
                ShowIMG.rectTransform.sizeDelta = new Vector2(w * ratio, h * ratio);
                ShowIMG.sprite = sprite;
                return;
            }
            if (w > h)
            {
                ratio = (float)W / (float)w;
                float imgH = h * ratio;
                if (imgH > H)
                {
                    ShowIMG.rectTransform.sizeDelta = new Vector2(imgH, H);
                    ShowIMG.sprite = sprite;
                }
                else
                {
                    ShowIMG.rectTransform.sizeDelta = new Vector2(W, imgH);
                    ShowIMG.sprite = sprite;
                }
            }
            else
            {
                ratio = (float)H / (float)h;
                ShowIMG.rectTransform.sizeDelta = new Vector2(w * ratio, H);
                ShowIMG.sprite = sprite;
            }
        }
        /// <summary>
        /// 等比缩放
        /// </summary>
        /// <param name="ShowTexture"></param>
        /// <param name="ShowIMG"></param>
        /// <param name="W"></param>
        /// <param name="H"></param>
        public static void SetTexture(UnityEngine.UI.Image ShowTexture, UnityEngine.UI.Image ShowIMG, float W = 640, float H = 480)
        {
            float w = ShowTexture.rectTransform.rect.width;
            float h = ShowTexture.rectTransform.rect.height;
            float ratio;
            if (w == h)
            {
                ratio = (float)H / (float)h; //保留小数
                ShowIMG.rectTransform.sizeDelta = new Vector2(w * ratio, h * ratio);
                ShowIMG.sprite = ShowTexture.sprite;
                return;
            }
            if (w > h)
            {
                ratio = (float)W / (float)w;
                float imgH = h * ratio;
                if (imgH > H)
                {
                    ShowIMG.rectTransform.sizeDelta = new Vector2(imgH, H);
                    ShowIMG.sprite = ShowTexture.sprite;
                }
                else
                {
                    ShowIMG.rectTransform.sizeDelta = new Vector2(W, imgH);
                    ShowIMG.sprite = ShowTexture.sprite;
                }
            }
            else
            {
                ratio = (float)H / (float)h;
                ShowIMG.rectTransform.sizeDelta = new Vector2(w * ratio, H);
                ShowIMG.sprite = ShowTexture.sprite;
            }
        }
        /// <summary>
        /// 等比缩放
        /// </summary>
        /// <param name="ShowTexture"></param>
        /// <param name="ShowIMG"></param>
        /// <param name="W"></param>
        /// <param name="H"></param>
        public static void SetRect(RectTransform ShowTexture, UnityEngine.UI.Image ShowIMG, float W = 640, float H = 480)
        {
            float w = ShowTexture.rect.width;
            float h = ShowTexture.rect.height;
            float ratio;
            if (w == h)
            {
                ratio = (float)H / (float)h; //保留小数
                ShowIMG.rectTransform.sizeDelta = new Vector2(w * ratio, h * ratio);
                return;
            }
            if (w > h)
            {
                ratio = (float)W / (float)w;
                float imgH = h * ratio;
                if (imgH > H)
                {
                    ShowIMG.rectTransform.sizeDelta = new Vector2(imgH, H);
                }
                else
                {
                    ShowIMG.rectTransform.sizeDelta = new Vector2(W, imgH);
                }
            }
            else
            {
                ratio = (float)H / (float)h;
                ShowIMG.rectTransform.sizeDelta = new Vector2(w * ratio, H);
            }
        }
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentTime
        {
            get
            {
                return System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <param name="Format"></param>
        /// <returns></returns>
        public static string GetCurrentTimeFormat(string Format)
        {
            return System.DateTime.Now.ToString(Format);
        }
        /// <summary>
        ///  点击事件
        /// </summary>
        /// <param name="Oneaction">单点</param>
        /// <param name="Twoaction">双点</param>
        /// <param name="keyCode">KeyCode值</param>
        public static IEnumerator OnClickEvent(Action Oneaction, Action Twoaction, KeyCode keyCode, float maxDTime = 0.25f)
        {
            int clickCount = 0;
            float dTime = 0;
            //float maxDTime = 0.25f;
            while (true)
            {
                if (clickCount > 0)
                {
                    // 记录到第一次点击后，开始记录时间  
                    dTime += Time.deltaTime;
                    // 时间超过了最大时间，打印结果，并重置  
                    if (dTime > maxDTime)
                    {
                        if (clickCount == 1)
                        {
                            Oneaction?.Invoke();
                        }
                        else
                        {
                            //Debug.Log(Twoaction.Method.Name);
                            Twoaction?.Invoke();
                        }
                        clickCount = 0;
                    }
                    else
                    {
                        if (keyCode == KeyCode.DownArrow || keyCode == KeyCode.UpArrow || keyCode == KeyCode.LeftArrow || keyCode == KeyCode.RightArrow)
                        {
                            if (Input.GetKeyDown(keyCode))
                            {
                                dTime = 0f;
                                ++clickCount;
                            }
                        }
                        else if (keyCode == KeyCode.Escape)
                        {
                            if (Input.GetKeyDown(keyCode))
                            {
                                dTime = 0f;
                                ++clickCount;
                            }
                        }
                        else if (Input.anyKeyDown)
                        {
                            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Escape))
                            {

                            }
                            else
                            {

                                dTime = 0f;
                                ++clickCount;
                            }
                        }
                        // 没有超过时间的情况下，又发生了一次点击，增加计数器  
                        //if (Input.GetKeyDown(keyCode))
                        //{
                        //    dTime = 0f;
                        //    ++clickCount;
                        //}
                    }
                }
                else
                {
                    // 监听第一次点击  
                    dTime = 0f;
                    if (keyCode == KeyCode.DownArrow || keyCode == KeyCode.UpArrow || keyCode == KeyCode.LeftArrow || keyCode == KeyCode.RightArrow)
                    {
                        if (Input.GetKeyDown(keyCode))
                        {
                            //clickCount = 0;
                            clickCount = 1;
                        }
                    }
                    else if (keyCode == KeyCode.Escape)
                    {
                        if (Input.GetKeyDown(keyCode))
                        {
                            //clickCount = 0;
                            clickCount = 1;
                        }
                    }
                    else if (Input.anyKeyDown)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Escape))
                        {

                        }
                        else
                        {
                            //  Debug.Log("as");
                            clickCount = 1;
                        }
                    }
                    //else if (Input.GetKeyDown(keyCode))
                    //    clickCount = 1;
                }
                // }

                yield return 0;

                //#endif
            }
        }
        /// <summary>
        ///  加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DESEncrypt(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DESDecrypt(string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        public static string APPVersion
        {
            get
            {
                return Application.version;
            }
        }
        /// <summary>
        /// 获取系统语言
        /// </summary>
        /// <returns></returns>
        public static SystemLanguage GetSystemLanguage
        {
            get
            {
                return Application.systemLanguage;
            }
        }
        /// <summary>
        /// 设置APP 帧率
        /// </summary>
        /// <param name="FrameRate"></param>
        public static void SetAPPFrameRate(int FrameRate)
        {
            Application.targetFrameRate = FrameRate;
        }
        /// <summary>
        /// 获取Unity版本
        /// </summary>
        /// <returns></returns>
        public static string GetUnityVersion
        {
            get
            {
                return "Unity :" + Application.unityVersion;
            }
        }
        /// <summary>
        /// 打开一个URL
        /// </summary>
        /// <param name="url"></param>
        public static void OpenURL(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                Application.OpenURL(url);
            }
        }
        /// <summary>
        /// 状态栏高度
        /// </summary>
        public static int StatusBarHegiht
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return AndroidPluginManager.Instance.customToolObject.Call<int>("GetStatusBarHeight");
#else
                return 0;
#endif
            }
        }
        /// <summary>
        /// 原生提示消息
        /// </summary>
        /// <param name="msg"></param>
        public static void AndroidToast(string msg)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidPluginManager.Instance.customToolObject.Call("Toast", msg);
#endif
        }
        /// <summary>
        /// 获取当前设备电量
        /// </summary>
        /// <returns></returns>
        public static int GetBatteryLevel
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return AndroidPluginManager.Instance.customToolObject.Call<int>("GetBatteryLevel", "level");
#else
                return 0;
#endif
            }
        }
        /// <summary>
        /// 获取电池充电状态
        /// </summary>
        /// <returns></returns>
        public static int GetBatteryStatus
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return AndroidPluginManager.Instance.customToolObject.Call<int>("GetBatteryLevel", "status");
#else
                return 0;
#endif
            }
        }
        /// <summary>
        /// 获取电池温度
        /// </summary>
        /// <returns></returns>
        public static int GetBatteryTemperature
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return AndroidPluginManager.Instance.customToolObject.Call<int>("GetBatteryLevel", "temperature");
#else
                return 0;
#endif
            }
        }
        /// <summary>
        /// 获取电池总电量
        /// </summary>
        /// <returns></returns>
        public static int GetBatteryScale
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return AndroidPluginManager.Instance.customToolObject.Call<int>("GetBatteryLevel", "scale");
#else
                return 0;
#endif

            }
        }
        /// <summary>
        /// 获取电池电压
        /// </summary>
        /// <returns></returns>
        public static int GetBatteryVoltage
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return AndroidPluginManager.Instance.customToolObject.Call<int>("GetBatteryLevel", "voltage");
#else
                return 0;
#endif
            }
        }
        /// <summary>
        /// 获取电池健康状况
        /// </summary>
        /// <returns></returns>
        public static int GetBatteryHealth
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                   return AndroidPluginManager.Instance.customToolObject.Call<int>("GetBatteryLevel", "health");
#else
                return 0;
#endif
            }
        }
        /// <summary>
        /// 是否连接WiFi
        /// </summary>
        /// <returns></returns>
        public static bool IsWiFi
        {
            get
            {
                if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 是否打开网络
        /// </summary>
        /// <returns></returns>
        public static bool IsNetwork
        {
            get
            {
                if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 当前网络不可用
        /// </summary>
        /// <returns></returns>
        public static bool NoNetwork
        {
            get
            {
                if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 屏幕宽度
        /// </summary>
        public static int ScreenWidth
        {
            get
            {
                return Screen.width;
            }
        }
        /// <summary>
        /// 屏幕高度
        /// </summary>
        public static int ScreenHeight
        {
            get
            {
                return Screen.height;
            }
        }
    }
}