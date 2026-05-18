# ADR-004: SafeEditorUtility 模式

## Status

Accepted

## Context

Runtime 程序集需要调用某些 Editor 功能（如 `AssetDatabase`、`EditorGUI`），但直接引用 Editor API 会导致运行时编译失败。`#if UNITY_EDITOR` 条件编译可以解决问题，但对于工具类方法，需要更优雅的模式。

## Decision

采用 `SafeEditorUtility` 模式：

1. Runtime 工具类命名为 `XxxSafeEditorUtility`，放置在 `Runtime/Unity/Utilities/`
2. `void` 返回值的公共方法使用 `[Conditional("UNITY_EDITOR")]`，构建时自动剔除
3. 有返回值的公共方法使用 `#if UNITY_EDITOR` 内部实现，运行时返回默认值
4. 日志统一使用 `AesirInspectorLogger`，不直接 `Debug.LogWarning`
5. Odin 相关操作通过 `OdinInspectorSafeEditorUtility` 桥梁调用

```csharp
// void 方法：构建剔除
[Conditional("UNITY_EDITOR")]
public static void PingObject(Object target) { /* Editor 实现 */ }

// 有返回值方法：双实现
public static bool TryGetAssetPath(Object target, out string path)
{
#if UNITY_EDITOR
    path = AssetDatabase.GetAssetPath(target);
    return !string.IsNullOrEmpty(path);
#else
    path = default;
    return false;
#endif
}
```

## Consequences

- **优点**: Runtime 程序集可安全调用 Editor 功能
- **优点**: 构建时自动剔除无用代码，零运行时开销
- **缺点**: 方法签名需同时满足 Runtime 和 Editor 两种约束
- **缺点**: 有返回值的方法需要维护两套实现
