using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PageItemControlLayoutBase
{
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

    private string _TextContent_1 = string.Empty;
    public string TextContent_1
    {
        get
        {
            return _TextContent_1;
        }
        set
        {
            _TextContent_1 = value;
        }
    }
    private string _TextContent_2 = string.Empty;
    public string TextContent_2
    {
        get
        {
            return _TextContent_2;
        }
        set
        {
            _TextContent_2 = value;
        }
    }

    private int _TextContent_1fontSize = 25;
    public int TextContent_1fontSize
    {
        get
        {
            return _TextContent_1fontSize;
        }
        set
        {
            _TextContent_1fontSize = value;
        }
    }

    private int _TextContent_2fontSize = 20;
    public int TextContent_2fontSize
    {
        get
        {
            return _TextContent_2fontSize;
        }
        set
        {
            _TextContent_2fontSize = value;
        }
    }

    private Sprite _BGImg = null;
    public Sprite BGImg
    {
        get
        {
            return _BGImg;
        }
        set
        {
            _BGImg = value;
        }
    }

    private Sprite _HeadImg = null;
    public Sprite HeadImg
    {
        get
        {
            return _HeadImg;
        }
        set
        {
            _HeadImg = value;
        }
    }


    public Align _Align = Align.left;

    public PageItemControlLayoutBase(int ColorBg, string TextContent_1, string TextContent_2, int TextContent_1fontSize, int TextContent_2fontSize, Sprite BGImg, Sprite HeadImg)
    {
        this.ColorBg = ColorInt32.Int2Color(ColorBg);
        this.TextContent_1 = TextContent_1;
        this.TextContent_2 = TextContent_2;
        this.TextContent_1fontSize = TextContent_1fontSize;
        this.TextContent_2fontSize = TextContent_2fontSize;
        this.BGImg = BGImg;
        this.HeadImg = HeadImg;
    }
}
public class PageItemControlLayouta : PageItemControlLayoutBase
{
    public PageItemControlLayouta(int ColorBg = ColorInt32.white, string TextContent_1 = "", string TextContent_2 = "", int TextContent_1fontSize = 30, int TextContent_2fontSize = 30, Sprite BGImg = null, Sprite HeadImg = null) : base
        (ColorBg: ColorBg,
        TextContent_1: TextContent_1,
        TextContent_2: TextContent_2,
        TextContent_1fontSize: TextContent_1fontSize,
        TextContent_2fontSize: TextContent_2fontSize,
        BGImg: BGImg,
        HeadImg: HeadImg)
    {
        this.ColorBg = ColorInt32.Int2Color(ColorBg);
        this.TextContent_1 = TextContent_1;
        this.TextContent_2 = TextContent_2;
        this.TextContent_1fontSize = TextContent_1fontSize;
        this.TextContent_2fontSize = TextContent_2fontSize;
        this.BGImg = BGImg;
        this.HeadImg = HeadImg;
    }
}

public class PageItemControlBtnLayoutaBase
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
    public Vertical _vertical = Vertical.bottom;
    /// <summary>
    ///  Btn布局配置
    /// </summary>
    /// <param name="Text">文字</param>
    /// <param name="ColorBg">背景颜色</param>
    /// <param name="ColorFont">字体颜色</param>
    /// <param name="IMG">图片</param>
    /// <param name="FontSize">字体大小</param>
    public PageItemControlBtnLayoutaBase(string Text, int ColorBg, int ColorFont, Sprite IMG = null, int FontSize = 18)
    {
        _text = Text;
        _colorBg = ColorInt32.Int2Color(ColorBg);
        _colorFont = ColorInt32.Int2Color(ColorFont);
        _Img = IMG;
        _fontSize = FontSize;
    }
}

public class PageItemControlBtnLayouta : PageItemControlBtnLayoutaBase
{

    public PageItemControlBtnLayouta(string Text = "", int ColorBg = ColorInt32.white, int ColorFont = ColorInt32.black, Sprite IMG = null, int FontSize = 18) : base
        (Text: Text, ColorBg: ColorBg, ColorFont: ColorFont, IMG: IMG, FontSize: FontSize)
    {
        //Text = Text;
        //this.ColorBg = ColorInt32.Int2Color(ColorBg);
        //this.ColorFont = ColorInt32.Int2Color(ColorFont);
        //IMG = IMG;
        //FontSize = FontSize;
    }
}

public class PageItemControl : ObjectBase
{
    public Image ImgBg;
    public Button BtnBg;
    public Image ImgHeadIcon;
    public GameObject GoMobile;
    public GameObject GoEye;
    public Transform ContentList;
    public Text TextContent_1;
    public Text TextContent_2;
    public GameObject BtnList;
    public Button BtnTemplated;
    public Text TextTemplated;
    private HorizontalLayoutGroup horizontal;


    


    public void Init(PageItemControlLayouta pageItemControlLayouta, UIEventListener.VoidDelegate OnClickEvent = null)
    {
        base.ExcuteInit();

        ImgBg.color = pageItemControlLayouta.ColorBg;
        if (pageItemControlLayouta.HeadImg != null)
        {
            ImgHeadIcon.sprite = pageItemControlLayouta.HeadImg;
        }

        horizontal = BtnList.GetComponent<HorizontalLayoutGroup>();
        if (pageItemControlLayouta.TextContent_1.Length > 0)
        {
            TextContent_1.text = pageItemControlLayouta.TextContent_1;
            TextContent_1.fontSize = pageItemControlLayouta.TextContent_1fontSize;
        }
        else
        {
            TextContent_1.gameObject.SetActive(false);
        }
        if (pageItemControlLayouta.TextContent_2.Length > 0)
        {
            TextContent_2.text = pageItemControlLayouta.TextContent_2;
            TextContent_2.fontSize = pageItemControlLayouta.TextContent_2fontSize;
        }
        else
        {
            TextContent_2.gameObject.SetActive(false);
        }
        BtnBg.gameObject.SetActive(true);
        if (pageItemControlLayouta.BGImg != null)
        {
            ImgBg.sprite = pageItemControlLayouta.BGImg;
        }

        if (OnClickEvent != null)
        {
            UIEventListener.Get(BtnBg.gameObject).onClick = OnClickEvent;
        }
    }
    public void AddButton(PageItemControlBtnLayouta pageItemControlLayouta, UIEventListener.VoidDelegate OnClickEvent = null)
    {
        GameObject newBtn = GameObject.Instantiate<GameObject>(BtnTemplated.gameObject);
        newBtn.SetActive(true);
        newBtn.transform.SetParent(BtnList.transform, false);
        newBtn.GetComponentInChildren<Text>().text = pageItemControlLayouta.Text;
        newBtn.GetComponentInChildren<Text>().color = pageItemControlLayouta.ColorFont;
        newBtn.GetComponent<Image>().color = pageItemControlLayouta.ColorBg;
        RectTransform rectTransform = BtnList.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + 200, rectTransform.sizeDelta.y);
        //rectTransform.offsetMin = new Vector2(rectTransform.sizeDelta.x + 200, 40);
        if (pageItemControlLayouta.IMG != null)
        {
            newBtn.GetComponent<Image>().sprite = pageItemControlLayouta.IMG;

        }
        if (OnClickEvent != null)
        {
            UIEventListener.Get(newBtn).onClick = OnClickEvent;
        }

    }

    public void SetBtnList_Align(TextAnchor vertical)
    {
        horizontal.childAlignment = vertical;
    }
    public void AddContent()
    {

    }

}
