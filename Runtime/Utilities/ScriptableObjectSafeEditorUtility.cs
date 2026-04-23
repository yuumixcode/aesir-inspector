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

using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RunLab.AesirInspector
{
    /// <summary>
    /// ScriptableObject 的编辑器安全工具类，不需要编写宏定义。仅编辑器阶段有效，打包后运行时调用，返回 null 或者其他默认值。
    /// </summary>
    [Summary("ScriptableObject 的编辑器安全工具类，不需要编写宏定义。仅编辑器阶段有效，打包后运行时调用，返回 null 或者其他默认值。")]
    public static class ScriptableObjectSafeEditorUtility
    {
        #region --- Public Methods ---

        /// <summary>
        /// 根据配置名称获取或创建编辑器 ScriptableObject 资源。
        /// 如果资源不存在则自动创建并保存到指定路径，同时将资源注册到 EditorBuildSettings 中。
        /// 打包后此方法将失效，返回 null。
        /// </summary>
        [Summary(
            "根据配置名称获取或创建编辑器 ScriptableObject 资源。如果资源不存在则自动创建并保存到指定路径，同时将资源注册到 EditorBuildSettings 中。打包后此方法将失效，返回 null。")]
        public static T GetOrCreateEditorScriptableObject<T>(string configName,
            string folderPath,
            string assetName) where T : ScriptableObject
        {
#if UNITY_EDITOR
            return Internal_GetOrCreateEditorScriptableObject<T>(configName, folderPath, assetName);
#else
            return null;
#endif
        }

        #endregion

        #region Internal

        static T Internal_GetOrCreateEditorScriptableObject<T>(string configName,
            string folderPath,
            string assetName) where T : ScriptableObject
        {
            if (EditorBuildSettings.TryGetConfigObject(configName, out T instance))
            {
                return instance;
            }

            // 确保文件夹存在
            if (!string.IsNullOrEmpty(folderPath) && !Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                AssetDatabase.Refresh();
            }

            var assetPath = folderPath + "/" + assetName + ".asset";
            var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset != null)
            {
                EditorBuildSettings.AddConfigObject(configName, asset, true);
                return asset;
            }

            asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, assetPath);
            EditorBuildSettings.AddConfigObject(configName, asset, true);
            AssetDatabase.ImportAsset(assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return asset;
        }

        #endregion
    }
}
