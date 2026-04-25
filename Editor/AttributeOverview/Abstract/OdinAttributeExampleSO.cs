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

namespace RunLab.AesirInspector
{
    using Sirenix.OdinInspector;

    /// <summary>
    /// Odin 序列化的特性案例 SO 泛型抽象基类，提供单例模式。
    /// </summary>
    [Summary("Odin 序列化的特性案例 SO 泛型抽象基类，提供单例模式")]
    public abstract class OdinAttributeExampleSO<T> : SerializedScriptableObject, IAesirInspectorReset
        where T : OdinAttributeExampleSO<T>
    {
        static T _asset;

        /// <summary>
        /// 获取单例实例，若不存在则自动创建。
        /// </summary>
        [Summary("获取单例实例，若不存在则自动创建")]
        public static T Instance
        {
            get
            {
                if (_asset)
                {
                    return _asset;
                }

                _asset = ScriptableObjectSafeEditorUtility
                    .GetSingletonAssetAndDeleteOther<T>(AesirInspectorPaths.AttributePanelsPath);
                return _asset;
            }
        }

        /// <summary>
        /// 重置案例数据到初始状态。
        /// </summary>
        [Summary("重置案例数据到初始状态")]
        public abstract void AesirInspectorReset();
    }
}

#endif
