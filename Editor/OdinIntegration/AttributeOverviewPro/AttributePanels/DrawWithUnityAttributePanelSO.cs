namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// DrawWithUnity 特性介绍面板，展示 DrawWithUnity 用法及案例预览。
    /// </summary>
    [Summary("DrawWithUnity 特性介绍面板，展示 DrawWithUnity 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Misc)]
    public class DrawWithUnityAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new DrawWithUnityAttributeData());
        }
    }
}