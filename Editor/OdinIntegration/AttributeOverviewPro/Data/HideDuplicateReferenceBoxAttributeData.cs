namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("HideDuplicateReferenceBox 特性的介绍数据，包含标题和案例预览项")]
    internal class HideDuplicateReferenceBoxAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("HideDuplicateReferenceBox", "HideDuplicateReferenceBox",
                "HideDuplicateReferenceBox 特性用于隐藏重复引用提示框。当多个属性引用同一个对象时，Odin 默认会显示一个提示框，此特性可以隐藏它。",
                "The HideDuplicateReferenceBox attribute hides the duplicate reference info box. When multiple properties reference the same object, Odin shows an info box by default; this attribute hides it.",
                OdinInspectorDocumentationLinks.HideDuplicateReferenceBoxUrl);

        public override BilingualData[] UsageTips { get; set; } = null;
        public override ParameterValue[] AttributeParameters { get; set; } = null;
        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = null;

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("No Parameters",
                HideDuplicateReferenceBoxExampleSO.Instance)
        };
    }
}
