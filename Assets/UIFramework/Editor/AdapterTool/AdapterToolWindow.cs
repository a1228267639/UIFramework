using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public enum ScreenOrientation
{
    Portrait,//竖屏
    landscape_L,//左横屏
    landscape_R,//右横屏
}
public enum GameViewSizeType
{
    AspectRatio, FixedResolution
}
public class AdapterToolWindow : EditorWindow
{
    static AdapterToolWindow window;

    public static Image image = null;
    public static GameViewSizeType viewSizeType = GameViewSizeType.FixedResolution;
    static List<DeviceConfig.Device> DeviceList;
    static string ConfigPath = @"Assets\UIFramework\Editor\Json\Devices.json";
    static bool IsSave;
    private Vector2 m_DeviceModelTablePosition = Vector2.zero;
    [MenuItem("Tools/AdapterTools/一键生成到桌面———横屏", false, 0)]
    private static void Onekey1()
    {
        Config.DefaultScreenOrientation = ScreenOrientation.landscape_R;
        InitImage();
        image.StartCoroutine(AutoCaputure());
    }

    [MenuItem("Tools/AdapterTools/一键生成到桌面———竖屏", false, 0)]
    private static void Onekey2()
    {
        Config.DefaultScreenOrientation = ScreenOrientation.Portrait;
        InitImage();
        image.StartCoroutine(AutoCaputure());
    }
    [MenuItem("Tools/AdapterTools/设备列表", false, 0)]
    private static void Init()
    {
        window = EditorWindow.GetWindow<AdapterToolWindow>();
        window.titleContent = new GUIContent("设备编辑器", EditorGUIUtility.IconContent("EditorSettings Icon").image);
        window.maxSize = new Vector2(600, 800);
        window.minSize = new Vector2(600, 800);
        window.Show();

        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(ConfigPath);
        DeviceConfig.Root root = JsonMapper.ToObject<DeviceConfig.Root>(textAsset.text);
        DeviceList = new List<DeviceConfig.Device>();

        foreach (var item in root.Devices)
        {
            DeviceConfig.Device device = new DeviceConfig.Device();
            device.id = item.id;
            device.brand = item.brand;
            device.model = item.model;
            device.width = item.width;
            device.height = item.height;
            device.enable = item.enable;
            DeviceList.Add(device);
        }
    }
    private void DrawEnableItme(DeviceConfig.Device obj, string Title, List<DeviceConfig.Device> Dic, float width = 30f)
    {
        DeviceConfig.Device device = null;
        device = Dic.Find(Ds => Ds.id == obj.id);
        EditorGUILayout.LabelField(new GUIContent(Title, obj.enable.ToString()), EditorStyles.label, GUILayout.Width(width));
        bool value = EditorGUILayout.Toggle(obj.enable, GUILayout.Width(position.width / 4 - 100));
        if (value != obj.enable)
        {
            IsSave = true;
            Dic.ForEach(Dc =>
            {
                if (Dc.id == device.id)
                {
                    device.enable = value;
                }
            });
        }
    }
    private void DrawBrandItme(DeviceConfig.Device obj, string Title, List<DeviceConfig.Device> Dic, float width = 30f)
    {
        DeviceConfig.Device device = null;
        device = Dic.Find(Ds => Ds.id == obj.id);
        EditorGUILayout.LabelField(new GUIContent(Title, obj.brand.ToString()), EditorStyles.label, GUILayout.Width(width));
        string value = EditorGUILayout.TextField(obj.brand.ToString(), GUILayout.Width(position.width / 4 - 50));
        if (value != obj.brand.ToString())
        {
            IsSave = true;
            Dic.ForEach(Dc =>
            {
                if (Dc.id == device.id)
                {
                    device.brand = value;
                }
            });
        }
    }
    private void DrawModelItme(DeviceConfig.Device obj, string Title, List<DeviceConfig.Device> Dic, float width = 30f)
    {
        DeviceConfig.Device device = null;
        device = Dic.Find(Ds => Ds.id == obj.id);
        EditorGUILayout.LabelField(new GUIContent(Title, obj.model.ToString()), EditorStyles.label, GUILayout.Width(width));
        string value = EditorGUILayout.TextField(obj.model.ToString(), GUILayout.Width(position.width / 4 - 50));
        if (value != obj.model.ToString())
        {
            IsSave = true;
            Dic.ForEach(Dc =>
            {
                if (Dc.id == device.id)
                {
                    device.model = value;
                }
            });
        }
    }
    private void DrawWidthtItme(DeviceConfig.Device obj, string Title, List<DeviceConfig.Device> Dic, float width = 20f)
    {
        DeviceConfig.Device device = null;
        device = Dic.Find(Ds => Ds.id == obj.id);
        EditorGUILayout.LabelField(new GUIContent(Title, obj.width.ToString()), EditorStyles.label, GUILayout.Width(width));
        string value = EditorGUILayout.TextField(obj.width.ToString(), GUILayout.Width(position.width / 4 - 80));
        if (value != obj.width.ToString())
        {
            IsSave = true;
            Dic.ForEach(Dc =>
            {
                if (Dc.id == device.id)
                {
                    device.width = int.Parse(value);
                }
            });
        }
    }
    private void DrawHeghtItme(DeviceConfig.Device obj, string Title, List<DeviceConfig.Device> Dic, float width = 20f)
    {
        DeviceConfig.Device device = null;
        device = Dic.Find(Ds => Ds.id == obj.id);
        EditorGUILayout.LabelField(new GUIContent(Title, obj.height.ToString()), EditorStyles.label, GUILayout.Width(width));
        string value = EditorGUILayout.TextField(obj.height.ToString(), GUILayout.Width(position.width / 4 - 80));
        if (value != obj.height.ToString())
        {
            IsSave = true;
            Dic.ForEach(Dc =>
            {
                if (Dc.id == device.id)
                {
                    device.height = int.Parse(value);
                }
            });
        }
    }







    private void OnGUI()
    {
        m_DeviceModelTablePosition = EditorGUILayout.BeginScrollView(m_DeviceModelTablePosition, GUILayout.Width(position.width));

        for (int i = 0; i < DeviceList.Count; i++)
        {
            InspectorTools.BeginVertical();
            EditorGUILayout.BeginHorizontal();

            DrawBrandItme(DeviceList[i], "品牌", DeviceList);
            DrawModelItme(DeviceList[i], "机型", DeviceList);
            DrawWidthtItme(DeviceList[i], "宽", DeviceList);
            DrawHeghtItme(DeviceList[i], "高", DeviceList);
            DrawEnableItme(DeviceList[i], "激活", DeviceList);
            EditorGUILayout.EndHorizontal();
            InspectorTools.EndVertical();
        }

        EditorGUILayout.EndScrollView();
        if (GUILayout.Button(new GUIContent("添加", EditorGUIUtility.IconContent("d_winbtn_mac_max_h").image), GUI.skin.button, GUILayout.Height(30)))
        {
            m_DeviceModelTablePosition = new Vector2(m_DeviceModelTablePosition.x, DeviceList.Count * 14);
            DeviceConfig.Device device = new DeviceConfig.Device();
            device.id = DeviceList.Count + 1;
            device.brand = "";
            device.model = "";
            device.width = 0;
            device.height = 0;
            device.enable = true;
            DeviceList.Add(device);
        }
        if (GUILayout.Button(new GUIContent("保存", EditorGUIUtility.IconContent("d_winbtn_mac_max_h").image), GUI.skin.button, GUILayout.Height(30)))
        {
            if (EditorUtility.DisplayDialog("标题", "是否保存", "确认", "取消"))
            {
                using (StreamWriter sw = new StreamWriter(ConfigPath))
                {
                    string json = LitJson.JsonMapper.ToJson(DeviceList);
                    sw.WriteLine("{\"Devices\":" + json + "}");
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }


    static MethodInfo getGroup;
    static object gameViewSizesInstance;

    [MenuItem("Tools/AdapterTools/测试", false, 0)]
    private static void Ste()
    {
        Dictionary<string, List<Device>> dic = Config.GetCaputureDevice();

        var sizesType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSizes");
        var singleType = typeof(ScriptableSingleton<>).MakeGenericType(sizesType);
        var instanceProp = singleType.GetProperty("instance");
        getGroup = sizesType.GetMethod("GetGroup");
        gameViewSizesInstance = instanceProp.GetValue(null, null);

        GameViewSizeType viewSizeType = GameViewSizeType.FixedResolution;
        foreach (var item in dic)
        {
            List<Device> allDevice = item.Value;

            for (int i = 0; i < allDevice.Count; i++)
            {
                Device device = allDevice[i];


                SetGameWindowSize(device, Config.DefaultScreenOrientation);

                var group = GetGroup(GetCurrentGroupType());
                var addCustomSize = getGroup.ReturnType.GetMethod("AddCustomSize"); // or group.GetType().
                var gvsType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSize");
                var ctor = gvsType.GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(string) });
                var newSize = ctor.Invoke(new object[] { (int)viewSizeType, device.resolution.x, device.resolution.y, device.GetName(ScreenOrientation.Portrait) });
                addCustomSize.Invoke(group, new object[] { newSize });



                //Type gameViewType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
                // EditorWindow window = GetWindow(gameViewType);

                //MethodInfo get_currentGameViewSize = gameViewType.GetMethod("AddCustomSize", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                //object gameViewSize = get_currentGameViewSize.Invoke(window, new object[] { });
                //Type gameViewSizeType = gameViewSize.GetType();

                //MethodInfo widthMethodInfo = gameViewSizeType.GetMethod("set_width", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                //widthMethodInfo.Invoke(gameViewSize, new object[] { device.resolution.x });

                //MethodInfo heightMethodInfo = gameViewSizeType.GetMethod("set_height", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                //heightMethodInfo.Invoke(gameViewSize, new object[] { device.resolution.y });

                //MethodInfo baseTextMethodInfo = gameViewSizeType.GetMethod("set_baseText", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                //baseTextMethodInfo.Invoke(gameViewSize, new object[] { device.GetName(ScreenOrientation.Portrait) });

            }
        }
        Debug.LogError("完成");
    }
    public static GameViewSizeGroupType GetCurrentGroupType()
    {
        var getCurrentGroupTypeProp = gameViewSizesInstance.GetType().GetProperty("currentGroupType");
        return (GameViewSizeGroupType)(int)getCurrentGroupTypeProp.GetValue(gameViewSizesInstance, null);
    }
    static object GetGroup(GameViewSizeGroupType type)
    {
        return getGroup.Invoke(gameViewSizesInstance, new object[] { (int)type });
    }

    private static void InitImage()
    {
        if (image != null)
        {
            return;
        }


        GameObject canvasGameObject = new GameObject("Canvas");
        Canvas canvas = canvasGameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler canvasScaler = canvasGameObject.AddComponent<CanvasScaler>();

        GameObject imageGameObject = new GameObject("Image");
        imageGameObject.transform.parent = canvasGameObject.transform;
        imageGameObject.transform.localPosition = Vector3.zero;

        image = imageGameObject.AddComponent<Image>();
    }

    private static void DestroyImage()
    {
        if (image != null && image.transform.parent != null)
        {
            Destroy(image.transform.parent.gameObject);
        }
    }

    public static IEnumerator AutoCaputure()
    {
        Dictionary<string, List<Device>> dic = Config.GetCaputureDevice();

        foreach (var item in dic)
        {
            List<Device> allDevice = item.Value;

            for (int i = 0; i < allDevice.Count; i++)
            {
                Device device = allDevice[i];
                if (!device.enable)
                {
                    continue;
                }
                SetGameWindowSize(device, Config.DefaultScreenOrientation);

                yield return new WaitForEndOfFrame();

                SetDeviceMask(device);

                yield return new WaitForEndOfFrame();

                Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

                screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                screenShot.Apply();

                byte[] bytes = screenShot.EncodeToPNG();

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory).Replace(@"\", "/");

                string outputPath = desktopPath + "/截图/" + item.Key + "/";

                if (!System.IO.Directory.Exists(outputPath))
                {
                    System.IO.Directory.CreateDirectory(outputPath);
                }

                string filename = outputPath + "/" + device.GetName(ScreenOrientation.landscape_L) + ".png";
                System.IO.File.WriteAllBytes(filename, bytes);
                Debug.Log(string.Format("截屏了一张图片: {0}", filename));
            }
        }

        SetGameWindowSize(Config.DefaultDevice, Config.DefaultScreenOrientation);

        Debug.LogError("完成");
    }

    public static void SetGameWindowSize(Device device, ScreenOrientation screenOrientation)
    {
        switch (screenOrientation)
        {
            case ScreenOrientation.Portrait:
                {
                    SetGameWindowSize(device.resolution, device.GetName(screenOrientation));
                }
                break;
            case ScreenOrientation.landscape_L:
            case ScreenOrientation.landscape_R:
                {
                    SetGameWindowSize(new Vector2(device.resolution.y, device.resolution.x), device.GetName(screenOrientation));
                }
                break;
        }
    }

    public static void SetGameWindowSize(Vector2 resolution, string text)
    {
        SetGameWindowSize((int)resolution.x, (int)resolution.y, text);
    }

    public static void SetGameWindowSize(int width, int height, string text)
    {
        Type gameViewType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        EditorWindow window = GetWindow(gameViewType);

        MethodInfo get_currentGameViewSize = gameViewType.GetMethod("get_currentGameViewSize", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        object gameViewSize = get_currentGameViewSize.Invoke(window, new object[] { });
        Type gameViewSizeType = gameViewSize.GetType();

        MethodInfo widthMethodInfo = gameViewSizeType.GetMethod("set_width", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        widthMethodInfo.Invoke(gameViewSize, new object[] { width });

        MethodInfo heightMethodInfo = gameViewSizeType.GetMethod("set_height", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        heightMethodInfo.Invoke(gameViewSize, new object[] { height });

        MethodInfo baseTextMethodInfo = gameViewSizeType.GetMethod("set_baseText", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        baseTextMethodInfo.Invoke(gameViewSize, new object[] { text });
    }

    public static void SetDeviceMask(Device deviceInfo)
    {
        if (deviceInfo == null)
        {
            return;
        }

        if (image == null)
        {
            InitImage();
        }

        string name = string.Format("{0} {1}_{2}.png", deviceInfo.brand, deviceInfo.model, (int)Config.DefaultScreenOrientation);

        Texture2D texture2D = EditorGUIUtility.Load(name) as Texture2D;

        if (texture2D == null)
        {
            image.enabled = false;
            return;
        }
        image.enabled = true;
        image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
        image.SetNativeSize();
    }

    private void OnDestroy()
    {
        DestroyImage();
    }
}
