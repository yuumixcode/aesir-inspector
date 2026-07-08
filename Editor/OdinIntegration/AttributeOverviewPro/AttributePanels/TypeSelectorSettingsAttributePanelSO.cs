namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// TypeSelectorSettings 特性介绍面板，展示 TypeSelectorSettings 用法及案例预览。
    /// </summary>
    [Summary("TypeSelectorSettings 特性介绍面板，展示 TypeSelectorSettings 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Misc)]
    public class TypeSelectorSettingsAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new TypeSelectorSettingsAttributeData());
        }
    }
}
