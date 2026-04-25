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

namespace RunLab.AesirInspector.Editor
{
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Aesir Inspector 特性总览编辑器窗口，以可搜索的树形菜单展示所有已注册的特性面板。
    /// </summary>
    [Summary("Aesir Inspector 特性总览编辑器窗口，以可搜索的树形菜单展示所有已注册的特性面板")]
    public class AttributeOverviewWindow : OdinMenuEditorWindow
    {
        static AttributeOverviewWindow _window;
        AttributeOverviewDatabaseSO _databaseSO;
        OdinMenuTree _tree;

        #region Public Methods

        /// <summary>
        /// 打开 Attribute Overview 窗口。
        /// </summary>
        [MenuItem(AesirInspectorMenuItems.AttributeOverviewPro, false, AesirInspectorMenuItems.AttributeOverviewProOrder)]
        [Summary("打开 Attribute Overview 窗口")]
        public static void OpenWindow()
        {
            _window = GetWindow<AttributeOverviewWindow>("Attribute Overview Pro");
            _window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1050, 750);
            _window.Show();
        }

        #endregion

        #region Internal

        protected override void Initialize()
        {
            _databaseSO = AttributeOverviewDatabaseSO.Instance;
            WindowPadding = new Vector4(15, 15, 15, 5);
            MenuWidth = 230f;
            _tree = new OdinMenuTree
            {
                Config =
                {
                    DrawSearchToolbar = true,
                    SearchTerm = "",
                    SearchFunction = menuItem =>
                    {
                        var str = menuItem.Name.ToLower().Replace(" ", "");
                        var searchStr = _tree.Config.SearchTerm.ToLower().Replace(" ", "");
                        return str.Contains(searchStr);
                    }
                },
                DefaultMenuStyle = new OdinMenuStyle
                {
                    Height = 24
                }
            };
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            if (_databaseSO == null || _databaseSO.AttributePanelMap == null)
            {
                return _tree;
            }

            foreach (var keyValuePair in _databaseSO.AttributePanelMap)
            {
                _tree.AddObjectAtPath(keyValuePair.Key, keyValuePair.Value);
            }

            _tree.SortMenuItemsByName();
            return _tree;
        }

        #endregion
    }
}

#endif
