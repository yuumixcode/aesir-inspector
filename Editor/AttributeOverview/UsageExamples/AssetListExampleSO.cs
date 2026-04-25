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

#if UNITY_EDITOR

namespace RunLab.AesirInspector
{
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;

    /// <summary>
    /// AssetList 特性使用案例，展示 Path、AutoPopulate、Tags、LayerNames、AssetNamePrefix 等参数用法。
    /// </summary>
    [Summary("AssetList 特性使用案例，展示 Path、AutoPopulate、Tags、LayerNames、AssetNamePrefix 等参数用法")]
    [AesirExample]
    public class AssetListExampleSO : AttributeExampleSO<AssetListExampleSO>
    {
        #region --- Public Methods ---

        /// <summary>
        /// 重置所有字段到默认值。
        /// </summary>
        [Summary("重置所有字段到默认值")]
        public override void AesirInspectorReset()
        {
            singleObject = null;
            assetList = null;
            autoPopulatedWhenInspected = null;
            gameObjectsWithTag = null;
            gameObjectsWithLayerNames = null;
            gameObjectsWithNamePrefix = null;
        }

        #endregion

        #region Serialized Fields

        [FoldoutGroup("$NoParameterTitleLabel", false)]
        [AssetList]
        [PreviewField(70, ObjectFieldAlignment.Center)]
        [InlineButton("LogSingleObject", "$LogButtonLabel")]
        public Texture2D singleObject;

        [FoldoutGroup("$PathTitleLabel", false)]
        [AssetList(Path = "/Plugins/Sirenix/")]
        [InlineButton("LogAssetList", "$LogButtonLabel")]
        public List<ScriptableObject> assetList;

        [FoldoutGroup("$AutoPopulateAndPathTitleLabel", false)]
        [AssetList(AutoPopulate = true, Path = "Plugins/Sirenix/")]
        [InlineButton("LogAutoPopulatedWhenInspected", "$LogButtonLabel")]
        public List<ScriptableObject> autoPopulatedWhenInspected;

        [FoldoutGroup("$TagsTitleLabel", false)]
        [AssetList(Tags = "EditorOnly,Respawn")]
        [InlineButton("LogGameObjectsWithTag", "$LogButtonLabel")]
        public List<GameObject> gameObjectsWithTag;

        [FoldoutGroup("$LayerNamesTitleLabel", false)]
        [AssetList(LayerNames = "Water")]
        [InlineButton("LogGameObjectsWithLayerNames", "$LogButtonLabel")]
        public GameObject[] gameObjectsWithLayerNames;

        [FoldoutGroup("$AssetNamePrefixTitleLabel", false)]
        [AssetList(AssetNamePrefix = "AesirInspector_")]
        [InlineButton("LogGameObjectsWithNamePrefix", "$LogButtonLabel")]
        public List<GameObject> gameObjectsWithNamePrefix;

        #endregion

        #region Internal

        static readonly BilingualData NoParameterTitleLabel = new BilingualData("无参数", "No Parameter");
        static readonly BilingualData PathTitleLabel = new BilingualData("参数：Path", "Parameter: Path");

        static readonly BilingualData AutoPopulateAndPathTitleLabel =
            new BilingualData("参数：AutoPopulate + Path", "Parameter: AutoPopulate + Path");

        static readonly BilingualData TagsTitleLabel = new BilingualData("参数：Tags", "Parameter: Tags");

        static readonly BilingualData LayerNamesTitleLabel =
            new BilingualData("参数：LayerNames", "Parameter: LayerNames");

        static readonly BilingualData AssetNamePrefixTitleLabel =
            new BilingualData("参数：AssetNamePrefix", "Parameter: AssetNamePrefix");

        static readonly BilingualData LogButtonLabel = new BilingualData("输出信息", "Output Info");

        void LogSingleObject()
        {
            Debug.Log("SingleObject = " + singleObject);
        }

        void LogAssetList()
        {
            if (assetList != null)
            {
                Debug.Log("assetList Count = " + assetList.Count);
            }
        }

        void LogAutoPopulatedWhenInspected()
        {
            if (autoPopulatedWhenInspected != null)
            {
                Debug.Log("autoPopulatedWhenInspected Count = " + autoPopulatedWhenInspected.Count);
            }
        }

        void LogGameObjectsWithTag()
        {
            if (gameObjectsWithTag != null)
            {
                Debug.Log("GameObjectsWithTag Count = " + gameObjectsWithTag.Count);
            }
        }

        void LogGameObjectsWithLayerNames()
        {
            if (gameObjectsWithLayerNames != null)
            {
                Debug.Log("gameObjectsWithLayerNames Length = " + gameObjectsWithLayerNames.Length);
            }
        }

        void LogGameObjectsWithNamePrefix()
        {
            if (gameObjectsWithNamePrefix != null)
            {
                Debug.Log("GameObjectsWithNamePrefix Count = " + gameObjectsWithNamePrefix.Count);
            }
        }

        #endregion
    }
}

#endif
