namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("DisableContextMenu 特性的介绍数据，包含标题和案例预览项")]
    internal class DisableContextMenuAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("DisableContextMenu", "DisableContextMenu",
                "DisableContextMenu 特性用于禁用字段的右键上下文菜单。",
                "The DisableContextMenu attribute disables the context menu for a field.",
                OdinInspectorDocumentationLinks.DisableContextMenuUrl);

        public override BilingualData[] UsageTips { get; set; } = null;
        public override ParameterValue[] AttributeParameters { get; set; } = null;
        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = null;

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("No Parameters",
                DisableContextMenuExampleSO.Instance)
        };
    }
}
