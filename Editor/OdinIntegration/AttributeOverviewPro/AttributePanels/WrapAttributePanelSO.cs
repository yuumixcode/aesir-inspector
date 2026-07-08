namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// Wrap 特性介绍面板，展示 Wrap 用法及案例预览。
    /// </summary>
    [Summary("Wrap 特性介绍面板，展示 Wrap 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Numbers)]
    public class WrapAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new WrapAttributeData());
        }
    }
}
