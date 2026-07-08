using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("MenuItemViewer 可视化面板")]
    public class MenuItemViewerSO : ScriptableObject, IAesirInspectorReset
    {
        [Summary("EditorBuildSettings 存储引用的 Key")]
        static readonly string ConfigName =
            OdinBridgeLocator.Bridge.GetFriendlyFullName(typeof(MenuItemViewerSO));

        [Summary("菜单单项检查器的菜单路径")]
        public static BilingualData ToolMenuPath = new BilingualData("菜单项检查器", "MenuItemViewer");

        [Summary("获取 MenuItemViewerSO 单例")]
        public static MenuItemViewerSO Instance =>
            ScriptableObjectSafeEditorUtility.GetOrCreateEditorScriptableObject<MenuItemViewerSO>(ConfigName,
                AesirInspectorPaths.MiniToolsAssetsFolderPath, "MenuItemViewer");

        void OnEnable()
        {
            bilingualHeaderControl = new BilingualHeaderControl("MenuItem 查看器", "MenuItem Viewer",
                "查看项目内的 MenuItem 的信息，便于规划菜单项",
                "View the information of MenuItems within the project to facilitate menu item planning",
                AesirInspectorWebLinks.GitUrl);
        }

        [Summary("将所有字段重置为默认值")]
        public void AesirInspectorReset()
        {
            assemblyFilter = null;
            menuItemInfos = null;
        }

        [PropertySpace(8, 8)]
        [BilingualButton("搜集项目所有菜单项，排除筛选项", "Collect MenuItems Exclude Filter", ButtonSizes.Large)]
        public void CollectMenuItems()
        {
            menuItemInfos = MenuItemViewerController.GetAllMenuItems(assemblyFilter);
        }

        public BilingualHeaderControl bilingualHeaderControl;

        [PropertySpace]
        [SerializeReference]
        [BilingualTitle("剔除特定程序集的菜单项", "Exclude MenuItems from Specific Assembly")]
        [HideLabel]
        public IAssemblyFilter assemblyFilter;

        [PropertyOrder(10)]
        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        public List<MenuItemInfo> menuItemInfos;
    }
}
