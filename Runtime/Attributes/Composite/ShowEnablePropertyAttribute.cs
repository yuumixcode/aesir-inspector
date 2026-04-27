using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 在检查器中显示属性并始终启用 GUI
    /// </summary>
    [IncludeMyAttributes]
    [ShowInInspector]
    [EnableGUI]
    [Summary("在检查器中显示属性并始终启用 GUI")]
    [AttributeUsage(AttributeTargets.Property)]
    [Conditional("UNITY_EDITOR")]
    public class ShowEnablePropertyAttribute : Attribute { }
}
