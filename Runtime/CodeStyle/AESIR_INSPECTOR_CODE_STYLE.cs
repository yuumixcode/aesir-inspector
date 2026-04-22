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

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Component = UnityEngine.Component;

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
        /// 伤害值
        /// </summary>
        [Summary("伤害值")]
        float DamageValue { get; }

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
    [HelpURL("https://example.com/aesir-inspector")]
    [AddComponentMenu("Aesir Inspector/Code Style Example")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class AesirInspectorCodeStyle : MonoBehaviour, IDamageable<float>
    {
        // 成员排序规范 (适配 Rider Unity Layout)：
        // 1. 公共委托 (Public Delegates)
        // 2. 公共枚举 (Public Enums)
        // 3. 静态字段与常量 (Static Fields & Constants - 常量优先)
        // 4. 序列化字段 (Serialized Fields - 保持 Inspector 排序)
        // 5. 非序列化字段 (Non-serialized Fields - 只读优先)
        // 6. 构造函数 (Constructors - 静态优先)
        // 7. 属性与索引器 (Properties & Indexers)
        // 8. Unity 事件函数 (Event Functions / Unity Messages)
        // 9. 接口实现 (Interface Implementations)
        // 10. 其他所有成员 (All other members - 方法与事件)
        // 11. 嵌套类型 (Nested Types)

        // --- 1. 公共委托 ---
        /// <summary>
        /// 状态改变委托
        /// </summary>
        [Summary("状态改变委托")]
        public delegate void StateChangedHandler(string stateName);

        // --- 2. 公共枚举 ---
        /// <summary>
        /// 内部状态枚举
        /// </summary>
        [Summary("内部状态枚举")]
        public enum InternalState
        {
            None = 0,
            Active = 1,
            Inactive = 2
        }

        // --- 3. 静态字段与常量 ---
        // 常量优先
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

        // --- 4. 序列化字段 ---
        // 保持 Inspector 排序，不自动按名称排序
        [Header("Player Settings")]
        [SerializeField]
        bool isPlayerDead;

        [SerializeField]
        PlayerStats stats;

        [Space(10)]
        [Header("Stats Settings")]
        [Tooltip("单个特性独占一行，提升可读性")]
        [SerializeField]
        [Range(0f, 100f)]
        float anotherStat;

        [SerializeField]
        [TextArea(3, 5)]
        string descriptionText;

        // --- 5. 非序列化字段 ---
        // 只读优先
        readonly int _instanceId;
        int _elapsedTimeInDays;
        int _maxHealth;

        // --- 6. 构造函数 ---
        // 静态构造函数优先
        static AesirInspectorCodeStyle() => SharedCount = 0;

        /// <summary>
        /// 构造函数示例
        /// </summary>
        [Summary("构造函数示例")]
        public AesirInspectorCodeStyle() => _instanceId = GetHashCode();

        // --- 7. 属性与索引器 ---
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
        void Awake()
        {
            Initialize();
        }

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
        }

        // --- 9. 接口实现 ---
        /// <summary>
        /// IDamageable 接口实现：处理伤害
        /// </summary>
        [Summary("IDamageable 接口实现：处理伤害")]
        public void Damage(float damageTaken)
        {
            _maxHealth -= (int)damageTaken;
        }

        // --- 10. 其他所有成员 (方法与事件) ---
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
        /// 启动协程示例
        /// </summary>
        [Summary("启动协程示例")]
        public void StartSampleRoutine()
        {
            StartCoroutine(SampleRoutine());
        }

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

        /// <summary>
        /// 事件订阅方法 (2)：若存在多个订阅方法，应在 On + 事件名后增加动作描述以示区分。
        /// </summary>
        [Summary("事件订阅方法 (2)：使用 On + 事件名 + 动作描述 命名。")]
        public void OnDoorOpenedNotifyUI()
        {
            Debug.Log("UI 回调：更新 UI 状态");
        }

        IEnumerator SampleRoutine()
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Coroutine waited for 1 second.");
        }

        void Initialize() { }

        void FormatExamples(int someExpression)
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
            if (targetTransform != null) // 正确写法
            {
                // targetTransform?.position; // 错误写法，可能导致逻辑不一致
            }

            // 字符串内插：提升可读性，优于字符串加法
            var message = $"Current state is: {someExpression} at {Time.time}";
            Debug.Log(message);

            // 断言：用于验证逻辑假设，仅在开发阶段生效
            Debug.Assert(someExpression >= 0, "Expression should never be negative");

            // 集合初始化：var 关键字简化类型声明
            var powerUps = new List<PlayerStats>();
            var dict = new Dictionary<string, List<GameObject>>();

            // 对象初始化器：简化对象创建和赋值
            var statsInfo = new PlayerStats
            {
                movementSpeed = 10,
                hitPoints = 100,
                hasHealthPotion = true
            };

            // LINQ 示例：提升逻辑处理的可读性，注意在 Update 中避开以防 GC
            var filtered = powerUps.Where(p => p.hitPoints > 50).Select(p => p.movementSpeed).ToList();

            // 异常处理：只针对不可预见的外部错误，不应作为业务逻辑流程控制
            try
            {
                // 可能发生错误的操作
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            // switch 语句：每个 case 独立分行，格式统一
            switch (someExpression)
            {
                case 0:
                    // 业务逻辑注释
                    break;
                case 1:
                    // 业务逻辑注释
                    break;
                case 2:
                    // 业务逻辑注释
                    break;
            }

            // if 语句规范：必须使用大括号，大括号换行
            if (someExpression > 0)
            {
                DoSomething(someExpression);
            }

            // for 循环：变量声明简化，大括号换行
            for (var i = 0; i < 100; i++)
            {
                DoSomething(i);
            }

            // 嵌套循环：缩进一致，避免过度嵌套
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    DoSomething(j);
                }
            }
        }

        void DoSomething(int x) { }

        // --- 11. 嵌套类型 ---
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

    /// <summary>
    /// 可序列化结构体：用于存储配置数据，字段命名简洁
    /// </summary>
    [Summary("可序列化结构体：用于存储配置数据，字段命名简洁")]
    [Serializable]
    public struct PlayerStats
    {
        /// <summary>
        /// 移动速度
        /// </summary>
        [Summary("移动速度")]
        public int movementSpeed;

        /// <summary>
        /// 生命值
        /// </summary>
        [Summary("生命值")]
        public int hitPoints;

        /// <summary>
        /// 是否持有生命药剂
        /// </summary>
        [Summary("是否持有生命药剂")]
        public bool hasHealthPotion;
    }

    /// <summary>
    /// ScriptableObject 规范：数据驱动设计的基础，帕斯卡命名，通过 CreateAssetMenu 创建。
    /// 示例：[CreateAssetMenu(fileName = "NewInspectorData", menuName = "Aesir Inspector/Data")]
    /// </summary>
    [Summary("ScriptableObject 规范：数据驱动设计的基础，帕斯卡命名，通过 CreateAssetMenu 创建")]
    public class AesirInspectorData : ScriptableObject
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        [Summary("配置名称")]
        [SerializeField]
        string configName;

        /// <summary>
        /// 配置名称
        /// </summary>
        [Summary("配置名称")]
        public string ConfigName => configName;
    }

    /// <summary>
    /// 静态工具类规范：使用 static 关键字，通常用于扩展方法或纯函数
    /// </summary>
    [Summary("静态工具类规范：使用 static 关键字，通常用于扩展方法或纯函数")]
    public static class AesirInspectorExtensions
    {
        /// <summary>
        /// 扩展方法示例：首个参数使用 this 关键字，帕斯卡命名
        /// </summary>
        /// <param name="gameObject">目标 GameObject</param>
        /// <returns>是否有指定组件</returns>
        [Summary("扩展方法示例：首个参数使用 this 关键字，帕斯卡命名")]
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component =>
            gameObject.GetComponent<T>() != null;
    }

    /// <summary>
    /// C# 9.0 Record 规范：用于不可变数据模型
    /// </summary>
    [Summary("C# 9.0 Record 规范：用于不可变数据模型")]
    public record PlayerInfo(
        string Name,
        int Level);
}

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// 在旧版 .NET 框架中使用 C# 9.0 init 属性或 Record 所需的兼容类
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IsExternalInit { }
}
