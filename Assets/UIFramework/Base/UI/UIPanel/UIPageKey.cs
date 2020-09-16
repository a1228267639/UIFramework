using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPageKey : MonoBehaviour
{
    public string CustomComponentName = "";
    // Use this for initialization
    void Start()
    {

    }

    public virtual string ComponentName
    {
        get
        {
            if (!string.IsNullOrEmpty(CustomComponentName))
                return CustomComponentName;
            if (null != GetComponent<InputField>())
                return "InputField";
            if (null != GetComponent<Button>())
                return "Button";
            if (null != GetComponent<Animator>())
                return "Animator";
            if (null != GetComponent<Image>())
                return "Image";
            if (null != GetComponent<ScrollRect>())
                return "ScrollRect";
            if (null != GetComponent<Text>())
                return "Text";
            if (null != GetComponent<RawImage>())
                return "RawImage";
            if (null != GetComponent<Toggle>())
                return "Toggle";
            if (null != GetComponent<Slider>())
                return "Slider";
            if (null != GetComponent<Scrollbar>())
                return "Scrollbar";
            if (null != GetComponent<ToggleGroup>())
                return "ToggleGroup";
            if (null != GetComponent<Canvas>())
                return "Canvas";
            if (null != GetComponent<RectTransform>())
                return "RectTransform";
            return "Transform";
        }
    }
}
