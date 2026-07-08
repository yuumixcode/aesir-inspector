namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// DisableContextMenu 特性介绍面板，展示 DisableContextMenu 用法及案例预览。
    /// </summary>
    [Summary("DisableContextMenu 特性介绍面板，展示 DisableContextMenu 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Misc)]
    public class DisableContextMenuAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new DisableContextMenuAttributeData());
        }
    }
}
