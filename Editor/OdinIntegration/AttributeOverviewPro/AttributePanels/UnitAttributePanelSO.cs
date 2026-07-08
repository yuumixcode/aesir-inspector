namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// Unit 特性介绍面板，展示 Unit 用法及案例预览。
    /// </summary>
    [Summary("Unit 特性介绍面板，展示 Unit 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Numbers)]
    public class UnitAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new UnitAttributeData());
        }
    }
}