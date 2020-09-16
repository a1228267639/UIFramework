using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.UI
{
    public class UIButton : Button, IUIInternationalised
    {
        [CustomLabel("语言Key")]
        [SerializeField]
        private string mLanguageKey;
        public string LanguageKey { get { return mLanguageKey; } set { mLanguageKey = value; } }
        [CustomLabel("颜色Key")]
        [SerializeField]
        private string mThemeColorKey;
        public string ThemeColorKey { get { return mThemeColorKey; } set { mThemeColorKey = value; } }

        public UIImage mUIImage { get { return this.GetAddComponent<UIImage>(); } set { } }

        public UIText mUIText;

        public void OnClick(UnityAction clickAction)
        {

            this.onClick.AddListener(clickAction);
        }
        public void RemoveClick(UnityAction clickAction)
        {
            this.onClick.RemoveListener(clickAction);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }
}
