using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    public sealed class BilingualTextAttributeDrawer : OdinAttributeDrawer<BilingualTextAttribute>,
        IDisposable
    {
        ValueResolver<Color> _iconColorResolver;
        Texture2D _iconTexture;
        GUIContent _overrideLabel;
        ValueResolver<string> _textProvider;

        public void Dispose()
        {
            AesirInspectorLanguageSettingsSO.LanguageChanged -= OnLanguageChanged;

            if (_iconTexture != null)
            {
                Object.DestroyImmediate(_iconTexture);
                _iconTexture = null;
            }
        }

        protected override void Initialize()
        {
            _textProvider = ValueResolver.GetForString(Property, GetCurrentText());
            _iconColorResolver =
                ValueResolver.Get(Property, Attribute.IconColor, EditorStyles.label.normal.textColor);
            _overrideLabel = new GUIContent();
            if (Attribute.Icon != SdfIconType.None)
            {
                _iconTexture = SdfIcons.CreateTransparentIconTexture(Attribute.Icon,
                    _iconColorResolver.GetValue(), 32, 32, 0);
            }

            AesirInspectorLanguageSettingsSO.LanguageChanged += OnLanguageChanged;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (Property.GetAttribute<HideLabelAttribute>() != null)
            {
                CallNextDrawer(null);
                return;
            }

            if (_textProvider.HasError)
            {
                SirenixEditorGUI.MessageBox(_textProvider.ErrorMessage, MessageType.Error,
                    GlobalConfig<GeneralDrawerConfig>.Instance.MessageBoxFontSize);
                CallNextDrawer(label);
            }
            else if (_iconColorResolver.HasError)
            {
                SirenixEditorGUI.MessageBox(_iconColorResolver.ErrorMessage, MessageType.Error,
                    GlobalConfig<GeneralDrawerConfig>.Instance.MessageBoxFontSize);
                CallNextDrawer(label);
            }
            else
            {
                var str = _textProvider.GetValue();
                GUIContent nextLabel;

                if (str == null && Attribute.Icon == SdfIconType.None)
                {
                    nextLabel = label;
                }
                else
                {
                    var name = str ?? label?.text ?? "";
                    if (Attribute.NicifyEnglishText && !AesirInspectorLanguageSettingsSO.CurrentIsChinese)
                    {
                        name = ObjectNames.NicifyVariableName(name);
                    }

                    _overrideLabel.text = name;
                    nextLabel = _overrideLabel;

                    if (Attribute.Icon != SdfIconType.None)
                    {
                        nextLabel.image = _iconTexture;
                    }
                }

                CallNextDrawer(nextLabel);
            }
        }

        void OnLanguageChanged()
        {
            _textProvider = ValueResolver.GetForString(Property, GetCurrentText());
            Property.Tree.DelayAction(() => Property.RefreshSetup());
        }

        string GetCurrentText() =>
            AesirInspectorLanguageSettingsSO.CurrentIsChinese ? Attribute.ChineseText : Attribute.EnglishText;
    }
}
