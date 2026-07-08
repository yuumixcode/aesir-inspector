namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// ShowDrawerChain 特性介绍面板，展示 ShowDrawerChain 用法及案例预览。
    /// </summary>
    [Summary("ShowDrawerChain 特性介绍面板，展示 ShowDrawerChain 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Debug)]
    public class ShowDrawerChainAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new ShowDrawerChainAttributeData());
        }
    }
}