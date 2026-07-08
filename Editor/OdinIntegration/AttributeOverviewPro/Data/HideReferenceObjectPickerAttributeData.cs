namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("HideReferenceObjectPicker 特性的介绍数据，包含标题和案例预览项")]
    internal class HideReferenceObjectPickerAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("HideReferenceObjectPicker", "HideReferenceObjectPicker",
                "HideReferenceObjectPicker 特性用于隐藏引用类型的对象选择器按钮。",
                "The HideReferenceObjectPicker attribute hides the object picker button for reference types.",
                OdinInspectorDocumentationLinks.HideReferenceObjectPickerUrl);

        public override BilingualData[] UsageTips { get; set; } = null;
        public override ParameterValue[] AttributeParameters { get; set; } = null;
        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = null;

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("No Parameters",
                HideReferenceObjectPickerExampleSO.Instance)
        };
    }
}
