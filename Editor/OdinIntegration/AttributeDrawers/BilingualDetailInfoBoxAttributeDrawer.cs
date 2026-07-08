using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [DrawerPriority(0.0, 10001.0)]
    [Summary("带有详细信息的双语信息框特性的 Drawer，根据当前编辑器语言渲染对应语言的信息文本、详细内容与图标样式")]
    public class BilingualDetailInfoBoxAttributeDrawer : OdinAttributeDrawer<BilingualDetailedInfoBoxAttribute>,
        IDisposable
    {
        bool _drawMessageBox;
        ValueResolver<string> _messageResolver;
        ValueResolver<string> _detailsResolver;
        MessageType _messageType;
        ValueResolver<bool> _visibleIfResolver;
        LocalPersistentContext<bool> _isExpanded;

        public void Dispose()
        {
            AesirInspectorLanguageSettingsSO.LanguageChanged -= OnLanguageChanged;
        }

        protected override void Initialize()
        {
            _visibleIfResolver = ValueResolver.Get(Property, Attribute.VisibleIf, true);
            _messageResolver = ValueResolver.GetForString(Property, GetCurrentText());
            _detailsResolver = ValueResolver.GetForString(Property, GetCurrentDetailsText());

            _isExpanded = Property.Context.GetPersistent(this, "isExpanded", false);
            _drawMessageBox = _visibleIfResolver.GetValue();

            switch (Attribute.InfoMessageType)
            {
                case InfoMessageType.Info:
                    _messageType = MessageType.Info;
                    break;
                case InfoMessageType.Warning:
                    _messageType = MessageType.Warning;
                    break;
                case InfoMessageType.Error:
                    _messageType = MessageType.Error;
                    break;
                default:
                    _messageType = MessageType.None;
                    break;
            }

            AesirInspectorLanguageSettingsSO.LanguageChanged += OnLanguageChanged;
        }

        void OnLanguageChanged()
        {
            _messageResolver = ValueResolver.GetForString(Property, GetCurrentText());
            _detailsResolver = ValueResolver.GetForString(Property, GetCurrentDetailsText());
            Property.Tree.DelayAction(() => Property.RefreshSetup());
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            var flag = true;
            if (_visibleIfResolver.HasError)
            {
                SirenixEditorGUI.MessageBox(_visibleIfResolver.ErrorMessage, MessageType.Error,
                    GlobalConfig<GeneralDrawerConfig>.Instance.MessageBoxFontSize);
                flag = false;
            }

            if (_messageResolver.HasError)
            {
                SirenixEditorGUI.MessageBox(_messageResolver.ErrorMessage, MessageType.Error,
                    GlobalConfig<GeneralDrawerConfig>.Instance.MessageBoxFontSize);
                flag = false;
            }

            if (_detailsResolver.HasError)
            {
                SirenixEditorGUI.MessageBox(_detailsResolver.ErrorMessage, MessageType.Error,
                    GlobalConfig<GeneralDrawerConfig>.Instance.MessageBoxFontSize);
                flag = false;
            }

            if (flag)
            {
                if (Attribute.GUIAlwaysEnabled)
                {
                    GUIHelper.PushGUIEnabled(true);
                }

                if (Event.current.type == EventType.Layout)
                {
                    _drawMessageBox = _visibleIfResolver.GetValue();
                }

                if (_drawMessageBox)
                {
                    var message = _messageResolver.GetValue();
                    var details = _detailsResolver.GetValue();

                    _isExpanded.Value = SirenixEditorGUI.DetailedMessageBox(message, details, _messageType, _isExpanded.Value);
                }

                if (Attribute.GUIAlwaysEnabled)
                {
                    GUIHelper.PopGUIEnabled();
                }
            }

            CallNextDrawer(label);
        }

        string GetCurrentText() =>
            AesirInspectorLanguageSettingsSO.CurrentIsChinese ? Attribute.ChineseText : Attribute.EnglishText;

        string GetCurrentDetailsText() =>
            AesirInspectorLanguageSettingsSO.CurrentIsChinese ? Attribute.DetailsChineseText : Attribute.DetailsEnglishText;
    }
}
