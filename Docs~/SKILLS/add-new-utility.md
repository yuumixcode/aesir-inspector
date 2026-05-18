# Skill: 添加新的 Utility 类

> 当需要新增一个工具类时，按此指南操作。

## 步骤

### 1. 确定类型和命名

| 类别 | 命名规则 | 目录 | 示例 |
|------|----------|------|------|
| Runtime 工具 | `XxxUtility` | `Runtime/Unity/Utilities/` | `PathUtility`, `RegexUtility` |
| Editor 安全封装 | `XxxSafeEditorUtility` | `Runtime/Unity/Utilities/` | `HierarchySafeEditorUtility` |
| Editor-Only 工具 | `XxxEditorUtility` | `Editor/Unity/` | `PackageManagerEditorUtility` |

### 2. 创建文件

**Runtime 工具**：
```csharp
namespace RunLab.AesirInspector
{
    /// <summary>
    /// {Description}。
    /// </summary>
    [Summary("{Description}。")]
    public static class XxxUtility
    {
        public static return_type MethodName(params) { /* ... */ }
        private static return_type Internal_MethodName(params) { /* ... */ }
    }
}
```

**Editor 安全封装**：
```csharp
namespace RunLab.AesirInspector
{
    /// <summary>
    /// {Description}。
    /// </summary>
    [Summary("{Description}。")]
    public static class XxxSafeEditorUtility
    {
        [Conditional("UNITY_EDITOR")]
        public static void VoidMethod(params) { /* Editor 实现 */ }

        public static bool TryMethod(params, out result)
        {
#if UNITY_EDITOR
            // Editor 实现
#else
            result = default;
            return false;
#endif
        }
    }
}
```

### 3. 规范

- 必须 `public static class`
- 私有方法加 `Internal_` 前缀
- 公共类型必须有 `/// <summary>` + `[Summary]`
- 日志使用 `AesirInspectorLogger`
- Odin 相关操作通过 `OdinInspectorSafeEditorUtility` 桥接

## 验证

1. 无 Odin 环境下编译通过
2. 构建后无运行时错误（`[Conditional]` 方法被剔除）
