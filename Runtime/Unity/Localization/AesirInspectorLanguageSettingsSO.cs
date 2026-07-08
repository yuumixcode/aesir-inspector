using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RunLab.AesirInspector
{
    [Summary("检查器语言枚举")]
    public enum InspectorLanguage
    {
        [Summary("中文")]
        Chinese = 0,

        [Summary("英文")]
        English = 1
    }

    [Summary("Aesir Inspector 检查器语言管理")]
    public class AesirInspectorLanguageSettingsSO : AesirInspectorSettings<AesirInspectorLanguageSettingsSO>
    {
        [SerializeField]
        [HideInInspector]
        InspectorLanguage currentLanguage = InspectorLanguage.Chinese;

        [Summary("当前是否为中文")]
        public static bool CurrentIsChinese => Instance.currentLanguage == InspectorLanguage.Chinese;

        [Summary("当前是否为英文")]
        public static bool CurrentIsEnglish => Instance.currentLanguage == InspectorLanguage.English;

        [Summary("语言变更事件")]
        public static event Action LanguageChanged;

        [Summary("设置为中文")]
        public static void SetChinese()
        {
            Instance.currentLanguage = InspectorLanguage.Chinese;
            LanguageChanged?.Invoke();
        }

        [Summary("设置为英文")]
        public static void SetEnglish()
        {
            Instance.currentLanguage = InspectorLanguage.English;
            LanguageChanged?.Invoke();
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void RegisterPlayModeChangeHandler()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.EnteredPlayMode)
            {
                LanguageChanged = null;
            }
        }
#endif
    }
}
