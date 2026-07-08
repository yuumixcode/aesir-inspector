namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("OnInspectorDispose 特性介绍面板，展示 OnInspectorDispose 各参数用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Misc)]
    public class OnInspectorDisposeAttributePanelSO : AbstractAttributePanelSO
    {
        public override void Initialize()
        {
            SetData(new OnInspectorDisposeAttributeData());
        }
    }
}
