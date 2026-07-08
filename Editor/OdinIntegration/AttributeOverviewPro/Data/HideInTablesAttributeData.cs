namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("HideInTables 特性的介绍数据，包含标题和案例预览项")]
    internal class HideInTablesAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("HideInTables", "HideInTables",
                "HideInTables 特性用于在表格列表（TableList）中隐藏该字段。",
                "The HideInTables attribute hides a field when displayed in a table list (TableList).",
                OdinInspectorDocumentationLinks.HideInTablesUrl);

        public override BilingualData[] UsageTips { get; set; } = null;
        public override ParameterValue[] AttributeParameters { get; set; } = null;
        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = null;

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("No Parameters",
                HideInTablesExampleSO.Instance)
        };
    }
}
