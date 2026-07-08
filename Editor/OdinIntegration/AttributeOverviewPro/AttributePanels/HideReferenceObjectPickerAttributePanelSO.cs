namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// HideReferenceObjectPicker 特性介绍面板，展示 HideReferenceObjectPicker 用法及案例预览。
    /// </summary>
    [Summary("HideReferenceObjectPicker 特性介绍面板，展示 HideReferenceObjectPicker 用法及案例预览")]
    [AttributeCategory(AesirAttributeCategory.TypeSpecifics)]
    public class HideReferenceObjectPickerAttributePanelSO : AbstractAttributePanelSO
    {
        /// <summary>
        /// 初始化面板数据。
        /// </summary>
        [Summary("初始化面板数据")]
        public override void Initialize()
        {
            SetData(new HideReferenceObjectPickerAttributeData());
        }
    }
}
