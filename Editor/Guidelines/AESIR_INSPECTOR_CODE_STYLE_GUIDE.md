# Aesir Inspector 编码指南

> **本指南适用于 Aesir Inspector 全部代码，特殊指定的模块除外。当前特殊模块：AttributeOverviewPro 中的 AttributeExample 示例代码。**

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

- 对应公开方法的私有实现加 `Internal_` 前缀。

## Odin Inspector 使用规范

> 仅适用于 Aesir Inspector 核心代码及 Aesir 系列脚本，与用户自定义的 Odin 相关代码无关。

- 优先用 Odin Attribute 构建 UI，优先用 OdinAttributeProcessor 动态注入特性，而非手动装饰或编写原始 Editor 代码。
- Processor：`internal` 修饰，与对应 Attribute 或被处理类定义在**同一脚本文件**中，无需 XML / `[Summary]`。
- Processor 访问私有成员：需 `nameof` 引用时，定义为**嵌套类**（仍 `internal`），此为"同文件"的合规形式。
- Drawer：继承 `OdinAttributeDrawer` 的类独立存于 `Drawers` 文件夹。
- 桥梁工具：`OdinInspectorSafeEditorUtility.cs` 保留 Odin Inspector 宏定义约束。

---

违反 **Null 检查** 或 **命名** 规范的代码视为不可接受的 Bug。
