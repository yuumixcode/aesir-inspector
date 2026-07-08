namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("ResponsiveButtonGroup 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class ResponsiveButtonGroupAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("ResponsiveButtonGroup", "ResponsiveButtonGroup",
                "ResponsiveButtonGroup 特性将方法按钮分组，并根据可用布局空间自动调整按钮的位置和大小。",
                "ResponsiveButtonGroup groups buttons into a group that will position and resize the buttons based on the amount of available layout space.",
                OdinInspectorDocumentationLinks.ResponsiveButtonGroupUrl);

        public override BilingualData[] UsageTips { get; set; } = null;

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(string).FullName, "GroupName",
                new BilingualData("按钮组的名称。",
                    "The name of the button group.")),
            new ParameterValue(typeof(bool).FullName, "UniformLayout",
                new BilingualData("是否统一按钮布局，默认为 false。",
                    "Whether to use uniform layout for buttons. Defaults to false.")),
            new ParameterValue(typeof(Sirenix.OdinInspector.ButtonSizes).FullName, "DefaultButtonSize",
                new BilingualData("默认按钮大小，默认为 Medium。",
                    "The default button size. Defaults to Medium."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = { };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Basic Usage",
                ResponsiveButtonGroupExampleSO.Instance)
        };
    }
}