namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("TextArea 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class TextAreaAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("TextArea", "TextArea",
                "TextArea 是 Unity 内置特性，Odin 增强了其绘制效果。它将字符串字段显示为高度灵活且可滚动的文本区域。可以指定最小和最大行数。",
                "TextArea is a Unity built-in attribute enhanced by Odin. It displays a string field as a height-flexible and scrollable text area. You can specify the minimum and maximum lines.",
                OdinInspectorDocumentationLinks.TextAreaUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData(
                "TextArea 指定最小和最大行数，会根据内容自动扩展；Multiline 和 MultiLineProperty 使用固定行数。",
                "TextArea specifies min/max lines and expands with content; Multiline and MultiLineProperty use a fixed number of lines.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(int).FullName, "MinLines",
                new BilingualData("最小行数。", "The minimum number of lines.")),
            new ParameterValue(typeof(int).FullName, "MaxLines",
                new BilingualData(
                    "最大行数。注意：最大行数指的是文本区域的最大大小，不是用户可输入的最大行数。",
                    "The maximum number of lines. Note: The maximum lines refers to the maximum size of the TextArea, not the maximum number of lines the user can enter."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = { };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Parameter: MinLines, MaxLines",
                TextAreaExampleSO.Instance)
        };
    }
}
