using System;
using System.Diagnostics;
#if ODIN_INSPECTOR_3_3
using Sirenix.OdinInspector;
#endif

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 在检查器中显示属性并始终启用 GUI
    /// </summary>
#if ODIN_INSPECTOR_3_3
    [IncludeMyAttributes]
    [ShowInInspector]
    [EnableGUI]
#endif
    [Summary("在检查器中显示属性并始终启用 GUI")]
    [AttributeUsage(AttributeTargets.Property)]
    [Conditional("UNITY_EDITOR")]
    public class ShowEnablePropertyAttribute : Attribute { }
}
