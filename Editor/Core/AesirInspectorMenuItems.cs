// ----------------------------------------------------------------------------
// MIT License
//
// Copyright (c) 2026 RunLab - Yuumix
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// ----------------------------------------------------------------------------

namespace RunLab.AesirInspector.Editor
{
    /// <summary>
    /// Aesir Inspector 所有 MenuItem 菜单路径和优先级的统一管理。
    /// </summary>
    public static class AesirInspectorMenuItems
    {
        #region Menu Roots

        /// <summary>
        /// Tools 菜单根路径。
        /// </summary>
        public const string ToolsMenuRoot = "Tools/Aesir Inspector";

        /// <summary>
        /// Assets 上下文菜单中 Script Doc Generator 的根路径。
        /// </summary>
        public const string AssetsScriptDocGeneratorRoot = "Assets/Aesir Inspector/Script Doc Generator";

        /// <summary>
        /// Assets 上下文菜单中 Process Summary 的根路径。
        /// </summary>
        public const string AssetsProcessSummaryRoot = "Assets/Aesir Inspector/Process Summary";

        #endregion

        #region Tools Menu Paths

        /// <summary>
        /// 打开 Getting Started 窗口的菜单路径。
        /// </summary>
        public const string GettingStarted = ToolsMenuRoot + "/Getting Started";

        /// <summary>
        /// 打开 Preferences 窗口的菜单路径。
        /// </summary>
        public const string Preferences = ToolsMenuRoot + "/Preferences";

        /// <summary>
        /// 打开 Attribute Overview Pro 窗口的菜单路径。
        /// </summary>
        public const string AttributeOverviewPro = ToolsMenuRoot + "/Attribute Overview Pro";

        /// <summary>
        /// 打开 Script Doc Generator 窗口的菜单路径。
        /// </summary>
        public const string ScriptDocGenerator = ToolsMenuRoot + "/Script Doc Generator";

        /// <summary>
        /// 打开 Mini Tools 窗口的菜单路径。
        /// </summary>
        public const string MiniTools = ToolsMenuRoot + "/Mini Tools";

        /// <summary>
        /// 打开 Extension Package Manager 窗口的菜单路径。
        /// </summary>
        public const string ExtensionPackageManager = ToolsMenuRoot + "/Extension Package Manager";

        /// <summary>
        /// 打开 Plugin Config Solutions 示例窗口的菜单路径。
        /// </summary>
        public const string SamplePluginConfigSolutions = ToolsMenuRoot + "/Samples/Plugin Config Solutions";

        /// <summary>
        /// 打开 RuntimeInitializeLoadType 示例窗口的菜单路径。
        /// </summary>
        public const string SampleRuntimeInitializeOnLoad = ToolsMenuRoot + "/Samples/RuntimeInitializeLoadType";

        /// <summary>
        /// Plugin Config Solutions 示例窗口标题。
        /// </summary>
        public const string SamplePluginConfigSolutionsWindowName = "Plugin Config Solutions";

        /// <summary>
        /// RuntimeInitializeLoadType 示例窗口标题。
        /// </summary>
        public const string SampleRuntimeInitializeOnLoadWindowName = "RuntimeInitializeLoadType";

        /// <summary>
        /// Getting Started 窗口标题。
        /// </summary>
        public const string GettingStartedWindowName = "Getting Started";

        /// <summary>
        /// Preferences 窗口标题。
        /// </summary>
        public const string PreferencesWindowName = "Preferences";

        #endregion

        #region Assets Context Menu Paths

        /// <summary>
        /// 将选中脚本添加到 Target Type 的菜单路径。
        /// </summary>
        public const string AddScriptToTargetType =
            AssetsScriptDocGeneratorRoot + "/Add To Target Type";

        /// <summary>
        /// 将选中脚本添加到 Target Type 并打开窗口的菜单路径。
        /// </summary>
        public const string AddScriptToTargetTypeAndOpenWindow =
            AssetsScriptDocGeneratorRoot + "/Add To Target Type And Open Window";

        /// <summary>
        /// 将选中脚本添加到 Temporary Types 的菜单路径。
        /// </summary>
        public const string AddScriptsToTemporaryTypes =
            AssetsScriptDocGeneratorRoot + "/Add To Temporary Types";

        /// <summary>
        /// 将选中脚本添加到 Temporary Types 并打开窗口的菜单路径。
        /// </summary>
        public const string AddScriptsToTemporaryTypesAndOpenWindow =
            AssetsScriptDocGeneratorRoot + "/Add To Temporary Types And Open Window";

        /// <summary>
        /// 同步 XML Summary 注释到 SummaryAttribute 的菜单路径。
        /// </summary>
        public const string ProcessSummarySync = AssetsProcessSummaryRoot + "/Sync";

        /// <summary>
        /// 用 SummaryAttribute 替换 XML Summary 注释的菜单路径。
        /// </summary>
        public const string ProcessSummaryReplace = AssetsProcessSummaryRoot + "/Replace";

        /// <summary>
        /// 移除所有 SummaryAttribute 的菜单路径。
        /// </summary>
        public const string ProcessSummaryRemove = AssetsProcessSummaryRoot + "/Remove";

        #endregion

        #region Tools Menu Orders

        // 基线策略：紧跟 Odin Inspector (末尾约 10005) 下方，间隔 11 产生分割线。
        // 子菜单内各项目间隔 5，对标 Odin 紧凑排列，组内无分割线。

        /// <summary>
        /// Getting Started 菜单项优先级。
        /// Odin Inspector 末尾项约 10005，+11 产生分割线前的首个项目。
        /// </summary>
        public const int GettingStartedOrder = 10000;

        /// <summary>
        /// Preferences 菜单项优先级。
        /// </summary>
        public const int PreferencesOrder = 10005;

        /// <summary>
        /// Attribute Overview Pro 菜单项优先级。
        /// Getting Started 和 Preferences 后，+11 产生分割线。
        /// </summary>
        public const int AttributeOverviewProOrder = 10016;

        /// <summary>
        /// Script Doc Generator 菜单项优先级。
        /// </summary>
        public const int ScriptDocGeneratorOrder = 10021;

        /// <summary>
        /// Mini Tools 菜单项优先级。
        /// </summary>
        public const int MiniToolsOrder = 10026;

        /// <summary>
        /// Extension Package Manager 菜单项优先级。
        /// </summary>
        public const int ExtensionPackageManagerOrder = 10031;

        /// <summary>
        /// Plugin Config Solutions 示例菜单项优先级。
        /// Extension PackageManager 为 10031，+11 产生分割线。
        /// </summary>
        public const int SamplePluginConfigSolutionsOrder = 10042;

        /// <summary>
        /// RuntimeInitializeLoadType 示例菜单项优先级。
        /// </summary>
        public const int SampleRuntimeInitializeOnLoadOrder = 10047;

        #endregion

        #region Assets Context Menu Orders

        // 基线策略：紧跟 Codely (110) 下方，间隔 11 产生分割线。
        // Script Doc Generator 各项紧密排列 (121-124)。

        /// <summary>
        /// Add To Target Type 菜单项优先级。
        /// Codely 为 110，+11 产生分割线。
        /// </summary>
        public const int AddScriptToTargetTypeOrder = 121;

        /// <summary>
        /// Add To Target Type And Open Window 菜单项优先级。
        /// </summary>
        public const int AddScriptToTargetTypeAndOpenWindowOrder = 122;

        /// <summary>
        /// Add To Temporary Types 菜单项优先级。
        /// </summary>
        public const int AddScriptsToTemporaryTypesOrder = 123;

        /// <summary>
        /// Add To Temporary Types And Open Window 菜单项优先级。
        /// </summary>
        public const int AddScriptsToTemporaryTypesAndOpenWindowOrder = 124;

        /// <summary>
        /// Process Summary Sync 菜单项优先级。
        /// Script Doc Generator 末尾 124，+11 产生分割线。
        /// </summary>
        public const int ProcessSummarySyncOrder = 135;

        /// <summary>
        /// Process Summary Replace 菜单项优先级。
        /// </summary>
        public const int ProcessSummaryReplaceOrder = 136;

        /// <summary>
        /// Process Summary Remove 菜单项优先级。
        /// </summary>
        public const int ProcessSummaryRemoveOrder = 137;

        #endregion
    }
}
