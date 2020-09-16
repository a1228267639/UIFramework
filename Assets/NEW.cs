using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
public class NEW : MonoBehaviour
{

    public Transform source;
    // Start is called before the first frame update
    void Start()
    {
        this.StartIE("SetLoopRotateX", source.SetLoopRotateX(2));
        this.StartIE("SetLoopRotateY", source.SetLoopRotateY(2));
        this.StartIE("SetLoopRotateZ", source.SetLoopRotateZ(2));
        this.Log(Tool.GetSystemLanguage);

   
    }

    public void OnGUI()
    {
        if (GUI.Button(new Rect(20,20,550,200), "AndroidToast"))
        {
            Tool.AndroidToast(Tool.StatusBarHegiht.ToString());
        }
        if (GUI.Button(new Rect(20, 220, 550, 200), "GetBatteryLevel"))
        {
            
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
