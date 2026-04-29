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
using System.Reflection;
using UnityEngine;

namespace RunLab.AesirInspector.OdinWrapper
{
    /// <summary>
    /// 通过反射封装 Odin 内部语法高亮处理器的静态工具类。
    /// </summary>
    [Summary("通过反射封装 Odin 内部语法高亮处理器的静态工具类")]
    public static class OdinCodeHighlighter
    {
        static readonly Type SyntaxHighlighterType = Type.GetType(
            "Sirenix.OdinInspector.Editor.Examples.SyntaxHighlighter," + "Sirenix.OdinInspector.Editor," +
            "Version=1.0.0.0," + "Culture=neutral," + "PublicKeyToken=null");

        static readonly MethodInfo ParseMethod =
            SyntaxHighlighterType?.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);

        /// <summary>
        /// 代码预览区域背景色。
        /// </summary>
        [Summary("代码预览区域背景色")]
        public static Color BackgroundColor { get; } = new Color(0.118f, 0.118f, 0.118f, 1f);

        /// <summary>
        /// 代码文本默认颜色。
        /// </summary>
        [Summary("代码文本默认颜色")]
        public static Color TextColor { get; } = new Color(0.863f, 0.863f, 0.863f, 1f);

        /// <summary>
        /// 对代码文本应用语法高亮，返回包含富文本标记的结果。
        /// </summary>
        [Summary("对代码文本应用语法高亮，返回包含富文本标记的结果")]
        public static string ApplyHighlighting(string code)
        {
            if (ParseMethod != null)
            {
                return ParseMethod.Invoke(null, new object[] { code }) as string ?? code;
            }

            Debug.LogError("[AesirCodeHighlighter] 无法获取 Odin SyntaxHighlighter.Parse 方法");
            return code;
        }
    }
}
