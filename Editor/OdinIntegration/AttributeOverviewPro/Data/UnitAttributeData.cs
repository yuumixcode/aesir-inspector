namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("Unit 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class UnitAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("Unit", "Unit",
                "Unit 特性允许在单位之间进行转换，并以更友好的单位显示值。",
                "Unit allows for converting between units, and displaying a value in a more user-friendly unit.",
                OdinInspectorDocumentationLinks.UnitUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData("可以通过 UnitNumberUtility.AddCustomUnit 注册自定义单位。",
                "Custom units can be registered via UnitNumberUtility.AddCustomUnit.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(Sirenix.OdinInspector.Units).FullName, "ActualUnit",
                new BilingualData("实际的单位。",
                    "The actual unit of the value.")),
            new ParameterValue(typeof(Sirenix.OdinInspector.Units).FullName, "DisplayUnit",
                new BilingualData("显示使用的单位。",
                    "The unit to display the value in.")),
            new ParameterValue(typeof(bool).FullName, "DisplayAsString",
                new BilingualData("是否以字符串形式显示单位转换后的值。",
                    "Whether to display the converted value as a string.")),
            new ParameterValue(typeof(bool).FullName, "ForceDisplayUnit",
                new BilingualData("是否强制显示单位。",
                    "Whether to force displaying the unit."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = { };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Basic Usage",
                UnitExampleSO.Instance)
        };
    }
}