namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// ColorPalette 特性介绍面板，展示 ColorPalette 用法及案例预览。
    /// </summary>
    [Summary("ColorPalette 特性介绍面板，展示 ColorPalette 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics)]
    public class ColorPaletteAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new ColorPaletteAttributeData());
        }
    }
}