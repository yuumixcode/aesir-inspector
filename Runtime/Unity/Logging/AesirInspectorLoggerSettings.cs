using UnityEngine;

namespace RunLab.AesirInspector
{
    [Summary("Aesir Inspector 日志配置")]
    public class AesirInspectorLoggerSettings : AesirInspectorSettings<AesirInspectorLoggerSettings>
    {
        [SerializeField]
        bool enableInfoLog;

        [SerializeField]
        bool enableWarningLog = true;

        [Summary("普通日志是否启用")]
        public static bool IsInfoEnabled => Instance != null && Instance.enableInfoLog;

        [Summary("警告日志是否启用")]
        public static bool IsWarningEnabled => Instance == null || Instance.enableWarningLog;
    }
}
