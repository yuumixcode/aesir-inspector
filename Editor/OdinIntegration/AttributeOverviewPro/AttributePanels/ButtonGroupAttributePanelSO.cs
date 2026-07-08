namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// ButtonGroup 特性介绍面板，展示 ButtonGroup 用法及案例预览。
    /// </summary>
    [Summary("ButtonGroup 特性介绍面板，展示 ButtonGroup 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Groups | AesirAttributeCategory.Buttons)]
    public class ButtonGroupAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new ButtonGroupAttributeData());
        }
    }
}