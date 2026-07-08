using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration
{
    [Summary("双语文本特性")]
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class BilingualTextAttribute : Attribute
    {
        public string ChineseText;
        public string EnglishText;

        [Summary("SDF 图标类型")]
        public SdfIconType Icon;

        [Summary("图标颜色")]
        public string IconColor;

        [Summary("是否美化英文文本")]
        public bool NicifyEnglishText;

        public BilingualTextAttribute(string chinese,
            string english = null,
            bool nicifyEnglishText = true,
            SdfIconType icon = SdfIconType.None,
            string iconColor = null)
        {
            ChineseText = chinese;
            EnglishText = english;
            NicifyEnglishText = nicifyEnglishText;
            Icon = icon;
            IconColor = iconColor;
        }
    }
}
