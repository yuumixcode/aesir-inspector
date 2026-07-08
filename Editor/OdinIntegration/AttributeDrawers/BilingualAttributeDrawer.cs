using System;
using Sirenix.OdinInspector.Editor;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("支持双语自动重绘的 Odin 特性绘制器基类")]
    public abstract class BilingualAttributeDrawer<TAttribute> : OdinAttributeDrawer<TAttribute>, IDisposable
        where TAttribute : Attribute
    {
        public void Dispose()
        {
            AesirInspectorLanguageSettingsSO.LanguageChanged -= InternalLanguageChanged;
        }

        protected override void Initialize()
        {
            AesirInspectorLanguageSettingsSO.LanguageChanged += InternalLanguageChanged;
            OnInitialize();
        }

        protected virtual void OnInitialize() { }

        protected virtual void OnLanguageChanged()
        {
            Property.Tree.DelayAction(() => Property.RefreshSetup());
        }

        void InternalLanguageChanged()
        {
            OnLanguageChanged();
        }
    }
}
