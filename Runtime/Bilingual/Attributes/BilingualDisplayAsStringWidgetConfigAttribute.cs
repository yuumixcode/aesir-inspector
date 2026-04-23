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
#if ODIN_INSPECTOR_3_3
using Sirenix.OdinInspector;
#endif

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 双语以字符串显示组件配置特性。
    /// </summary>
    [Summary("双语以字符串显示组件配置特性")]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [Conditional("UNITY_EDITOR")]
    public class BilingualDisplayAsStringWidgetConfigAttribute : Attribute
    {
        public BilingualDisplayAsStringWidgetConfigAttribute(bool overflow = false,
            TextAlignment alignment = TextAlignment.Left,
            int fontSize = 13,
            bool enableRichText = false,
            string format = null)
        {
            Overflow = overflow;
            Alignment = alignment;
            FontSize = fontSize;
            EnableRichText = enableRichText;
            Format = format ?? string.Empty;
        }

        /// <summary>
        /// 文本对齐方式
        /// </summary>
        [Summary("文本对齐方式")]
        public TextAlignment Alignment { get; set; }

        /// <summary>
        /// 是否启用富文本
        /// </summary>
        [Summary("是否启用富文本")]
        public bool EnableRichText { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        [Summary("字体大小")]
        public int FontSize { get; set; }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        [Summary("格式化字符串")]
        public string Format { get; set; }

        /// <summary>
        /// 是否允许溢出
        /// </summary>
        [Summary("是否允许溢出")]
        public bool Overflow { get; set; }

#if ODIN_INSPECTOR_3_3
        /// <summary>
        /// 创建 Odin 的 DisplayAsString 特性。
        /// </summary>
        [Summary("创建 Odin 的 DisplayAsString 特性")]
        public DisplayAsStringAttribute CreateDisplayAsStringAttribute() =>
            new DisplayAsStringAttribute(Overflow)
            {
                Alignment = Alignment,
                FontSize = FontSize,
                EnableRichText = EnableRichText,
                Format = Format
            };
#endif
    }
}
