using LitJson;
using System.Collections.Generic;
using UnityEngine;


public static class Config
{
    public static ScreenOrientation DefaultScreenOrientation = ScreenOrientation.landscape_R;

    public static Device DefaultDevice = new Device(0, "", "", 750, 1334, true);

    public static List<Device> AllDevice = new List<Device>()
    {
        // new Device("iPhone", "11 Pro Max", 1242, 2688),
        //new Device("iPhone", "11 Pro", 1125, 2436),
        //new Device("iPhone", "11", 828, 1792),
        //new Device("iPhone", "XR", 828, 1792),
        //new Device("iPhone", "XS Max", 1242, 2688),
        //new Device("iPhone", "XS", 1125, 2436),
        //new Device("iPhone", "X", 1125, 2436),
        //new Device("iPhone", "8 Plus", 1080, 1920),
        //new Device("iPhone", "8", 750, 1334),
        //new Device("iPhone", "7 Plus", 1080, 1920),
        //new Device("iPhone", "7", 750, 1334),
        //new Device("iPhone", "6 Plus", 1242, 2208),
        //new Device("iPhone", "6", 750, 1334),
        //new Device("iPhone", "5", 640, 1136),

        //new Device("iPad", "mini 2", 1536, 2048),
        //new Device("iPad", "mini 3", 1536, 2048),
        //new Device("iPad", "mini 4", 1536, 2048),
        //new Device("iPad", "(第五代)", 1536, 2048),
        //new Device("iPad", "(第六代)", 1536, 2048),
        //new Device("iPad", "Air (第一代)", 1536, 2048),
        //new Device("iPad", "Air 2", 1536, 2048),
        //new Device("iPad", "Pro 9.7英寸", 1536, 2048),
        //new Device("iPad", "Pro 10.5英寸", 1668, 2224),
        //new Device("iPad", "Pro 12.9英寸 (第一代)", 2048, 2732),
        //new Device("iPad", "Pro 12.9英寸 (第二代)", 2048, 2732),

        //new Device("iPad", "mini 5", 1536, 2048),
        //new Device("iPad", "(第七代)", 1620, 2160),
        //new Device("iPad", "Air 3", 1668, 2224),
        //new Device("iPad", "Pro 11英寸", 1668, 2388),
        //new Device("iPad", "Pro 12.9英寸 (第三代)", 2048, 2732),
    };


    public static List<Device> GetDevices()
    {
        TextAsset textAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(@"Assets\UIFramework\Editor\Json\Devices.json");
        DeviceConfig.Root root = JsonMapper.ToObject<DeviceConfig.Root>(textAsset.text);
        List<Device> list = new List<Device>();
        foreach (var item in root.Devices)
        {
            Device device = new Device(item.id, item.brand, item.model, item.width, item.height, item.enable);
            list.Add(device);
        }
        return list;
    }

    public static List<Device> GetAllResolution()
    {
        List<Device> list = new List<Device>();

        for (int i = 0; i < AllDevice.Count; i++)
        {
            Device device = AllDevice[i];

            bool isAdd = true;

            for (int j = 0; j < list.Count; j++)
            {
                if (device.resolution == list[j].resolution)
                {
                    isAdd = false;
                    break;
                }
            }

            if (isAdd)
            {
                list.Add(new Device(0, "", "", (int)device.resolution.x, (int)device.resolution.y, true));
            }
        }

        return list;
    }

    public static Dictionary<string, List<Device>> GetCaputureDevice()
    {
        Dictionary<string, List<Device>> dic = new Dictionary<string, List<Device>>();

        // dic.Add("分辨率", GetAllResolution());
        //    dic.Add("设备", AllDevice);
        dic.Add("设备", GetDevices());

        return dic;
    }
}

namespace DeviceConfig
{
    public class Device
    {
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int height { get; set; }
        public bool enable { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Device> Devices { get; set; }
    }
}