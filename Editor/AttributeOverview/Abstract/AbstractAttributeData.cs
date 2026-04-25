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

#if UNITY_EDITOR && ODIN_INSPECTOR_3_3

namespace RunLab.AesirInspector
{
    using System.Linq;
    using UnityEngine;
    /// <summary>
    /// 特性信息数据抽象基类，定义面板所需的全部显示数据。
    /// </summary>
    [Summary("特性信息数据抽象基类，定义面板所需的全部显示数据")]
    public abstract class AbstractAttributeData
    {
        /// <summary>
        /// 顶部说明控件。
        /// </summary>
        [Summary("顶部说明控件")]
        public abstract HeaderBilingualWidget HeaderWidget { get; set; }

        /// <summary>
        /// 使用提示数组。
        /// </summary>
        [Summary("使用提示数组")]
        public abstract BilingualData[] UsageTips { get; set; }

        /// <summary>
        /// 特性参数数组。
        /// </summary>
        [Summary("特性参数数组")]
        public abstract ParameterValue[] AttributeParameters { get; set; }

        /// <summary>
        /// 被解析的字符串参数数组。
        /// </summary>
        [Summary("被解析的字符串参数数组")]
        public abstract ResolvedStringParameterValue[] ResolvedStringParameters { get; set; }

        /// <summary>
        /// 使用案例预览项数组。
        /// </summary>
        [Summary("使用案例预览项数组")]
        public abstract AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; }

        #region --- Public Methods ---

        /// <summary>
        /// 获取初始显示的案例 ScriptableObject。
        /// </summary>
        [Summary("获取初始显示的案例 ScriptableObject")]
        public ScriptableObject GetInitialExample()
        {
            if (ExamplePreviewItems == null || ExamplePreviewItems.Length == 0)
            {
                return null;
            }

#if ODIN_INSPECTOR_3_3
            return ExamplePreviewItems.Any(x => x.ExampleType == AttributeExampleType.OdinSerialized)
                ? ExamplePreviewItems[0].OdinSerializedExample
                : ExamplePreviewItems[0].UnitySerializedExample;
#else
            return ExamplePreviewItems[0].UnitySerializedExample;
#endif
        }

        #endregion
    }
}

#endif
