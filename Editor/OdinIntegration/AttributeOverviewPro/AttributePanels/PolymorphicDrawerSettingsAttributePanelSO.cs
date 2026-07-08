namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// PolymorphicDrawerSettings 特性介绍面板，展示 PolymorphicDrawerSettings 用法及案例预览。
    /// </summary>
    [Summary("PolymorphicDrawerSettings 特性介绍面板，展示 PolymorphicDrawerSettings 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics)]
    public class PolymorphicDrawerSettingsAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new PolymorphicDrawerSettingsAttributeData());
        }
    }
}