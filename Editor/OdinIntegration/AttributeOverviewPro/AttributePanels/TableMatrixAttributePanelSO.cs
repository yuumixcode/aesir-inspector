namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// TableMatrix 特性介绍面板，展示 TableMatrix 用法及案例预览。
    /// </summary>
    [Summary("TableMatrix 特性介绍面板，展示 TableMatrix 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics | AesirAttributeCategory.Collections)]
    public class TableMatrixAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new TableMatrixAttributeData());
        }
    }
}
