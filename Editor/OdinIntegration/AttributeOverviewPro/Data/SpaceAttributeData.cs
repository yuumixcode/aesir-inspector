namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("Space 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class SpaceAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("Space", "Space",
                "Space 是 Unity 内置特性，Odin 增强了其绘制效果。它在 Inspector 中添加间距。",
                "Space is a Unity built-in attribute enhanced by Odin. It adds spacing in the Inspector.",
                OdinInspectorDocumentationLinks.SpaceUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData(
                "PropertySpace 和 Space 特性功能几乎相同，但 PropertySpace 还可以应用于属性（Property）。",
                "PropertySpace and Space attributes are virtually identical, but PropertySpace can also be applied to properties.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(float).FullName, "Height",
                new BilingualData("间距高度。", "The height of the space."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = { };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Basic Usage",
                SpaceExampleSO.Instance)
        };
    }
}
