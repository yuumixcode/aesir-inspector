using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    [Summary("双语按钮特性的 Drawer，根据当前语言显示中文或英文按钮文本")]
    public class BilingualButtonAttributeDrawer : OdinAttributeDrawer<BilingualButtonAttribute>
    {
        ButtonAttribute _buttonAttribute;
        ValueResolver<string> _chineseGetter;
        ValueResolver<string> _englishGetter;

        protected override void Initialize()
        {
            _buttonAttribute = Property.GetAttribute<ButtonAttribute>();
            _chineseGetter = ValueResolver.GetForString(Property, Attribute.ChineseName);
            _englishGetter = ValueResolver.GetForString(Property, Attribute.EnglishName);
            _buttonAttribute.Name =
                $"@{nameof(AesirInspectorLanguageSettingsSO)}.{nameof(AesirInspectorLanguageSettingsSO.CurrentIsChinese)} ? \"{_chineseGetter.GetValue()}\" : \"{_englishGetter.GetValue()}\"";
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }
    }
}
