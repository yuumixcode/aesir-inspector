using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// Searchable 特性的介绍数据。
    /// </summary>
    [Summary("Searchable 特性的介绍数据，包含标题、参数说明和案例预览项")]
    internal class SearchableAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("Searchable", "Searchable", "Searchable 特性为列表、数组或类添加一个搜索框，方便快速筛选内容。",
                "The Searchable attribute adds a search field to a list, array, or class, allowing for quick content filtering.",
                OdinInspectorDocumentationLinks.SearchableUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData("默认开启模糊搜索和递归搜索，可以匹配子对象的字段内容。",
                "Fuzzy search and recursive search are enabled by default, matching content in child object fields."),
            new BilingualData("可以作用于集合上，也可以直接作用于类声明上，使其所有实例都可搜索。",
                "Can be applied to collections or directly to class declarations to make all instances searchable."),
            new BilingualData("通过 FilterOptions 可以精细控制搜索匹配的范围（如仅匹配属性名、匹配值类型等）。",
                "FilterOptions allows fine-grained control over search matching (e.g., property name only, value type only)."),
            new BilingualData("自定义类推荐实现 ISearchFilterable 接口，可以实现满足特殊条件的精准搜索。",
                "Custom classes are recommended to implement the ISearchFilterable interface for precise search with special conditions.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(bool).FullName, "FuzzySearch",
                new BilingualData("是否启用模糊搜索。默认值为 true。",
                    "Whether to enable fuzzy searching. Default is true.")),
            new ParameterValue(typeof(bool).FullName, "Recursive",
                new BilingualData("是否递归搜索子属性。默认值为 true。",
                    "Whether to search child properties recursively. Default is true.")),
            new ParameterValue(typeof(SearchFilterOptions).FullName, "FilterOptions",
                new BilingualData("搜索过滤选项，控制搜索方式，默认为 SearchFilterOptions.All。",
                    "Search filter options that control how searching works. Defaults to SearchFilterOptions.All.")),
            new ParameterValue(">>> SearchFilterOptions", "SearchFilterOptions.PropertyName",
                new BilingualData("可以匹配成员名称。",
                    "Matches member names.")),
            new ParameterValue(">>> SearchFilterOptions", "SearchFilterOptions.PropertyNiceName",
                new BilingualData("可以匹配成员的 NiceName，即大写字母开头、单词分开的显示名称。",
                    "Matches the NiceName of members, which is the display name with capitalized words separated.")),
            new ParameterValue(">>> SearchFilterOptions", "SearchFilterOptions.TypeOfValue",
                new BilingualData("可以匹配值的类型。",
                    "Matches the type of the value.")),
            new ParameterValue(">>> SearchFilterOptions", "SearchFilterOptions.ValueToString",
                new BilingualData("可以匹配任意值转换为字符串的结果。",
                    "Matches the result of converting any value to a string.")),
            new ParameterValue(">>> SearchFilterOptions", "SearchFilterOptions.ISearchFilterableInterface",
                new BilingualData("自定义实现搜索过滤规则，在需要被搜索的元素（自定义类）上实现 ISearchFilterable 接口。",
                    "Custom search filter rules by implementing the ISearchFilterable interface on the elements (custom classes) to be searched.")),
            new ParameterValue(">>> SearchFilterOptions", "SearchFilterOptions.All",
                new BilingualData("以上所有选项的合集，任何方式均可匹配。",
                    "A combination of all the above options; any method can match."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = { };

        public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } =
        {
            new AttributeExamplePreviewItem().InitializeUnitySerializedExample("Basic Usage",
                SearchableExampleSO.Instance)
        };
    }
}
