using System.Collections.Generic;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// TypeFilter 特性的介绍数据。
    /// </summary>
    [Summary("TypeFilter 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class TypeFilterAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("Type Filter", "类型过滤器",
                "TypeFilter 特性为属性提供了一个下拉列表，用于选择并实例化不同的类型。这在处理多态性时非常有用。",
                "The TypeFilter attribute provides a dropdown for a property to select and instantiate different types. This is very useful when dealing with polymorphism.",
                OdinInspectorDocumentationLinks.TypeFilterUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData("你可以指定一个方法或字段名，该成员应返回一个可选类型的列表。",
                "You can specify a method or field name that returns a list of selectable types."),
            new BilingualData("此特性通常与接口或抽象类一起使用，以便在运行时选择具体的实现类。",
                "This attribute is often used with interfaces or abstract classes to select concrete implementation classes at runtime."),
            new BilingualData("TypeFilter 需要 Odin 序列化才能工作，无法在 EditorOnly 状态下使用。",
                "TypeFilter requires Odin serialization to work and cannot be used in EditorOnly state.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } = new ParameterValue[3]
        {
            new ParameterValue(typeof(string).FullName, "FilterGetter",
                new BilingualData("返回可选类型列表的方法或字段名，支持所有解析器。",
                    "The name of the method or field that returns the list of selectable types. Supports all resolvers.")),
            new ParameterValue(typeof(string).FullName, "DropdownTitle",
                new BilingualData("类型选择下拉框的标题。",
                    "The title of the type selection dropdown.")),
            new ParameterValue(typeof(bool).FullName, "DrawValueNormally",
                new BilingualData("是否额外多绘制一个正常的类实例。默认为 false。",
                    "Whether to additionally draw a normal class instance. Defaults to false."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } =
        {
            new ResolvedStringParameterValue("FilterGetter", ResolverType.ValueResolver, "IEnumerable<Type>",
                "None", new List<ParameterValue>
                {
                    new ParameterValue("InspectorProperty", "$property",
                        new BilingualData("当前属性对应的 InspectorProperty 对象。",
                            "The InspectorProperty object for the current property.")),
                    new ParameterValue("T", "$value",
                        new BilingualData("当前属性的值。", "The current value of the property."))
                })
        };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Basic Usage",
                TypeFilterExampleSO.Instance)
        };
    }
}
