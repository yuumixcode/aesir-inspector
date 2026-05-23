using System;
using UnityEngine;

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 双语字符串显示控件，以字段的形式支持多语言。
    /// </summary>
    [Summary("双语字符串显示控件，以字段的形式支持多语言")]
    [Serializable]
    public class BilingualDisplayAsStringControl
    {
        [Summary("字体大小")]
        public int fontSize = 13;

        [Summary("文本对齐方式")]
        public TextAlignment alignment = TextAlignment.Left;

        [Summary("是否启用富文本")]
        public bool enableRichText = true;

        [Summary("格式化字符串")]
        public string format = "";

        [Summary("是否溢出")]
        public bool overflow;

        public BilingualDisplayAsStringControl(string chinese, string english = null)
        {
            ChineseDisplay = chinese;
            EnglishDisplay = english ?? chinese;
        }

        /// <summary>
        /// 中文显示文本
        /// </summary>
        [Summary("中文显示文本")]
        public string ChineseDisplay { get; set; }

        /// <summary>
        /// 英文显示文本
        /// </summary>
        [Summary("英文显示文本")]
        public string EnglishDisplay { get; set; }
    }
}
