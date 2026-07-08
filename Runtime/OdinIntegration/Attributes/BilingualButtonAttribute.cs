using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace RunLab.AesirInspector.OdinIntegration
{
    [Summary("双语按钮特性。必须硬编码使用特性，不能使用 OdinAttributeProcessor 动态添加使用。")]
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    public class BilingualButtonAttribute : ShowInInspectorAttribute
    {
        public BilingualButtonAttribute(string chineseName,
            string englishName = null,
            ButtonSizes buttonSize = ButtonSizes.Medium,
            ButtonStyle style = ButtonStyle.Box,
            SdfIconType icon = SdfIconType.None,
            IconAlignment buttonIconAlignment = IconAlignment.LeftOfText,
            int buttonHeight = -1,
            bool stretch = true,
            bool drawResult = true,
            bool expanded = false,
            float buttonAlignment = 0.5f,
            bool displayParameters = true,
            bool dirtyOnClick = true)
        {
            ChineseName = chineseName;
            EnglishName = englishName ?? chineseName;
            ButtonSize = buttonSize;
            ButtonHeight = buttonHeight;
            Style = style;
            Icon = icon;
            ButtonIconAlignment = buttonIconAlignment;
            Stretch = stretch;
            DrawResult = drawResult;
            Expanded = expanded;
            ButtonAlignment = buttonAlignment;
            DisplayParameters = displayParameters;
            DirtyOnClick = dirtyOnClick;
        }

        [Summary("按钮对齐方式 (0-1)")]
        public float ButtonAlignment { get; set; }

        [Summary("按钮高度")]
        public int ButtonHeight { get; set; }

        [Summary("图标对齐方式")]
        public IconAlignment ButtonIconAlignment { get; set; }

        [Summary("按钮大小")]
        public ButtonSizes ButtonSize { get; set; }

        [Summary("中文名称")]
        [OdinDesignerBinding]
        public string ChineseName { get; set; }

        [Summary("点击时是否标记为脏")]
        public bool DirtyOnClick { get; set; }

        [Summary("是否显示参数")]
        public bool DisplayParameters { get; set; }

        [Summary("是否绘制结果")]
        public bool DrawResult { get; set; }

        [Summary("英文名称")]
        [OdinDesignerBinding]
        public string EnglishName { get; set; }

        [Summary("是否展开")]
        public bool Expanded { get; set; }

        [Summary("SDF 图标类型")]
        public SdfIconType Icon { get; set; }

        [Summary("是否拉伸")]
        public bool Stretch { get; set; }

        [Summary("按钮样式")]
        public ButtonStyle Style { get; set; }

        [Summary("创建 Odin 的 Button 特性")]
        public ButtonAttribute CreateButton()
        {
            var button = new ButtonAttribute(ChineseName, ButtonSize)
            {
                Style = Style,
                Icon = Icon,
                IconAlignment = ButtonIconAlignment,
                ButtonAlignment = ButtonAlignment,
                Stretch = Stretch,
                DrawResult = DrawResult,
                Expanded = Expanded,
                DisplayParameters = DisplayParameters,
                DirtyOnClick = DirtyOnClick
            };
            // 如果 ButtonHeight 大于 ButtonSize 对应的高度，则覆盖
            if (ButtonHeight > (int)ButtonSize)
            {
                button.ButtonHeight = ButtonHeight;
            }

            return button;
        }
    }

#if UNITY_EDITOR
    internal sealed class BilingualAttributeProcessor<T> : OdinAttributeProcessor<T> where T : class
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty,
            MemberInfo member,
            List<Attribute> attributes)
        {
            if (member.MemberType == MemberTypes.Method &&
                member.GetCustomAttribute<BilingualButtonAttribute>() != null)
            {
                var button = member.GetCustomAttribute<BilingualButtonAttribute>();
                var chineseButton = button.CreateButton();
                attributes.Add(chineseButton);
            }
        }
    }

#endif
}
