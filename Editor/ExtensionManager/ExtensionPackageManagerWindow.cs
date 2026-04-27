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
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace RunLab.AesirInspector.Editor.ExtensionManager
{
    [Summary("扩展包管理器窗口")]
    public class ExtensionPackageManagerWindow : OdinEditorWindow
    {
        static readonly BilingualData WindowName = new BilingualData("扩展包管理器", "Extension Package Manager");

        [PropertyOrder(-1000)]
        public HeaderBilingualWidget headerWidget;

        [PropertyOrder(-800)]
        [ListDrawerSettings(ShowFoldout = false, HideAddButton = true, HideRemoveButton = true,
            OnTitleBarGUI = nameof(DrawRefreshCardListButton))]
        [BilingualText("卡片列表", "Card List")]
        public List<ExtensionPackageCard> cardList;

        Texture2D _refreshIcon;

        [Summary("打开扩展包管理器窗口")]
        [MenuItem(AesirInspectorMenuItems.ExtensionPackageManager, false,
            AesirInspectorMenuItems.ExtensionPackageManagerOrder)]
        public static void Open()
        {
            var window = GetWindow<ExtensionPackageManagerWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenterXY(800f, 600f);
            window.minSize = new Vector2(720f, 520f);
            window.Show();
            _ = window.UpdatePackageInstallationStates();
            AesirInspectorLogger.Info("打开 Extension Package Manager 窗口，检测预设包的安装状态。");
        }

        #region Event Functions

        protected override void OnEnable()
        {
            base.OnEnable();
            headerWidget = new HeaderBilingualWidget("扩展包管理器", "Extension Package Manager",
                "快捷安装推荐的 Aesir 系列和其他常用开源 Unity Packages，基于 Git URL 方式",
                "Install recommended Aesir series and open-source Unity Packages quickly, Based On Git URL.");
            WindowPadding = new Vector4(10f, 10f, 10f, 10f);
            if (_refreshIcon == null)
            {
                _refreshIcon =
                    SdfIcons.CreateTransparentIconTexture(SdfIconType.ArrowClockwise, Color.white, 32, 32, 0);
            }

            UpdateTitle();
            AesirInspectorLanguageSettingsSO.OnLanguageChanged -= OnLanguageChanged;
            AesirInspectorLanguageSettingsSO.OnLanguageChanged += OnLanguageChanged;
            PackageManagerEditorUtility.OnPackagesChanged -= OnPackagesChanged;
            PackageManagerEditorUtility.OnPackagesChanged += OnPackagesChanged;
            Events.registeredPackages -= OnRegisteredPackagesEditor;
            Events.registeredPackages += OnRegisteredPackagesEditor;

            cardList = new List<ExtensionPackageCard>
            {
                ExtensionPackageCard.AesirToolkitCore,
                ExtensionPackageCard.AesirToolkitModules,
                ExtensionPackageCard.GitAmendImprovedTimers
            };
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            AesirInspectorLanguageSettingsSO.OnLanguageChanged -= OnLanguageChanged;
            PackageManagerEditorUtility.OnPackagesChanged -= OnPackagesChanged;
            Events.registeredPackages -= OnRegisteredPackagesEditor;
        }

        #endregion

        #region Internal

        void DrawRefreshCardListButton()
        {
            var text = new BilingualData("检测安装状态", "Check Packages Installed");
            var content = new GUIContent(text, _refreshIcon);
            if (SirenixEditorGUI.ToolbarButton(content))
            {
                _ = UpdatePackageInstallationStates();
            }
        }

        [Summary("刷新卡片安装状态（异步）")]
        async Task UpdatePackageInstallationStates()
        {
            await PackageManagerEditorUtility.ListPackagesAsyncOffline();
            if (cardList == null)
            {
                return;
            }

            foreach (var card in cardList)
            {
                var isInstalled = PackageManagerEditorUtility.IsPackageInstalled(card.PackageName);
                card.State = isInstalled
                    ? ExtensionPackageCard.PackageState.Installed
                    : ExtensionPackageCard.PackageState.NotInstalled;
            }
        }

        void OnPackagesChanged()
        {
            _ = UpdatePackageInstallationStates();
            Repaint();
        }

        [Summary("当包注册变更完成后回调，仅在本窗口打开时生效")]
        void OnRegisteredPackagesEditor(PackageRegistrationEventArgs args)
        {
            _ = HandleRegisteredPackagesEditorAsync(args);
        }

        [Summary("处理包注册变更后的异步刷新并捕获异常")]
        async Task HandleRegisteredPackagesEditorAsync(PackageRegistrationEventArgs args)
        {
            try
            {
                while (EditorApplication.isCompiling || EditorApplication.isUpdating)
                {
                    await Task.Delay(100);
                }

                _ = UpdatePackageInstallationStates();
                Repaint();
            }
            catch (Exception e)
            {
                AesirInspectorLogger.Error($"处理包注册变更回调时发生异常：{e}");
            }
        }

        void OnLanguageChanged()
        {
            UpdateTitle();
            Repaint();
        }

        void UpdateTitle()
        {
            titleContent = new GUIContent(WindowName);
        }

        #endregion
    }
}
