using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// 在 Odin Inspector 中绘制 BilingualTitleAttribute 标题。
    /// </summary>
    [DrawerPriority(1)]
    [Summary("在 Odin Inspector 中绘制 BilingualTitleAttribute 标题，支持根据检查器语言动态切换标题与子标题")]
    public class BilingualTitleAttributeDrawer : OdinAttributeDrawer<BilingualTitleAttribute>,
        IDisposable
    {
        ValueResolver<string> _subTitleResolver;
        ValueResolver<string> _titleResolver;

        public void Dispose()
        {
            AesirInspectorLanguageSettingsSO.LanguageChanged -= OnLanguageChanged;
        }

        protected override void Initialize()
        {
            _titleResolver = ValueResolver.GetForString(Property, GetAttributeTitle());
            _subTitleResolver = ValueResolver.GetForString(Property, GetAttributeSubTitle());
            AesirInspectorLanguageSettingsSO.LanguageChanged += OnLanguageChanged;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (Attribute.BeforeSpace)
            {
                if (Property != Property.Tree.GetRootProperty(0))
                {
                    EditorGUILayout.Space();
                }
            }

            var flag = true;
            if (_titleResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_titleResolver.ErrorMessage);
                flag = false;
            }

            if (_subTitleResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_subTitleResolver.ErrorMessage);
                flag = false;
            }

            if (flag)
            {
                SirenixEditorGUI.Title(_titleResolver.GetValue(), _subTitleResolver.GetValue(),
                    (TextAlignment)Attribute.TitleAlignment, Attribute.HorizontalLine, Attribute.Bold);
            }

            CallNextDrawer(label);
        }

        void OnLanguageChanged()
        {
            _titleResolver = ValueResolver.GetForString(Property, GetAttributeTitle());
            _subTitleResolver = ValueResolver.GetForString(Property, GetAttributeSubTitle());
            Property.Tree.DelayAction(() => Property.RefreshSetup());
        }

        string GetAttributeTitle() =>
            AesirInspectorLanguageSettingsSO.CurrentIsChinese ? Attribute.ChineseTitle : Attribute.EnglishTitle;

        string GetAttributeSubTitle() =>
            AesirInspectorLanguageSettingsSO.CurrentIsChinese ? Attribute.ChineseSubTitle : Attribute.EnglishSubTitle;
    }
}
