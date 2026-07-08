namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// HideInTables 特性介绍面板，展示 HideInTables 用法及案例预览。
    /// </summary>
    [Summary("HideInTables 特性介绍面板，展示 HideInTables 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics)]
    public class HideInTablesAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new HideInTablesAttributeData());
        }
    }
}
