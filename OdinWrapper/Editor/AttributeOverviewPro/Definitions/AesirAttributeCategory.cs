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
    /// Odin Inspector 特性分类枚举，用于将特性面板归入对应分类。
    /// </summary>
    [Summary("Odin Inspector 特性分类枚举，用于将特性面板归入对应分类")]
    [Flags]
    public enum AesirAttributeCategory
    {
        None = 0,
        Essentials = 1 << 0,
        Buttons = 1 << 1,
        Collections = 1 << 2,
        Groups = 1 << 3,
        Conditionals = 1 << 4,
        Numbers = 1 << 5,
        TypeSpecifics = 1 << 6,
        Validation = 1 << 7,
        Misc = 1 << 8,
        Meta = 1 << 9,
        Unity = 1 << 10,
        Debug = 1 << 11
    }

    /// <summary>
    /// AesirAttributeCategory 枚举扩展方法。
    /// </summary>
    [Summary("AesirAttributeCategory 枚举扩展方法")]
    public static class AesirAttributeCategoryExtensions
    {
        /// <summary>
        /// 高性能 HasFlag 实现，避免装箱操作。
        /// </summary>
        [Summary("高性能 HasFlag 实现，避免装箱操作")]
        public static bool HasFlagFast(this AesirAttributeCategory value, AesirAttributeCategory flag) =>
            (value & flag) != 0;
    }
}
