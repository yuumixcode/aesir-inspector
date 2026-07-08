namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AttributeCategory(AesirAttributeCategory.Conditionals | AesirAttributeCategory.TypeSpecifics)]
    internal class HideInInlineEditorsAttributePanelSO : AbstractAttributePanelSO
    {
        public override void Initialize()
        {
            SetData(new HideInInlineEditorsAttributeData());
        }
    }
}
