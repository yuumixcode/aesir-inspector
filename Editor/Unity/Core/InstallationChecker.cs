using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace RunLab.AesirInspector.Editor
{
    [Summary("安装方式分类。用于条件判断文件是否在 Packages/ 目录，例如决定文档路径解析策略。")]
    public enum InstallMode
    {
        Unknown = 0,
        Upm = 1,
        AssetFolder = 2
    }

    [Summary("编辑器加载时缓存安装方式与 UPM 来源。供 SummaryTool、ScriptDocGenerator 等模块查询包来源以适配路径行为。")]
    [InitializeOnLoad]
    public static class InstallationChecker
    {
        static InstallationChecker() => Detect();

        public static InstallMode InstallMode { get; private set; } = InstallMode.Unknown;

        [Summary("UPM 安装时的具体来源（Registry / Git / Local 等）；非 UPM 时为 PackageSource.Unknown。")]
        public static PackageSource UpmPackageSource { get; private set; } = PackageSource.Unknown;

        public static bool IsUpm => InstallMode == InstallMode.Upm;

        public static bool IsAssetFolder => InstallMode == InstallMode.AssetFolder;

        static void Detect()
        {
            try
            {
                PerformDetection();
            }
            catch (Exception ex)
            {
                InstallMode = InstallMode.Unknown;
                UpmPackageSource = PackageSource.Unknown;
                Debug.LogWarning($"[Aesir Inspector] 安装方式检测失败：{ex.Message}");
            }
        }

        static void PerformDetection()
        {
            var info = PackageInfo.FindForAssembly(Assembly.GetExecutingAssembly());
            if (info == null)
            {
                InstallMode = InstallMode.AssetFolder;
                return;
            }

            InstallMode = InstallMode.Upm;
            UpmPackageSource = info.source;
        }
    }
}
