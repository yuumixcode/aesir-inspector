namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// TableColumnWidth 特性介绍面板，展示 TableColumnWidth 用法及案例预览。
    /// </summary>
    [Summary("TableColumnWidth 特性介绍面板，展示 TableColumnWidth 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Collections)]
    public class TableColumnWidthAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new TableColumnWidthAttributeData());
        }
    }
}
