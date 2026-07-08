namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// HideMonoScript 特性介绍面板，展示 HideMonoScript 用法及案例预览。
    /// </summary>
    [Summary("HideMonoScript 特性介绍面板，展示 HideMonoScript 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics)]
    public class HideMonoScriptAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new HideMonoScriptAttributeData());
        }
    }
}
