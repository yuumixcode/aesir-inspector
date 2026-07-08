namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// HideDuplicateReferenceBox 特性介绍面板，展示 HideDuplicateReferenceBox 用法及案例预览。
    /// </summary>
    [Summary("HideDuplicateReferenceBox 特性介绍面板，展示 HideDuplicateReferenceBox 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Misc)]
    public class HideDuplicateReferenceBoxAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new HideDuplicateReferenceBoxAttributeData());
        }
    }
}
