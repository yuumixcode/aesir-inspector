namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// ShowInInspector 特性的介绍数据。
    /// </summary>
    [Summary("ShowInInspector 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class ShowInInspectorAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("Show In Inspector", "在检查器中显示",
                "ShowInInspector 特性用于在 Inspector 中显示非序列化的成员，如私有字段、属性或方法返回值。",
                "The ShowInInspector attribute is used to display non-serialized members in the inspector, such as private fields, properties, or method return values.",
                OdinInspectorDocumentationLinks.ShowInInspectorUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData("通常 Unity 只显示公有的序列化字段。使用此特性，你可以查看那些通常隐藏的状态，而无需将它们设为公有或添加 [SerializeField]。",
                "Normally, Unity only displays public serialized fields. With this attribute, you can view states that are normally hidden without having to make them public or adding [SerializeField]."),
            new BilingualData("请注意，仅使用 [ShowInInspector] 不会使成员变得可序列化；它只是在 Inspector 中显示它。任何更改都不会被保存。",
                "Note that using [ShowInInspector] alone does not make a member serializable; it only displays it in the inspector. Any changes will not be saved."),
            new BilingualData("当没有 ShowInInspector 特性时，检查器中没有出现的任何字段或属性都不会被序列化。",
                "Any field or property that does not appear in the inspector without the ShowInInspector attribute is not serialized."),
            new BilingualData("可以使用 Odin 提供的 Serialization Debugger 工具来查询类中哪些成员是序列化的，哪些不是。",
                "You can use Odin's Serialization Debugger tool to check which members in a class are serialized and which are not."),
            new BilingualData("通常用于游戏运行时的观察数据，而且可以在游戏过程中实时修改（但修改不会持久化）。",
                "Commonly used for observing data at runtime, and can be modified during gameplay (but modifications are not persisted).")
        };

        public override ParameterValue[] AttributeParameters { get; set; } = null;

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = null;

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Basic Usage",
                ShowInInspectorExampleSO.Instance)
        };
    }
}
