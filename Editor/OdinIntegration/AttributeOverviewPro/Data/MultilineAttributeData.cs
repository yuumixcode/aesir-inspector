namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("Multiline 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class MultilineAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("Multiline", "Multiline",
                "Multiline 是 Unity 内置特性，Odin 增强了其绘制效果。它将字符串字段显示为固定行数的多行文本框。",
                "Multiline is a Unity built-in attribute enhanced by Odin. It displays a string field as a multi-line text field with a fixed number of lines.",
                OdinInspectorDocumentationLinks.MultilineUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData(
                "TextArea 指定最小和最大行数，会根据内容扩展；Multiline 和 MultiLineProperty 指定固定行数，不会扩展。Odin 的 MultiLineProperty 可以应用于任何成员类型。",
                "TextArea specifies min/max lines and expands with content; Multiline and MultiLineProperty use a fixed number of lines. Odin's MultiLineProperty can be applied to any member type.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(int).FullName, "Lines",
                new BilingualData("显示的行数。", "The number of lines to display."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = { };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Parameter: Lines",
                MultilineExampleSO.Instance)
        };
    }
}
