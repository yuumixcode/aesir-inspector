namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// RequiredListLength 特性介绍面板，展示 RequiredListLength 用法及案例预览。
    /// </summary>
    [Summary("RequiredListLength 特性介绍面板，展示 RequiredListLength 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Validation)]
    public class RequiredListLengthAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new RequiredListLengthAttributeData());
        }
    }
}