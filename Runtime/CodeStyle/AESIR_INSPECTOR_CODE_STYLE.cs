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
// 方法与区域规范 (Methods & Regions):
// 1. 供外部调用的公开方法必须使用 #region --- Public Methods --- 宏定义分区域。
// 2. 所有公开方法（构造函数除外）必须同时包含 XML /// <summary> 和 [Summary] 特性。XML 注释仅保留 summary 标签，移除 param, returns 等多余标签。
// 3. 公共构造函数不需要添加 XML (/// <summary>) 和 [Summary] 特性。
// 4. 内部/私有方法必须使用 #region Internal 宏定义分区域。
// 5. 如果私有方法逻辑上对应某个公开方法（如同名逻辑实现），私有方法应增加 Internal_ 前缀。

// Odin Inspector 规范 (Odin Inspector Integration):
// 注意：以下 Processor、Drawer 等针对特殊类的规范，其前提是仅针对 Aesir Inspector 核心开发。如果用户自定义其他的特殊类扩展，是不受此规范约束。
// 1. 优先使用 Odin Attribute 来构建 UI，而非编写原始的 Editor 代码。
// 2. 模块必须保证在未安装 Odin Inspector 时依然可以正常编译。所有对 Sirenix.OdinInspector 命名空间的引用、特性的应用、以及对 Odin API 的调用，都必须使用 #if ODIN_INSPECTOR_3_3 宏定义包裹。仅包裹 Odin 特有的类型或 API，标准 C# 类型属性不应被包裹。注意：OdinInspectorSafeEditorUtility 是编辑器桥梁工具，其自身及对其公开方法的调用不需要被宏包裹。
// 3. 优先选择使用 OdinAttributeProcessor 的方式去动态添加特性，而不是在原本的类中通过大量的宏定义 (#if ODIN_INSPECTOR_3_3) 装饰字段或方法。
// 4. 自定义的 OdinAttributeProcessor 必须与对应的 Attribute 或受其处理的类定义在同一个脚本文件中。继承 OdinAttributeDrawer 的类，依旧独立在 Drawers 文件夹。
// 5. Processor 必须使用 internal 访问修饰符。
// 6. Processor 与 Drawer 必须使用 #if UNITY_EDITOR && ODIN_INSPECTOR_3_3 宏定义进行包裹。
// 7. 继承自 OdinAttributeProcessor<T> 的类不需要编写 XML (/// <summary>) 和 [Summary] 特性注释。
// 8. 只有当用户可调用的类中包含了 Odin Inspector 的内容才使用 #region --- Odin Inspector ---。对于不推荐用户调用的类中，比如继承了 OdinAttributeProcessor 的类，是不需要包裹在 Odin Inspector 区域内的。
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR && ODIN_INSPECTOR_3_3
using Sirenix.OdinInspector.Editor;
#endif

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter  
// ReSharper disable ConvertToAutoPropertyWhenPossible  
#pragma warning disable CS0067

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 普通枚举规范：帕斯卡命名法。
    /// 1. C# 默认基础类型为 int，无需显式指定 : int。
    /// 2. 建议显式为成员赋值（使用连续整数 0, 1, 2...），以防止在 Unity 序列化时因顺序调整导致数据错乱。
    /// 3. 普通枚举用于互斥状态，不需要二进制/2的幂表示。
    /// 4. 所有枚举必须包含一个显式的 0 值成员（通常命名为 None 或 Default），以作为安全的默认状态。
    /// </summary>
    [Summary("普通枚举规范：使用整数赋值；无需二进制表示；必须包含业务含义明确的 0 值成员（如 None）。")]
    public enum Direction
    {
        None = 0,
        North = 1,
        South = 2,
        East = 3,
        West = 4
    }

    /// <summary>
    /// Flags 特性枚举规范：帕斯卡命名法，首字母大写，用于可组合的状态。
    /// 1. 必须使用 [Flags] 特性。
    /// 2. 基础枚举值必须是二进制位（2 的幂：1, 2, 4, 8...），建议使用位移操作 (1 &lt;&lt; n) 提高可读性。
    /// 3. 复合枚举成员（组合多个状态）应使用按位或 (|) 运算符组合已定义的成员，以确保维护性和意图清晰。
    /// </summary>
    [Summary("Flags 特性枚举规范：基础值使用二进制位（位移操作）；复合值使用按位或 (|) 组合。")]
    [Flags]
    public enum AttackModes
    {
        None = 0,
        Melee = 1 << 0,                   // 1
        Ranged = 1 << 1,                  // 2
        Special = 1 << 2,                 // 4
        MeleeAndSpecial = Melee | Special // 5 (1 | 4)
    }

    /// <summary>
    /// 接口规范：I 前缀 + 帕斯卡命名法
    /// </summary>
    [Summary("接口规范：I 前缀 + 帕斯卡命名法")]
    public interface IDamageable
    {
        /// <summary>
        /// 伤害类型名称
        /// </summary>
        [Summary("伤害类型名称")]
        string DamageTypeName { get; }

        /// <summary>
        /// 应用伤害
        /// </summary>
        [Summary("应用伤害")]
        bool ApplyDamage(string description, float damage, int numberOfHits);
    }

    /// <summary>
    /// 泛型接口规范：参数类型标注清晰
    /// </summary>
    [Summary("泛型接口规范：参数类型标注清晰")]
    public interface IDamageable<in T>
    {
        /// <summary>
        /// 受伤害处理
        /// </summary>
        [Summary("受伤害处理")]
        void Damage(T damageTaken);
    }

    /// <summary>
    /// Aesir Inspector 代码风格示例，展示本项目的规范和最佳实践，基于 Rider 默认推荐格式
    /// </summary>
    [Summary("Aesir Inspector 代码风格示例，展示本项目的规范和最佳实践，基于 Rider 默认推荐格式")]
    [AesirInspectorExample]
    public sealed class AesirInspectorCodeStyle : MonoBehaviour, IDamageable<float>
    {
        /// <summary>
        /// 最大计数器
        /// </summary>
        [Summary("最大计数器")]
        public const int MaxCount = 100;

        /// <summary>
        /// 共享计数
        /// </summary>
        [Summary("共享计数")]
        public static int SharedCount;

        /// <summary>
        /// 缓存的颜色属性 ID
        /// </summary>
        [Summary("缓存的颜色属性 ID")]
        static readonly int ColorPropertyId = Shader.PropertyToID("_BaseColor");

        [Header("Player Settings")]
        [SerializeField]
        bool isPlayerDead;

        [Space(10)]
        [Header("Stats Settings")]
        [Tooltip("单个特性独占一行，提升可读性")]
        [SerializeField]
        [Range(0f, 100f)]
        float anotherStat;

        // 只读优先
        readonly int _instanceId;
        int _elapsedTimeInDays;
        int _maxHealth;

        public AesirInspectorCodeStyle() => _instanceId = GetHashCode();

        /// <summary>
        /// 最大生命值（只读）
        /// </summary>
        [Summary("最大生命值（只读）")]
        public int MaxHealthReadOnly => _maxHealth;

        /// <summary>
        /// 当前计数（私有 Setter）
        /// </summary>
        [Summary("当前计数（私有 Setter）")]
        public int CurrentCount { get; private set; }

        /// <summary>
        /// 最大生命值
        /// </summary>
        [Summary("最大生命值")]
        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        // --- 8. Unity 事件函数 ---
        // 按照执行顺序或 Rider 默认顺序排列
        void Awake() { }

#if UNITY_EDITOR
        // 编辑器专用代码：使用条件编译确保不会被包含进构建包
        /// <summary>
        /// 重置组件（编辑器专用）
        /// </summary>
        [Summary("重置组件（编辑器专用）")]
        public void Reset()
        {
            _maxHealth = 100;
        }
#endif

        void Start()
        {
            Debug.Log($"{nameof(AesirInspectorCodeStyle)} initialized successfully.");
        }

        void Update()
        {
            // 性能优化提示：
            // 1. 避免在 Update 中调用 GetComponent, Find 或分配内存（如 new, LINQ）。
            // 2. 避免在每帧进行字符串拼接。
            Debug.Log("Update");
        }

        /// <summary>
        /// IDamageable 接口实现：处理伤害
        /// </summary>
        [Summary("IDamageable 接口实现：处理伤害")]
        public void Damage(float damageTaken)
        {
            _maxHealth -= (int)damageTaken;
        }

        /// <summary>
        /// 正在开门
        /// </summary>
        [Summary("正在开门")]
        public event Action OpeningDoor;

        /// <summary>
        /// 门已打开
        /// </summary>
        [Summary("门已打开")]
        public event Action DoorOpened;

        /// <summary>
        /// 得分
        /// </summary>
        [Summary("得分")]
        public event Action<int> PointsScored;

        /// <summary>
        /// 触发自定义事件
        /// </summary>
        [Summary("触发自定义事件")]
        public event Action<CustomEventArgs> ThingHappened;

        /// <summary>
        /// 设置最大生命值
        /// </summary>
        [Summary("设置最大生命值")]
        public void SetMaxHealth(int newMaxValue) => _maxHealth = newMaxValue;

        /// <summary>
        /// 触发门已打开事件
        /// </summary>
        [Summary("触发门已打开事件")]
        public void RaiseDoorOpened()
        {
            DoorOpened?.Invoke();
        }

        /// <summary>
        /// 触发得分事件
        /// </summary>
        [Summary("触发得分事件")]
        public void RaisePointsScored(int points)
        {
            PointsScored?.Invoke(points);
        }

        /// <summary>
        /// 事件订阅方法 (1)：基础命名规范为 On + 事件名。
        /// 允许同一个类中多个方法订阅同一事件（多播委托）。
        /// </summary>
        [Summary("事件订阅方法 (1)：基础命名规范为 On + 事件名。")]
        public void OnDoorOpened()
        {
            Debug.Log("基础回调：门已打开");
        }

        void FormatExamples()
        {
            // 性能优化：使用 TryGetComponent 避免两次底层查询
            if (TryGetComponent<BoxCollider>(out var boxCollider))
            {
                boxCollider.enabled = true;
            }

            // 性能优化：使用 CompareTag 替代字符串直接比较
            if (gameObject.CompareTag("Player"))
            {
                // 处理逻辑
            }

            // Unity 对象 Null 检查警告：
            // 严禁对 UnityEngine.Object 及其派生类（如 MonoBehaviour, Transform）使用 ?. 或 ?? 运算符。
            // 因为 Unity 对象的 null 检查是自定义的（处理 C++ 层面的销毁），原生 C# 运算符会绕过这种检查。
            var targetTransform = transform;
            if (targetTransform != null)
            {
                // targetTransform.position; // 正确写法
            }
        }

        /// <summary>
        /// 事件参数结构体：参数较多时用结构体整合
        /// </summary>
        [Summary("事件参数结构体：参数较多时用结构体整合")]
        public struct CustomEventArgs
        {
            /// <summary>
            /// 对象 ID
            /// </summary>
            [Summary("对象 ID")]
            public int ObjectID { get; }

            /// <summary>
            /// 颜色
            /// </summary>
            [Summary("颜色")]
            public Color Color { get; }

            public CustomEventArgs(int objectId, Color color)
            {
                ObjectID = objectId;
                Color = color;
            }
        }
    }

    // --- 11. Odin Inspector 规范 ---
    /// <summary>
    /// 编辑器扩展组织规范：优先选择使用 OdinAttributeProcessor 的方式去动态添加特性。
    /// 自定义的 OdinAttributeProcessor 必须与对应的 Attribute 或受其处理的类定义在同一个脚本文件中。
    /// 继承 OdinAttributeDrawer 的类，依旧独立在 Drawers 文件夹。
    /// </summary>
    [Summary("编辑器扩展组织规范：优先使用 Processor 注入特性；Processor 放在对应脚本中。")]
    internal class AesirInspectorExampleAttribute : Attribute { }

#if UNITY_EDITOR && ODIN_INSPECTOR_3_3

    internal sealed class AesirInspectorAttributeExampleProcessor<T> : OdinAttributeProcessor<T>
        where T : class
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            // 为类本身添加特性，例如：
            // attributes.Add(new InlinePropertyAttribute());
        }

        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty,
            MemberInfo member,
            List<Attribute> attributes)
        {
            // 为成员（字段、方法、属性）添加特性，例如：
            // if (member.Name == "someField") attributes.Add(new PropertyOrderAttribute(10));
        }
    }

#endif
}
