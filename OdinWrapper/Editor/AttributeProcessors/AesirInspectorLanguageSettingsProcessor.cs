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
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace RunLab.AesirInspector.OdinWrapper.Editor
{
    internal sealed class
        AesirInspectorLanguageSettingsProcessor : OdinAttributeProcessor<AesirInspectorLanguageSettingsSO>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty,
            MemberInfo member,
            List<Attribute> attributes)
        {
            switch (member.Name)
            {
                case nameof(AesirInspectorLanguageSettingsSO.SetChinese):
                    attributes.Add(new ButtonAttribute("Switch Chinese", ButtonSizes.Large));
                    attributes.Add(
                        new ShowIfAttribute(nameof(AesirInspectorLanguageSettingsSO.CurrentIsEnglish)));
                    attributes.Add(new ShowInInspectorAttribute());
                    break;
                case nameof(AesirInspectorLanguageSettingsSO.SetEnglish):
                    attributes.Add(new ButtonAttribute("设置为英文", ButtonSizes.Large));
                    attributes.Add(
                        new ShowIfAttribute(nameof(AesirInspectorLanguageSettingsSO.CurrentIsChinese)));
                    attributes.Add(new ShowInInspectorAttribute());
                    break;
            }
        }
    }
}
