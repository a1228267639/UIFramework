using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class TranslateLanguageTool : EditorWindow
{
    //可以直接到百度翻译API的官网申请
    //一定要去申请，不然程序的翻译功能不能使用
    private static string appId = "20200329000407763";
    private static string thekey = "hpu2lyBPvPBfJrcLoBc9";
    Language language;
    [MenuItem("Tools/翻译工具")]
    public static void Open()
    {
        GetWindow<TranslateLanguageTool>("翻译工具");
    }

    private Dictionary<string, Dictionary<int, string>> dic = new Dictionary<string, Dictionary<int, string>>();

    string sourceStr = "";
    string id = "";
    string splitStr = ";";
    string JsonStr = "";
    static string consleStr;
    public void OnGUI()
    {
        //id = EditorGUILayout.TextField("id", id);
        EditorGUILayout.BeginHorizontal();
        JsonStr = EditorGUILayout.TextField("Json格式化", JsonStr);
        if (GUILayout.Button("格式化"))
        {
            JsonStr= Regex.Replace(JsonStr,@"\s", "");
            consleStr = JsonStr.Replace("\"", "\\\"");
        }
        if (GUILayout.Button("实体生成"))
        {
            //UTF8Encoding utf8 = new UTF8Encoding(); // Create a UTF-8 encoding. 

            //using (StreamWriter sw = new StreamWriter(generateFilePath, false, utf8))
            //{

            //}
            LitJson.JsonData jsonData = LitJson.JsonMapper.ToObject(JsonStr);
            string cosStr;
            consleStr = "public class Root\n";
            consleStr += "{\n\n";
            JsonToCsharp(jsonData);
            //foreach (KeyValuePair<string, LitJson.JsonData> key in jsonData)
            //{
            //    string keytype = key.Value.GetJsonType() == LitJson.JsonType.Object ? key.Key.ToString() : JsonTypeToString(key.Value.GetJsonType());
            //    string keyValue = key.Value.GetJsonType() == LitJson.JsonType.Object ? key.Key.ToString().Substring(0, 1).ToUpper() + key.Key.Substring(1) + ";" : key.Key.ToString() + ";";

            //    if (key.Value.GetJsonType() == LitJson.JsonType.Object)
            //    {
            //        consleStr += $"  public {keytype} {keyValue}  \n";
            //    }
            //    else if (key.Value.GetJsonType() == LitJson.JsonType.Array)
            //    {
            //        consleStr += $"  public {keyValue + keytype} {keyValue}  \n";
            //    }
            //    else
            //    {
            //        consleStr += $"  public {keytype}  {keyValue} \n";
            //    }
            //}
            consleStr += "\n}\n";
            //foreach (KeyValuePair<string, LitJson.JsonData> key in jsonData)
            //{
            //    string keytype = key.Value.GetJsonType() == LitJson.JsonType.Object ? key.Key.ToString() : JsonTypeToString(key.Value.GetJsonType());
            //    string keyValue = key.Value.GetJsonType() == LitJson.JsonType.Object ? key.Key.ToString().Substring(0, 1).ToUpper() + key.Key.Substring(1) : key.Key.ToString() + ";";
            //    if (key.Value.GetJsonType() == LitJson.JsonType.Object)
            //    {
            //        consleStr += $"public class {keytype} ";
            //        consleStr += "\n{\n";

            //        foreach (KeyValuePair<string, LitJson.JsonData> keys in key.Value)
            //        {
            //            string keystype = keys.Value.GetJsonType() == LitJson.JsonType.Object ? keys.Key.ToString() : JsonTypeToString(keys.Value.GetJsonType());
            //            string keysValue = keys.Value.GetJsonType() == LitJson.JsonType.Object ? keys.Key.ToString().Substring(0, 1).ToUpper() + keys.Key.Substring(1) : keys.Key.ToString() + ";";


            //            if (keys.Value.GetJsonType() == LitJson.JsonType.Array)
            //            {
            //                consleStr += $"  public {keyValue + keytype} {keyValue}  \n";
            //            }
            //            else
            //            {
            //                consleStr += $"  public {keystype}  {keysValue} \n\n";
            //            }
            //        }
            //        consleStr += "\n}\n"; ;
            //    }

            //}
        }
        EditorGUILayout.EndHorizontal();


        sourceStr = EditorGUILayout.TextField("中文", sourceStr);
        splitStr = EditorGUILayout.TextField("分隔符", splitStr);
        language = (Language)EditorGUILayout.EnumPopup("翻译语言", language, GUILayout.Width(position.width));

        if (GUILayout.Button("程序专用翻译"))
        {
            consleStr = "";
            string[] sources = sourceStr.Split(splitStr.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (sources.Length >= 1)
            {
                foreach (var str in sources)
                {
                    switch (language)
                    {
                        case Language.自动监测:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.自动监测, "自动", true);
                            break;
                        case Language.日文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.日文, "日文", true);
                            break;
                        case Language.阿拉伯文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.阿拉伯文, "阿拉伯文", true);
                            break;
                        case Language.法文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.法文, "法文", true);
                            break;
                        case Language.德文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.德文, "德文", true);
                            break;
                        case Language.葡萄牙文文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.葡萄牙文文, "葡萄牙文文", true);
                            break;
                        case Language.俄文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.俄文, "俄文", true);
                            break;
                        case Language.西班牙文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.西班牙文, "西班牙文", true);
                            break;
                        case Language.泰文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.泰文, "泰文", true);
                            break;
                        case Language.越南文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.越南文, "越南文", true);
                            break;
                        case Language.繁体中文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.繁体中文, "繁体中文", true);
                            break;
                        case Language.英文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.英文, "英文", true);
                            break;
                        case Language.希腊文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.希腊文, "希腊文", true);
                            break;
                        case Language.粤语:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.粤语, "粤语", true);
                            break;
                    }
                }

            }


        }
        if (GUILayout.Button("翻译"))
        {
            consleStr = "";
            if (string.IsNullOrEmpty(sourceStr))
            {
                return;
            }
            string[] sources = sourceStr.Split(splitStr.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (sources.Length >= 1)
            {
                foreach (var str in sources)
                {
                    switch (language)
                    {
                        case Language.自动监测:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.自动监测, "自动");
                            break;
                        case Language.日文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.日文, "日文");
                            break;
                        case Language.阿拉伯文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.阿拉伯文, "阿拉伯文");
                            break;
                        case Language.法文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.法文, "法文");
                            break;
                        case Language.德文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.德文, "德文");
                            break;
                        case Language.葡萄牙文文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.葡萄牙文文, "葡萄牙文");
                            break;
                        case Language.俄文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.俄文, "俄文");
                            break;
                        case Language.西班牙文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.西班牙文, "西班牙文");
                            break;
                        case Language.泰文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.泰文, "泰文");
                            break;
                        case Language.越南文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.越南文, "越南文");
                            break;
                        case Language.繁体中文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.繁体中文, "繁体中文");
                            break;
                        case Language.英文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.英文, "英文");
                            break;
                        case Language.希腊文:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.希腊文, "希腊文");
                            break;
                        case Language.粤语:
                            GetTranslationFromBaiduFanyi(str, Language.中文, Language.粤语, "粤语");
                            break;
                    }
                }
            }
        }
        if (GUILayout.Button("翻译全部(慎点)"))
        {
            consleStr = "";
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.自动监测, "自动", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.日文, "日文", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.阿拉伯文, "阿拉伯", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.法文, "法文", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.德文, "德文", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.葡萄牙文文, "葡萄牙文", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.俄文, "俄文", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.西班牙文, "西班牙", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.泰文, "泰语", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.越南文, "越南语", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.繁体中文, "繁体中文", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.英文, "英文", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.希腊文, "希腊文", true);
            GetTranslationFromBaiduFanyi(sourceStr, Language.中文, Language.粤语, "粤语", true);

        }
        EditorGUILayout.TextArea(consleStr, GUILayout.Height(position.width));
    }

    static string languageFrom;
    static string languageTo;
    public static TranslationResult GetTranslationFromBaiduFanyi(string q, Language from, Language to, string language = "", bool isJson = false)
    {

        string jsonResult = string.Empty;

        //源语言
        switch (from)
        {
            case Language.自动监测:
                languageFrom = "auto";
                break;
            case Language.中文:
                languageFrom = "zh";
                break;
            case Language.日文:
                languageFrom = "jp";
                break;
            case Language.阿拉伯文:
                languageFrom = "ara";
                break;
            case Language.法文:
                languageFrom = "fra";
                break;
            case Language.德文:
                languageFrom = "de";
                break;
            case Language.葡萄牙文文:
                languageFrom = "pt";
                break;
            case Language.俄文:
                languageFrom = "ru";
                break;
            case Language.西班牙文:
                languageFrom = "spa";
                break;
            case Language.泰文:
                languageFrom = "th";
                break;
            case Language.越南文:
                languageFrom = "vie";
                break;
            case Language.繁体中文:
                languageFrom = "cht";
                break;
            case Language.英文:
                languageFrom = "en";
                break;
            case Language.希腊文:
                languageFrom = "el";
                break;
            case Language.粤语:
                languageFrom = "yue";
                break;
        }
        switch (to)
        {
            case Language.自动监测:
                languageTo = "auto";
                break;
            case Language.中文:
                languageTo = "zh";
                break;
            case Language.日文:
                languageTo = "jp";
                break;
            case Language.阿拉伯文:
                languageTo = "ara";
                break;
            case Language.法文:
                languageTo = "fra";
                break;
            case Language.德文:
                languageTo = "de";
                break;
            case Language.葡萄牙文文:
                languageTo = "pt";
                break;
            case Language.俄文:
                languageTo = "ru";
                break;
            case Language.西班牙文:
                languageTo = "spa";
                break;
            case Language.泰文:
                languageTo = "th";
                break;
            case Language.越南文:
                languageTo = "vie";
                break;
            case Language.繁体中文:
                languageTo = "cht";
                break;
            case Language.英文:
                languageTo = "en";
                break;
            case Language.希腊文:
                languageTo = "el";
                break;
            case Language.粤语:
                languageTo = "yue";
                break;
        }
        //目标语言
        // string languageTo = to.ToString().ToLower();
        //随机数
        string randomNum = System.DateTime.Now.Millisecond.ToString();
        //md5加密
        string md5Sign = GetMD5WithString(appId + q + randomNum + thekey);
        //url
        string url = String.Format("http://api.fanyi.baidu.com/api/trans/vip/translate?q={0}&from={1}&to={2}&appid={3}&salt={4}&sign={5}",
            q,
            languageFrom,
            languageTo,
            appId,
            randomNum,
            md5Sign
            );
        WebClient wc = new WebClient();
        try
        {
            jsonResult = wc.DownloadString(url);
        }
        catch
        {
            jsonResult = string.Empty;
        }
        //Debug.Log(jsonResult);
        //结果转json
        TranslationResult temp = LitJson.JsonMapper.ToObject<TranslationResult>(jsonResult);
        //for (int i = 0; i < temp.trans_result.Length; i++)
        //{
        //    Translation temp1 = temp.trans_result[i];             //新建一个变量相互交换
        //    temp.trans_result[i] = temp.trans_result[temp.trans_result.Length - i - 1];
        //    temp.trans_result[temp.trans_result.Length - i - 1] = temp1;//相互交换
        //}

        if (null != temp)
        {
            if (isJson)
            {
                for (int i = 0; i < temp.trans_result.Length; i++)
                {
                    string str = "语言 : " + language + @"  {""Key"":" + q + @",""Value"":" + @"""" + temp.trans_result[i].dst + @"""" + "}" + "\n";
                    consleStr = str + consleStr;
                    //Debug.Log(str);
                }
            }
            else
            {
                for (int i = 0; i < temp.trans_result.Length; i++)
                {
                    string str = q + "  :  \"" + temp.trans_result[i].dst + "," + "\n";
                    consleStr = str + consleStr;
                    // Debug.Log(str);
                }
            }
            wc.Dispose();
            return temp;
        }
        return null;
    }

    private static string GetMD5WithString(string input)
    {
        if (input == null)
        {
            return null;
        }
        MD5 md5Hash = MD5.Create();
        //将输入字符串转换为字节数组并计算哈希数据  
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        //创建一个 Stringbuilder 来收集字节并创建字符串  
        StringBuilder sBuilder = new StringBuilder();
        //循环遍历哈希数据的每一个字节并格式化为十六进制字符串  
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        //返回十六进制字符串  
        return sBuilder.ToString();
    }

    private static string JsonTypeToString(LitJson.JsonType jsonType)
    {
        switch (jsonType)
        {
            case LitJson.JsonType.Float:
                return "float";
            case LitJson.JsonType.Array:
                return "List<#>";
            case LitJson.JsonType.Boolean:
                return "bool";
            case LitJson.JsonType.Double:
                return "double";
            case LitJson.JsonType.Int:
                return "int";
            case LitJson.JsonType.Long:
                return "long";
            case LitJson.JsonType.Object:
                return "object";
            case LitJson.JsonType.String:
                return "string";
        }
        return "object";
    }

    private static void JsonToCsharp(LitJson.JsonData jsonData)
    {
        foreach (KeyValuePair<string, LitJson.JsonData> key in jsonData)
        {
            if (key.Value != null)
            {
                string keytype = key.Value.GetJsonType() == LitJson.JsonType.Object ? key.Key.ToString() : JsonTypeToString(key.Value.GetJsonType());
                string keyValue = key.Value.GetJsonType() == LitJson.JsonType.Object ? key.Key.ToString().Substring(0, 1).ToUpper() + key.Key.Substring(1) + ";" : key.Key.ToString() + ";";

                if (key.Value.GetJsonType() == LitJson.JsonType.Object)
                {
                    consleStr += $"  public {keytype} {keyValue}  \n";
                    JsonToCsharp2(key.Value);
                }
                else if (key.Value.GetJsonType() == LitJson.JsonType.Array)
                {
                    consleStr += $"  public {keytype.Replace("#", keyValue.Replace(";", "")) } {keyValue}  \n";
                }
                else
                {
                    consleStr += $"  public {keytype}  {keyValue} \n";
                }
            }

        }
    }
    private static void JsonToCsharp2(LitJson.JsonData jsonData)
    {
        foreach (KeyValuePair<string, LitJson.JsonData> key in jsonData)
        {
            string keytype = key.Value.GetJsonType() == LitJson.JsonType.Object ? key.Key.ToString() : JsonTypeToString(key.Value.GetJsonType());
            string keyValue = key.Value.GetJsonType() == LitJson.JsonType.Object ? key.Key.ToString().Substring(0, 1).ToUpper() + key.Key.Substring(1) + ";" : key.Key.ToString() + ";";

            if (key.Value.GetJsonType() == LitJson.JsonType.Object)
            {
                consleStr += $"public class {keytype} ";
                consleStr += "\n{\n";
                JsonToCsharp2(key.Value);
                consleStr += "\n}\n";

            }
            else if (key.Value.GetJsonType() == LitJson.JsonType.Array)
            {
                consleStr += $"  public {keytype.Replace("#", keyValue.Replace(";", "")) } {keyValue}  \n";
                JsonToCsharp3(key.Value);
            }
            else
            {
                consleStr += $"  public {keytype}  {keyValue} \n";
            }
        }
    }

    private static void JsonToCsharp3(LitJson.JsonData jsonData)
    {
        for (int i = 0; i < 1; i++)
        {
            if (jsonData[i].GetJsonType() == LitJson.JsonType.Object)
            {
                JsonToCsharp(jsonData[i]);
            }
            else
            {
                Debug.Log(jsonData[i]);
            }
        }
    }
}
[System.Serializable]
public class Translation
{
    public string src { get; set; }
    public string dst { get; set; }
}

public enum Language
{
    //百度翻译API官网提供了多种语言，这里只列了几种
    自动监测,//
    中文, //
    英文,//
    日文,//
    西班牙文,//
    法文,//
    泰文,//
    阿拉伯文,//
    俄文,//
    葡萄牙文文,//
    德文,//
    希腊文,//
    越南文,//
    繁体中文,//
    粤语,//
}

[System.Serializable]
public class TranslationResult
{
    //错误码，翻译结果无法正常返回
    public string Error_code { get; set; }
    public string Error_msg { get; set; }
    public string from { get; set; }
    public string to { get; set; }
    public string Query { get; set; }
    //翻译正确，返回的结果
    //这里是数组的原因是百度翻译支持多个单词或多段文本的翻译，在发送的字段q中用换行符（\n）分隔
    public Translation[] trans_result { get; set; }
}