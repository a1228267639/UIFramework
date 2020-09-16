using UnityEngine;


public class Device
{
    public int id;
    public string brand = string.Empty;
    public string model = string.Empty;
    public Vector2 resolution = Vector2.zero;
    public bool enable;

    public Device(int id, string brand, string model, int width, int height,bool enable)
    {
        this.id = id;
        this.brand = brand;
        this.model = model;
        resolution.x = width;
        resolution.y = height;
        this.enable = enable;
    }

    public string GetName(ScreenOrientation screenOrientation)
    {
        string name = string.Empty;
        switch (screenOrientation)
        {
            case ScreenOrientation.Portrait:
                name = string.Format("{0} {1} {2}x{3}", brand, model, resolution.x, resolution.y);
                break;
            case ScreenOrientation.landscape_L:
            case ScreenOrientation.landscape_R:
                name = string.Format("{0} {1} {2}x{3}", brand, model, resolution.y, resolution.x);
                break;
        }

        return name.Trim();
    }
}
