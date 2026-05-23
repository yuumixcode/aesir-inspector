using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration
{
    /// <summary>
    /// 带有详细信息的双语信息框特性。
    /// </summary>
    [Summary("带有详细信息的双语信息框特性")]
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class BilingualDetailedInfoBoxAttribute : Attribute
    {
        public BilingualDetailedInfoBoxAttribute(string chinese,
            string english = null,
            string detailsChinese = null,
            string detailsEnglish = null,
            InfoMessageType infoMessageType = InfoMessageType.Info,
            string visibleIf = null,
            bool guiAlwaysEnabled = false)
        {
            ChineseText = chinese;
            EnglishText = english;
            DetailsChineseText = detailsChinese;
            DetailsEnglishText = detailsEnglish;
            InfoMessageType = infoMessageType;
            VisibleIf = visibleIf;
            GUIAlwaysEnabled = guiAlwaysEnabled;
        }

        /// <summary>
        /// 中文主要信息
        /// </summary>
        [Summary("中文主要信息")]
        public string ChineseText { get; set; }

        /// <summary>
        /// 英文主要信息
        /// </summary>
        [Summary("英文主要信息")]
        public string EnglishText { get; set; }

        /// <summary>
        /// 中文详细信息
        /// </summary>
        [Summary("中文详细信息")]
        public string DetailsChineseText { get; set; }

        /// <summary>
        /// 英文详细信息
        /// </summary>
        [Summary("英文详细信息")]
        public string DetailsEnglishText { get; set; }

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
        /// GUI 是否始终启用
        /// </summary>
        [Summary("GUI 是否始终启用")]
        public bool GUIAlwaysEnabled { get; set; }
    }
}
