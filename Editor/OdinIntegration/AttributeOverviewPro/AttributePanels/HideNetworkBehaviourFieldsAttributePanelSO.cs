namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// HideNetworkBehaviourFields 特性介绍面板，展示 HideNetworkBehaviourFields 用法及案例预览。
    /// </summary>
    [Summary("HideNetworkBehaviourFields 特性介绍面板，展示 HideNetworkBehaviourFields 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics)]
    public class HideNetworkBehaviourFieldsAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new HideNetworkBehaviourFieldsAttributeData());
        }
    }
}
