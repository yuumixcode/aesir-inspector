# Conventions

> Aesir Inspector 代码风格与命名规范。本文件为 AI Agent 提供完整的编码约定参考。

---

## 命名

| 标识符 | 规则 | 示例 |
|---|---|---|
| 类、接口、方法 | `PascalCase`，接口 `I` 前缀 | `PlayerManager`, `IDamageable` |
| 私有非序列化字段 | `_camelCase` | `_health` |
| 序列化字段 `[SerializeField]` | `camelCase`（无下划线） | `moveSpeed` |
| 常量 / 静态只读 | `PascalCase` | `MaxScore` |

## 枚举

- 普通：含 `None = 0`，显式赋值（防插入导致序列化错乱）。
  ```csharp
  public enum State { None = 0, Idle = 1, Run = 2, Jump = 3 }
  ```
- Flags：必须 `[Flags]`，值为 `1 << n`，复合用 `|`。
  ```csharp
  [Flags] public enum Modes { None = 0, A = 1 << 0, B = 1 << 1, AB = A | B }
  ```

## Unity 禁忌

- **严禁**对 `UnityEngine.Object` 派生类使用 `?.` / `??`（绕过 Unity 生命周期检查，会导致逻辑错误）。
- **严禁**在 `Update` 中调用 `GetComponent`、`Find`、字符串拼接、LINQ。

## 事件

| 角色 | 命名 | 示例 |
|---|---|---|
| 事件 | 无 `On` 前缀 | `DoorOpened` |
| 订阅方法 | `On` + 事件名 | `OnDoorOpened` |
| 多订阅方法 | `On` + 事件名 + 动作描述 | `OnDoorOpenedUpdateUI` |
| 触发方法 | `Raise` + 事件名 | `RaiseDoorOpened` |

## 注释

- 公共类型（类、结构体、枚举、接口）**必须同时**具备 `/// <summary>` 和 `[Summary("...")]`。
- 公共方法、属性、事件：注释可选，仅在用途不直观时添加。
- XML 仅保留 `<summary>`，移除 `<param>` / `<returns>`。
- 公共构造函数例外：无需注释。

## 方法

- `Internal_` 前缀的方法名，必须是一个私有或者受保护或者内部的方法和一个公开方法重名，才能使用 `Internal_` 前缀。

## Odin Inspector 使用规范

> 仅适用于 Aesir Inspector 核心代码及 Aesir 系列脚本。

- 优先用 Odin Attribute 构建 UI，优先用 OdinAttributeProcessor 动态注入特性。
- Processor：`internal` 修饰，与对应 Attribute 或被处理类定义在**同一脚本文件**中，无需 XML / `[Summary]`。
- Processor 访问私有成员：需 `nameof` 引用时，定义为**嵌套类**（仍 `internal`）。
- Drawer：继承 `OdinAttributeDrawer` 的类独立存于 `Drawers` 文件夹。
- 桥梁工具：`OdinInspectorSafeEditorUtility.cs` 保留 Odin Inspector 宏定义约束。

## Utility 类命名

| 类别 | 命名规则 | 示例 |
|---|---|---|
| Runtime | `XxxUtility` | `PathUtility`, `RegexUtility` |
| Editor 安全封装 | `XxxSafeEditorUtility` | `HierarchySafeEditorUtility` |
| Editor-Only 编辑器工具 | `XxxEditorUtility` | `PackageManagerEditorUtility` |

## Editor 安全封装

Runtime 程序集需要调用 Editor 功能时，通过 `SafeEditorUtility` 模式：

- **不要求**整个类用 `#if UNITY_EDITOR` 包裹；运行时保留空方法实现。
- 返回值为 `void` 的公共方法**必须**添加 `[Conditional("UNITY_EDITOR")]`。
- 有返回值的公共方法在运行时返回默认值，不抛出警告或错误。
- Odin 相关操作通过 `OdinInspectorSafeEditorUtility` 桥梁调用。

```csharp
public static class ProjectSafeEditorUtility
{
    [Conditional("UNITY_EDITOR")]
    public static void PingObject(Object target) { /* Editor 实现 */ }

    public static bool TryGetAssetPath(Object target, out string path)
    {
#if UNITY_EDITOR
        // Editor 实现...
#else
        path = default;
        return false;
#endif
    }
}
```

## AttributeOverviewPro 特定规则

### Data-Panel-Example 三件套

新增 Odin 特性介绍页面，**必须同时创建三个文件**：

| 层 | 目录 | 命名 | 职责 |
|---|---|---|---|
| 数据 | `Data/` | `{AttributeName}AttributeData.cs` | 纯数据，`internal`，无需 `[Summary]` |
| 面板 | `AttributePanels/` | `{AttributeName}AttributePanelSO.cs` | 仅绑定数据，**禁止**添加 GUI 代码 |
| 案例 | `UsageExamples/` | `{AttributeName}ExampleSO.cs` | 必须标注 `[AesirExample]` |

### 隐藏条件属性命名

- 集合为空：`{FieldName}IsEmpty`，如 `UsageTipIsEmpty`
- 对象为空：`{Target}IsNull`，如 `CurrentExampleIsNull`

### 双语标签命名

- 私有：`_camelCase` + `Label` 后缀，如 `_usageTipsLabel`
- 公共：`PascalCase` + `Label` 后缀，如 `ResolverTypeLabel`

### 事件订阅规范

```csharp
// 先 - 再 +，防止重复
AesirInspectorLanguageSettingsSO.OnLanguageChanged -= Internal_OnLanguageChanged;
AesirInspectorLanguageSettingsSO.OnLanguageChanged += Internal_OnLanguageChanged;

// OnDestroy 中取消
AesirInspectorLanguageSettingsSO.OnLanguageChanged -= Internal_OnLanguageChanged;
```
