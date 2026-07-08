using System.Collections.Generic;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// SuffixLabel 特性的介绍数据。
    /// </summary>
    [Summary("SuffixLabel 特性的介绍数据，包含标题、参数说明、解析字符串参数和案例预览项")]
    internal class SuffixLabelAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("SuffixLabel", "SuffixLabel", "SuffixLabel 特性用于在属性输入框的末尾添加一个后缀标签。",
                "The SuffixLabel attribute adds a label to the end of a property's input field.",
                OdinInspectorDocumentationLinks.SuffixLabelUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData("常用于标识单位（如 '米'、'秒'）或提供额外的上下文信息。",
                "Commonly used to indicate units (e.g., 'm', 's') or provide extra context."),
            new BilingualData("支持静态字符串、引用成员（$）以及 Odin 表达式（@）。",
                "Supports static strings, member references ($), and Odin expressions (@)."),
            new BilingualData("设置 overlay 为 true 可以将标签绘制在输入框内部（右对齐）。",
                "Setting overlay to true draws the label inside the input field (right-aligned).")
        };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(string).FullName, "label",
                new BilingualData("后缀标签显示的文本或表达式，支持所有解析器。",
                    "The suffix label text or expression. Supports all resolvers.")),
            new ParameterValue(typeof(bool).FullName, "overlay",
                new BilingualData("是否将标签覆盖在属性输入框上（内部显示），默认为 false。",
                    "Whether the label should be overlaid on top of the property's input field. Defaults to false.")),
            new ParameterValue("SdfIconType", "icon",
                new BilingualData("后缀图标类型，默认为 SdfIconType.None。",
                    "The suffix icon type. Defaults to SdfIconType.None.")),
            new ParameterValue(typeof(string).FullName, "IconColor",
                new BilingualData("图标的颜色，支持命名颜色、十六进制和 RGBA 表达式。",
                    "The color of the icon. Supports named colors, hex, and RGBA expressions."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } =
        {
            new ResolvedStringParameterValue("Label", ResolverType.ValueResolver, typeof(string).FullName,
                "None", new List<ParameterValue>())
        };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Basic Usage",
                SuffixLabelExampleSO.Instance)
        };
    }
}
