namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("特性参数数据类，包含参数的返回类型、名称及描述")]
    public class ParameterValue
    {
        BilingualData _parameterDescriptionData;

        public ParameterValue(string returnType, string parameterName, string parameterDescription)
        {
            ReturnType = returnType;
            ParameterName = parameterName;
            ParameterDescription = parameterDescription;
            _parameterDescriptionData = BilingualData.Empty;
        }

        public ParameterValue(string returnType, string parameterName, BilingualData parameterDescriptionData)
        {
            ReturnType = returnType;
            ParameterName = parameterName;
            _parameterDescriptionData = parameterDescriptionData;
            ParameterDescription = string.Empty;
        }

        public ParameterValue() { }

        [Summary("参数返回类型")]
        public string ReturnType { get; set; }

        [Summary("参数名称")]
        public string ParameterName { get; set; }

        [Summary("参数描述（字符串形式）")]
        public string ParameterDescription { get; set; }

        [Summary("获取当前语言的参数描述")]
        public string GetDescription() =>
            _parameterDescriptionData != BilingualData.Empty
                ? _parameterDescriptionData.GetCurrentOrFallback()
                : ParameterDescription;
    }
}
