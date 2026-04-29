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

using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 检查器语言枚举。
    /// </summary>
    [Summary("检查器语言枚举")]
    public enum InspectorLanguage
    {
        [Summary("中文")]
        Chinese = 0,

        [Summary("英文")]
        English = 1
    }

    /// <summary>
    /// Aesir Inspector 检查器语言设置。
    /// </summary>
    [Summary("Aesir Inspector 检查器语言管理")]
    public class AesirInspectorLanguageSettingsSO : ScriptableObject
    {
        static readonly string ConfigName =
            OdinBridgeLocator.Bridge.GetFriendlyFullName(typeof(AesirInspectorLanguageSettingsSO));

        [SerializeField]
        InspectorLanguage currentLanguage = InspectorLanguage.Chinese;

        public static AesirInspectorLanguageSettingsSO Instance =>
            ScriptableObjectSafeEditorUtility
                .GetOrCreateEditorScriptableObject<AesirInspectorLanguageSettingsSO>(ConfigName,
                    AesirInspectorPaths.PreferencesAssetsFolderPath, "AesirInspectorLanguageSettings");

        public static bool CurrentIsChinese
        {
            get
            {
                if (Instance == null)
                {
                    return true;
                }

                return Instance.currentLanguage == InspectorLanguage.Chinese;
            }
        }

        public static bool CurrentIsEnglish
        {
            get
            {
                if (Instance == null)
                {
                    return false;
                }

                return Instance.currentLanguage == InspectorLanguage.English;
            }
        }

        public static event Action OnLanguageChanged;

        public static void SetChinese()
        {
            Instance.currentLanguage = InspectorLanguage.Chinese;
            OnLanguageChanged?.Invoke();
        }

        public static void SetEnglish()
        {
            Instance.currentLanguage = InspectorLanguage.English;
            OnLanguageChanged?.Invoke();
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
                OnLanguageChanged = null;
            }
        }
#endif
    }
}
