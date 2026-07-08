namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("HideNetworkBehaviourFields 特性的介绍数据，包含标题和案例预览项")]
    internal class HideNetworkBehaviourFieldsAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("HideNetworkBehaviourFields", "HideNetworkBehaviourFields",
                "HideNetworkBehaviourFields 特性应用于类，防止 NetworkBehaviour 的特殊属性（Network Channel 和 Network Send Interval）在 Inspector 中显示。此特性对非 NetworkBehaviour 派生的类无效。",
                "Apply HideNetworkBehaviourFields to your class to prevent the special 'Network Channel' and 'Network Send Interval' properties from being shown in the inspector for a NetworkBehaviour. This attribute has no effect on classes that are not derived from NetworkBehaviour.",
                OdinInspectorDocumentationLinks.HideNetworkBehaviourFieldsUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData(
                "此特性仅对 NetworkBehaviour 派生类有效。",
                "This attribute only has effect on classes derived from NetworkBehaviour.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } = null;
        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = null;

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("No Parameters",
                HideNetworkBehaviourFieldsExampleSO.Instance)
        };
    }
}
