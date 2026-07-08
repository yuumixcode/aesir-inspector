namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// LabelWidth 特性介绍面板。
    /// </summary>
    [Summary("LabelWidth 特性介绍面板，展示 LabelWidth 特性的用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Misc)]
    public class LabelWidthAttributePanelSO : AbstractAttributePanelSO
    {
        public override void Initialize()
        {
            SetData(new LabelWidthAttributeData());
        }
    }
}
