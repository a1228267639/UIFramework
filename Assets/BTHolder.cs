using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class BTHolder : MonoBehaviour, UnityBroadcastHelper.IBroadcastListener
{
    UnityBroadcastHelper helper;
    void Start()
    {
        this.QuitApp();
        //if (helper == null)
        //{
        //    helper = UnityBroadcastHelper.Register(
        //        new string[] { "android.intent.action.SCREEN_ON", "android.intent.action.SCREEN_OFF", "android.intent.action.CLOSE_SYSTEM_DIALOGS" }, this);
        //}
        StartCoroutine(PermissionIEt());
    }

    IEnumerator PermissionIEt()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
            Tools.Tool.AndroidToast("请求用户权限" + Permission.CoarseLocation);
        }
        else
        {
            Tools.Tool.AndroidToast("具有请求用户权限" + Permission.CoarseLocation);
        }
        yield return new WaitForSeconds(1);
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Tools.Tool.AndroidToast("请求用户权限" + Permission.FineLocation);
        }
        else
        {
            Tools.Tool.AndroidToast("具有请求用户权限" + Permission.FineLocation);
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 250, 200), ""))
        {
            //   AndroidJavaObject cusaJC = new AndroidJavaObject("com.Unity.Tools.DynamicPermissionTool", AndroidPluginManager.Instance.UnityAndroidActivity);
            // AndroidJavaObject cusaJO = cusaJC.CallStatic<AndroidJavaObject>("instance");
            // cusaJO.Call("Init", AndroidPluginManager.Instance.UnityAndroidActivity);
            // cusaJC.Call("requestNecessaryPermissions", AndroidPluginManager.Instance.UnityAndroidActivity, "android.permission.ACCESS_FINE_LOCATION", 1);

            AndroidJavaClass javaClass = new AndroidJavaClass("com.Unity.Tools.WifiHelper");
            AndroidJavaObject javaObject = javaClass.CallStatic<AndroidJavaObject>("instance");
            javaObject.Call("Init", AndroidPluginManager.Instance.UnityAndroidActivity);
            Tools.Tool.AndroidToast(javaObject.Call<bool>("isWifiEnable").ToString());
            javaObject.Call<bool>("openWifi");

            //if (!javaObject.Call<bool>("isWifiEnable"))
            //{
            //    Tools.Tool.AndroidToast("打开WIFI");
            //    javaObject.Call<bool>("openWifi");
            //}
            //else
            //{
            //    Tools.Tool.AndroidToast("关闭WIFI");
            //    javaObject.Call<bool>("closeWifi");
            //}


            //Tools.Tool.AndroidToast(getWIFIState());
        }
    }
    const int WIFI_STATE_DISABLED = 1;

    const int WIFI_STATE_ENABLED = 3;

    const int WIFI_STATE_DISABLING = 0;

    const int WIFI_STATE_ENABLING = 2;

    public static string getWIFIState()
    {
        string stateString = "WIFI_STATE_UNKNOWN";
#if UNITY_ANDROID
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject wifiManager = currentActivity.Call<AndroidJavaObject>("getSystemService", new AndroidJavaObject("java.lang.String", "wifi"));
        if (wifiManager != null)
        {
            int wifiState = wifiManager.Call<int>("getWifiState");
            switch (wifiState)
            {
                case WIFI_STATE_DISABLED:
                    Debug.Log("WIFI:DISABLED");
                    stateString = "WIFI_STATE_DISABLED";
                    break;
                case WIFI_STATE_DISABLING:
                    Debug.Log("WIFI:DISABLING");
                    stateString = "WIFI_STATE_DISABLING";
                    break;
                case WIFI_STATE_ENABLED:
                    Debug.Log("WIFI:ENABLED");
                    stateString = "WIFI_STATE_ENABLED";
                    break;
                case WIFI_STATE_ENABLING:
                    Debug.Log("WIFI:ENABLING");
                    stateString = "WIFI_STATE_ENABLING";
                    break;
            }
        }
#endif
        return stateString;
    }

    public void OnReceive(string action, Dictionary<string, string> dictionary)
    {
        string value = "";
        foreach (var key in dictionary)
        {
            value += "Key  :" + key.Key + " Value  :" + key.Value + "\n";
        }
        Tools.Tool.AndroidToast(action + "\n" + value);
    }
}