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
using Sirenix.Utilities;
using UnityEditor;

namespace RunLab.AesirInspector.OdinWrapper.Editor
{
    /// <summary>
    /// Odin 环境下的 IOdinBridge 实现，委托给 Sirenix.Utilities 的扩展方法。
    /// </summary>
    public class OdinInspectorBridge : IOdinBridge
    {
        public bool IsAvailable => true;
        public string GetFriendlyName(Type type) => type.GetNiceName();
        public string GetFriendlyFullName(Type type) => type.GetNiceFullName();

        public string GetGenericConstraintsString(Type type, bool full) =>
            type.GetGenericConstraintsString(full);
    }

    /// <summary>
    /// 在 OdinWrapper 程序集加载时注入 OdinBridge 实现。
    /// </summary>
    [InitializeOnLoad]
    public static class OdinBridgeInitializer
    {
        static OdinBridgeInitializer() => OdinBridgeLocator.Bridge = new OdinInspectorBridge();
    }
}
