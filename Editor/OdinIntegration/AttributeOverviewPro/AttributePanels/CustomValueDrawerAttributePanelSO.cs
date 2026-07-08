namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// CustomValueDrawer 特性介绍面板。
    /// </summary>
    [Summary("CustomValueDrawer 特性介绍面板，展示 CustomValueDrawer 各参数用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Essentials)]
    public class CustomValueDrawerAttributePanelSO : AbstractAttributePanelSO
    {
        public override void Initialize()
        {
            SetData(new CustomValueDrawerAttributeData());
        }
    }
}
