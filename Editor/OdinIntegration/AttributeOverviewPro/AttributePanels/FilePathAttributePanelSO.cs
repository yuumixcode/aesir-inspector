namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// FilePath 特性介绍面板。
    /// </summary>
    [Summary("FilePath 特性介绍面板，展示 FilePath 特性的用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.Validation | AesirAttributeCategory.TypeSpecifics)]
    public class FilePathAttributePanelSO : AbstractAttributePanelSO
    {
        public override void Initialize()
        {
            SetData(new FilePathAttributeData());
        }
    }
}
