namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("DisallowModificationsIn 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class DisallowModificationsInAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("DisallowModificationsIn", "DisallowModificationsIn",
                "DisallowModificationsIn 特性用于禁用/灰显成员，防止对其进行修改，并启用验证，在属性被修改前引入时提供错误消息。此特性仅在 Prefab 实例上生效。",
                "DisallowModificationsIn disables / grays out members, preventing modifications from being made and enables validation, providing error messages in case a modification was made prior to introducing the attribute. This attribute only takes effect on prefab instances.",
                OdinInspectorDocumentationLinks.DisallowModificationsInUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData("此特性仅在 Prefab 实例上生效，在 ScriptableObject 中仅展示声明方式。",
                "This attribute only takes effect on prefab instances; in ScriptableObject it only shows the declaration pattern.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(Sirenix.OdinInspector.PrefabKind).FullName, "PrefabKind",
                new BilingualData("指定在哪种 Prefab 类型中禁止修改。",
                    "Specifies which Prefab kind to disallow modifications in."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = { };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Parameter: PrefabKind",
                DisallowModificationsInExampleSO.Instance)
        };
    }
}