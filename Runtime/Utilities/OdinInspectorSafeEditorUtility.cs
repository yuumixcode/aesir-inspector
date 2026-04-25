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
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
#if UNITY_EDITOR
using Sirenix.Utilities;
#endif

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 用于兼容 Odin Inspector 的 Editor-Only Mode 的工具类。
    /// 这是一个桥梁工具类，用于在不直接依赖 Odin Inspector 的情况下安全地调用其功能。
    /// </summary>
    [Summary("用于兼容 Odin Inspector 的 Editor-Only Mode 的工具类。桥梁工具类，安全跨越依赖。")]
    public static class OdinInspectorSafeEditorUtility
    {
        #region Public Methods

        /// <summary>
        /// 获取成员的值（支持字段和属性）。
        /// </summary>
        [Summary("获取成员的值（支持字段和属性）。")]
        public static object GetMemberValue(MemberInfo member, object obj)
        {
            if ((object)(member as FieldInfo) != null)
            {
                return ((FieldInfo)member).GetValue(obj);
            }

            return (object)(member as PropertyInfo) != null
                ? ((PropertyInfo)member).GetGetMethod(true).Invoke(obj, null)
                : throw new ArgumentException($"Can't get the value of a {member.GetType().Name}");
        }

        /// <summary>
        /// 获取成员的返回类型（支持字段、属性、方法和事件）。
        /// </summary>
        [Summary("获取成员的返回类型（支持字段、属性、方法和事件）。")]
        public static Type GetReturnType(MemberInfo memberInfo)
        {
            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo.FieldType;
            }

            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                return propertyInfo.PropertyType;
            }

            var methodInfo = memberInfo as MethodInfo;
            if (methodInfo != null)
            {
                return methodInfo.ReturnType;
            }

            var eventInfo = memberInfo as EventInfo;
            return eventInfo != null ? eventInfo.EventHandlerType : null;
        }

        /// <summary>
        /// 判断成员是否为静态成员。
        /// </summary>
        [Summary("判断成员是否为静态成员。")]
        public static bool IsStatic(MemberInfo member)
        {
            var fieldInfo = member as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo.IsStatic;
            }

            var propertyInfo = member as PropertyInfo;
            if (propertyInfo != null)
            {
                return !propertyInfo.CanRead
                    ? propertyInfo.GetSetMethod(true).IsStatic
                    : propertyInfo.GetGetMethod(true).IsStatic;
            }

            var methodBase = member as MethodBase;
            if (methodBase != null)
            {
                return methodBase.IsStatic;
            }

            var eventInfo = member as EventInfo;
            if (eventInfo != null)
            {
                return eventInfo.GetRaiseMethod(true).IsStatic;
            }

            var type = member as Type;
            if (!(type != null))
            {
                throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture,
                    "Unable to determine IsStatic for member {0}.{1}MemberType was {2} but only fields, properties, methods, events and types are supported.",
                    member.DeclaringType?.FullName, member.Name, member.GetType().FullName));
            }

            return type.IsSealed && type.IsAbstract;
        }

        /// <summary>
        /// 获取成员上的指定类型特性。
        /// </summary>
        [Summary("获取成员上的指定类型特性。")]
        public static IEnumerable<T> GetAttributes<T>(ICustomAttributeProvider member) where T : Attribute =>
            GetAttributes<T>(member, false);

        /// <summary>
        /// 获取成员上的指定类型特性。
        /// </summary>
        [Summary("获取成员上的指定类型特性。")]
        public static IEnumerable<T> GetAttributes<T>(ICustomAttributeProvider member, bool inherit)
            where T : Attribute
        {
            try
            {
                return member.GetCustomAttributes(typeof(T), inherit).Cast<T>();
            }
            catch
            {
                return new T[0];
            }
        }

        /// <summary>
        /// 判断方法是否为扩展方法。
        /// </summary>
        [Summary("判断方法是否为扩展方法。")]
        public static bool IsExtensionMethod(MethodBase method)
        {
            var declaringType = method.DeclaringType;
            return declaringType != null && declaringType.IsSealed && !declaringType.IsGenericType &&
                   !declaringType.IsNested && method.IsDefined(typeof(ExtensionAttribute), false);
        }

        /// <summary>
        /// 获取类型的所有基类和接口。
        /// </summary>
        [Summary("获取类型的所有基类和接口。")]
        public static IEnumerable<Type> GetBaseTypes(Type type, bool includeSelf = false)
        {
            var first = GetBaseClasses(type, includeSelf).Concat(type.GetInterfaces());
            if (includeSelf && type.IsInterface)
            {
                first = first.Concat(new Type[1]
                {
                    type
                });
            }

            return first;
        }

        /// <summary>
        /// 获取类型的所有基类。
        /// </summary>
        [Summary("获取类型的所有基类。")]
        public static IEnumerable<Type> GetBaseClasses(Type type, bool includeSelf = false)
        {
            if (type?.BaseType != null)
            {
                if (includeSelf)
                {
                    yield return type;
                }

                for (var current = type.BaseType; current != null; current = current.BaseType)
                    yield return current;
            }
        }

        /// <summary>
        /// 获取泛型约束字符串。如果是 Odin 环境，则调用 Odin 的扩展方法。
        /// </summary>
        [Summary("获取泛型约束字符串。")]
        public static string GetGenericConstraintsString(Type type, bool useFullTypeNames = false)
        {
#if UNITY_EDITOR && ODIN_INSPECTOR_3_3
            return type.GetGenericConstraintsString(useFullTypeNames);
#else
            return string.Empty;
#endif
        }

        /// <summary>
        /// 获取类型的友好名称。如果是 Odin 环境，则调用 Odin 的扩展方法。
        /// </summary>
        [Summary("获取类型的友好名称。")]
        public static string GetNiceName(Type type)
        {
#if UNITY_EDITOR && ODIN_INSPECTOR_3_3
            return type.GetNiceName();
#else
            return type.Name;
#endif
        }

        /// <summary>
        /// 获取类型的友好全名。如果是 Odin 环境，则调用 Odin 的扩展方法。
        /// </summary>
        [Summary("获取类型的友好全名。")]
        public static string GetNiceFullName(Type type)
        {
#if UNITY_EDITOR && ODIN_INSPECTOR_3_3
            return type.GetNiceFullName();
#else
            return type.FullName;
#endif
        }

        #endregion
    }
}
