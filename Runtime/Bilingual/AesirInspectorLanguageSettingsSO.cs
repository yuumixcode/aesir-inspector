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
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;
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
            OdinInspectorSafeEditorUtility.GetNiceFullName(typeof(AesirInspectorLanguageSettingsSO));

        [SerializeField]
        [Summary("当前语言设置")]
        [HideInInspector]
        InspectorLanguage currentLanguage = InspectorLanguage.Chinese;

        [SerializeField]
        [PropertyOrder(-100)]
        HeaderBilingualWidget headerWidget;

        /// <summary>
        /// 获取语言设置实例。
        /// </summary>
        [Summary("获取语言设置实例")]
        public static AesirInspectorLanguageSettingsSO Instance =>
            ScriptableObjectSafeEditorUtility
                .GetOrCreateEditorScriptableObject<AesirInspectorLanguageSettingsSO>(ConfigName,
                    AesirInspectorPaths.PreferencesAssetsFolderPath, "AesirInspectorLanguageSettings");

        /// <summary>
        /// 当前是否为中文。
        /// </summary>
        [Summary("当前是否为中文")]
        public static bool IsChinese
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

        /// <summary>
        /// 当前是否为英文。
        /// </summary>
        [Summary("当前是否为英文")]
        public static bool IsEnglish
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

        void OnEnable()
        {
            headerWidget = new HeaderBilingualWidget("检查器语言设置", "Inspector Language Settings",
                "管理检查器的显示语言，支持中文和英文切换",
                "Manage the display language of the inspector, support Chinese and English switching",
                AesirInspectorWebLinks.GitUrl);
        }

        public static event Action OnLanguageChanged;

        /// <summary>
        /// 设置为中文。
        /// </summary>
        [Summary("设置为中文")]
        public static void SetChinese()
        {
            Instance.currentLanguage = InspectorLanguage.Chinese;
            OnLanguageChanged?.Invoke();
        }

        /// <summary>
        /// 设置为英文。
        /// </summary>
        [Summary("设置为英文")]
        public static void SetEnglish()
        {
            Instance.currentLanguage = InspectorLanguage.English;
            OnLanguageChanged?.Invoke();
        }

        #region Internal

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void InitializeEditor()
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

        #endregion
    }

#if UNITY_EDITOR

    internal sealed class
        AesirInspectorLanguageSettingsProcessor : OdinAttributeProcessor<AesirInspectorLanguageSettingsSO>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty,
            MemberInfo member,
            List<Attribute> attributes)
        {
            switch (member.Name)
            {
                case nameof(AesirInspectorLanguageSettingsSO.SetChinese):
                    attributes.Add(new ButtonAttribute("Switch Chinese", ButtonSizes.Large));
                    attributes.Add(new ShowIfAttribute("IsEnglish"));
                    attributes.Add(new ShowInInspectorAttribute());
                    break;
                case nameof(AesirInspectorLanguageSettingsSO.SetEnglish):
                    attributes.Add(new ButtonAttribute("设置为英文", ButtonSizes.Large));
                    attributes.Add(new ShowIfAttribute("IsChinese"));
                    attributes.Add(new ShowInInspectorAttribute());
                    break;
            }
        }
    }

#endif
}
