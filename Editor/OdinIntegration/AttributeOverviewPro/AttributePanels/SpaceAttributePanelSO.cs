namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// Space 特性介绍面板，展示 Space 用法及案例预览。
    /// </summary>
    [Summary("Space 特性介绍面板，展示 Space 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Unity)]
    public class SpaceAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new SpaceAttributeData());
        }
    }
}
