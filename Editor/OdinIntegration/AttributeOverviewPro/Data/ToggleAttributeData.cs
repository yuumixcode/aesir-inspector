namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("Toggle 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class ToggleAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("Toggle", "Toggle",
                "Toggle 特性用于任何字段或属性，允许在 Inspector 中启用或禁用该属性。可用于创建可开关的属性。",
                "Toggle is used on any field or property, and allows to enable or disable the property in the inspector. Use this to create a property that can be turned off or on.",
                OdinInspectorDocumentationLinks.ToggleUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData("Toggle 特性通过指定目标布尔字段名来控制开关状态。",
                "The Toggle attribute controls the toggle state by specifying a target boolean field name."),
            new BilingualData("可应用于自定义类或结构体，使其支持开关控制。",
                "Can be applied to custom classes or structs to enable toggle control.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(string).FullName, "MemberName",
                new BilingualData("控制开关状态的布尔成员名称。",
                    "The name of the boolean member that controls the toggle state."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = { };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Basic Usage",
                ToggleExampleSO.Instance)
        };
    }
}