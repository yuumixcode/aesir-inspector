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

using System.IO;
using System.Linq;
using UnityEditor;

namespace RunLab.AesirInspector.Editor
{
    /// <summary>
    /// 右键快捷处理 Summary 特性。
    /// </summary>
    [Summary("右键快捷处理 Summary 特性")]
    public static class SummaryToolMenuItems
    {
        [MenuItem(AesirInspectorMenuItems.ProcessSummarySync, false, AesirInspectorMenuItems.ProcessSummarySyncOrder)]
        public static void QuickSyncSummary()
        {
            if (Selection.objects.Length == 1)
            {
                WriteSyncSummary(AssetDatabase.GetAssetPath(Selection.activeObject));
            }
            else
            {
                foreach (var obj in Selection.objects)
                {
                    WriteSyncSummary(AssetDatabase.GetAssetPath(obj));
                }
            }
        }

        [MenuItem(AesirInspectorMenuItems.ProcessSummaryReplace, false, AesirInspectorMenuItems.ProcessSummaryReplaceOrder)]
        public static void QuickReplaceSummary()
        {
            if (Selection.objects.Length == 1)
            {
                WriteReplaceSummary(AssetDatabase.GetAssetPath(Selection.activeObject));
            }
            else
            {
                foreach (var obj in Selection.objects)
                {
                    WriteReplaceSummary(AssetDatabase.GetAssetPath(obj));
                }
            }
        }

        [MenuItem(AesirInspectorMenuItems.ProcessSummaryRemove, false, AesirInspectorMenuItems.ProcessSummaryRemoveOrder)]
        public static void QuickRemoveSummary()
        {
            if (Selection.objects.Length == 1)
            {
                WriteRemoveSummary(AssetDatabase.GetAssetPath(Selection.activeObject));
            }
            else
            {
                foreach (var obj in Selection.objects)
                {
                    WriteRemoveSummary(AssetDatabase.GetAssetPath(obj));
                }
            }
        }

        [MenuItem(AesirInspectorMenuItems.ProcessSummarySync, true)]
        static bool CanSyncSummary() => IsScriptAsset();

        [MenuItem(AesirInspectorMenuItems.ProcessSummaryReplace, true)]
        static bool CanReplaceSummary() => IsScriptAsset();

        [MenuItem(AesirInspectorMenuItems.ProcessSummaryRemove, true)]
        static bool CanRemoveSummary() => IsScriptAsset();

        static bool IsScriptAsset()
        {
            var selectedObject = Selection.activeObject;
            return selectedObject && Selection.objects.All(obj => obj is MonoScript);
        }

        static void WriteSyncSummary(string filePath)
        {
            var sourceCode = File.ReadAllText(filePath);
            var processor = new XmlSummaryTool(sourceCode).ParseSourceScript();
            sourceCode = processor.GetProcessedSourceScript(XmlSummaryTool.ProcessMode.SyncSummary);
            File.WriteAllText(filePath, sourceCode);
            AssetDatabase.Refresh();
        }

        static void WriteReplaceSummary(string filePath)
        {
            var sourceCode = File.ReadAllText(filePath);
            var processor = new XmlSummaryTool(sourceCode).ParseSourceScript();
            sourceCode = processor.GetProcessedSourceScript(XmlSummaryTool.ProcessMode.ReplaceSummary);
            File.WriteAllText(filePath, sourceCode);
            AssetDatabase.Refresh();
        }

        static void WriteRemoveSummary(string filePath)
        {
            var sourceCode = File.ReadAllText(filePath);
            var processor = new XmlSummaryTool(sourceCode).ParseSourceScript();
            sourceCode = processor.GetProcessedSourceScript(XmlSummaryTool.ProcessMode.RemoveSummary);
            File.WriteAllText(filePath, sourceCode);
            AssetDatabase.Refresh();
        }
    }
}
