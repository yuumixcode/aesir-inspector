namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("Range 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class RangeAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("Range", "Range",
                "Range 是 Unity 内置特性，Odin 增强了其绘制效果。它将数值限制在指定的最小值和最大值之间，并以滑块形式显示。",
                "Range is a Unity built-in attribute enhanced by Odin. It clamps the value between a specified minimum and maximum, displaying it as a slider.",
                OdinInspectorDocumentationLinks.RangeUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData(
                "Odin 的 PropertyRange 特性类似 Unity 的 Range 特性，但还支持属性和表达式。",
                "Odin's PropertyRange attribute is similar to Unity's Range attribute, but also works on properties and supports expressions.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(double).FullName, "Min",
                new BilingualData("最小值。", "The minimum value.")),
            new ParameterValue(typeof(double).FullName, "Max",
                new BilingualData("最大值。", "The maximum value."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = { };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Parameter: Min, Max",
                RangeExampleSO.Instance)
        };
    }
}
