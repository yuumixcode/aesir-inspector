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
using System.Reflection;
#if ODIN_INSPECTOR_3_3
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
#endif

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 双语字符串显示控件，以字段的形式支持多语言。
    /// </summary>
    [Summary("双语字符串显示控件，以字段的形式支持多语言")]
    [Serializable]
    public class DisplayAsStringBilingualWidget
    {
        public DisplayAsStringBilingualWidget(string chinese, string english = null)
        {
            ChineseDisplay = chinese;
            EnglishDisplay = english ?? chinese;
        }

        /// <summary>
        /// 中文显示文本
        /// </summary>
        [Summary("中文显示文本")]
        [ShowIfChinese]
        public string ChineseDisplay { get; set; }

        /// <summary>
        /// 英文显示文本
        /// </summary>
        [Summary("英文显示文本")]
        [ShowIfEnglish]
        public string EnglishDisplay { get; set; }
    }
}

#if UNITY_EDITOR && ODIN_INSPECTOR_3_3
namespace RunLab.AesirInspector
{
    internal sealed class
        BilingualDisplayAsStringProcessor : OdinAttributeProcessor<DisplayAsStringBilingualWidget>
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            attributes.Add(new HideLabelAttribute());
            attributes.Add(new InlinePropertyAttribute());

            var config = property.GetAttribute<DisplayAsStringBilingualWidgetConfigAttribute>();
            if (config == null)
            {
                attributes.Add(new BilingualInfoBoxAttribute(
                    $"{nameof(DisplayAsStringBilingualWidget)} 字段必须添加 {nameof(DisplayAsStringBilingualWidgetConfigAttribute)} 才能生效",
                    $"{nameof(DisplayAsStringBilingualWidget)} field must add {nameof(DisplayAsStringBilingualWidgetConfigAttribute)} to take effect",
                    InfoMessageType.Warning));
            }
        }

        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty,
            MemberInfo member,
            List<Attribute> attributes)
        {
            switch (member.Name)
            {
                case nameof(DisplayAsStringBilingualWidget.ChineseDisplay)
                    or nameof(DisplayAsStringBilingualWidget.EnglishDisplay):
                    attributes.Add(new HideLabelAttribute());
                    attributes.Add(new ShowInInspectorAttribute());
                    attributes.Add(new EnableGUIAttribute());

                    var config = parentProperty.GetAttribute<DisplayAsStringBilingualWidgetConfigAttribute>();
                    if (config != null)
                    {
                        attributes.Add(config.CreateDisplayAsStringAttribute());
                    }

                    break;
            }
        }
    }
}
#endif
