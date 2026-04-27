# Utilities 编码指南

> **本指南适用于 Aesir Inspector 所有 Utility
类，与 [Aesir Inspector 编码指南](AESIR_INSPECTOR_CODE_STYLE_GUIDE.md) 互补。**

---

## 类声明

- **必须**为 `public static class`。
- 私有方法加 `Internal_` 前缀。

```csharp
public static class PathUtility
{
    public static string Normalize(string path)
    {
        // ...
    }

    private static string Internal_NormalizeSeparators(string path)
    {
        // ...
    }
}
```

## 命名

| 类别             | 命名规则                   | 示例                            |
|----------------|------------------------|-------------------------------|
| Runtime        | `XxxUtility`           | `PathUtility`, `RegexUtility` |
| Editor 安全封装    | `XxxSafeEditorUtility` | `HierarchySafeEditorUtility`  |
| Editor-Only 编辑器工具 | `XxxEditorUtility`     | `PackageManagerEditorUtility` |

## Editor 安全封装

Runtime 程序集需要调用 Editor 功能时，通过 `SafeEditorUtility` 模式实现运行时安全：

- **不要求**整个类用 `#if UNITY_EDITOR` 包裹；运行时保留空方法实现，便于测试。
- 返回值为 `void` 的公共方法**必须**添加 `[Conditional("UNITY_EDITOR")]`，以实现构建剔除。
- 有返回值的公共方法在运行时返回默认值，不抛出警告或错误。
- Odin 相关操作通过 `OdinInspectorSafeEditorUtility` 桥梁调用。
- 日志统一使用 `AesirInspectorLogger`。

```csharp
public static class ProjectSafeEditorUtility
{
    [Conditional("UNITY_EDITOR")]
    public static void PingObject(Object target)
    {
        // Editor 实现...
    }

    public static bool TryGetAssetPath(Object target, out string path)
    {
#if UNITY_EDITOR
        // Editor 实现...
#else
        path = default;
        return false;
#endif
    }

    private static void Internal_PingAndSelect(Object target)
    {
        // ...
    }
}
```

## 注释

- 公共类型**必须同时**具备 `/// <summary>` 和 `[Summary("...")]`。
- 公共方法、属性：注释可选，仅在用途不直观时添加。

