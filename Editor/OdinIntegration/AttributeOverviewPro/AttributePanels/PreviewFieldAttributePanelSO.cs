namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// PreviewField 特性介绍面板，展示 PreviewField 用法及案例预览。
    /// </summary>
    [Summary("PreviewField 特性介绍面板，展示 PreviewField 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics | AesirAttributeCategory.TypeSpecifics)]
    public class PreviewFieldAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new PreviewFieldAttributeData());
        }
    }
}
