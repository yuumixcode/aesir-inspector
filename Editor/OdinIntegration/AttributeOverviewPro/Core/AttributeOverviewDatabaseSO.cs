using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("特性总览数据库，发现所有面板类型、创建缺失的 SO 资源并构建分类菜单映射")]
    public class AttributeOverviewDatabaseSO : SerializedScriptableObject, IAesirInspectorReset
    {
        static AttributeOverviewDatabaseSO _instance;

        static readonly AesirAttributeCategory[] Categories =
        {
            AesirAttributeCategory.Essentials,
            AesirAttributeCategory.Buttons,
            AesirAttributeCategory.Collections,
            AesirAttributeCategory.Groups,
            AesirAttributeCategory.Conditionals,
            AesirAttributeCategory.Numbers,
            AesirAttributeCategory.TypeSpecifics,
            AesirAttributeCategory.Validation,
            AesirAttributeCategory.Misc,
            AesirAttributeCategory.Meta,
            AesirAttributeCategory.Unity,
            AesirAttributeCategory.Debug
        };

        public Dictionary<string, AbstractAttributePanelSO[]> AttributePanelArrayMap;

        public Dictionary<string, AbstractAttributePanelSO> AttributePanelMap =
            new Dictionary<string, AbstractAttributePanelSO>();

        [Summary("是否已完成完整初始化（含所有面板的 Initialize 调用）")]
        [SerializeField]
        bool isFullyInitialized;

        [Summary("上次完整初始化时的程序集哈希，用于检测域重载后是否需要重新初始化")]
        [SerializeField]
        string lastAssemblyHash;

        bool _isInitializing;

        [Summary("获取数据库单例实例")]
        public static AttributeOverviewDatabaseSO Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }

                _instance = ScriptableObjectSafeEditorUtility
                    .GetSingletonAssetAndDeleteOther<AttributeOverviewDatabaseSO>(AesirInspectorPaths
                        .AttributeOverviewDatabasePath);

                if (_instance != null && !_instance.isFullyInitialized)
                {
                    _instance.Initialize();
                }

                return _instance;
            }
        }

        public void AesirInspectorReset() => Initialize();

        [Button("初始化数据库", ButtonSizes.Large)]
        [Summary("初始化数据库：发现所有面板类型，创建缺失的 SO 资源，构建菜单映射")]
        public void Initialize()
        {
            if (_isInitializing)
            {
                return;
            }

            _isInitializing = true;

            try
            {
                var currentHash = ComputeAssemblyHash();

                // 快速路径：序列化数据完整且程序集未变化时，仅重建运行时映射
                if (isFullyInitialized && lastAssemblyHash == currentHash && AttributePanelArrayMap != null &&
                    AttributePanelArrayMap.Count > 0)
                {
                    RebuildAttributePanelMap();
                    return;
                }

                var allPanels = GetAllAttributePanels();
                if (allPanels == null || allPanels.Length == 0)
                {
                    AttributePanelArrayMap = new Dictionary<string, AbstractAttributePanelSO[]>();
                    AttributePanelMap = new Dictionary<string, AbstractAttributePanelSO>();
                    isFullyInitialized = false;
                    return;
                }

                // 一次遍历完成分类，避免多次 LINQ Where + ToArray
                var categoryBuckets = new Dictionary<string, List<AbstractAttributePanelSO>>();
                foreach (var cat in Categories)
                {
                    categoryBuckets[cat.ToString()] = new List<AbstractAttributePanelSO>();
                }

                var categoryMap = new Dictionary<Type, AesirAttributeCategory>();
                foreach (var panel in allPanels)
                {
                    if (panel == null)
                    {
                        continue;
                    }

                    categoryMap.TryGetValue(panel.GetType(), out var cat);
                    if (cat == 0)
                    {
                        var attr = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
                        cat = attr?.Category ?? AesirAttributeCategory.Misc;
                        categoryMap[panel.GetType()] = cat;
                    }

                    foreach (var definedCat in Categories)
                    {
                        if ((cat & definedCat) != 0)
                        {
                            categoryBuckets[definedCat.ToString()].Add(panel);
                        }
                    }
                }

                AttributePanelArrayMap = new Dictionary<string, AbstractAttributePanelSO[]>();
                foreach (var cat in Categories)
                {
                    var key = cat.ToString();
                    AttributePanelArrayMap[key] = categoryBuckets[key].ToArray();
                }

                // 初始化各面板并构建映射
                AttributePanelMap = new Dictionary<string, AbstractAttributePanelSO>();
                var totalPanels = allPanels.Length;

                for (var i = 0; i < totalPanels; i++)
                {
                    var panel = allPanels[i];
                    if (panel == null)
                    {
                        continue;
                    }

                    EditorUtility.DisplayProgressBar("Initializing Aesir Inspector",
                        $"Initializing Panel: {panel.name} ({i + 1}/{totalPanels})", (float)i / totalPanels);

                    panel.Initialize();
                }

                RebuildAttributePanelMap();

                isFullyInitialized = true;
                lastAssemblyHash = currentHash;
                EditorUtility.SetDirty(this);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
                _isInitializing = false;
            }
        }

        /// <summary>
        /// 从已有的 AttributePanelArrayMap 重建 AttributePanelMap，无需重新初始化面板。
        /// 域重载后 Instance 首次访问时调用，避免对每个面板重复调用 Initialize()。
        /// </summary>
        void RebuildAttributePanelMap()
        {
            AttributePanelMap = new Dictionary<string, AbstractAttributePanelSO>();
            if (AttributePanelArrayMap == null)
            {
                return;
            }

            foreach (var (category, panelArray) in AttributePanelArrayMap)
            {
                if (panelArray == null)
                {
                    continue;
                }

                foreach (var panel in panelArray)
                {
                    if (panel == null)
                    {
                        continue;
                    }

                    if (panel.BilingualHeaderControl?.headerName == null)
                    {
                        continue;
                    }

                    var menuName = panel.BilingualHeaderControl.headerName.ChineseDisplay.SplitPascalCase();
                    var key = category + "/" + menuName;
                    AttributePanelMap.TryAdd(key, panel);
                }
            }
        }

        /// <summary>
        /// 基于所有面板类型的程序集名称计算简单哈希，用于检测域重载后代码是否发生变化。
        /// </summary>
        static string ComputeAssemblyHash()
        {
            var types = TypeCache.GetTypesDerivedFrom<AbstractAttributePanelSO>();
            var hash = 0;
            foreach (var t in types)
            {
                if (t.IsAbstract || t.IsInterface)
                {
                    continue;
                }

                if (t.AssemblyQualifiedName != null)
                {
                    hash ^= t.AssemblyQualifiedName.GetHashCode();
                }
            }

            return hash.ToString();
        }

        static AbstractAttributePanelSO[] GetAllAttributePanels()
        {
            var panelTypes = TypeCache.GetTypesDerivedFrom<AbstractAttributePanelSO>()
                .Where(t => !t.IsAbstract && !t.IsInterface).ToArray();

            var existingAssets = AssetDatabase.FindAssets("t:" + typeof(AbstractAttributePanelSO))
                .Select(guid =>
                    AssetDatabase.LoadAssetAtPath<AbstractAttributePanelSO>(AssetDatabase.GUIDToAssetPath(guid)))
                .Where(x => x != null)
                .GroupBy(x => x.GetType())
                .ToDictionary(g => g.Key, g => g.First());

            var list = new List<AbstractAttributePanelSO>();
            var needsRefresh = false;

            for (var i = 0; i < panelTypes.Length; i++)
            {
                var type = panelTypes[i];
                EditorUtility.DisplayProgressBar("Initializing Aesir Inspector",
                    $"Discovering Panel: {type.Name} ({i + 1}/{panelTypes.Length})",
                    (float)i / panelTypes.Length * 0.5f);

                if (existingAssets.TryGetValue(type, out var asset))
                {
                    list.Add(asset);
                }
                else
                {
                    asset = (AbstractAttributePanelSO)CreateInstance(type);
                    var fileName = type.Name.EndsWith("SO")
                        ? type.Name.Remove(type.Name.Length - 2)
                        : type.Name;
                    var path = AesirInspectorPaths.AttributePanelsPath + "/" + fileName + ".asset";
                    if (!Directory.Exists(AesirInspectorPaths.AttributePanelsPath))
                    {
                        Directory.CreateDirectory(AesirInspectorPaths.AttributePanelsPath);
                    }

                    AssetDatabase.CreateAsset(asset, path);
                    needsRefresh = true;
                    list.Add(asset);
                }
            }

            if (needsRefresh)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            return list.ToArray();
        }
    }
}
