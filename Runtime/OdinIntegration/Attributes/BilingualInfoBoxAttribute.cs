using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration
{
    /// <summary>
    /// 双语信息框特性。
    /// </summary>
    [Summary("双语信息框特性")]
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class BilingualInfoBoxAttribute : Attribute
    {
        public BilingualInfoBoxAttribute(string chinese,
            string english = null,
            InfoMessageType infoMessageType = InfoMessageType.Info,
            SdfIconType icon = SdfIconType.None,
            string visibleIf = "",
            string iconColor = null,
            bool guiAlwaysEnabled = false)
        {
            ChineseText = chinese;
            EnglishText = english;
            InfoMessageType = infoMessageType;
            Icon = icon;
            VisibleIf = visibleIf;
            IconColor = iconColor;
            GUIAlwaysEnabled = guiAlwaysEnabled;
        }

        /// <summary>
        /// GUI 是否始终启用
        /// </summary>
        [Summary("GUI 是否始终启用")]
        public bool GUIAlwaysEnabled { get; set; }

        /// <summary>
        /// SDF 图标类型
        /// </summary>
        [Summary("SDF 图标类型")]
        public SdfIconType Icon { get; set; }

        /// <summary>
        /// 图标颜色
        /// </summary>
        [Summary("图标颜色")]
        public string IconColor { get; set; }

        /// <summary>
        /// 信息消息类型
        /// </summary>
        [Summary("信息消息类型")]
        public InfoMessageType InfoMessageType { get; set; }

        /// <summary>
        /// 显示条件表达式
        /// </summary>
        [Summary("显示条件表达式")]
        public string VisibleIf { get; set; }

        /// <summary>
        /// 中文文本
        /// </summary>
        [Summary("中文文本")]
        public string ChineseText { get; set; }

        /// <summary>
        /// 英文文本
        /// </summary>
        [Summary("英文文本")]
        public string EnglishText { get; set; }

        /// <summary>
        /// 是否定义了图标
        /// </summary>
        [Summary("是否定义了图标")]
        public bool HasDefinedIcon => Icon != SdfIconType.None;
    }
}
