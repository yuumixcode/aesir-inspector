using RunLab.AesirInspector.Editor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
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
        [MenuItem(AesirInspectorMenuItems.AttributeOverviewPro, false,
            AesirInspectorMenuItems.AttributeOverviewProOrder)]
        [Summary("打开 Attribute Overview 窗口")]
        public static void OpenWindow()
        {
            _window = GetWindow<AttributeOverviewWindow>("Attribute Overview Pro");
            _window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1050, 750);
            _window.Show();
        }

        #endregion

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
    }
}
