namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// ShowInInlineEditors 特性介绍面板，展示 ShowInInlineEditors 用法及案例预览。
    /// </summary>
    [Summary("ShowInInlineEditors 特性介绍面板，展示 ShowInInlineEditors 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Conditionals)]
    public class ShowInInlineEditorsAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new ShowInInlineEditorsAttributeData());
        }
    }
}
