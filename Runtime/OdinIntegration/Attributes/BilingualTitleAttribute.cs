using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration
{
    /// <summary>
    /// 双语标题特性。
    /// </summary>
    [Summary("双语标题特性")]
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class BilingualTitleAttribute : Attribute
    {
        public BilingualTitleAttribute(string chineseTitle,
            string englishTitle = null,
            string chineseSubTitle = null,
            string englishSubTitle = null,
            TitleAlignments titleAlignment = TitleAlignments.Left,
            bool horizontalLine = true,
            bool bold = true,
            bool beforeSpace = true)
        {
            ChineseTitle = chineseTitle;
            EnglishTitle = englishTitle;
            ChineseSubTitle = chineseSubTitle;
            EnglishSubTitle = englishSubTitle;
            TitleAlignment = titleAlignment;
            HorizontalLine = horizontalLine;
            Bold = bold;
            BeforeSpace = beforeSpace;
        }

        /// <summary>
        /// 是否在标题前添加空格
        /// </summary>
        [Summary("是否在标题前添加空格")]
        public bool BeforeSpace { get; set; }

        /// <summary>
        /// 是否加粗
        /// </summary>
        [Summary("是否加粗")]
        public bool Bold { get; set; }

        /// <summary>
        /// 是否显示水平分割线
        /// </summary>
        [Summary("是否显示水平分割线")]
        public bool HorizontalLine { get; set; }

        /// <summary>
        /// 中文副标题
        /// </summary>
        [Summary("中文副标题")]
        public string ChineseSubTitle { get; set; }

        /// <summary>
        /// 英文副标题
        /// </summary>
        [Summary("英文副标题")]
        public string EnglishSubTitle { get; set; }

        /// <summary>
        /// 标题对齐方式
        /// </summary>
        [Summary("标题对齐方式")]
        public TitleAlignments TitleAlignment { get; set; }

        /// <summary>
        /// 中文标题
        /// </summary>
        [Summary("中文标题")]
        public string ChineseTitle { get; set; }

        /// <summary>
        /// 英文标题
        /// </summary>
        [Summary("英文标题")]
        public string EnglishTitle { get; set; }
    }
}
