/*文件说明
 * CreateTime:  2019/04/30/13:20:56
 * Projectname:  FST_Project
 * FileName:  Gesture.cs
 * Developers:  五峰杰
 * Creator:  杨智杰
 * Description:
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TouchMoveDir
{
    Right,//右
    Left,//左
    Up,//上
    Down,//下
    Idle//静止
}

public class Gesture : MonoBehaviour
{

    private Touch oldTouch1;  //上次触摸点1(手指1)
    private Touch oldTouch2;  //上次触摸点2(手指2)
    private TouchMoveDir moveDir;
    public GameObject targetObject;
    public GameObject newTargetObject;

    public GameObject oldObject;
    private float minDis = 5;
    public float minZ = -30;
    public float maxZ = 30;
    public float SminScale, SmaxScale;
    public float TminScale, TmaxScale;

    public delegate void TouchRightEvent(Vector2 deltaPosition);
    public delegate void TouchLeftEvent(Vector2 deltaPosition);
    public delegate void TouchUpEvent(Vector2 deltaPosition);
    public delegate void TouchDownEvent(Vector2 deltaPosition);
    public delegate void TouchIdleEvent(Vector2 deltaPosition);
    public delegate void TouchEvent(TouchMoveDir moveDir, Vector2 deltaPosition);


    public event TouchRightEvent touchRigEvent;
    public event TouchLeftEvent touchLeftEvent;
    public event TouchUpEvent touchUpEvent;
    public event TouchDownEvent touchDownEvent;
    public event TouchIdleEvent touchIdleEvent;
    public event TouchEvent touchEvent;
    private Vector2 deltaDir;

    //public UnityEngine.UI.Text text;
    private void Start()
    {
        //oldObject = gameObject;
        Application.targetFrameRate = 90;
    }

    public void UpdateGesture()
    {
        //没有触摸，就是触摸点为0
        if (Input.touchCount <= 0)
        {
            return;
        }
        //如果是UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            if (Input.GetTouch(0).deltaPosition.sqrMagnitude > minDis)
            {
                deltaDir = Input.GetTouch(0).deltaPosition;
                if (Mathf.Abs(deltaDir.x) > Mathf.Abs(deltaDir.y))
                {
                    moveDir = deltaDir.x > 0 ? TouchMoveDir.Right : TouchMoveDir.Left;
                }
                if (Mathf.Abs(deltaDir.y) > Mathf.Abs(deltaDir.x))
                {
                    moveDir = deltaDir.y > 0 ? TouchMoveDir.Up : TouchMoveDir.Down;
                }
            }
        }
        //if (CheckAngle(camera_main.transform.localEulerAngles.y) >= maxY)
        //{
        //    camera_main.transform.localEulerAngles = new Vector3(0, maxY, 0);
        //}
        //if (CheckAngle(camera_main.transform.localEulerAngles.y) <= minY)
        //{
        //    camera_main.transform.localEulerAngles = new Vector3(0, minY, 0);
        //}
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            moveDir = TouchMoveDir.Idle;
        }
        if (moveDir == TouchMoveDir.Right && Input.touchCount == 1)
        {
            //  targetObject.transform.localEulerAngles -= Vector3.up * 2.8f;
            touchEvent?.Invoke(moveDir, deltaDir);
        }
        if (moveDir == TouchMoveDir.Left && Input.touchCount == 1)
        {
            // targetObject.transform.localEulerAngles += Vector3.up * 2.8f;
            touchEvent?.Invoke(moveDir, deltaDir);
        }
        if (moveDir == TouchMoveDir.Down && Input.touchCount == 1)
        {
            //if (CheckAngle(targetObject.transform.parent.transform.localEulerAngles.x) <= minZ)
            //{
            //    targetObject.transform.parent.transform.localEulerAngles = new Vector3(minZ,targetObject.transform.parent.transform.localEulerAngles.y, targetObject.transform.parent.transform.localEulerAngles.z);
            //}
            //else
            //{
            //    targetObject.transform.parent.transform.localEulerAngles -= Vector3.right * 2.8f;
            //}
            touchEvent?.Invoke(moveDir, deltaDir);
        }
        if (moveDir == TouchMoveDir.Up && Input.touchCount == 1)
        {
            //if (CheckAngle(targetObject.transform.parent.transform.localEulerAngles.x) >= maxZ)
            //{

            //    targetObject.transform.parent.transform.localEulerAngles = new Vector3(maxZ,targetObject.transform.parent.transform.localEulerAngles.y, targetObject.transform.parent.transform.localEulerAngles.z);
            //}
            //else
            //{
            //    targetObject.transform.parent.transform.localEulerAngles += Vector3.right * 2.8f;
            //}
            touchEvent?.Invoke(moveDir, deltaDir);
        }
        //多点触摸, 放大缩小
        //Touch newTouch1 = Input.GetTouch(0);
        //Touch newTouch2 = Input.GetTouch(1);
        ////第2点刚开始接触屏幕, 只记录，不做处理
        //if (newTouch2.phase == TouchPhase.Began)
        //{
        //    oldTouch2 = newTouch2;
        //    oldTouch1 = newTouch1;
        //    return;
        //}

        ////计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型
        //float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        //float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);
        ////两个距离之差，为正表示放大手势， 为负表示缩小手势
        //float offset = newDistance - oldDistance;
        ////放大因子， 一个像素按 0.01倍来算(100可调整)
        //float scaleFactor = offset / 1000f;
        //Vector3 localScale = targetObject.transform.localScale;
        //Vector3 scale = new Vector3(localScale.x + scaleFactor,
        //    localScale.y + scaleFactor,
        //    localScale.z + scaleFactor);
        ////在什么情况下进行缩放

        //    if (scale.x >= SminScale && scale.y >= SminScale && scale.z >= SminScale && scale.x <= SmaxScale && scale.y <= SmaxScale && scale.z <= SmaxScale) //0.45f  1.15f
        //    {
        //        targetObject.transform.localScale = scale;
        //    }


        //    if (scale.x >= TminScale && scale.y >= TminScale && scale.z >= TminScale && scale.x <= TmaxScale && scale.y <= TmaxScale && scale.z <= TmaxScale)//0.8f 1.5f
        //    {
        //        targetObject.transform.localScale = scale;
        //    }


        ////text.text = targetObject.name +"---"+ targetObject.transform.localScale.ToString() +"******" +oldObject.name +"----"+ oldObject.transform.lossyScale.ToString();
        ////记住最新的触摸点，下次使用
        //oldTouch1 = newTouch1;
        //oldTouch2 = newTouch2;
    }
    void Update()
    {


    }
    public float CheckAngle(float value)  // 将大于180度角进行以负数形式输出
    {
        float angle = value - 180;

        if (angle > 0)
        {
            print(angle - 180);
            return angle - 180;
        }

        if (value == 0)
        {
            return 0;
        }
        print(angle + 180);
        return angle + 180;
    }
}