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

using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinWrapper.Editor
{
    /// <summary>
    /// AssetsOnly 特性使用案例，展示对 GameObject、Material、MeshRenderer 等类型的资源约束。
    /// </summary>
    [Summary("AssetsOnly 特性使用案例，展示对 GameObject、Material、MeshRenderer 等类型的资源约束")]
    [AesirExample]
    public class AssetsOnlyExampleSO : AttributeExampleSO<AssetsOnlyExampleSO>
    {
        [Title("No Parameters")]
        [AssetsOnly]
        public GameObject somePrefab;

        [AssetsOnly]
        public Material materialAsset;

        [AssetsOnly]
        public MeshRenderer someMeshRendererOnPrefab;

        /// <summary>
        /// 重置所有字段到默认值。
        /// </summary>
        [Summary("重置所有字段到默认值")]
        public override void AesirInspectorReset()
        {
            somePrefab = null;
            materialAsset = null;
            someMeshRendererOnPrefab = null;
        }
    }
}
