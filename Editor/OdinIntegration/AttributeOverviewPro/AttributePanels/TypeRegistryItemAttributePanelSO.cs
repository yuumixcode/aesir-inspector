namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// TypeRegistryItem 特性介绍面板，展示 TypeRegistryItem 用法及案例预览。
    /// </summary>
    [Summary("TypeRegistryItem 特性介绍面板，展示 TypeRegistryItem 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Misc)]
    public class TypeRegistryItemAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new TypeRegistryItemAttributeData());
        }
    }
}
