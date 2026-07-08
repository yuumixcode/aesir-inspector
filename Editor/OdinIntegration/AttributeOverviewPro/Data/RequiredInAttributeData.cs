namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// RequiredIn 特性的介绍数据。
    /// </summary>
    [Summary("RequiredIn 特性的介绍数据，包含标题、参数说明、解析字符串参数和案例预览项")]
    internal class RequiredInAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("RequiredIn", "RequiredIn",
                "RequiredIn 是 Required 特性的变体，专门用于预制体（Prefab）对象。它允许你指定属性在特定的预制体类型中不能为空。",
                "RequiredIn is a variant of the Required attribute specifically for Prefab objects. It allows you to specify that a property must not be null in certain prefab kinds.",
                OdinInspectorDocumentationLinks.RequiredInUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData("适用于预制体资源或预制体实例中的脚本属性。",
                "Applicable to script properties in prefab assets or prefab instances."),
            new BilingualData("通过 PrefabKind 参数，你可以精确控制在哪些预制体状态下触发必填检查。",
                "Via the PrefabKind parameter, you can precisely control in which prefab states the required check is triggered."),
            new BilingualData("支持自定义错误消息，并且支持解析器（Resolvers）。",
                "Supports custom error messages and resolvers.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue("PrefabKind", "Kind",
                new BilingualData("指定该属性必须存在的预制体类型，可以用 | 组合多种类型。",
                    "Specifies the prefab kinds where this property must be present. Multiple kinds can be combined with |.")),
            new ParameterValue(">>> PrefabKind", "PrefabKind.None",
                new BilingualData("无意义，枚举占位符。", "No meaning, enum placeholder.")),
            new ParameterValue(">>> PrefabKind", "PrefabKind.InstanceInScene",
                new BilingualData("当前脚本挂载的物体是预制体且是场景中的实例时生效，判断标记的属性是否为空。",
                    "Takes effect when the GameObject is a prefab instance in the scene, checking if the marked property is null.")),
            new ParameterValue(">>> PrefabKind", "PrefabKind.InstanceInPrefab",
                new BilingualData("当前脚本挂载的物体是嵌套在其他预制体中的实例时生效，判断标记的属性是否为空。",
                    "Takes effect when the GameObject is a nested prefab instance inside another prefab, checking if the marked property is null.")),
            new ParameterValue(">>> PrefabKind", "PrefabKind.Regular",
                new BilingualData("当前脚本挂载的物体是 Regular Prefab 时生效，判断标记的属性是否为空。",
                    "Takes effect when the GameObject is a regular prefab asset, checking if the marked property is null.")),
            new ParameterValue(">>> PrefabKind", "PrefabKind.Variant",
                new BilingualData("当前脚本挂载的物体是 Prefab Variant（变体）时生效，判断标记的属性是否为空。",
                    "Takes effect when the GameObject is a prefab variant, checking if the marked property is null.")),
            new ParameterValue(">>> PrefabKind", "PrefabKind.NonPrefabInstance",
                new BilingualData("当前脚本挂载的物体是场景中的非预制体实例时生效，判断标记的属性是否为空。",
                    "Takes effect when the GameObject is a non-prefab instance in the scene, checking if the marked property is null.")),
            new ParameterValue(">>> PrefabKind", "PrefabKind.PrefabInstance",
                new BilingualData("PrefabInstance = InstanceInPrefab | InstanceInScene。",
                    "PrefabInstance = InstanceInPrefab | InstanceInScene.")),
            new ParameterValue(">>> PrefabKind", "PrefabKind.PrefabAsset",
                new BilingualData("PrefabAsset = Variant | Regular。",
                    "PrefabAsset = Variant | Regular.")),
            new ParameterValue(">>> PrefabKind", "PrefabKind.PrefabInstanceAndNonPrefabInstance",
                new BilingualData("PrefabInstanceAndNonPrefabInstance = InstanceInPrefab | InstanceInScene | NonPrefabInstance。",
                    "PrefabInstanceAndNonPrefabInstance = InstanceInPrefab | InstanceInScene | NonPrefabInstance.")),
            new ParameterValue(">>> PrefabKind", "PrefabKind.All",
                new BilingualData("All = PrefabInstanceAndNonPrefabInstance | PrefabAsset。",
                    "All = PrefabInstanceAndNonPrefabInstance | PrefabAsset.")),
            new ParameterValue(typeof(string).FullName, "ErrorMessage",
                new BilingualData("当验证失败时显示的自定义错误消息，支持所有解析器。",
                    "Custom error message to display when validation fails. Supports all resolvers."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = { };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Basic Usage",
                RequiredInExampleSO.Instance)
        };
    }
}
