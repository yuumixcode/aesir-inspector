namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// TextArea 特性介绍面板，展示 TextArea 用法及案例预览。
    /// </summary>
    [Summary("TextArea 特性介绍面板，展示 TextArea 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Unity)]
    public class TextAreaAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new TextAreaAttributeData());
        }
    }
}
