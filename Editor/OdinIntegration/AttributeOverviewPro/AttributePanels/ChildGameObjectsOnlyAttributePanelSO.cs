namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// ChildGameObjectsOnly 特性介绍面板，展示 ChildGameObjectsOnly 用法及案例预览。
    /// </summary>
    [Summary("ChildGameObjectsOnly 特性介绍面板，展示 ChildGameObjectsOnly 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics | AesirAttributeCategory.Validation)]
    public class ChildGameObjectsOnlyAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new ChildGameObjectsOnlyAttributeData());
        }
    }
}