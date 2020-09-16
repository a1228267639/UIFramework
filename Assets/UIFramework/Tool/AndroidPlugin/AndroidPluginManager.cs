using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class AndroidPluginManager : MonoBehaviour
{
    private static AndroidPluginManager _instance;
    public AndroidJavaClass UnityAndroidJavaClass;
    public AndroidJavaObject UnityAndroidActivity;
    public AndroidJavaClass customToolClass;
    public AndroidJavaObject customToolObject;


    public static AndroidPluginManager Instance
    {
        get
        {

            if (FindObjectOfType<AndroidPluginManager>() != null)
            {
                _instance = FindObjectOfType<AndroidPluginManager>();
            }
            else
            {
                GameObject go = new GameObject("AndroidPluginManager");
                _instance = go.GetAddComponent<AndroidPluginManager>();
            }
            return _instance;
        }
    }

    // Start is called before the first frame update
    public void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        UnityAndroidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        UnityAndroidActivity = UnityAndroidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
        customToolClass = new AndroidJavaClass("com.Unity.Tools.UTool");
        customToolObject = customToolClass.CallStatic<AndroidJavaObject>("instance");
        customToolObject.Call("Init", UnityAndroidActivity);
#endif
    }



}
