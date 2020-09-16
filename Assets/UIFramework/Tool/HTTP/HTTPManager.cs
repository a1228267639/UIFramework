using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UIFramework;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.Networking;
//2020/05/08/16:08:01
//YZJ_UIFramework
//.cs
//杨智杰
//云笔

public class HTTPManager : MonoBehaviour
{
    private static HTTPManager instance = null;
    public static HTTPManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("HTTPManager");
                instance = go.AddComponent<HTTPManager>();
            }
            return instance;
        }
    }

    private Dictionary<string, string> mRrequestHeader = new Dictionary<string, string>();  //  header
    public HttpClient Client { get; private set; }
    private Task currentLock = Task.CompletedTask;
    private void Awake()
    {
        instance = gameObject.GetComponent<HTTPManager>();
        //mRrequestHeader.Add("Authorization", "Basice WJ0b29sczozMTcxNTUyQjcwMTkwNUI2NkNEN0ZENDM3Q0JFRjMxNjYyOTU0RDU5");
        //mRrequestHeader.Add("Content-Type", "application/json;charset=UTF-8");
        //mRrequestHeader.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36");

        Client = new HttpClient();
        // 设置 7 秒超时
        Client.Timeout = new TimeSpan(0, 0, 7);
    }
    public IEnumerator Get(string IEName, string URL, Action<UnityWebRequest, object[]> DoneCallBack, Action<UnityWebRequest> ProgressCallBack = null, Action<UnityWebRequest> ErrorCallBack = null, params object[] arg)
    {
        this.Log($"{IEName} : {URL}");
        using (UnityWebRequest webRequest = new UnityWebRequest(URL, "GET"))
        {
            webRequest.method = UnityWebRequest.kHttpVerbGET;
            webRequest.timeout = 20;
            webRequest.SetRequestHeader("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36");
            foreach (var v in mRrequestHeader)
            {
                webRequest.SetRequestHeader(v.Key, v.Value);
            }
            yield return webRequest.SendWebRequest();
            while (!webRequest.isDone)
            {
                ProgressCallBack?.Invoke(webRequest);
                yield return 0;
            }
            if (webRequest.error != null)
            {
                ErrorCallBack?.Invoke(webRequest);
            }
            if (webRequest.isDone)
            {
                DoneCallBack?.Invoke(webRequest, arg);
            }
        }
    }
    public IEnumerator Get(string IEName, string URL, Action<byte[], object[]> DoneCallBack, Action<UnityWebRequest> ProgressCallBack = null, Action<UnityWebRequest> ErrorCallBack = null, params object[] arg)
    {
        this.Log($"{IEName} : {URL}");
        float startTime = Time.realtimeSinceStartup;
        using (UnityWebRequest webRequest = new UnityWebRequest(URL, "GET"))
        {
            webRequest.method = UnityWebRequest.kHttpVerbGET;
            webRequest.timeout = 20;
            webRequest.SetRequestHeader("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36");
            foreach (var v in mRrequestHeader)
            {
                webRequest.SetRequestHeader(v.Key, v.Value);
            }
            webRequest.SendWebRequest();
            while (!webRequest.isDone)
            {
                ProgressCallBack?.Invoke(webRequest);
                yield return 0;
            }
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                ErrorCallBack?.Invoke(webRequest);
            }
            if (webRequest.isDone)
            {
                try
                {
                    DoneCallBack?.Invoke(webRequest.downloadHandler.data, arg);
                }
                catch (Exception e)
                {
                    this.Log(e);
                }
                //this.Log($"{IEName}：用时:{(Time.realtimeSinceStartup - startTime).ToString("F4")}");
            }
        }
    }
    public IEnumerator Post(string IEName, string URL, string jsonData, Action<UnityWebRequest, object[]> DoneCallBack, Action<UnityWebRequest> ProgressCallBack = null, Action<UnityWebRequest> ErrorCallBack = null, params object[] arg)
    {
        this.Log($"{IEName} : {URL}");
        using (UnityWebRequest webRequest = new UnityWebRequest(URL, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.method = UnityWebRequest.kHttpVerbPOST;
            webRequest.timeout = 20;
            webRequest.SetRequestHeader("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36");
            foreach (var v in mRrequestHeader)
            {
                webRequest.SetRequestHeader(v.Key, v.Value);
            }
            yield return webRequest.SendWebRequest();
            while (!webRequest.isDone)
            {
                ProgressCallBack?.Invoke(webRequest);
                yield return 0;
            }
            if (webRequest.error != null)
            {
                ErrorCallBack?.Invoke(webRequest);
            }
            if (webRequest.isDone)
            {
                DoneCallBack?.Invoke(webRequest, arg);
            }
        }
    }
    public IEnumerator Put(string IEName, string URL, string jsonData, Action<UnityWebRequest, object[]> DoneCallBack, Action<UnityWebRequest> ProgressCallBack = null, Action<UnityWebRequest> ErrorCallBack = null, params object[] arg)
    {
        this.Log($"{IEName} : {URL}");
        using (UnityWebRequest webRequest = new UnityWebRequest(URL, "PUT"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.method = UnityWebRequest.kHttpVerbPUT;
            webRequest.timeout = 20;
            webRequest.SetRequestHeader("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36");
            foreach (var v in mRrequestHeader)
            {
                webRequest.SetRequestHeader(v.Key, v.Value);
            }
            yield return webRequest.SendWebRequest();
            while (!webRequest.isDone)
            {
                ProgressCallBack?.Invoke(webRequest);
                yield return 0;
            }
            if (webRequest.error != null)
            {
                ErrorCallBack?.Invoke(webRequest);
            }
            if (webRequest.isDone)
            {
                DoneCallBack?.Invoke(webRequest, arg);
            }
        }
    }
    public IEnumerator GetIMG(string IEName, string URL, Action<Texture, object[]> DoneCallBack, Action<UnityWebRequest> ProgressCallBack = null, Action<UnityWebRequest> ErrorCallBack = null, params object[] arg)
    {
        this.Log($"{IEName} : {URL}");
        float startTime = Time.realtimeSinceStartup;
        using (UnityWebRequest webRequest = new UnityWebRequest(URL, "GET"))
        {
            DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
            webRequest.downloadHandler = texDl;
            webRequest.method = UnityWebRequest.kHttpVerbGET;
            webRequest.timeout = 20;
            webRequest.SetRequestHeader("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36");
            foreach (var v in mRrequestHeader)
            {
                webRequest.SetRequestHeader(v.Key, v.Value);
            }
            webRequest.SendWebRequest();
            while (!webRequest.isDone)
            {
                ProgressCallBack?.Invoke(webRequest);
                yield return 0;
            }
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                ErrorCallBack?.Invoke(webRequest);
            }
            if (webRequest.isDone)
            {
                try
                {
                    DoneCallBack?.Invoke(texDl.texture, arg);
                }
                catch (Exception e)
                {
                    this.Log(e);
                }
                //this.Log($"{IEName}：用时:{(Time.realtimeSinceStartup - startTime).ToString("F4")}");
            }
        }
    }
    public IEnumerator GetMP3(string IEName, string URL, Action<AudioClip, object[]> DoneCallBack, Action<UnityWebRequest> ProgressCallBack = null, Action<UnityWebRequest> ErrorCallBack = null, params object[] arg)
    {
        this.Log($"{IEName} : {URL}");
        float startTime = Time.realtimeSinceStartup;
        using (var www = UnityWebRequestMultimedia.GetAudioClip(URL, AudioType.MPEG))
        {
            www.method = UnityWebRequest.kHttpVerbGET;
            www.timeout = 20;
            www.SetRequestHeader("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36");
            www.SendWebRequest();
            while (!www.isDone)
            {
                ProgressCallBack?.Invoke(www);
                yield return 0;
            }
            if (www.isNetworkError || www.isHttpError)
            {
                ErrorCallBack?.Invoke(www);
            }
            if (www.isDone)
            {
                try
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    DoneCallBack?.Invoke(clip, arg);
                }
                catch (Exception e)
                {
                    this.Log(e);
                }
                // this.Log($"{IEName}：用时:{(Time.realtimeSinceStartup - startTime).ToString("F4")}");
            }
        }
    }
    public void GetIMG(string IEName, string URL, Action<Texture, object[]> DoneCallBack, Action<UnityWebRequest> ProgressCallBack = null, Action<UnityWebRequest> ErrorCallBack = null, MonoBehaviour mono = null, params object[] arg)
    {
        if (mono)
        {
            mono.StartIE(IEName, GetIMG(IEName, URL, DoneCallBack, ProgressCallBack, ErrorCallBack, arg));
        }
        else
        {
            this.StartIE(IEName, GetIMG(IEName, URL, DoneCallBack, ProgressCallBack, ErrorCallBack, arg));
        }
    }
    public void GetMP3(string IEName, string URL, Action<AudioClip, object[]> DoneCallBack, Action<UnityWebRequest> ProgressCallBack = null, Action<UnityWebRequest> ErrorCallBack = null, MonoBehaviour mono = null, params object[] arg)
    {
        if (mono)
        {
            mono.StartIE(IEName, GetMP3(IEName, URL, DoneCallBack, ProgressCallBack, ErrorCallBack, arg));
        }
        else
        {
            this.StartIE(IEName, GetMP3(IEName, URL, DoneCallBack, ProgressCallBack, ErrorCallBack, arg));
        }
    }
    public void Get(string IEName, string URL, Action<byte[], object[]> DoneCallBack, Action<UnityWebRequest> ProgressCallBack = null, Action<UnityWebRequest> ErrorCallBack = null, MonoBehaviour mono = null, params object[] arg)
    {
        if (mono)
        {
            mono.StartIE(IEName, Get(IEName, URL, DoneCallBack, ProgressCallBack, ErrorCallBack, arg));
        }
        else
        {
            this.StartIE(IEName, Get(IEName, URL, DoneCallBack, ProgressCallBack, ErrorCallBack, arg));
        }
    }
    public void Get(string IEName, string URL, Action<UnityWebRequest, object[]> DoneCallBack, Action<UnityWebRequest> ProgressCallBack = null, Action<UnityWebRequest> ErrorCallBack = null, MonoBehaviour mono = null, params object[] arg)
    {
        if (mono)
        {
            mono.StartIE(IEName, Get(IEName, URL, DoneCallBack, ProgressCallBack, ErrorCallBack, arg));
        }
        else
        {
            this.StartIE(IEName, Get(IEName, URL, DoneCallBack, ProgressCallBack, ErrorCallBack, arg));
        }
    }
    public void Post(string IEName, string URL, string jsonData, Action<UnityWebRequest, object[]> DoneCallBack, Action<UnityWebRequest> ProgressCallBack = null, Action<UnityWebRequest> ErrorCallBack = null, MonoBehaviour mono = null, params object[] arg)
    {
        if (mono)
        {
            mono.StartIE(IEName, Post(IEName, URL, jsonData, DoneCallBack, ProgressCallBack, ErrorCallBack, arg));
        }
        else
        {
            this.StartIE(IEName, Post(IEName, URL, jsonData, DoneCallBack, ProgressCallBack, ErrorCallBack, arg));
        }
    }
    public void Put(string IEName, string URL, string jsonData, Action<UnityWebRequest, object[]> DoneCallBack, Action<UnityWebRequest> ProgressCallBack = null, Action<UnityWebRequest> ErrorCallBack = null, MonoBehaviour mono = null, params object[] arg)
    {
        if (mono)
        {
            mono.StartIE(IEName, Put(IEName, URL, jsonData, DoneCallBack, ProgressCallBack, ErrorCallBack, arg));
        }
        else
        {
            this.StartIE(IEName, Put(IEName, URL, jsonData, DoneCallBack, ProgressCallBack, ErrorCallBack, arg));
        }
    }

    public async void GetAsync<T>(string URL, Action<object> CallBack)
    {
        TaskCompletionSource<bool> taskLock = new TaskCompletionSource<bool>();
        await GetLock(taskLock.Task);

        HttpResponseMessage responseMessage;
        try
        {
            responseMessage = await Client.GetAsync(URL);
            Debug.Log($"responseMessage: {responseMessage}");
        }
        catch (Exception e)
        {
            CallBack(null);
            Debug.Log($"GetAsync Execute Done {URL}\nWith Error Message: {e.Message}");
            taskLock.SetResult(true);
            return;
        }
        if (responseMessage != null && responseMessage.IsSuccessStatusCode)
        {
            string responseString = await responseMessage.Content.ReadAsStringAsync();
            Debug.Log($"GetAsync Response {responseString}");

            Type type = typeof(T);
            if (type.FullName == "System.String")
            {
                CallBack(responseString);
                //callback((T)TypeDescriptor.GetConverter(type).ConvertTo(null, null, responseString, type));
            }
            else if (type.IsValueType)
            {
                CallBack((T)TypeDescriptor.GetConverter(type).ConvertFromString(null, null, responseString));
            }
            else
            {
                T jsonObject;
                try
                {
                    jsonObject = LitJson.JsonMapper.ToObject<T>(responseString);
                }
                catch
                {
                    Debug.Log("JSON返回异常");
                    CallBack(null);
                    return;
                }

                CallBack(jsonObject);
            }
        }
        else
        {
            CallBack(null);
            //throw new Exception($"HttpClient GetAsync Error {responseMessage.StatusCode}");
        }
    }

    public async void GetAsync(string URL, Action<Texture2D> CallBack)
    {
        TaskCompletionSource<bool> taskLock = new TaskCompletionSource<bool>();
        await GetLock(taskLock.Task);
        Stream urlContents;
        //HttpResponseMessage responseMessage;
        try
        {
            var bytes = Client.GetByteArrayAsync(URL).Result;
            Debug.Log(bytes.Length);
            if (bytes != null)
            {
                Texture2D Texture = new Texture2D(462, 354, TextureFormat.RGB24, false);
                Texture.LoadImage(bytes);
                CallBack(Texture);
            }

        }
        catch (Exception e)
        {
            Debug.Log(e.Data);
            CallBack(null);
        }
    }

    private Task GetLock(Task waitLock)
    {
        Task previousLock = currentLock;
        currentLock = waitLock;
        return previousLock;
    }

    private void OnDestroy()
    {
        Client.Dispose();
    }

}