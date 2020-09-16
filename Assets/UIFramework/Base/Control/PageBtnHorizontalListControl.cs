using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class PageBtnHorizontalListControl : BasePanel
{
    public HorizontalLayoutGroup HorizontalLayout;
    public List<GameObject> BtnList = new List<GameObject>();
    
    /// <summary>
    /// 按钮状态委托
    /// </summary>
    public delegate void BtnState();
    /// <summary>
    /// 按钮初始化事件
    /// </summary>
    public event BtnState BtnCancelState;

    public GameObject Btn1;
    public GameObject Btn2;
    public GameObject Btn3;
    public GameObject Btn4;
    public GameObject Btn5;

    // Use this for initialization
    void Start()
    {
        Init();
        BtnCancelState += Btn1Even;
        BtnCancelState += Btn2Even;
        BtnCancelState += Btn3Even;
        BtnCancelState += Btn4Even;
        BtnCancelState += Btn5Even;
    }

    private void Btn1Even()
    {
        Debug.Log("Btn1 初始化状态");
    }
    private void Btn2Even()
    {
        Debug.Log("Btn2 初始化状态");
    }
    private void Btn3Even()
    {
        Debug.Log("Btn3 初始化状态");
    }
    private void Btn4Even()
    {
        Debug.Log("Btn4 初始化状态");
    }
    private void Btn5Even()
    {
        Debug.Log("Btn5 初始化状态");
    }

    void Init()
    {
        //AddBtnList(Btn1, Btn1SelectEvent);
        //AddBtnList(Btn2, Btn2SelectEvent);
        //AddBtnList(Btn3, Btn3SelectEvent);

    }

    void Btn1SelectEvent()
    {
        Debug.Log("点击到Btn1");

    }
    void Btn2SelectEvent()
    {
        Debug.Log("点击到Btn2");

    }
    void Btn3SelectEvent()
    {
        Debug.Log("点击到Btn3");

    }
    void Btn4SelectEvent()
    {
        Debug.Log("点击到Btn4");

    }
    void Btn5SelectEvent()
    {
        Debug.Log("点击到Btn5");
      
    }

    public void AddBtnList(GameObject Btn, UnityAction btnSelectState)
    {
        BtnList.Add(Btn);
        Btn.GetComponent<Button>().onClick.AddListener(btnSelectState);
        Btn.GetComponent<Button>().onClick.AddListener(() => { BtnCancelState?.Invoke(); });
    }

}
