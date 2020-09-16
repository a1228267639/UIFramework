using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tools;

public class TestT : MonoBehaviour
{
    public Gesture gesture;
    public Text text;
    public Button button;
    public UIImage img;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("");
        //DownloadMgr.Instance.DownloadAsync(downloadUnit);

        gesture.touchEvent += Gesture_touchEvent;
    }



    public void StartIET()
    {
        this.StartIE("UpdateGesture", Tools.Tool.WaitUpdateTimeAction(() =>
        {
            gesture.UpdateGesture();
        }, 0.05f));
    }
    public void StopIET()
    {
        this.StopIE("UpdateGesture");
    }

    private void Gesture_touchEvent(TouchMoveDir moveDir, Vector2 deltaPosition)
    {
        text.text = moveDir + "" + deltaPosition.ToString();
    }

    private void Gesture_touchDownEvent(Vector2 deltaPosition)
    {
        text.text = deltaPosition.ToString();
    }

    private void Gesture_touchUpEvent(Vector2 deltaPosition)
    {
        text.text = deltaPosition.ToString();
    }

    private void Gesture_touchRigEvent(Vector2 deltaPosition)
    {
        text.text = deltaPosition.ToString();
    }

    private void Gesture_touchLeftEvent(Vector2 deltaPosition)
    {
        text.text = deltaPosition.ToString();
    }
    public PiovtDirection center;
    // Update is called once per frame
    void Update()
    {
        img.transform.GetRectTransform().SetPiovtDirection(center);
    }
}
