using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : MonoBehaviour
{
    public AndroidJavaClass androidJavaClass;
    public AndroidJavaObject androidJavaObject;
    public AndroidJavaClass customToolClass;
    public AndroidJavaObject customToolObject;
    public  string DeviceCode;
    public UnityEngine.UI.Text DeviceCode_Text;
    // Start is called before the first frame update
    void Awake()
    {
        androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //获取MainActivity的实例对象
        androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass cusaJC = new AndroidJavaClass("com.risenb.wifiproject.MainActivity");
        AndroidJavaObject cusaJO = cusaJC.CallStatic<AndroidJavaObject>("instance");
        cusaJO.Call("Init", androidJavaObject);
        DeviceCode = cusaJO.Call<string>("GetDeviceId");
#if UNITY_ANDROID && !UNITY_EDITOR

#endif
        if (string.IsNullOrEmpty(DeviceCode)) //安卓获取的设备码如果为空 就获取unity自带的设备识别码
        {
            DeviceCode = SystemInfo.deviceUniqueIdentifier;
        }
        DeviceCode_Text.text = "序列号 ：\n" + DeviceCode;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
