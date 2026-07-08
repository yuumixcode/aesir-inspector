namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// DisableInInlineEditors 特性介绍面板，展示 DisableInInlineEditors 用法及案例预览。
    /// </summary>
    [Summary("DisableInInlineEditors 特性介绍面板，展示 DisableInInlineEditors 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Conditionals)]
    public class DisableInInlineEditorsAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new DisableInInlineEditorsAttributeData());
        }
    }
}
