namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// EnableGUI 特性的介绍数据。
    /// </summary>
    [Summary("EnableGUI 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class EnableGUIAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("EnableGUI", "EnableGUI", "强制启用 property，使其正常显示。",
                "Forces a property to be enabled so it displays normally.",
                OdinInspectorDocumentationLinks.EnableGuiUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData("部分 property 是灰色显示无法修改，可以使用 [EnableGUI] 来恢复正常显示，但仅影响外观，实际值仍不可修改。",
                "Some properties are grayed out and cannot be modified. Use [EnableGUI] to restore normal display, but it only affects appearance; the actual value still cannot be modified."),
            new BilingualData("常与 [ReadOnly] 或 [ShowInInspector] 的只读属性配合使用，使其在 Inspector 中可以获取焦点。",
                "Often used with [ReadOnly] or [ShowInInspector] read-only properties to make them focusable in the Inspector.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } = { };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = { };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Basic Usage",
                EnableGUIExampleSO.Instance)
        };
    }
}
