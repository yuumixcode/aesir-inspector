namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// MultiLineProperty 特性介绍面板，展示 MultiLineProperty 用法及案例预览。
    /// </summary>
    [Summary("MultiLineProperty 特性介绍面板，展示 MultiLineProperty 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics )]
    public class MultiLinePropertyAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new MultiLinePropertyAttributeData());
        }
    }
}