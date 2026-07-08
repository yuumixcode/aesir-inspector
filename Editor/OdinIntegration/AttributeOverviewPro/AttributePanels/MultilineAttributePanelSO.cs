namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// Multiline 特性介绍面板，展示 Multiline 用法及案例预览。
    /// </summary>
    [Summary("Multiline 特性介绍面板，展示 Multiline 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Unity)]
    public class MultilineAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new MultilineAttributeData());
        }
    }
}
