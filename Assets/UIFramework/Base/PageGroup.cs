using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PageGroup : MonoBehaviour
{
    private List<BasePanel> _PageList = new List<BasePanel>();

    public void ShowPage(string name, bool isMain, params object[] pars)
    {
        if (isMain)
        {
            while (this.transform.childCount > 0)
            {
                Transform page = this.transform.GetChild(0);
                page.GetComponent<BasePanel>().ExcuteFree();
                page.GetComponent<BasePanel>().OnHide();
                SignalObjectManager.Instance.Despawn(page);
            }
            _PageList.Clear();
        }
        ShowNewPage(name, pars);
    }

    public void ReplacePage(string name, params object[] pars)
    {
        if (_PageList.Count > 0)
        {
            BasePanel childPage = _PageList[_PageList.Count - 1];
            childPage.ExcuteFree();
            SignalObjectManager.Instance.Despawn(childPage.transform);
            _PageList.Remove(childPage);
        }
        ShowNewPage(name, pars);
    }

    /// <summary>
    /// 显示新的页面
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pars"></param>
    private void ShowNewPage(string name, params object[] pars)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform page = this.transform.GetChild(i);
            page.gameObject.GetComponent<BasePanel>().OnHide();
        }

        Transform childPage = SignalObjectManager.Instance.Spawn(name).transform;
        if (childPage == null)
            return;
        childPage.GetComponent<RectTransform>().SetParent(this.transform, false);
        //childPage.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //childPage.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        //childPage.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);

        BasePanel pageBase = childPage.GetComponent<BasePanel>();
        if (pars != null)
        {
            for (int i = 0; i < pars.Length; i++)
            {
                pageBase.SetPar(i, pars[i]);
            }
        }
        pageBase.ExcuteInit();
        pageBase.OnOpen();
        _PageList.Add(pageBase);
    }

    public void ClosePage()
    {
        if (_PageList.Count <= 1)
            return;
        BasePanel childPage = _PageList[_PageList.Count - 1];
        childPage.ExcuteFree();
        childPage.OnClose();
        SignalObjectManager.Instance.Despawn(childPage.transform);
        _PageList.Remove(childPage);

        if (_PageList.Count >= 0)
        {
            BasePanel lastPage = _PageList[_PageList.Count - 1];
            Dictionary<int, object> pars = new Dictionary<int, object>(lastPage._Pars);
            lastPage.ExcuteFree();
            lastPage._Pars = pars;
            lastPage.ExcuteInit();
            lastPage.OnRefresh();
        }
    }

    public int PageCount()
    {
        return _PageList.Count;
    }
}
