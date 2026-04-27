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
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR

namespace RunLab.AesirInspector
{
    /// <summary>
    /// AssetList 特性 CustomFilterMethod 参数使用案例，展示如何通过自定义方法过滤资源列表。
    /// </summary>
    [Summary("AssetList 特性 CustomFilterMethod 参数使用案例，展示如何通过自定义方法过滤资源列表")]
    [AesirExample]
    public class AssetListExampleWithCustomFilterMethodSO :
        AttributeExampleSO<AssetListExampleWithCustomFilterMethodSO>
    {
        static readonly BilingualData CustomFilterMethodTitleLabel =
            new BilingualData("参数: Custom Filter Method", "Parameter: Custom Filter Method");

        static readonly BilingualData LogButtonLabel = new BilingualData("输出信息", "Output Info");

        [Title("$CustomFilterMethodTitleLabel")]
        [AssetList(CustomFilterMethod = "$HasRigidbodyComponent")]
        [InlineButton("LogRigidbodyPrefabs", "$LogButtonLabel")]
        public List<GameObject> rigidbodyPrefabs;

        /// <summary>
        /// 重置所有字段到默认值。
        /// </summary>
        [Summary("重置所有字段到默认值")]
        public override void AesirInspectorReset()
        {
            rigidbodyPrefabs = null;
        }

        bool HasRigidbodyComponent(GameObject obj) => obj.GetComponent<Rigidbody>() != null;

        void LogRigidbodyPrefabs()
        {
            if (rigidbodyPrefabs != null)
            {
                foreach (var prefab in rigidbodyPrefabs)
                {
                    if (prefab != null)
                    {
                        Debug.Log("Rigidbody Prefab: " + prefab.name);
                    }
                }
            }
        }
    }
}

#endif
