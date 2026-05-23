namespace RunLab.AesirInspector
{
    [Summary("Aesir Inspector 编辑器资源路径常量。用于各模块定位 Preferences、Attribute Overview Pro、MiniTools 等资产。")]
    public static class AesirInspectorPaths
    {
        public const string EditorDefaultResourcesPath = "Assets/Editor Default Resources/Aesir Inspector";

        public const string PreferencesAssetsFolderPath = EditorDefaultResourcesPath + "/Preferences";

        public const string AttributeOverviewDatabasePath =
            EditorDefaultResourcesPath + "/Attribute Overview Pro";

        public const string AttributePanelsPath =
            EditorDefaultResourcesPath + "/Attribute Overview Pro/Panels";

        public const string AttributeExamplesPath =
            EditorDefaultResourcesPath + "/Attribute Overview Pro/Attribute Examples";

        public const string MiniToolsAssetsFolderPath = EditorDefaultResourcesPath + "/MiniTools";
    }
}
