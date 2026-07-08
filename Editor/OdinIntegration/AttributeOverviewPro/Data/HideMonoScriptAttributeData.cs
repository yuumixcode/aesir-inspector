namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("HideMonoScript 特性的介绍数据，包含标题和案例预览项")]
    internal class HideMonoScriptAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("HideMonoScript", "HideMonoScript",
                "HideMonoScript 特性用于隐藏 Inspector 顶部的 MonoBehaviour 脚本图标区域。",
                "The HideMonoScript attribute hides the MonoBehaviour script icon area at the top of the Inspector.",
                OdinInspectorDocumentationLinks.HideMonoScriptUrl);

        public override BilingualData[] UsageTips { get; set; } = null;
        public override ParameterValue[] AttributeParameters { get; set; } = null;
        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = null;

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("No Parameters",
                HideMonoScriptExampleSO.Instance)
        };
    }
}
