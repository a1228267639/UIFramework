using System.Collections.Generic;
using UnityEngine;
public class UnityBroadcastHelper
{
    public interface IBroadcastListener
    {
        void OnReceive(string action, Dictionary<string, string> dictionary);
    }
    private class ListenerAdapter : AndroidJavaProxy
    {
        readonly IBroadcastListener listener;
        readonly UnityBroadcastHelper helper;
        public ListenerAdapter(IBroadcastListener listener, UnityBroadcastHelper helper) : base("com.Unity.Tools.BroadcastListener")
        {
            this.listener = listener;
            this.helper = helper;
        }
        void onReceive(string action)
        {
            AndroidJavaObject javaObject = helper.javaObject;
            if (!javaObject.Call<bool>("hasKeyValue"))
            {
                return;
            }
            string[] keys = javaObject.Call<string[]>("getKeys");
            string[] values = javaObject.Call<string[]>("getValues");
            javaObject.Call("pop");
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            Debug.Log("onReceive: dictionary: " + dictionary);
            int n = keys.Length;
            for (int i = 0; i < n; i++)
            {
                dictionary[keys[i]] = values[i];
            }
            listener.OnReceive(action, dictionary);
        }
    }
    private readonly AndroidJavaObject javaObject;
    private UnityBroadcastHelper(string[] actions, IBroadcastListener listener)
    {
        ListenerAdapter adapter = new ListenerAdapter(listener, this);
        javaObject = new AndroidJavaObject("com.Unity.Tools.UnityBroadcastHelper", actions, adapter, AndroidPluginManager.Instance.UnityAndroidActivity);
    }
    public static UnityBroadcastHelper Register(string[] actions, IBroadcastListener listener)
    {
        return new UnityBroadcastHelper(actions, listener);
    }
    public void Stop()
    {
        javaObject.Call("stop");
    }
}