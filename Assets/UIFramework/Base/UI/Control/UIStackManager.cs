using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStackManager : MonoBehaviour
{
    /// <summary>
    /// Õý³£
    /// </summary>
    public List<BasePanel> m_normalStack = new List<BasePanel>();

    public List<BasePanel> m_fixedStack = new List<BasePanel>();
    /// <summary>
    /// µ¯´°
    /// </summary>
    public List<BasePanel> m_popupStack = new List<BasePanel>();

    public List<BasePanel> m_topBarStack = new List<BasePanel>();


    public void OnUIOpen(BasePanel ui)
    {
        switch (ui.m_UIType)
        {
            case UIType.Fixed: m_fixedStack.Add(ui); break;
            case UIType.Normal: m_normalStack.Add(ui); break;
            case UIType.PopUp: m_popupStack.Add(ui); break;
            case UIType.TopBar: m_topBarStack.Add(ui); break;
        }
    }

    public void OnUIClose(BasePanel ui)
    {
        switch (ui.m_UIType)
        {
            case UIType.Fixed: m_fixedStack.Remove(ui); break;
            case UIType.Normal: m_normalStack.Remove(ui); break;
            case UIType.PopUp: m_popupStack.Remove(ui); break;
            case UIType.TopBar: m_topBarStack.Remove(ui); break;
        }
    }
    public void CloseLastUIWindow(UIType uiType = UIType.Normal)
    {
        BasePanel ui = GetLastUI(uiType);

        if (ui != null)
        {
            UIManager.CloseUIWindow(ui);
        }
    }

    public BasePanel GetLastUI(UIType uiType)
    {
        switch (uiType)
        {
            case UIType.Fixed:
                if (m_fixedStack.Count > 0)
                    return m_fixedStack[m_fixedStack.Count - 1];
                else
                    return null;
            case UIType.Normal:
                if (m_normalStack.Count > 0)
                    return m_normalStack[m_normalStack.Count - 1];
                else
                    return null;
            case UIType.PopUp:
                if (m_popupStack.Count > 0)
                    return m_popupStack[m_popupStack.Count - 1];
                else
                    return null;
            case UIType.TopBar:
                if (m_topBarStack.Count > 0)
                    return m_topBarStack[m_topBarStack.Count - 1];
                else
                    return null;
        }

        throw new System.Exception("CloseLastUIWindow does not support GameUI");
    }
}
