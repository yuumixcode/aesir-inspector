using System.Collections.Generic;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("CustomContextMenu 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class CustomContextMenuAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("CustomContextMenu", "CustomContextMenu",
                "CustomContextMenu 特性为字段添加自定义右键菜单项，可以指定菜单路径和回调方法名。",
                "The CustomContextMenu attribute adds custom context menu items to a field, specifying a menu path and callback method name.",
                OdinInspectorDocumentationLinks.CustomContextMenuUrl);

        public override BilingualData[] UsageTips { get; set; } = { };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(string).FullName, "MenuItemPath",
                new BilingualData("右键菜单项的路径，使用 / 分隔层级。",
                    "The menu item path, using / to separate hierarchy levels.")),
            new ParameterValue(typeof(string).FullName, "MethodName",
                new BilingualData("回调方法的名称。",
                    "The name of the callback method."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } =
        {
            new ResolvedStringParameterValue("MethodName", ResolverType.ActionResolver,
                typeof(void).FullName, "None", new List<ParameterValue>())
        };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("No Parameters",
                CustomContextMenuExampleSO.Instance),
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Resolved Parameters",
                CustomContextMenuExampleWithActionSO.Instance)
        };
    }
}
