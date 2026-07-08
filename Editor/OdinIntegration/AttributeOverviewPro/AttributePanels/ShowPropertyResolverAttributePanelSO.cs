namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// ShowPropertyResolver 特性介绍面板，展示 ShowPropertyResolver 用法及案例预览。
    /// </summary>
    [Summary("ShowPropertyResolver 特性介绍面板，展示 ShowPropertyResolver 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Debug)]
    public class ShowPropertyResolverAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new ShowPropertyResolverAttributeData());
        }
    }
}
