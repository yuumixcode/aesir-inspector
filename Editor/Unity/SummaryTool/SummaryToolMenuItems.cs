using System.IO;
using System.Linq;
using UnityEditor;

namespace RunLab.AesirInspector.Editor
{
    [Summary("右键快捷处理 Summary 特性")]
    public static class SummaryToolMenuItems
    {
        [MenuItem(AesirInspectorMenuItems.ProcessSummarySync, false,
            AesirInspectorMenuItems.ProcessSummarySyncOrder)]
        public static void QuickSyncSummary() =>
            ProcessSelectedScripts(XmlSummaryTool.ProcessMode.SyncSummary);

        [MenuItem(AesirInspectorMenuItems.ProcessSummaryReplace, false,
            AesirInspectorMenuItems.ProcessSummaryReplaceOrder)]
        public static void QuickReplaceSummary() =>
            ProcessSelectedScripts(XmlSummaryTool.ProcessMode.ReplaceSummary);

        [MenuItem(AesirInspectorMenuItems.ProcessSummaryRemove, false,
            AesirInspectorMenuItems.ProcessSummaryRemoveOrder)]
        public static void QuickRemoveSummary() =>
            ProcessSelectedScripts(XmlSummaryTool.ProcessMode.RemoveSummary);

        [MenuItem(AesirInspectorMenuItems.ProcessSummarySync, true)]
        static bool CanSyncSummary() => IsScriptAsset();

        [MenuItem(AesirInspectorMenuItems.ProcessSummaryReplace, true)]
        static bool CanReplaceSummary() => IsScriptAsset();

        [MenuItem(AesirInspectorMenuItems.ProcessSummaryRemove, true)]
        static bool CanRemoveSummary() => IsScriptAsset();

        static bool IsScriptAsset() =>
            Selection.activeObject && Selection.objects.All(obj => obj is MonoScript);

        static void ProcessSelectedScripts(XmlSummaryTool.ProcessMode processMode)
        {
            foreach (var obj in Selection.objects)
            {
                WriteProcessedSummary(AssetDatabase.GetAssetPath(obj), processMode);
            }
        }

        static void WriteProcessedSummary(string filePath, XmlSummaryTool.ProcessMode processMode)
        {
            var sourceCode = File.ReadAllText(filePath);
            var processor = new XmlSummaryTool(sourceCode).ParseSourceScript();
            sourceCode = processor.GetProcessedSourceScript(processMode);
            File.WriteAllText(filePath, sourceCode);
            AssetDatabase.Refresh();
        }
    }
}
