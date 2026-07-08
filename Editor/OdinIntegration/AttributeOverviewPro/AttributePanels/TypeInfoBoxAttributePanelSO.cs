namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// TypeInfoBox 特性介绍面板，展示 TypeInfoBox 用法及案例预览。
    /// </summary>
    [Summary("TypeInfoBox 特性介绍面板，展示 TypeInfoBox 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Essentials)]
    public class TypeInfoBoxAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new TypeInfoBoxAttributeData());
        }
    }
}
