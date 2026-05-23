using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 反射工具类，提供程序集、命名空间及成员的反射操作方法
    /// </summary>
    [Summary("反射工具类，提供程序集、命名空间及成员的反射操作方法")]
    public static class ReflectionUtility
    {
        /// <summary>
        /// 获取名称中包含指定字符串的所有程序集
        /// </summary>
        [Summary("获取名称中包含指定字符串的所有程序集")]
        public static Assembly[] GetAssembliesOfNameContainString(string partOfAssemblyName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.FullName.Contains(partOfAssemblyName)).ToArray();
            return assemblies.Length > 0 ? assemblies : Array.Empty<Assembly>();
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

        /// <summary>
        /// 获取成员的值（支持字段和属性）。
        /// </summary>
        [Summary("获取成员的值（支持字段和属性）。")]
        public static object GetMemberValue(MemberInfo member, object obj)
        {
            if (member is FieldInfo fieldInfo)
            {
                return fieldInfo.GetValue(obj);
            }

            if (member is PropertyInfo propertyInfo)
            {
                return propertyInfo.GetGetMethod(true).Invoke(obj, null);
            }

            throw new ArgumentException($"Can't get the value of a {member.GetType().Name}");
        }

        /// <summary>
        /// 获取成员的返回类型（支持字段、属性、方法和事件）。
        /// </summary>
        [Summary("获取成员的返回类型（支持字段、属性、方法和事件）。")]
        public static Type GetReturnType(MemberInfo memberInfo)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.FieldType;
            }

            if (memberInfo is PropertyInfo propertyInfo)
            {
                return propertyInfo.PropertyType;
            }

            if (memberInfo is MethodInfo methodInfo)
            {
                return methodInfo.ReturnType;
            }

            if (memberInfo is EventInfo eventInfo)
            {
                return eventInfo.EventHandlerType;
            }

            return null;
        }

        /// <summary>
        /// 判断成员是否为静态成员。
        /// </summary>
        [Summary("判断成员是否为静态成员。")]
        public static bool IsStatic(MemberInfo member)
        {
            if (member is FieldInfo fieldInfo)
            {
                return fieldInfo.IsStatic;
            }

            if (member is PropertyInfo propertyInfo)
            {
                return !propertyInfo.CanRead
                    ? propertyInfo.GetSetMethod(true).IsStatic
                    : propertyInfo.GetGetMethod(true).IsStatic;
            }

            if (member is MethodBase methodBase)
            {
                return methodBase.IsStatic;
            }

            if (member is EventInfo eventInfo)
            {
                return eventInfo.GetRaiseMethod(true).IsStatic;
            }

            if (member is Type type)
            {
                return type.IsSealed && type.IsAbstract;
            }

            throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture,
                "Unable to determine IsStatic for member {0}.{1}MemberType was {2} but only fields, properties, methods, events and types are supported.",
                member.DeclaringType?.FullName, member.Name, member.GetType().FullName));
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
                return Array.Empty<T>();
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
                first = first.Concat(new[] { type });
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
    }
}
