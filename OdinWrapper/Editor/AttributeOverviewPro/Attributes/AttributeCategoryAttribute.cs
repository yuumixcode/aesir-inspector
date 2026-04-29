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

namespace RunLab.AesirInspector.OdinWrapper.Editor
{
    /// <summary>
    /// 为特性面板 SO 标记所属的 Odin 特性分类。
    /// </summary>
    [Summary("为特性面板 SO 标记所属的 Odin 特性分类")]
    [AttributeUsage(AttributeTargets.Class)]
    public class AttributeCategoryAttribute : Attribute
    {
        public AttributeCategoryAttribute(AesirAttributeCategory category) => Category = category;

        /// <summary>
        /// 所属特性分类。
        /// </summary>
        [Summary("所属特性分类")]
        public AesirAttributeCategory Category { get; }
    }
}
