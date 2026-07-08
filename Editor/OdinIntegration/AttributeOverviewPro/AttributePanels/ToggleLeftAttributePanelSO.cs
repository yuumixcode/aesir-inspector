namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// ToggleLeft 特性介绍面板，展示 ToggleLeft 用法及案例预览。
    /// </summary>
    [Summary("ToggleLeft 特性介绍面板，展示 ToggleLeft 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics)]
    public class ToggleLeftAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new ToggleLeftAttributeData());
        }
    }
}
