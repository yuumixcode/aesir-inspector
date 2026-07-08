using System.Collections.Generic;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// CustomValueDrawer 特性的介绍数据，包含标题、参数说明、解析字符串参数和案例预览项。
    /// </summary>
    [Summary("CustomValueDrawer 特性的介绍数据，包含标题、参数说明、解析字符串参数和案例预览项")]
    internal class CustomValueDrawerAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("Custom Value Drawer", "Custom Value Drawer",
                "使用 CustomValueDrawer 特性，代替声明一个 Attribute，同时声明一个对应 Drawer 类的流程。CustomValueDrawer 支持撤销，重做，多选。",
                "Instead of making a new attribute, and a new drawer, for a one-time thing, you can with this attribute, make a method that acts as a custom property drawer. These drawers will out of the box have support for undo/redo and multi-selection.",
                OdinInspectorDocumentationLinks.CustomValueDrawerUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData("唯一的参数是绘制相关的方法名，且方法的返回值必须为自定义绘制字段的类型。",
                "The only parameter is the method name for drawing, and the method's return type must match the drawn field's type."),
            new BilingualData("注意使用 UNITY_EDITOR 宏定义包裹编辑器专用代码。",
                "Note: Use the UNITY_EDITOR macro to wrap editor-only code."),
            new BilingualData("可以选择是否接入 Odin 的绘制链，默认是不接入的。",
                "You can optionally chain into Odin's drawing pipeline; by default it does not chain."),
            new BilingualData("Odin 提供的 InspectorProperty 类型对象可以获得很多信息，类似于 SerializedProperty。",
                "The InspectorProperty object provided by Odin can access a lot of information, similar to SerializedProperty.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } = new ParameterValue[1]
        {
            new ParameterValue(typeof(string).FullName, "Action",
                new BilingualData("设置自定义绘制方法或表达式。",
                    "A resolved string that defines the custom drawer action to take, such as an expression or method invocation."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } =
        {
            new ResolvedStringParameterValue("Action", ResolverType.ValueResolver, "T", "None",
                new List<ParameterValue>
                {
                    new ParameterValue("T", "$value",
                        new BilingualData("代表应用此特性的成员当前值，类型为成员类型",
                            "Representing the member that has attribute applied to it.")),
                    new ParameterValue("GUIContent", "$label",
                        new BilingualData("代表成员的标签", "Representing the label of the member.")),
                    new ParameterValue("Func<GUIContent, bool>", "$callNextDrawer",
                        new BilingualData("是否进入 Odin 的下一层绘制链，传入 GUIContent 类型的 label 参数。默认不接入绘制链。",
                            "Whether to enter Odin's next drawing chain, passing a GUIContent label parameter. By default, it does not enter the drawing chain."))
                })
        };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Basic Parameters",
                CustomValueDrawerExampleSO.Instance)
        };
    }
}
