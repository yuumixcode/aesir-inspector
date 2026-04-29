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
using System.Diagnostics;
using UnityEngine;

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 双语顶部说明控件，用于模块的简单介绍。
    /// </summary>
    [Serializable]
    [Summary("双语顶部说明控件，用于模块的简单介绍")]
    public class BilingualHeaderControl
    {
        [SerializeField]
        public BilingualDisplayAsStringControl headerName;

        [SerializeField]
        public BilingualDisplayAsStringControl headerIntroduction;

        readonly string _chineseIntroduction;
        readonly string _englishIntroduction;
        string _targetUrl;

        public BilingualHeaderControl(string chineseName,
            string englishName = null,
            string chineseIntroduction = null,
            string englishIntroduction = null,
            string targetUrl = null)
        {
            headerName = new BilingualDisplayAsStringControl(chineseName, englishName)
            {
                fontSize = 30,
                alignment = TextAlignment.Left
            };
            _chineseIntroduction = chineseIntroduction;
            _englishIntroduction = englishIntroduction ?? chineseIntroduction;
            headerIntroduction =
                new BilingualDisplayAsStringControl(_chineseIntroduction, _englishIntroduction)
                {
                    fontSize = 14,
                    enableRichText = true,
                    alignment = TextAlignment.Left
                };
            _targetUrl = targetUrl ?? AesirInspectorWebLinks.GitUrl;
        }

        /// <summary>
        /// 是否隐藏标题介绍
        /// </summary>
        [Summary("是否隐藏标题介绍")]
        public bool HideHeaderIntroduction => string.IsNullOrWhiteSpace(_chineseIntroduction) &&
                                              string.IsNullOrWhiteSpace(_englishIntroduction);

        /// <summary>
        /// 打开相关文档链接
        /// </summary>
        [Summary("打开相关文档链接")]
        public void OpenUrl()
        {
            var validatedUrl = UrlUtility.ValidateAndNormalizeUrl(_targetUrl, AesirInspectorWebLinks.GitUrl);
            Application.OpenURL(validatedUrl);
        }

        public void PlaceholderMethod1() { }

        [Summary("切换当前语言")]
        [Conditional("UNITY_EDITOR")]
        public void SwitchLanguage()
        {
            if (AesirInspectorLanguageSettingsSO.CurrentIsChinese)
            {
                AesirInspectorLanguageSettingsSO.SetEnglish();
            }
            else
            {
                AesirInspectorLanguageSettingsSO.SetChinese();
            }
        }

        public void PlaceholderMethod2() { }
    }
}
