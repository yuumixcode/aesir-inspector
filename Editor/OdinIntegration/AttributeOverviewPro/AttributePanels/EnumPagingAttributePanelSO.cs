namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// EnumPaging 特性介绍面板，展示 EnumPaging 用法及案例预览。
    /// </summary>
    [Summary("EnumPaging 特性介绍面板，展示 EnumPaging 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics | AesirAttributeCategory.Buttons)]
    public class EnumPagingAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new EnumPagingAttributeData());
        }
    }
}