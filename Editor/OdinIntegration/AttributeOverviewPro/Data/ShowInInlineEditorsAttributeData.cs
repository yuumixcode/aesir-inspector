namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("ShowInInlineEditors 特性的介绍数据，包含标题和案例预览项")]
    internal class ShowInInlineEditorsAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("ShowInInlineEditors", "ShowInInlineEditors",
                "ShowInInlineEditors 特性用于确保属性在内联编辑器中显示。默认情况下，内联编辑器会隐藏某些属性，此特性可以覆盖该行为。",
                "The ShowInInlineEditors attribute ensures a property is shown in inline editors. By default, inline editors hide certain properties; this attribute overrides that behavior.",
                OdinInspectorDocumentationLinks.ShowInInlineEditorUrl);

        public override BilingualData[] UsageTips { get; set; } = null;
        public override ParameterValue[] AttributeParameters { get; set; } = null;
        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = null;

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("No Parameters",
                ShowInInlineEditorExampleSO.Instance)
        };
    }
}
