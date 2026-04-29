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
using System.Runtime.CompilerServices;

namespace RunLab.AesirInspector.OdinWrapper.Editor
{
    /// <summary>
    /// 标记一个类为 Aesir Inspector 特性示例类，编译时自动捕获源文件绝对路径。
    /// </summary>
    [Summary("标记一个类为 Aesir Inspector 特性示例类，编译时自动捕获源文件绝对路径")]
    [AttributeUsage(AttributeTargets.Class)]
    public class AesirExampleAttribute : Attribute
    {
        public AesirExampleAttribute([CallerFilePath] string filePath = "unknown") =>
            FilePath = filePath;

        /// <summary>
        /// 示例类的源文件绝对路径。
        /// </summary>
        [Summary("示例类的源文件绝对路径")]
        public string FilePath { get; }
    }
}
