namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// MinMaxSlider 特性介绍面板。
    /// </summary>
    [Summary("MinMaxSlider 特性介绍面板，展示 MinMaxSlider 各参数用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Numbers | AesirAttributeCategory.Validation)]
    public class MinMaxSliderAttributePanelSO : AbstractAttributePanelSO
    {
        public override void Initialize()
        {
            SetData(new MinMaxSliderAttributeData());
        }
    }
}
