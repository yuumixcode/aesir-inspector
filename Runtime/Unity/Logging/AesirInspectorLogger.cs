using System.Diagnostics;
using System.Runtime.CompilerServices;
using Debug = UnityEngine.Debug;

namespace RunLab.AesirInspector
{
    [Summary("Aesir Inspector 日志工具")]
    public static class AesirInspectorLogger
    {
        [Summary("输出信息日志")]
        [Conditional("UNITY_EDITOR")]
        public static void Info(string message)
        {
            if (!AesirInspectorLoggerSettings.IsInfoEnabled)
            {
                return;
            }

            Debug.Log($"<color=#00FF00>[Aesir Inspector]</color> {message}");
        }

        [Summary("输出警告日志")]
        [Conditional("UNITY_EDITOR")]
        public static void Warning(string message)
        {
            if (!AesirInspectorLoggerSettings.IsWarningEnabled)
            {
                return;
            }

            Debug.LogWarning($"<color=#FFFF00>[Aesir Inspector]</color> {message}");
        }

        [Summary("输出错误日志")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Conditional("UNITY_EDITOR")]
        public static void Error(string message)
        {
            Debug.LogError($"<color=#FF0000>[Aesir Inspector]</color> {message}");
        }

        [Summary("输出自定义前缀的信息日志")]
        [Conditional("UNITY_EDITOR")]
        public static void Info(string prefix, string message)
        {
            if (!AesirInspectorLoggerSettings.IsInfoEnabled)
            {
                return;
            }

            Debug.Log($"<color=#00FF00>[{prefix}]</color> {message}");
        }

        [Summary("输出自定义前缀的警告日志")]
        [Conditional("UNITY_EDITOR")]
        public static void Warning(string prefix, string message)
        {
            if (!AesirInspectorLoggerSettings.IsWarningEnabled)
            {
                return;
            }

            Debug.LogWarning($"<color=#FFFF00>[{prefix}]</color> {message}");
        }

        [Summary("输出自定义前缀的错误日志")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Conditional("UNITY_EDITOR")]
        public static void Error(string prefix, string message)
        {
            Debug.LogError($"<color=#FF0000>[{prefix}]</color> {message}");
        }
    }
}
