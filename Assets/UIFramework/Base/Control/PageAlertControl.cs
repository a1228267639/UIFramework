using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using DG.Tweening;


public enum HintType
{
    ´íÎó,
    ¾¯¸æ,
    Ä¬ÈÏ
}

public class HinitEntity
{
    public string HintMessage;
    public HintType hintType;
    public bool isAnim;
    public HinitEntity()
    {

    }
    public HinitEntity(string message, HintType hintType, bool isAnim)
    {
        this.HintMessage = message;
        this.hintType = hintType;
        this.isAnim = isAnim;
    }

}


public class PageAlertControl : ObjectBase
{
    private Queue MessageQueue = new Queue();
    public UIImage HintIMG;
    public UIText HintMessage;
    public HintType hintType;
    private bool isOnShow;
    public CanvasGroup mCanvasGroup;
    public Color DefColor;
    public Color ErrorColor;
    public Color WarningColor;

    public void AddListener(HinitEntity message)
    {
        MessageQueue.Enqueue(message);
    }




    private void OnEnable()
    {
        switch (hintType)
        {
            case HintType.Ä¬ÈÏ:
                HintIMG.color = DefColor;
                break;
            case HintType.´íÎó:
                HintIMG.color = ErrorColor;
                break;
            case HintType.¾¯¸æ:
                HintIMG.color = WarningColor;
                break;
        }
    }



    public void ShowMessage(HinitEntity hinitEntity)
    {
        isOnShow = true;
        HintMessage.text = hinitEntity.HintMessage;
        switch (hinitEntity.hintType)
        {
            case HintType.Ä¬ÈÏ:
                HintIMG.color = DefColor;
                break;
            case HintType.´íÎó:
                HintIMG.color = ErrorColor;
                break;
            case HintType.¾¯¸æ:
                HintIMG.color = WarningColor;
                break;
        }
        if (hinitEntity.isAnim)
        {
            transform.GetRectTransform().DOAnchorPos(new Vector2(0, HintIMG.canvas.transform.GetRectTransform().rect.height * 0.2f), 0.2f).SetEase(Ease.Linear).OnComplete(() =>
              {
                  mCanvasGroup.DOFade(0, 0.2f).SetDelay(1).SetEase(Ease.Linear).OnComplete(() =>
                  {
                      mCanvasGroup.alpha = 1;
                      transform.GetRectTransform().anchoredPosition = new Vector2(0, 0);
                      isOnShow = false;
                  });
              });
        }
        else
        {
            mCanvasGroup.alpha = 1;
            transform.GetRectTransform().anchoredPosition = new Vector2(0, Screen.height / 4);
            this.StartIE("Hide", Tools.Tool.WaitTimeAction(() =>
            {
                isOnShow = false;
                transform.GetRectTransform().anchoredPosition = new Vector2(0, -105);

            }, 1));

        }
    }

    int testIndex;
    [ContextMenu("²âÊÔ")]
    private void Test()
    {

        int type = Random.Range(0, 3);
        switch (type)
        {
            case 0:
                AddListener(new HinitEntity("Ä¬ÈÏ" + (testIndex += 1), HintType.Ä¬ÈÏ, true));
                break;
            case 1:
                AddListener(new HinitEntity("¾¯¸æ" + (testIndex += 1), HintType.¾¯¸æ, true));
                break;
            case 2:
                AddListener(new HinitEntity("´íÎó" + (testIndex += 1), HintType.´íÎó, true));
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        this.StartIE("MessageQueue", Tools.Tool.WaitUpdateTimeAction(() =>
        {
            while (MessageQueue.Count > 0)
            {
                if (isOnShow)
                {
                    return;
                }
                if (!isOnShow)
                {
                    HinitEntity message = (HinitEntity)MessageQueue.Dequeue();
                    ShowMessage(message);
                }
            }
        }, 0.1f));
    }
}