namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// Toggle 特性介绍面板，展示 Toggle 用法及案例预览。
    /// </summary>
    [Summary("Toggle 特性介绍面板，展示 Toggle 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics)]
    public class ToggleAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new ToggleAttributeData());
        }
    }
}