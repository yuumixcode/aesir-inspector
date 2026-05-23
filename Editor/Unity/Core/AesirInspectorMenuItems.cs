namespace RunLab.AesirInspector.Editor
{
    [Summary("集中管理 MenuItem 路径与优先级数值，避免硬编码散落且确保菜单顺序可维护。所有 MenuItem 和 Odin MenuItem 特性应引用此处常量，而非内联字符串。")]
    public static class AesirInspectorMenuItems
    {
        public const string ToolsAesirRoot = "Tools/Aesir";
        public const string ToolsAesirInspectorRoot = "Tools/Aesir/Inspector";
        public const string AssetsScriptDocGeneratorRoot = "Assets/Aesir Inspector/Script Doc Generator";
        public const string AssetsProcessSummaryRoot = "Assets/Aesir Inspector/Process Summary";

        public const string GettingStarted = ToolsAesirRoot + "/Getting Started";
        public const int GettingStartedOrder = -980;
        public const string GettingStartedWindowName = "Getting Started";

        public const string Preferences = ToolsAesirInspectorRoot + "/Preferences";
        public const int PreferencesOrder = -880;
        public const string PreferencesWindowName = "Preferences";

        public const string AttributeOverviewPro = ToolsAesirInspectorRoot + "/Attribute Overview Pro";
        public const int AttributeOverviewProOrder = -900;

        public const string ScriptDocGenerator = ToolsAesirInspectorRoot + "/Script Doc Generator";
        public const int ScriptDocGeneratorOrder = -895;

        public const string MiniTools = ToolsAesirInspectorRoot + "/Mini Tools";
        public const int MiniToolsOrder = -885;

        public const string ExtensionPackageManager = ToolsAesirInspectorRoot + "/Extension Package Manager";
        public const int ExtensionPackageManagerOrder = -890;

        public const string SamplePluginConfigSolutions =
            ToolsAesirInspectorRoot + "/Samples/Plugin Config Solutions";
        public const int SamplePluginConfigSolutionsOrder = -800;
        public const string SamplePluginConfigSolutionsWindowName = "Plugin Config Solutions";

        public const string SampleRuntimeInitializeOnLoad =
            ToolsAesirInspectorRoot + "/Samples/RuntimeInitializeLoadType";
        public const int SampleRuntimeInitializeOnLoadOrder = -795;
        public const string SampleRuntimeInitializeOnLoadWindowName = "RuntimeInitializeLoadType";

        public const string AddScriptToTargetType = AssetsScriptDocGeneratorRoot + "/Add To Target Type";
        public const int AddScriptToTargetTypeOrder = -50;

        public const string AddScriptToTargetTypeAndOpenWindow =
            AssetsScriptDocGeneratorRoot + "/Add To Target Type And Open Window";
        public const int AddScriptToTargetTypeAndOpenWindowOrder = -48;

        public const string AddScriptsToTemporaryTypes =
            AssetsScriptDocGeneratorRoot + "/Add To Temporary Types";
        public const int AddScriptsToTemporaryTypesOrder = -43;

        public const string AddScriptsToTemporaryTypesAndOpenWindow =
            AssetsScriptDocGeneratorRoot + "/Add To Temporary Types And Open Window";
        public const int AddScriptsToTemporaryTypesAndOpenWindowOrder = -40;

        public const string ProcessSummarySync = AssetsProcessSummaryRoot + "/Sync";
        // Script Doc Generator 末尾 124，+11 产生分割线
        public const int ProcessSummarySyncOrder = -28;

        public const string ProcessSummaryReplace = AssetsProcessSummaryRoot + "/Replace";
        public const int ProcessSummaryReplaceOrder = -25;

        public const string ProcessSummaryRemove = AssetsProcessSummaryRoot + "/Remove";
        public const int ProcessSummaryRemoveOrder = -23;
    }
}
