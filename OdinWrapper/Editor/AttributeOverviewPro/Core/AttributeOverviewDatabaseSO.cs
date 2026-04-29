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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;

namespace RunLab.AesirInspector.OdinWrapper.Editor
{
    /// <summary>
    /// Attribute Overview 数据库单例 SO，负责发现并管理所有 AbstractAttributePanelSO 资源。
    /// </summary>
    [Summary("Attribute Overview 数据库单例 SO，负责发现并管理所有 AbstractAttributePanelSO 资源")]
    public class AttributeOverviewDatabaseSO : SerializedScriptableObject, IAesirInspectorReset
    {
        static AttributeOverviewDatabaseSO _instance;

        /// <summary>
        /// 按分类分组的面板数组映射。
        /// </summary>
        [Summary("按分类分组的面板数组映射")]
        public Dictionary<string, AbstractAttributePanelSO[]> AttributePanelArrayMap;

        /// <summary>
        /// 用于 OdinMenuTree 的扁平面板映射，键为 "分类/中文名"。
        /// </summary>
        [Summary("用于 OdinMenuTree 的扁平面板映射，键为 \"分类/中文名\"")]
        public Dictionary<string, AbstractAttributePanelSO> AttributePanelMap =
            new Dictionary<string, AbstractAttributePanelSO>();

        /// <summary>
        /// 获取数据库单例实例。
        /// </summary>
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

                if (_instance != null && (_instance.AttributePanelMap == null ||
                                          _instance.AttributePanelMap.Count == 0))
                {
                    _instance.Initialize();
                }

                return _instance;
            }
        }

        /// <summary>
        /// 重置数据库（重新初始化）。
        /// </summary>
        [Summary("重置数据库（重新初始化）")]
        public void AesirInspectorReset() => Initialize();

        /// <summary>
        /// 初始化数据库：发现所有面板类型，创建缺失的 SO 资源，构建菜单映射。
        /// </summary>
        [Button("初始化数据库", ButtonSizes.Large)]
        [Summary("初始化数据库：发现所有面板类型，创建缺失的 SO 资源，构建菜单映射")]
        public AttributeOverviewDatabaseSO Initialize()
        {
            var allPanels = Internal_GetAllAttributePanels();
            if (allPanels == null || allPanels.Length == 0)
            {
                AttributePanelArrayMap = new Dictionary<string, AbstractAttributePanelSO[]>();
                AttributePanelMap = new Dictionary<string, AbstractAttributePanelSO>();
                return this;
            }

            AttributePanelArrayMap = new Dictionary<string, AbstractAttributePanelSO[]>
            {
                {
                    nameof(AesirAttributeCategory.Essentials),
                    Internal_FilterPanels(allPanels, AesirAttributeCategory.Essentials)
                },
                {
                    nameof(AesirAttributeCategory.Buttons),
                    Internal_FilterPanels(allPanels, AesirAttributeCategory.Buttons)
                },
                {
                    nameof(AesirAttributeCategory.Collections),
                    Internal_FilterPanels(allPanels, AesirAttributeCategory.Collections)
                },
                {
                    nameof(AesirAttributeCategory.Groups),
                    Internal_FilterPanels(allPanels, AesirAttributeCategory.Groups)
                },
                {
                    nameof(AesirAttributeCategory.Conditionals),
                    Internal_FilterPanels(allPanels, AesirAttributeCategory.Conditionals)
                },
                {
                    nameof(AesirAttributeCategory.Numbers),
                    Internal_FilterPanels(allPanels, AesirAttributeCategory.Numbers)
                },
                {
                    nameof(AesirAttributeCategory.TypeSpecifics),
                    Internal_FilterPanels(allPanels, AesirAttributeCategory.TypeSpecifics)
                },
                {
                    nameof(AesirAttributeCategory.Validation),
                    Internal_FilterPanels(allPanels, AesirAttributeCategory.Validation)
                },
                {
                    nameof(AesirAttributeCategory.Misc),
                    Internal_FilterPanels(allPanels, AesirAttributeCategory.Misc)
                },
                {
                    nameof(AesirAttributeCategory.Meta),
                    Internal_FilterPanels(allPanels, AesirAttributeCategory.Meta)
                },
                {
                    nameof(AesirAttributeCategory.Unity),
                    Internal_FilterPanels(allPanels, AesirAttributeCategory.Unity)
                },
                {
                    nameof(AesirAttributeCategory.Debug),
                    Internal_FilterPanels(allPanels, AesirAttributeCategory.Debug)
                }
            };

            AttributePanelMap = new Dictionary<string, AbstractAttributePanelSO>();
            foreach (var (category, panelArray) in AttributePanelArrayMap)
            {
                foreach (var panel in panelArray)
                {
                    if (panel == null)
                    {
                        continue;
                    }

                    panel.Initialize();

                    if (panel.BilingualHeaderControl?.headerName == null)
                    {
                        continue;
                    }

                    var menuName = panel.BilingualHeaderControl.headerName.ChineseDisplay;
                    var key = category + "/" + menuName;
                    AttributePanelMap.TryAdd(key, panel);
                }
            }

            return this;
        }

        static AbstractAttributePanelSO[] Internal_FilterPanels(AbstractAttributePanelSO[] panels,
            AesirAttributeCategory category)
        {
            return panels.Where(x =>
            {
                if (x == null)
                {
                    return false;
                }

                var attr = x.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
                return attr != null && attr.Category.HasFlagFast(category);
            }).ToArray();
        }

        static AbstractAttributePanelSO[] Internal_GetAllAttributePanels()
        {
            var panelTypes = TypeCache.GetTypesDerivedFrom<AbstractAttributePanelSO>()
                .Where(t => !t.IsAbstract && !t.IsInterface).ToArray();

            var existingAssets = AssetDatabase.FindAssets("t:" + typeof(AbstractAttributePanelSO))
                .Select(guid =>
                    AssetDatabase.LoadAssetAtPath<AbstractAttributePanelSO>(
                        AssetDatabase.GUIDToAssetPath(guid))).Where(x => x != null).GroupBy(x => x.GetType())
                .ToDictionary(g => g.Key, g => g.First());

            var list = new List<AbstractAttributePanelSO>();
            var needsRefresh = false;

            foreach (var type in panelTypes)
            {
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
