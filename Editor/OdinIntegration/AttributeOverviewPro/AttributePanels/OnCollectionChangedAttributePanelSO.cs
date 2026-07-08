namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// OnCollectionChanged 特性介绍面板，展示 OnCollectionChanged 用法及案例预览。
    /// </summary>
    [Summary("OnCollectionChanged 特性介绍面板，展示 OnCollectionChanged 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Misc)]
    public class OnCollectionChangedAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new OnCollectionChangedAttributeData());
        }
    }
}