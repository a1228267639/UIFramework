/*文件说明
*创建时间：2020/01/15/15:18:00
*项目：UIFramework
*文件格式：.cs
*作者：云笔
*公司：杨智杰
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PageTitleLayoutBase
{
    private string _title = string.Empty;
    public string Title
    {
        get
        {
            return _title;
        }
        set
        {
            _title = value;
        }
    }

    private Color _colorBg = Color.white;
    public Color ColorBg
    {
        get
        {
            return _colorBg;
        }
        set
        {
            _colorBg = value;
        }
    }

    private Color _colorFont = Color.white;
    public Color ColorFont
    {
        get
        {
            return _colorFont;
        }
        set
        {
            _colorFont = value;
        }
    }

    private float _height = 100;
    public float Height
    {
        get
        {
            return _height;
        }
        set
        {
            _height = value;
        }
    }

    private int _fontSize = 18;
    public int FontSize
    {
        get
        {
            return _fontSize;
        }
        set
        {
            _fontSize = value;
        }
    }
    private Sprite _backImg;
    public Sprite BackImg
    {
        get
        {
            return _backImg;
        }
        set
        {
            _backImg = value;
        }
    }
    private Sprite _menuImg;

    public Sprite MenuImg
    {
        get
        {
            return _menuImg;
        }
        set
        {
            _menuImg = value;
        }
    }

    public Align _Align = Align.left;
    public PageTitleLayoutBase(string Title, Align _Align, int ColorBg, int ColorFont, Sprite BackImg, Sprite MenuImg, float Height, int FontSize)
    {
        this._Align = _Align;
        this.Title = Title;
        this.ColorBg = ColorInt32.Int2Color(ColorBg);
        this.ColorFont = ColorInt32.Int2Color(ColorFont);
        this.BackImg = BackImg;
        this.MenuImg = MenuImg;
        this.Height = Height;
        this.FontSize = FontSize;
    }
}

public class PageTitleLayout : PageTitleLayoutBase
{


    /// <summary>
    /// 标题布局配置
    /// </summary>
    /// <param name="Title">标题</param>
    /// <param name="ColorBg">背景颜色</param>
    /// <param name="ColorFont">字体颜色</param>
    /// <param name="BackImg">返回图片</param>
    /// <param name="MenuImg">菜单图片</param>
    /// <param name="Height">布局高</param>
    /// <param name="FontSize">字体大小</param>
    public PageTitleLayout(string Title = "", Align _Align = Align.left, int ColorBg = ColorInt32.white, int ColorFont = ColorInt32.black, Sprite BackImg = null, Sprite MenuImg = null, float Height = 100, int FontSize = 40) : base
        (Title: Title, _Align: _Align, ColorBg: ColorBg, ColorFont: ColorFont, BackImg: BackImg, MenuImg: MenuImg, Height: Height, FontSize: FontSize)
    {
        //Title = Title;
        //ColorBg = ColorBg;
        //ColorFont = ColorFont;
        //BackImg = BackImg;
        //MenuImg = MenuImg;
        //_height = Height;
        //_fontSize = FontSize;
    }
}

public class PageTitleBtnLayout
{
    private string _text = string.Empty;
    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value;
        }
    }

    private int _fontSize = 18;
    public int FontSize
    {
        get
        {
            return _fontSize;
        }
        set
        {
            _fontSize = value;
        }
    }
    private Color _colorBg = Color.white;
    public Color ColorBg
    {
        get
        {
            return _colorBg;
        }
        set
        {
            _colorBg = value;
        }
    }

    private Color _colorFont = Color.white;
    public Color ColorFont
    {
        get
        {
            return _colorFont;
        }
        set
        {
            _colorFont = value;
        }
    }

    private Sprite _Img;
    public Sprite IMG
    {
        get
        {
            return _Img;
        }
        set
        {
            _Img = value;
        }
    }

    private RectTransform _Parent;

    public RectTransform Parent
    {
        get
        {
            return _Parent;
        }
        set
        {
            _Parent = value;
        }
    }

    /// <summary>
    ///  Btn布局配置
    /// </summary>
    /// <param name="Text">文字</param>
    /// <param name="ColorBg">背景颜色</param>
    /// <param name="ColorFont">字体颜色</param>
    /// <param name="IMG">图片</param>
    /// <param name="FontSize">字体大小</param>
    public PageTitleBtnLayout(string Text, int ColorBg, int ColorFont,RectTransform Parent, Sprite IMG = null, int FontSize = 18)
    {
        _text = Text;
        _colorBg = ColorInt32.Int2Color(ColorBg);
        _colorFont = ColorInt32.Int2Color(ColorFont);
        _Img = IMG;
        _Parent = Parent;
        _fontSize = FontSize;
    }
}

public enum Align
{
    left,
    right,
    center
}
public enum Vertical
{
    top,
    bottom,
    center
}

public class PageTitleControl : ObjectBase
{

    public UIImage IMGBG;
    public UIButton BtnBack;
    public UIButton BtnTemplated;
    public UIText Title;
    public RectTransform TitleRight;
    public RectTransform TitleLeft;
    public RectTransform TitleCenter;




    /// <summary>
    /// 标题栏初始化
    /// </summary>
    /// <param name="pageTitleLayout"></param>
    /// <param name="backEvent"></param>
    public void Init(PageTitleLayout pageTitleLayout, UIEventListener.VoidDelegate backEvent = null)
    {
        base.ExcuteInit();
        //for (int i = 0; i < TitleRight.transform.childCount; i++)
        //{
        //    if (TitleRight.transform.GetChild(i).gameObject != BtnTemplated.gameObject)
        //        GameObject.Destroy(TitleRight.transform.GetChild(i).gameObject);
        //}
        IMGBG.color = pageTitleLayout.ColorBg;
        Title.color = pageTitleLayout.ColorFont;
        Title.text = pageTitleLayout.Title;
        Title.fontSize = pageTitleLayout.FontSize;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(0, pageTitleLayout.Height);

        if (backEvent != null)
        {
            BtnBack.gameObject.SetActive(true);
            if (pageTitleLayout.BackImg != null)
            {
                BtnBack.GetComponent<Image>().sprite = pageTitleLayout.BackImg;
            }
            UIEventListener.Get(BtnBack.gameObject).onClick = backEvent;
        }
        else
            BtnBack.gameObject.SetActive(false);
    }

    /// <summary>
    /// 添加Btn
    /// </summary>
    /// <param name="pageTitleBtnLayout"></param>
    /// <param name="btnEvent"></param>
    public void AddButton(PageTitleBtnLayout pageTitleBtnLayout, UIEventListener.VoidDelegate btnEvent = null)
    {
        GameObject newBtn = GameObject.Instantiate<GameObject>(BtnTemplated.gameObject);
        newBtn.SetActive(true);
        if (pageTitleBtnLayout.IMG != null)
        {
            newBtn.GetComponent<Image>().sprite = pageTitleBtnLayout.IMG;
            newBtn.GetComponent<Image>().color = pageTitleBtnLayout.ColorBg;
        }
        else
            newBtn.GetComponent<Image>().enabled = false;
        if (!string.IsNullOrEmpty(pageTitleBtnLayout.Text))
        {
            newBtn.transform.GetChild(0).GetComponent<Text>().text = pageTitleBtnLayout.Text;
            newBtn.transform.GetChild(0).GetComponent<Text>().color = pageTitleBtnLayout.ColorFont;
            newBtn.transform.GetChild(0).GetComponent<Text>().fontSize = pageTitleBtnLayout.FontSize;
        }
        newBtn.GetComponent<RectTransform>().SetParent(pageTitleBtnLayout.Parent);
        newBtn.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        newBtn.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        UIEventListener.Get(newBtn).onClick = btnEvent;
    }
}
