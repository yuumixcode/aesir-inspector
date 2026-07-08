namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// DisallowModificationsIn 特性介绍面板，展示 DisallowModificationsIn 用法及案例预览。
    /// </summary>
    [Summary("DisallowModificationsIn 特性介绍面板，展示 DisallowModificationsIn 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Validation)]
    public class DisallowModificationsInAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new DisallowModificationsInAttributeData());
        }
    }
}