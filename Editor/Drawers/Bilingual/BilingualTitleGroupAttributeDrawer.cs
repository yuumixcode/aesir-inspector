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

using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace RunLab.AesirInspector.Editor
{
    /// <summary>
    /// 在 Odin Inspector 中绘制 BilingualTitleGroupAttribute 分组标题。
    /// </summary>
    [Summary("在 Odin Inspector 中绘制 BilingualTitleGroupAttribute 分组标题，支持根据检查器语言动态切换标题与子标题")]
    public class BilingualTitleGroupAttributeDrawer : OdinGroupDrawer<BilingualTitleGroupAttribute>
    {
        ValueResolver<string> _subtitleHelper;
        ValueResolver<string> _titleHelper;

        protected override void Initialize()
        {
            ReloadResolver();
            AesirInspectorLanguageSettingsSO.OnLanguageChanged -= ReloadResolver;
            AesirInspectorLanguageSettingsSO.OnLanguageChanged += ReloadResolver;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            var property = Property;
            var attribute = Attribute;
            if (property != property.Tree.GetRootProperty(0))
            {
                EditorGUILayout.Space();
            }

            SirenixEditorGUI.Title(_titleHelper.GetValue(), _subtitleHelper.GetValue(),
                (TextAlignment)attribute.TitleAlignment, attribute.HorizontalLine, attribute.BoldTitle);
            GUIHelper.PushIndentLevel(EditorGUI.indentLevel + (attribute.Indent ? 1 : 0));
            for (var index = 0; index < property.Children.Count; ++index)
            {
                var child = property.Children[index];
                child.Draw(child.Label);
            }

            GUIHelper.PopIndentLevel();
        }

        void ReloadResolver()
        {
            _titleHelper = ValueResolver.GetForString(Property, Attribute.TitleData);
            _subtitleHelper = ValueResolver.GetForString(Property, Attribute.SubtitleData);
        }
    }
}
