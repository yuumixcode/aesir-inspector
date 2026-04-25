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

namespace RunLab.AesirInspector
{
    [Serializable]
    [Summary("访问修饰符类型")]
    public enum AccessModifierType
    {
        [Summary("公共访问修饰符")]
        Public = 0,

        [Summary("受保护的内部访问修饰符")]
        ProtectedInternal = 1,

        [Summary("受保护访问修饰符")]
        Protected = 2,

        [Summary("内部访问修饰符")]
        Internal = 4,

        [Summary("私有受保护访问修饰符")]
        PrivateProtected = 8,

        [Summary("私有访问修饰符")]
        Private = 16,

        [Summary("无访问修饰符")]
        None = 32
    }

    [Summary("访问修饰符扩展方法")]
    public static class AccessModifierTypeExtensions
    {
        [Summary("将访问修饰符类型转换为对应的字符串表示")]
        public static string ConvertToString(this AccessModifierType modifier)
        {
            return modifier switch
            {
                AccessModifierType.Public => "public",
                AccessModifierType.Private => "private",
                AccessModifierType.Protected => "protected",
                AccessModifierType.Internal => "internal",
                AccessModifierType.ProtectedInternal => "protected internal",
                AccessModifierType.PrivateProtected => "private protected",
                _ => ""
            };
        }
    }
}
