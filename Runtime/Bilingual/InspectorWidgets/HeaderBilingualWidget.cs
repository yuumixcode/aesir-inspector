// ----------------------------------------------------------------------------
// MIT License
// 
// Copyright (c) 2026 RunLab - Yuumix
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
#if ODIN_INSPECTOR_3_3
using Sirenix.OdinInspector;
#endif

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 双语顶部说明控件，用于模块的简单介绍。
    /// </summary>
    [Serializable]
    [Summary("双语顶部说明控件，用于模块的简单介绍")]
    public class HeaderBilingualWidget
    {
        [BilingualDisplayAsStringWidgetConfig(false, TextAlignment.Left, 30)]
        [SerializeField]
        DisplayAsStringBilingualWidget headerName;

        [BilingualDisplayAsStringWidgetConfig(false, TextAlignment.Left, 14, true)]
        [SerializeField]
        DisplayAsStringBilingualWidget headerIntroduction;

        string _chineseIntroduction;
        string _englishIntroduction;
        string _targetUrl;

        public HeaderBilingualWidget(string chineseName,
            string englishName = null,
            string chineseIntroduction = null,
            string englishIntroduction = null,
            string targetUrl = null)
        {
            headerName = new DisplayAsStringBilingualWidget(chineseName, englishName);
            _chineseIntroduction = chineseIntroduction;
            _englishIntroduction = englishIntroduction ?? chineseIntroduction;
            headerIntroduction =
                new DisplayAsStringBilingualWidget(_chineseIntroduction, _englishIntroduction);
            _targetUrl = targetUrl ?? AesirInspectorWebLinks.GitWebsite;
        }

        /// <summary>
        /// 顶部标题控件名称
        /// </summary>
        [Summary("顶部标题控件名称")]
        public DisplayAsStringBilingualWidget HeaderName => headerName;

        /// <summary>
        /// 是否隐藏标题介绍
        /// </summary>
        [Summary("是否隐藏标题介绍")]
        bool HideHeaderIntroduction => string.IsNullOrWhiteSpace(_chineseIntroduction) &&
                                       string.IsNullOrWhiteSpace(_englishIntroduction);

        void PlaceholderMethod1() { }

        [Summary("切换当前语言")]
        [Conditional("UNITY_EDITOR")]
        void SwitchLanguage()
        {
            if (AesirInspectorLanguageSettings.IsChinese)
            {
                AesirInspectorLanguageSettings.SetEnglish();
            }
            else
            {
                AesirInspectorLanguageSettings.SetChinese();
            }
        }

        /// <summary>
        /// 打开相关文档链接
        /// </summary>
        [Summary("打开相关文档链接")]
        public void OpenUrl()
        {
            var validatedUrl =
                UrlUtility.ValidateAndNormalizeUrl(_targetUrl, AesirInspectorWebLinks.GitWebsite);
            Application.OpenURL(validatedUrl);
        }

        void PlaceholderMethod2() { }

        /// <summary>
        /// 修改控件数据。
        /// </summary>
        [Summary("修改控件数据")]
        public HeaderBilingualWidget ModifyWidget(string chineseName,
            string englishName = null,
            string chineseIntroduction = null,
            string englishIntroduction = null,
            string targetUrl = null)
        {
            headerName.ChineseDisplay = chineseName;
            headerName.EnglishDisplay = englishName ?? chineseName;
            headerIntroduction.ChineseDisplay = chineseIntroduction;
            headerIntroduction.EnglishDisplay = englishIntroduction;
            _targetUrl = targetUrl ?? AesirInspectorWebLinks.GitWebsite;
            return this;
        }
    }

#if UNITY_EDITOR && ODIN_INSPECTOR_3_3

    #region --- Odin Inspector ---

    internal sealed class BilingualHeaderProcessor : OdinAttributeProcessor<HeaderBilingualWidget>
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            attributes.Add(new InlinePropertyAttribute());
            attributes.Add(new HideLabelAttribute());
        }

        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty,
            MemberInfo member,
            List<Attribute> attributes)
        {
            switch (member.Name)
            {
                case "headerName":
                    attributes.Add(new PropertyOrderAttribute(0));
                    attributes.Add(new PropertySpaceAttribute(13));
                    attributes.Add(new BoxGroupAttribute("OuterBox"));
                    attributes.Add(new HorizontalGroupAttribute("OuterBox/HoriTop", 0.75f));
                    break;
                case "headerIntroduction":
                    attributes.Add(new HideIfAttribute("HideHeaderIntroduction"));
                    attributes.Add(new PropertyOrderAttribute(30));
                    attributes.Add(new BoxGroupAttribute("OuterBox"));
                    attributes.Add(new HorizontalGroupAttribute("OuterBox/HoriBottom", 0.98f));
                    attributes.Add(new PropertySpaceAttribute(10, 8));
                    break;
                case "PlaceholderMethod1":
                    attributes.Add(new PropertyOrderAttribute(-10));
                    attributes.Add(new OnInspectorGUIAttribute());
                    attributes.Add(new BoxGroupAttribute("OuterBox", false));
                    attributes.Add(new HorizontalGroupAttribute("OuterBox/HoriTop", 0.01f));
                    break;
                case "SwitchLanguage":
                    attributes.Add(new PropertyOrderAttribute(5));
                    attributes.Add(new BoxGroupAttribute("OuterBox"));
                    attributes.Add(new PropertySpaceAttribute(8, 5));
                    attributes.Add(new HorizontalGroupAttribute("OuterBox/HoriTop", 0.22f));
                    attributes.Add(new VerticalGroupAttribute("OuterBox/HoriTop/VerRight"));
                    attributes.Add(new BilingualButtonAttribute("中文", "English", buttonHeight: 24,
                        icon: SdfIconType.Translate));
                    break;
                case nameof(HeaderBilingualWidget.OpenUrl):
                    attributes.Add(new PropertyOrderAttribute(10));
                    attributes.Add(new BoxGroupAttribute("OuterBox"));
                    attributes.Add(new HorizontalGroupAttribute("OuterBox/HoriTop", 0.22f));
                    attributes.Add(new VerticalGroupAttribute("OuterBox/HoriTop/VerRight"));
                    attributes.Add(new BilingualButtonAttribute("文档", "Documentation", buttonHeight: 24,
                        icon: SdfIconType.Link45deg));
                    break;
                case "PlaceholderMethod2":
                    attributes.Add(new BoxGroupAttribute("OuterBox"));
                    attributes.Add(new HorizontalGroupAttribute("OuterBox/HoriBottom", 0.01f));
                    attributes.Add(new OnInspectorGUIAttribute());
                    break;
            }
        }
    }

    #endregion

#endif
}
