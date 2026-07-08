namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// ResponsiveButtonGroup 特性介绍面板，展示 ResponsiveButtonGroup 用法及案例预览。
    /// </summary>
    [Summary("ResponsiveButtonGroup 特性介绍面板，展示 ResponsiveButtonGroup 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Groups | AesirAttributeCategory.Buttons)]
    public class ResponsiveButtonGroupAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new ResponsiveButtonGroupAttributeData());
        }
    }
}