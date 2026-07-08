namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// ValueDropdown 特性介绍面板。
    /// </summary>
    [Summary("ValueDropdown 特性介绍面板，展示 ValueDropdown 特性的用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Conditionals | AesirAttributeCategory.Essentials)]
    public class ValueDropdownAttributePanelSO : AbstractAttributePanelSO
    {
        public override void Initialize()
        {
            SetData(new ValueDropdownAttributeData());
        }
    }
}
