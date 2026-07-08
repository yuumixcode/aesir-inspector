using System.Collections;
using System.Collections.Generic;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// ValueDropdown 特性的介绍数据。
    /// </summary>
    [Summary("ValueDropdown 特性的介绍数据，包含标题、参数说明、解析字符串参数和案例预览项")]
    internal class ValueDropdownAttributeData : AbstractAttributeData
    {
        public override BilingualHeaderControl BilingualHeaderControl { get; set; } =
            new BilingualHeaderControl("ValueDropdown", "ValueDropdown",
                "ValueDropdown 特性用于在属性上提供一个自定义的下拉选择列表。",
                "The ValueDropdown attribute provides a custom dropdown list for selecting values for a property.",
                OdinInspectorDocumentationLinks.ValueDropdownUrl);

        public override BilingualData[] UsageTips { get; set; } =
        {
            new BilingualData("可以引用字段、属性、方法或使用 Odin 表达式来获取可选值列表。",
                "Can reference a field, property, method, or use an Odin expression to get the list of values."),
            new BilingualData("支持树状结构显示，通过在值前面增加路径字符串实现（如 'Group/Item'）。",
                "Supports tree-view structures by adding path strings to labels (e.g., 'Group/Item')."),
            new BilingualData("可以使用 ValueDropdownList<T> 来方便地定义带有标签和实际值的选项列表。",
                "Use ValueDropdownList<T> to easily define options with custom labels and their corresponding values."),
            new BilingualData("支持多选（作用于列表/集合上），并可以配置是否排除已选值。",
                "Supports multi-selection when applied to collections and can be configured to exclude already selected values.")
        };

        public override ParameterValue[] AttributeParameters { get; set; } =
        {
            new ParameterValue(typeof(string).FullName, "values",
                new BilingualData("获取可选值列表的成员名或表达式，支持所有解析器。",
                    "The member name or expression to get the list of values. Supports all resolvers.")),
            new ParameterValue(typeof(int).FullName, "NumberOfItemsBeforeEnablingSearch",
                new BilingualData("当可选列表的元素数量大于等于该值时，开启搜索框。",
                    "When the number of items in the dropdown is greater than or equal to this value, the search field is enabled.")),
            new ParameterValue(typeof(bool).FullName, "IsUniqueList",
                new BilingualData("当作用于列表时，是否保证列表项唯一。",
                    "Whether to ensure items in the list are unique.")),
            new ParameterValue(typeof(bool).FullName, "DrawDropdownForListElements",
                new BilingualData("当作用于列表时，是否让元素修改时以下拉选择框的样式修改，默认为 true。",
                    "When applied to a list, whether to draw a dropdown for each element when modifying. Defaults to true.")),
            new ParameterValue(typeof(bool).FullName, "DisableListAddButtonBehaviour",
                new BilingualData("是否禁用列表添加时触发下拉选择框，默认为 false。",
                    "Whether to disable the dropdown selection when adding items to the list. Defaults to false.")),
            new ParameterValue(typeof(bool).FullName, "ExcludeExistingValuesInList",
                new BilingualData("是否剔除当前已存在的值（包括当前字段的值），默认为 false。",
                    "Whether to exclude existing values already in the list, including the current field value. Defaults to false.")),
            new ParameterValue(typeof(bool).FullName, "ExpandAllMenuItems",
                new BilingualData("如果显示为树状图，是否默认展开所有项，默认为 false。",
                    "Whether to expand all menu items by default in tree-view mode. Defaults to false.")),
            new ParameterValue(typeof(bool).FullName, "AppendNextDrawer",
                new BilingualData("是否在原有的绘制方式后附加下拉按钮，而不是直接替换，默认为 false。",
                    "Whether to append a dropdown button next to the original drawer instead of replacing it. Defaults to false.")),
            new ParameterValue(typeof(bool).FullName, "DisableGUIInAppendedDrawer",
                new BilingualData("配合 AppendNextDrawer 使用，是否禁用原有绘制的交互，避免两种方式都可以修改值，默认为 false。",
                    "If true with AppendNextDrawer, the original drawer will be disabled to prevent dual modification. Defaults to false.")),
            new ParameterValue(typeof(bool).FullName, "DisableListRemoveButtonBehaviour",
                new BilingualData("是否禁用列表删除时触发下拉选择框，默认为 false。",
                    "Whether to disable the dropdown selection when removing items from the list. Defaults to false.")),
            new ParameterValue(typeof(bool).FullName, "DoubleClickToConfirm",
                new BilingualData("是否需要双击鼠标才能确认选择，默认为 false。",
                    "Whether double-click is required to confirm a selection. Defaults to false.")),
            new ParameterValue(typeof(bool).FullName, "FlattenTreeView",
                new BilingualData("如果返回列表支持树状显示，是否放弃树状显示取消缩进，默认为 false。",
                    "If the returned list supports tree-view, whether to flatten the tree and remove indentation. Defaults to false.")),
            new ParameterValue(typeof(int).FullName, "DropdownWidth",
                new BilingualData("整个下拉选择框的宽度。", "The width of the entire dropdown.")),
            new ParameterValue(typeof(int).FullName, "DropdownHeight",
                new BilingualData("整个下拉选择框的高度，而不是单个选项的高度。",
                    "The height of the entire dropdown, not the height of a single item.")),
            new ParameterValue(typeof(string).FullName, "DropdownTitle",
                new BilingualData("下拉选择框的标题。", "The title of the dropdown.")),
            new ParameterValue(typeof(bool).FullName, "SortDropdownItems",
                new BilingualData("是否对可选列表中的元素排序，默认为 false。",
                    "Whether to sort the items in the dropdown. Defaults to false.")),
            new ParameterValue(typeof(bool).FullName, "HideChildProperties",
                new BilingualData("是否隐藏子属性（如 Vector3 的分量），默认为 false。",
                    "Whether to hide child properties (such as Vector3 components). Defaults to false.")),
            new ParameterValue(typeof(bool).FullName, "CopyValues",
                new BilingualData("下拉框选择的值应该是原始值的副本还是引用（对于引用类型），默认为 true。",
                    "Whether the selected value should be a copy of the original value or a reference (for reference types). Defaults to true.")),
            new ParameterValue(typeof(bool).FullName, "OnlyChangeValueOnConfirm",
                new BilingualData("如果为 true，只有在完全确认下拉框中的选择时，实际属性值才会更改，默认为 false。",
                    "If true, the actual property value will only change once when the dropdown selection is fully confirmed. Defaults to false."))
        };

        public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } =
        {
            new ResolvedStringParameterValue("Values", ResolverType.ValueResolver,
                typeof(IEnumerable).FullName, "None", new List<ParameterValue>
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
                ValueDropdownExampleSO.Instance)
        };
    }
}
