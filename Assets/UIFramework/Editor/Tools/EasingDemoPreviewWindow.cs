//********作者：Axuan*************
//********blog：https://me.csdn.net/XiaoGuiXuan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class EasingDemoPreviewWindow : EditorWindow
{
    [MenuItem("Window/Dotween Easing Demo")]
    public static void ShowWindow()
    {
        var window = GetWindow<EasingDemoPreviewWindow>("Dotween Easing Demo");
    }

    const int POINT_COUNT = 60;
    const float TIME_DELTATIME = 0.02f;
    const int POINT_LENGTH = 10;
    const float BALL_POS_Z = -50;

    Ease _ease = Ease.Linear;
    private int easeEnumLength = 0;

    Vector2 rectStartPos = new Vector2(50,200);

    private GameObject ballObj;
    private Mesh ballMesh;
    private Vector3 ballTmpPos = new Vector3(200, 200, -10);
    private Color ballColor = new Color(1, 0.35f, 0.117f, 0.5f);
    private Material ballMat;
    private float ballMoveTimer;
    private float ballMoveDuration = 5;
    private bool isBallMove = false;
    private Vector2 ballTargetPos;
    private Vector3 ballStartPos;
    private GameObject ballDirLight;

    private float overshootOrAmplitude = DOTween.defaultEaseOvershootOrAmplitude; // 过冲或振幅
    private float period = DOTween.defaultEasePeriod; // 周期

    private void OnEnable()
    {
        var easeArray = System.Enum.GetValues(typeof(Ease));
        easeEnumLength = easeArray.Length;

        if (ballObj==null)
        {
            ballObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ballObj.hideFlags = HideFlags.HideAndDontSave;
            ballObj.transform.localEulerAngles = new Vector3(0, 190, 0);
            ballObj.SetActive(false);

            var mesh = ballObj.GetComponent<MeshFilter>().sharedMesh;
            ballMesh = new Mesh();
            ballMesh.vertices = mesh.vertices;
            ballMesh.uv = mesh.uv;
            ballMesh.triangles = mesh.triangles;
            ballMesh.RecalculateBounds();
            ballMesh.RecalculateNormals();
        }
        
        if (ballMat == null)
        {
            ballMat = new Material(Shader.Find("Standard"));
            ballMat.SetColor("_Color", ballColor);
            ballMat.hideFlags = HideFlags.DontSave;
        }

        if(ballDirLight==null)
        {
            ballDirLight = new GameObject();
            var light = ballDirLight.AddComponent<Light>();
            light.type = LightType.Directional;
            ballDirLight.transform.localEulerAngles = new Vector3(320, 180, 0);
            ballDirLight.hideFlags = HideFlags.HideAndDontSave;
            ballDirLight.transform.localPosition = new Vector3(1000,1000,1000);
        }
        EditorApplication.update += Update;
    }

    private void OnDisable()
    {
        if (ballObj != null)
        {
            DestroyImmediate(ballObj);
        }
        if (ballMat != null)
        {
            DestroyImmediate(ballMat);
        }
        if (ballMesh != null)
        {
            DestroyImmediate(ballMesh);
        }
        if (ballDirLight != null)
        {
            DestroyImmediate(ballDirLight);
        }
        EditorApplication.update -= Update;
    }

    private void OnGUI()
    {
        HandleEvent();

        DrawBgGridUI();

        _ease = (Ease)EditorGUILayout.EnumPopup("Easing:", _ease);

        DrawCurveUI();

        DrawballUI();
    }
    
    void HandleEvent()
    {
        if(Event.current.type == EventType.KeyDown)
        {
            int e = (int)_ease;
            switch (Event.current.keyCode)
            {
                case KeyCode.UpArrow:
                    e--;
                    if (e < 0)
                    {
                        e = 0;
                    }
                    _ease = (Ease)e;
                    break;
                case KeyCode.DownArrow:
                    e++;
                    if (e > easeEnumLength - 1)
                    {
                        e = easeEnumLength - 1;
                    }
                    _ease = (Ease)e;
                    break;
            }
            isBallMove = false;
            Repaint();
        }

        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            if(Event.current.mousePosition.y < rectStartPos.y)
            {
                return;
            }
            isBallMove = true;
            ballStartPos = ballTmpPos;
            ballTargetPos = Event.current.mousePosition;
            ballMoveTimer = 0;
        }
    }

    private void Update()
    {
        if (isBallMove)
        {
            ballMoveTimer += TIME_DELTATIME;
            if (ballMoveTimer > ballMoveDuration)
            {
                ballMoveTimer = ballMoveDuration;
                isBallMove = false;
            }
            var lerp = Evaluate(_ease, ballMoveTimer, ballMoveDuration, overshootOrAmplitude, period);
            ballTmpPos.x = ballStartPos.x + (ballTargetPos.x - ballStartPos.x) * lerp;
            ballTmpPos.y = ballStartPos.y + (ballTargetPos.y - ballStartPos.y) * lerp;
            Repaint();
        }
    }

    void DrawBgGridUI()
    {
        
        //MCGrid.Draw(new Rect(rectStartPos.x, rectStartPos.y, POINT_COUNT * POINT_LENGTH, POINT_COUNT * POINT_LENGTH), rectStartPos, MCGrid.Type.BlackChecked);
    }

    void DrawCurveUI()
    {
        for (int i = 0; i < POINT_COUNT; i++)
        {
            var time = i * 1f;
            var value = POINT_COUNT * Evaluate(_ease, time, POINT_COUNT, overshootOrAmplitude, period);

            var p1 = new Vector2(rectStartPos.x + time * POINT_LENGTH, rectStartPos.y + POINT_COUNT * POINT_LENGTH - value * POINT_LENGTH);
            if (i >= POINT_COUNT - 1)
            {
                continue;
            }
            var time2 = (i + 1) * 1f;
            var value2 = POINT_COUNT * Evaluate(_ease, time2, POINT_COUNT, overshootOrAmplitude, period);
            var p2 = new Vector2(rectStartPos.x + time2 * POINT_LENGTH, rectStartPos.y + POINT_COUNT * POINT_LENGTH - value2 * POINT_LENGTH);


            Handles.color = Color.red;
            Handles.DrawLine(p1, p2);
        }
        Handles.color = Color.white;
    }

    void DrawballUI()
    {
        ballMoveDuration = EditorGUILayout.FloatField("duration:", ballMoveDuration);
        overshootOrAmplitude = EditorGUILayout.FloatField("overshootOrAmplitude:", overshootOrAmplitude);
        period = EditorGUILayout.FloatField("period:", period);
        EditorGUI.BeginChangeCheck();
        ballColor = EditorGUILayout.ColorField("ballColor:", ballColor);
        if (EditorGUI.EndChangeCheck())
        {
            ballMat.SetColor("_Color", ballColor);
        }
       // ballDirLight.transform.localEulerAngles = EditorGUILayout.Vector3Field("ballDirLight:", ballDirLight.transform.localEulerAngles);

        Matrix4x4 m = new Matrix4x4();
        m.SetTRS(new Vector3(ballTmpPos.x, ballTmpPos.y, BALL_POS_Z), Quaternion.Euler(0,0,0), Vector3.one * 80);

        ballMat.SetPass(0);
        Graphics.DrawMeshNow(ballMesh, m * GUI.matrix);
    }

    public static float Evaluate(Ease easeType, float time, float duration, float overshootOrAmplitude, float period)
    {
        return DG.Tweening.Core.Easing.EaseManager.Evaluate(easeType,null,time, duration, overshootOrAmplitude, period);
    }


}
