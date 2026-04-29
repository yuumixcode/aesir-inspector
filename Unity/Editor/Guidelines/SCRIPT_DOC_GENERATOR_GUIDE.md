# ScriptDocGenerator 编码规范

> **本规范适用于 ScriptDocGenerator 模块，与 [Aesir Inspector 编码指南](../../Editor/Guidelines/AESIR_INSPECTOR_CODE_STYLE_GUIDE.md) 互补。**

---

## 架构

模块按职责分离为四部分：

| 角色 | 基类 | 职责 |
|---|---|---|
| 数据与面板 | `SerializedScriptableObject` | 状态存储、Odin Inspector UI、重置逻辑 |
| 逻辑控制 | `public static class` | 纯逻辑，无状态，被面板和菜单调用 |
| 窗口 | `OdinEditorWindow` | 承载面板，管理窗口生命周期 |
| 菜单 | `public static class` | `[MenuItem]` 入口，含验证方法 |

```
ScriptDocGeneratorSO        ← 数据与面板
ScriptDocGeneratorController ← 逻辑控制
ScriptDocGeneratorWindow     ← 窗口
ScriptDocGeneratorMenuItems   ← 菜单
```

## 单例

面板类和文档生成器设置类通过 `ScriptableObjectSafeEditorUtility` 获取单例：

```csharp
public static XxxSO Instance =>
    ScriptableObjectSafeEditorUtility.GetOrCreateEditorScriptableObject<XxxSO>(
        ConfigName, RootPath, AssetName);
```

## IAesirInspectorReset

实现 `IAesirInspectorReset` 的类将所有字段重置逻辑拆分为独立方法，命名 `ResetXxx()`，由 `AesirInspectorReset()` 统一调用：

```csharp
public void AesirInspectorReset()
{
    ResetDocFolderPath();
    ResetTypeSource();
    ResetSingleType();
    // ...
}
```

## 公共 / 私有方法

公开方法为薄包装，实际逻辑在 `Internal_` 前缀的私有方法中：

```csharp
public void AnalyzeType() => Internal_AnalyzeType();
public void GenerateDoc() => Internal_GenerateDoc();
```

## 文档生成器策略

`DocGeneratorSettingsSO` 为抽象基类，子类重写 `GetGeneratedDoc` 实现不同文档格式。子类存放于 `DocGeneratorSettings` 文件夹。

```
DocGeneratorSettingsSO (abstract)
└── CnScriptingAPISettingsSO
```

## 文档内容生成

- 使用 `static StringBuilder` 方法按章节构建，每个章节一个方法（如 `CreateMethodsContent`、`CreatePropertiesContent`）。
- 渲染前先遍历数据判断是否需要该章节（避免空标题）。
- 数据为空时尽早 `return`。

```csharp
static StringBuilder CreateMethodsContent(IMethodData[] methodDataArray)
{
    var sb = new StringBuilder();
    if (methodDataArray.Length <= 1) return sb;
    // 预扫描判断是否需要渲染
    var hasApiMember = false;
    foreach (var methodData in methodDataArray) { /* ... */ }
    if (!hasApiMember) return sb;
    // 渲染
    return sb;
}
```

## 事件通信

面板类通过静态事件向窗口发送通知，窗口在 `OnEnable` 订阅、`OnDestroy` 取消订阅：

```csharp
// 面板
public static event Action<ToastPosition, SdfIconType, string, Color, float> ToastRequested;

// 窗口
protected override void OnEnable()
{
    ScriptDocGeneratorSO.ToastRequested -= ShowToast;
    ScriptDocGeneratorSO.ToastRequested += ShowToast;
}
protected override void OnDestroy()
{
    ScriptDocGeneratorSO.ToastRequested -= ShowToast;
    base.OnDestroy();
}
```

## 菜单项

- 路径和排序使用 `AesirInspectorMenuItems` 常量。
- 每个 `[MenuItem]` 操作方法配对一个验证方法。

```csharp
[MenuItem(AesirInspectorMenuItems.AddScriptToTargetType, false, ...)]
public static void AddScriptToTargetType() { /* ... */ }

[MenuItem(AesirInspectorMenuItems.AddScriptToTargetType, true)]
public static bool AddScriptToTargetTypeValidate() { /* ... */ }
```

## 文件输出

- 编码：`UTF8Encoding(false)`（无 BOM）。
- 写入前确保目录存在：`PathSafeEditorUtility.EnsureDirectoryExists`。
