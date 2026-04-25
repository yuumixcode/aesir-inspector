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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 反射工具类，提供程序集及命名空间的反射操作方法
    /// </summary>
    [Summary("反射工具类，提供程序集及命名空间的反射操作方法")]
    public static class ReflectionUtility
    {
        #region Public Methods

        /// <summary>
        /// 获取名称中包含指定字符串的所有程序集
        /// </summary>
        [Summary("获取名称中包含指定字符串的所有程序集")]
        public static Assembly[] GetAssembliesOfNameContainString(string partOfAssemblyName)
        {
            try
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(assembly => assembly.FullName.Contains(partOfAssemblyName)).ToArray();
                return assemblies.Length > 0 ? assemblies : Array.Empty<Assembly>();
            }
            catch (Exception ex)
            {
                throw new Exception($"发生错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取指定程序集中的所有命名空间
        /// </summary>
        [Summary("获取指定程序集中的所有命名空间")]
        public static List<string> GetNamespacesInAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();
            var namespaces = types.Select(type => type.Namespace).Where(ns => ns != null).Distinct().ToList();
            return namespaces;
        }

        #endregion
    }
}
