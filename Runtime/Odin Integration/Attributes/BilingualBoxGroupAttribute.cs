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
using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration
{
    /// <summary>
    /// 双语盒状分组特性。
    /// </summary>
    [Summary("双语盒状分组特性")]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class BilingualBoxGroupAttribute : PropertyGroupAttribute
    {
        public BilingualBoxGroupAttribute(string groupId,
            string chinese,
            string english,
            bool showLabel,
            bool centerLabel = false,
            float order = 0.0f) : base(groupId, order)
        {
            LanguageData = new BilingualData(chinese, english);
            ShowLabel = showLabel;
            CenterLabel = centerLabel;
        }

        public BilingualBoxGroupAttribute(string groupId, string chinese, string english = null) :
            base(groupId) => LanguageData = new BilingualData(chinese, english);

        public BilingualBoxGroupAttribute() : this("_DefaultMultiLanguageBoxGroup", "Null", "Null", false) { }

        /// <summary>
        /// 双语数据
        /// </summary>
        [Summary("双语数据")]
        public BilingualData LanguageData { get; set; }

        /// <summary>
        /// 是否显示标签
        /// </summary>
        [Summary("是否显示标签")]
        public bool ShowLabel { get; set; }

        /// <summary>
        /// 是否居中标签
        /// </summary>
        [Summary("是否居中标签")]
        public bool CenterLabel { get; set; }

        /// <summary>
        /// 是否包含合并值
        /// </summary>
        [Summary("是否包含合并值")]
        public bool HasCombineValues { get; set; }

        /// <summary>
        /// 统一 Group 的设置，自定义合并规则。
        /// </summary>
        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            if (other is not BilingualBoxGroupAttribute multiLanguageBoxGroupAttribute)
            {
                return;
            }

            if (!ShowLabel || !multiLanguageBoxGroupAttribute.ShowLabel)
            {
                ShowLabel = false;
                multiLanguageBoxGroupAttribute.ShowLabel = false;
            }

            CenterLabel |= multiLanguageBoxGroupAttribute.CenterLabel;
            HasCombineValues = true;
        }
    }
}
