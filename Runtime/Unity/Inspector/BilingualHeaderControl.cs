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
        /// <summary>
        /// 标题名称控件
        /// </summary>
        [Summary("标题名称控件")]
        public BilingualDisplayAsStringControl headerName;

        /// <summary>
        /// 标题介绍控件
        /// </summary>
        [Summary("标题介绍控件")]
        public BilingualDisplayAsStringControl headerIntroduction;

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
            headerIntroduction = new BilingualDisplayAsStringControl(chineseIntroduction,
                englishIntroduction ?? chineseIntroduction)
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
        public bool HideHeaderIntroduction => string.IsNullOrWhiteSpace(headerIntroduction.ChineseDisplay) &&
                                              string.IsNullOrWhiteSpace(headerIntroduction.EnglishDisplay);

        /// <summary>
        /// 打开相关文档链接
        /// </summary>
        [Summary("打开相关文档链接")]
        public void OpenUrl()
        {
            var validatedUrl = UrlUtility.ValidateAndNormalizeUrl(_targetUrl, AesirInspectorWebLinks.GitUrl);
            Application.OpenURL(validatedUrl);
        }

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
    }
}
