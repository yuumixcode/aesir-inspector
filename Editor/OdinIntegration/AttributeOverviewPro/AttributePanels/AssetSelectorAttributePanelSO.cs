namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// AssetSelector 特性介绍面板，展示 AssetSelector 用法及案例预览。
    /// </summary>
    [Summary("AssetSelector 特性介绍面板，展示 AssetSelector 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics)]
    public class AssetSelectorAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new AssetSelectorAttributeData());
        }
    }
}