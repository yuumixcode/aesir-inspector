namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("DrawWithUnity 特性的介绍数据，包含标题和案例预览项")]
    internal class DrawWithUnityAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("DrawWithUnity", "DrawWithUnity",
                "DrawWithUnity 特性应用于字段或属性，使 Odin 使用 Unity 的旧绘制系统来绘制它。如果你想要选择性地禁用 Odin 对特定成员的绘制，可以使用此特性。",
                "DrawWithUnity can be applied to a field or property to make Odin draw it using Unity's old drawing system. Use it if you want to selectively disable Odin drawing for a particular member.",
                OdinInspectorDocumentationLinks.DrawWithUnityUrl);

        public override BilingualData[] UsageTips { get; set; } = null;
        public override ParameterValue[] AttributeParameters { get; set; } = null;
        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = { };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("No Parameters",
                DrawWithUnityExampleSO.Instance)
        };
    }
}