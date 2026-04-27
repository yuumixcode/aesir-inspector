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

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 特性案例序列化类型。
    /// </summary>
    [Summary("特性案例序列化类型")]
    public enum AttributeExampleType
    {
        UnitySerialized = 0,
        OdinSerialized = 1
    }

    /// <summary>
    /// 单个特性使用案例预览项，持有案例名称与对应的 ScriptableObject 引用。
    /// </summary>
    [Summary("单个特性使用案例预览项，持有案例名称与对应的 ScriptableObject 引用")]
    public class AttributeExamplePreviewItem
    {
        SerializedScriptableObject _odinSerializedExample;

        ScriptableObject _unitySerializedExample;

        /// <summary>
        /// 案例序列化类型。
        /// </summary>
        [Summary("案例序列化类型")]
        public AttributeExampleType ExampleType { get; private set; }

        /// <summary>
        /// 案例显示名称。
        /// </summary>
        [Summary("案例显示名称")]
        public string ItemName { get; private set; }

        /// <summary>
        /// Unity 原生序列化的案例 ScriptableObject。
        /// </summary>
        [Summary("Unity 原生序列化的案例 ScriptableObject")]
        public ScriptableObject UnitySerializedExample
        {
            get
            {
                if (ExampleType == AttributeExampleType.UnitySerialized)
                {
                    return _unitySerializedExample;
                }

                Debug.LogError("Odin 序列化的案例应该获取 " + nameof(OdinSerializedExample));
                return null;
            }
        }

        /// <summary>
        /// Odin 序列化的案例 ScriptableObject。
        /// </summary>
        [Summary("Odin 序列化的案例 ScriptableObject")]
        public SerializedScriptableObject OdinSerializedExample
        {
            get
            {
                if (ExampleType == AttributeExampleType.OdinSerialized)
                {
                    return _odinSerializedExample;
                }

                Debug.LogError("Unity 原生序列化的案例应该获取 " + nameof(UnitySerializedExample));
                return null;
            }
        }

        /// <summary>
        /// 初始化为 Unity 序列化案例。
        /// </summary>
        [Summary("初始化为 Unity 序列化案例")]
        public AttributeExamplePreviewItem InitializeUnitySerializedExample(string itemName,
            ScriptableObject unitySerializedExample)
        {
            ExampleType = AttributeExampleType.UnitySerialized;
            ItemName = itemName;
            _unitySerializedExample = unitySerializedExample;
            return this;
        }

        /// <summary>
        /// 初始化为 Odin 序列化案例。
        /// </summary>
        [Summary("初始化为 Odin 序列化案例")]
        public AttributeExamplePreviewItem InitializeOdinSerializedExample(string itemName,
            SerializedScriptableObject odinSerializedExample)
        {
            ExampleType = AttributeExampleType.OdinSerialized;
            ItemName = itemName;
            _odinSerializedExample = odinSerializedExample;
            return this;
        }

        /// <summary>
        /// 重置当前案例到初始状态。
        /// </summary>
        [Summary("重置当前案例到初始状态")]
        public void Reset()
        {
            switch (ExampleType)
            {
                case AttributeExampleType.OdinSerialized:
                    if (_odinSerializedExample is IAesirInspectorReset canResetOdin)
                    {
                        canResetOdin.AesirInspectorReset();
                        Debug.Log(_odinSerializedExample.GetType().Name + " 重置成功！");
                    }
                    else
                    {
                        Debug.LogWarning("当前案例 " + _odinSerializedExample.GetType().Name +
                                         " 未实现 IAesirInspectorReset 接口！");
                    }

                    break;

                case AttributeExampleType.UnitySerialized:
                    if (_unitySerializedExample is IAesirInspectorReset canResetUnity)
                    {
                        canResetUnity.AesirInspectorReset();
                        Debug.Log(_unitySerializedExample.GetType().Name + " 重置成功！");
                    }
                    else
                    {
                        Debug.LogWarning("当前案例 " + _unitySerializedExample.GetType().Name +
                                         " 未实现 IAesirInspectorReset 接口！");
                    }

                    break;
            }
        }
    }
}
