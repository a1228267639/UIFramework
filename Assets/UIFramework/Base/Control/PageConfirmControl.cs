using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Tools;

public class PageConfirmLayout
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

    private string _yesText = string.Empty;
    public string YesBtnText
    {
        get
        {
            return _yesText;
        }
        set
        {
            _yesText = value;
        }
    }
    private string _noText = string.Empty;
    public string NoBtnText
    {
        get
        {
            return _noText;
        }
        set
        {
            _noText = value;
        }
    }
    private string _contentText = string.Empty;
    public string ContentText
    {
        get
        {
            return _contentText;
        }
        set
        {
            _contentText = value;
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

    private Color _contentTextColor = Color.black;
    public Color ContentTextColor
    {
        get
        {
            return _contentTextColor;
        }
        set
        {
            _contentTextColor = value;
        }
    }

    private Color _yesBtnColor = Color.white;
    public Color YesBtnColor
    {
        get
        {
            return _yesBtnColor;
        }
        set
        {
            _yesBtnColor = value;
        }
    }
    private Color _noBtnColor = Color.white;
    public Color NoBtnColor
    {
        get
        {
            return _noBtnColor;
        }
        set
        {
            _noBtnColor = value;
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

    private int _fontSize = 40;
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
    private Sprite _YesBtnImg;
    public Sprite YesBtnImg
    {
        get
        {
            return _YesBtnImg;
        }
        set
        {
            _YesBtnImg = value;
        }
    }
    private Sprite _noImg;
    public Sprite NoBtnImg
    {
        get
        {
            return _noImg;
        }
        set
        {
            _noImg = value;
        }
    }

    /// <summary>
    /// 标题布局配置
    /// </summary>
    /// <param name="Title">标题</param>
    /// <param name="ContentText">内容</param>
    /// <param name="YesBtnText">按钮文字</param>
    /// <param name="NoBtnText">按钮文字</param>
    /// <param name="ColorBg">背景颜色</param>
    /// <param name="ContentTextColor">字体颜色</param>
    /// <param name="YesBtnColor">确定按钮的颜色</param>
    /// <param name="NoBtnColor">取消按钮的颜色</param>
    /// <param name="YesBtnImg">返回图片</param>
    /// <param name="NoBtnImg">菜单图片</param>
    /// <param name="Height">布局高</param>
    /// <param name="FontSize">字体大小</param>
    public PageConfirmLayout(string Title, string ContentText, string YesBtnText, string NoBtnText, int ColorBg, int ContentTextColor, int YesBtnColor, int NoBtnColor, Sprite YesBtnImg, Sprite NoBtnImg, float Height, int FontSize)
    {
        this.Title = Title;
        this.ContentText = ContentText;
        this.YesBtnText = YesBtnText;
        this.NoBtnText = NoBtnText;
        this.ColorBg = ColorInt32.Int2Color(ColorBg);
        this.ContentTextColor = ColorInt32.Int2Color(ContentTextColor);
        this.YesBtnColor = ColorInt32.Int2Color(YesBtnColor);
        this.NoBtnColor = ColorInt32.Int2Color(NoBtnColor);
        this.YesBtnImg = YesBtnImg;
        this.NoBtnImg = NoBtnImg;
        this.Height = Height;
        this.FontSize = FontSize;
    }
    public PageConfirmLayout()
    {

    }

}

public class PageConfirmBtnLayout
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

    /// <summary>
    ///  Btn布局配置
    /// </summary>
    /// <param name="Text">文字</param>
    /// <param name="ColorBg">背景颜色</param>
    /// <param name="ColorFont">字体颜色</param>
    /// <param name="IMG">图片</param>
    /// <param name="FontSize">字体大小</param>
    public PageConfirmBtnLayout(string Text, Color ColorBg, Color ColorFont, Sprite IMG = null, int FontSize = 18)
    {
        _text = Text;
        _colorBg = ColorBg;
        _colorFont = ColorFont;
        _Img = IMG;
        _fontSize = FontSize;
    }
    public PageConfirmBtnLayout()
    {

    }
}

public class PageConfirmControl : ObjectBase
{
    public RectTransform ConfirmIMG;
    public UIImage ImgBg;
    public UIText ContentText;
    public UIButton YesBtn;
    public UIButton NoBtn;
    public RectTransform BtnContent;

    public void Init(PageConfirmLayout pageConfirmLayout, UIEventListener.VoidDelegate yesEvent = null, UIEventListener.VoidDelegate noEvent = null)
    {
        base.ExcuteInit();
        this.gameObject.SetActive(true);
        transform.GetComponent<CanvasGroup>().alpha = 1;
        //ConfirmIMG.sizeDelta = new Vector2(transform.GetRectTransform().rect.width * 0.75f, transform.GetRectTransform().rect.height * 0.26f);
        //ContentText.rectTransform.sizeDelta = new Vector2(ConfirmIMG.rect.width * 0.875f, ConfirmIMG.rect.height * 0.4f);
        //BtnContent.sizeDelta = new Vector2(ConfirmIMG.rect.width * 0.875f, ConfirmIMG.rect.height * 0.4f);
        //YesBtn.GetRectTransform().sizeDelta = new Vector2(BtnContent.rect.width * 0.37f, BtnContent.rect.height * 0.6f);
        //NoBtn.GetRectTransform().sizeDelta = new Vector2(BtnContent.rect.width * 0.37f, BtnContent.rect.height * 0.6f);

        ConfirmIMG.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        ConfirmIMG.transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);

        ImgBg.color = pageConfirmLayout.ColorBg;
        ContentText.color = pageConfirmLayout.ContentTextColor;
        ContentText.text = pageConfirmLayout.ContentText;
        ContentText.fontSize = pageConfirmLayout.FontSize;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, pageConfirmLayout.Height);
        if (yesEvent != null)
        {
            YesBtn.gameObject.SetActive(true);
            if (pageConfirmLayout.YesBtnImg != null)
            {
                YesBtn.GetComponent<Image>().sprite = pageConfirmLayout.YesBtnImg;
            }
            YesBtn.GetComponent<Image>().color = pageConfirmLayout.YesBtnColor;
            YesBtn.GetComponentInChildren<Text>().text = pageConfirmLayout.YesBtnText;
            UIEventListener.Get(YesBtn.gameObject).onClick = yesEvent;
            YesBtn.onClick.RemoveAllListeners();
            YesBtn.onClick.AddListener(() => { HideConfirm(); });
        }
        else
            YesBtn.gameObject.SetActive(false);
        if (noEvent != null)
        {
            NoBtn.gameObject.SetActive(true);
            if (pageConfirmLayout.NoBtnImg != null)
            {
                NoBtn.GetComponent<Image>().sprite = pageConfirmLayout.NoBtnImg;
            }
            NoBtn.GetComponent<Image>().color = pageConfirmLayout.NoBtnColor;
            NoBtn.GetComponentInChildren<Text>().text = pageConfirmLayout.NoBtnText;
            UIEventListener.Get(NoBtn.gameObject).onClick = noEvent;
            NoBtn.onClick.RemoveAllListeners();
            NoBtn.onClick.AddListener(() => { HideConfirm(); });
        }
        else
            NoBtn.gameObject.SetActive(false);
    }


    public void HideConfirm()
    {
        transform.GetComponent<CanvasGroup>().DOFade(0f, 0.2f).SetEase(Ease.Linear);
        ConfirmIMG.transform.DOScale(0.8f, 0.3f).SetEase(Ease.Linear);
    }

    [ContextMenu("测试")]
    public void TestInit()
    {
        PageConfirmLayout pageConfirmBtnLayout = new PageConfirmLayout();
        pageConfirmBtnLayout.ContentText = "测试123";
        pageConfirmBtnLayout.YesBtnText = "YES";
        pageConfirmBtnLayout.NoBtnText = "No";
        Init(pageConfirmBtnLayout,
         (eve1) =>
        {
            this.Log("YES");
        }, (eve2) =>
        {
            this.Log("No");
        });
    }

}