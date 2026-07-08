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
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("在 Odin Inspector 框架中渲染由 BilingualBoxGroupAttribute 分组的属性。")]
    public class BilingualBoxGroupAttributeDrawer : OdinGroupDrawer<BilingualBoxGroupAttribute>
    {
        ValueResolver<string> _labelGetter;

        protected override void Initialize()
        {
            _labelGetter = ValueResolver.GetForString(Property, Attribute.LanguageData);
            AesirInspectorLanguageSettingsSO.LanguageChanged -= ReloadResolver;
            AesirInspectorLanguageSettingsSO.LanguageChanged += ReloadResolver;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            _labelGetter.DrawError();
            string label1 = null;
            if (Attribute.ShowLabel)
            {
                label1 = _labelGetter.GetValue();
                if (string.IsNullOrEmpty(label1))
                {
                    label1 = "Null";
                }
            }

            SirenixEditorGUI.BeginBox(label1, Attribute.CenterLabel);
            for (var index = 0; index < Property.Children.Count; index++)
            {
                var child = Property.Children[index];
                child.Draw(child.Label);
            }

            SirenixEditorGUI.EndBox();
        }

        void ReloadResolver()
        {
            _labelGetter = ValueResolver.GetForString(Property, Attribute.LanguageData);
        }
    }
}
