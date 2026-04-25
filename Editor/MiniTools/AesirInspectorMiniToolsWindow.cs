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

#if UNITY_EDITOR && ODIN_INSPECTOR_3_3

using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace RunLab.AesirInspector.Editor
{
    /// <summary>
    /// Aesir Inspector Mini Tools 窗口，整合常用编辑器小工具入口
    /// </summary>
    [Summary("Aesir Inspector Mini Tools 窗口，整合常用编辑器小工具入口")]
    public class AesirInspectorMiniToolsWindow : OdinMenuEditorWindow
    {
        // --- 2. 静态字段/常量 ---

        static readonly BilingualData SyntaxHighlighterMenuName =
            new BilingualData("语法高亮处理器", "Syntax Highlighter");

        static readonly BilingualData MenuItemViewerMenuName =
            new BilingualData("菜单项查看器", "MenuItem Viewer");

        static object _lastSelection;

        // --- 4. 非序列化字段 ---

        OdinSyntaxHighlighterSO _highlighter;
        MenuItemViewerSO _menuItemViewer;
        OdinMenuStyle _menuStyle;

        // 7. 业务逻辑方法

        #region --- Public Methods ---

        /// <summary>
        /// 打开 Aesir Inspector Mini Tools 窗口
        /// </summary>
        [Summary("打开 Aesir Inspector Mini Tools 窗口")]
        [MenuItem(AesirInspectorMenuItems.MiniTools, false,
            AesirInspectorMenuItems.MiniToolsOrder)]
        public static void Open()
        {
            var window = GetWindow<AesirInspectorMiniToolsWindow>();
            window.titleContent = new GUIContent("Aesir Inspector Mini Tools");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.Show();
        }

        #endregion

        #region Event Functions

        protected override void OnEnable()
        {
            base.OnEnable();
            _highlighter = OdinSyntaxHighlighterSO.Instance;
            _menuItemViewer = MenuItemViewerSO.Instance;
            MenuWidth = 220f;
            WindowPadding = new Vector4(10f, 10f, 10f, 10f);
            _menuStyle = new OdinMenuStyle
            {
                Height = 30,
                Offset = 16.00f,
                IndentAmount = 15.00f,
                IconSize = 16.00f,
                IconOffset = 0.00f,
                NotSelectedIconAlpha = 0.85f,
                IconPadding = 3.00f,
                TriangleSize = 17.00f,
                TrianglePadding = 8.00f,
                AlignTriangleLeft = false,
                Borders = true,
                BorderPadding = 13.00f,
                BorderAlpha = 0.50f,
                SelectedColorDarkSkin = new Color(0.243f, 0.373f, 0.588f, 1.000f),
                SelectedColorLightSkin = new Color(0.243f, 0.490f, 0.900f, 1.000f)
            };
            AesirInspectorLanguageSettings.LanguageChanged -= CustomRebuild;
            AesirInspectorLanguageSettings.LanguageChanged += CustomRebuild;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            AesirInspectorLanguageSettings.LanguageChanged -= CustomRebuild;
            _lastSelection = null;
        }

        #endregion

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false, _menuStyle)
            {
                { SyntaxHighlighterMenuName, _highlighter },
                { MenuItemViewerMenuName, _menuItemViewer }
            };
            tree.Config.DrawSearchToolbar = true;
            tree.EnumerateTree().AddThumbnailIcons().SortMenuItemsByName();
            return tree;
        }

        #region Internal

        void CustomRebuild()
        {
            _lastSelection = MenuTree.Selection.SelectedValue;
            ForceMenuTreeRebuild();
            TrySelectMenuItemWithObject(_lastSelection);
        }

        #endregion
    }
}

#endif
