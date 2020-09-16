using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AspectRatioFitter))]
[RequireComponent(typeof(UIImage))]
public class AspectRatioFitterHelper : MonoBehaviour
{

   [ContextMenu("Set Aspect Ratio")]
    void Start()
    {
        Rect rect = GetComponent<UIImage>().sprite.rect;
        AspectRatioFitter fitter = GetComponent<AspectRatioFitter>();
        fitter.aspectRatio = rect.width / rect.height;
    }


}
