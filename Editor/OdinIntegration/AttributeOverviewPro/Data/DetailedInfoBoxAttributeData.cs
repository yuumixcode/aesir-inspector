using System.Collections.Generic;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    internal class DetailedInfoBoxAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("DetailedInfoBox", "DetailedInfoBox",
                "DetailedInfoBox 特性用于在属性上方绘制一个可折叠的消息框，点击可展开显示详细信息。",
                "The DetailedInfoBox attribute is used to draw a collapsible message box above a property that can be expanded to show more details.",
                OdinInspectorDocumentationLinks.DetailedInfoBoxUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData("与 InfoBox 类似，但支持可折叠的详细内容区域。",
                "Similar to InfoBox, but supports a collapsible details section."),
            new BilingualData("详细内容区域可以手动展开或折叠，减少编辑器中的视觉杂乱。",
                "The details section can be manually expanded or collapsed, reducing visual clutter in the editor."),
            new BilingualData("Message、Details 和 VisibleIf 参数均支持字符串解析（$ 和 @ 表达式）。",
                "Message, Details, and VisibleIf parameters all support string resolution ($ and @ expressions).")
        };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(string).FullName, "message",
                new BilingualData("折叠状态下显示的文本。",
                    "The text displayed in the collapsed state.")),
            new ParameterValue(typeof(string).FullName, "details",
                new BilingualData("展开状态下显示的详细内容文本。",
                    "The detailed content text displayed when expanded.")),
            new ParameterValue("InfoMessageType", "infoMessageType",
                new BilingualData("消息框的类型（Info, Warning, Error, None）。",
                    "The type of the message box (Info, Warning, Error, None).")),
            new ParameterValue(typeof(string).FullName, "visibleIf",
                new BilingualData("可选成员名或表达式，用于控制消息框是否显示。",
                    "An optional member name or expression used to control whether the message box is displayed."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } =
        {
            new ResolvedStringParameterValue("Message", ResolverType.ValueResolver, typeof(string).FullName,
                "None", new List<ParameterValue>()),
            new ResolvedStringParameterValue("Details", ResolverType.ValueResolver, typeof(string).FullName,
                "None", new List<ParameterValue>()),
            new ResolvedStringParameterValue("Visible If", ResolverType.ValueResolver, typeof(bool).FullName,
                "True", new List<ParameterValue>())
        };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Basic Usage",
                DetailedInfoBoxExampleSO.Instance),
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Message Expression",
                DetailedInfoBoxExampleWithMessageSO.Instance),
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Details Expression",
                DetailedInfoBoxExampleWithDetailsSO.Instance),
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("VisibleIf Expression",
                DetailedInfoBoxExampleWithVisibleIfSO.Instance)
        };
    }
}
