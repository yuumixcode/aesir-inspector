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
#if ODIN_INSPECTOR_3_3
using Sirenix.OdinInspector;
#endif

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 双语标题分组特性。
    /// </summary>
    [Summary("双语标题分组特性")]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
#if ODIN_INSPECTOR_3_3
    public class BilingualTitleGroupAttribute : PropertyGroupAttribute
#else
    public class BilingualTitleGroupAttribute : Attribute
#endif
    {
        public BilingualTitleGroupAttribute(string groupId,
            string chineseTitle,
            string englishTitle = null,
            string chineseSubtitle = null,
            string englishSubtitle = null,
#if ODIN_INSPECTOR_3_3
            TitleAlignments titleAlignment = TitleAlignments.Left,
#else
            int titleAlignment = 0,
#endif
            bool horizontalLine = true,
            bool boldTitle = true,
            bool indent = false,
            float order = 0)
#if ODIN_INSPECTOR_3_3
            : base(groupId, order)
#endif
        {
            TitleData = new BilingualData(chineseTitle, englishTitle);
            chineseSubtitle ??= string.Empty;
            SubtitleData = new BilingualData(chineseSubtitle, englishSubtitle);
#if ODIN_INSPECTOR_3_3
            TitleAlignment = titleAlignment;
#endif
            HorizontalLine = horizontalLine;
            BoldTitle = boldTitle;
            Indent = indent;
        }

        /// <summary>
        /// 是否加粗标题
        /// </summary>
        [Summary("是否加粗标题")]
        public bool BoldTitle { get; set; }

        /// <summary>
        /// 是否显示水平分割线
        /// </summary>
        [Summary("是否显示水平分割线")]
        public bool HorizontalLine { get; set; }

        /// <summary>
        /// 是否缩进
        /// </summary>
        [Summary("是否缩进")]
        public bool Indent { get; set; }

        /// <summary>
        /// 副标题数据
        /// </summary>
        [Summary("副标题数据")]
        public BilingualData SubtitleData { get; set; }

#if ODIN_INSPECTOR_3_3
        /// <summary>
        /// 标题对齐方式
        /// </summary>
        [Summary("标题对齐方式")]
        public TitleAlignments TitleAlignment { get; set; }
#endif

        /// <summary>
        /// 标题数据
        /// </summary>
        [Summary("标题数据")]
        public BilingualData TitleData { get; set; }

#if ODIN_INSPECTOR_3_3
        /// <summary>
        /// 合并属性组特性值。
        /// </summary>
        [Summary("合并属性组特性值")]
        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            // 非默认值优先的原则
            var multiLanguageTitleGroupAttribute = other as BilingualTitleGroupAttribute;
            if (multiLanguageTitleGroupAttribute == null)
            {
                return;
            }

            if (!TitleData.Equals(multiLanguageTitleGroupAttribute.TitleData))
            {
                TitleData = multiLanguageTitleGroupAttribute.TitleData;
            }

            if (!SubtitleData.Equals(multiLanguageTitleGroupAttribute.SubtitleData))
            {
                SubtitleData = multiLanguageTitleGroupAttribute.SubtitleData;
            }

            if (TitleAlignment != TitleAlignments.Left)
            {
                multiLanguageTitleGroupAttribute.TitleAlignment = TitleAlignment;
            }
            else
            {
                TitleAlignment = multiLanguageTitleGroupAttribute.TitleAlignment;
            }

            if (!HorizontalLine)
            {
                multiLanguageTitleGroupAttribute.HorizontalLine = HorizontalLine;
            }
            else
            {
                HorizontalLine = multiLanguageTitleGroupAttribute.HorizontalLine;
            }

            if (!BoldTitle)
            {
                multiLanguageTitleGroupAttribute.BoldTitle = BoldTitle;
            }
            else
            {
                BoldTitle = multiLanguageTitleGroupAttribute.BoldTitle;
            }

            if (Indent)
            {
                multiLanguageTitleGroupAttribute.Indent = Indent;
            }
            else
            {
                Indent = multiLanguageTitleGroupAttribute.Indent;
            }
        }
#endif
    }
}
