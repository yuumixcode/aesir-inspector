namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// Range 特性介绍面板，展示 Range 用法及案例预览。
    /// </summary>
    [Summary("Range 特性介绍面板，展示 Range 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Unity | AesirAttributeCategory.Validation)]
    public class RangeAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new RangeAttributeData());
        }
    }
}
