# OVDF 文件格式规范

> Odin Visual Designer File (.ovdf) — Odin Inspector 4.0+ Visual Designer 的持久化格式。

---

## 概述

OVDF 是 Odin Inspector Visual Designer 的自定义、人类可读的文件格式，用于保存 Inspector 可视化编辑产生的属性布局变更（添加 Attribute、调整 Position 等）。文件设计为可通过版本控制共享。

- **最低 Odin 版本**：4.0（Visual Designer 随 4.0 引入）
- **当前格式版本**：`OVDF v1.1`（Odin 4.0.1.2 起升级）

---

## 文件结构

### 完整示例

```
OVDF v1.1
RunLab.AesirInspector.BilingualHeaderControl, RunLab.AesirInspector

# $RkTyZDL7uYoeUWC3NnzNoJ
Position: $root:13
+ [Sirenix.OdinInspector.BoxGroupAttribute]

# _headerIntroduction
Position: $RkTyZDL7uYoeUWC3NnzNoJ:1

# _headerName
Position: $RkTyZDL7uYoeUWC3NnzNoJ:0
```

### 逐行解析

| 行 | 格式 | 说明 |
|---|---|---|
| 1 | `OVDF v{major}.{minor}` | 格式版本头。当前为 `OVDF v1.1`（v1.0 → v1.1 于 Odin 4.0.1.2） |
| 2 | `{FullTypeName}, {AssemblyName}` | 目标类型标识。使用完整类型名 + 程序集名 |
| 空行 | — | 版本头与内容之间的分隔 |
| `# ${id}` | 节点定义 | `${id}` 是 Visual Designer 生成的唯一标识符（Base64 风格），用于引用和定位 |
| `# {fieldName}` | 字段节点 | 直接使用 C# 字段名（`[SerializeField]` 私有字段名） |
| `Position: {ref}:{index}` | 位置声明 | `ref` 为父节点 ID（`$root` 表示根级），`index` 为排序索引 |
| `+ [{FullAttributeTypeName}]` | Attribute 添加 | 为所属节点添加指定 Attribute |

### 节点层级关系

- `Position: $root:N` — 根级节点，N 为全局排序索引
- `Position: ${parentId}:N` — `${parentId}` 所指节点的子节点，N 为同级排序索引
- 子节点顺序由 `index` 决定，索引从 0 开始

### 类型引用方式

- **目标类型**：`FullTypeName, AssemblyName`（如 `RunLab.AesirInspector.BilingualHeaderControl, RunLab.AesirInspector`）
- **Attribute 类型**：`[Sirenix.OdinInspector.BoxGroupAttribute]`，使用完整命名空间 + 类名

---

## 版本差异

### v1.0 → v1.1 变更（Odin 4.0.1.2）

| 特性 | v1.0 | v1.1 |
|------|------|------|
| 类型解析 | 仅 `TypeName, Assembly` | 额外存储脚本 .meta GUID（MonoBehaviour/ScriptableObject） |
| 脚本重命名/移动 | 类型解析会断开 | 通过 GUID 仍可解析 |
| `FormerlySerializedAs` | 不支持 | 支持，可用于字段重命名追踪 |
| 代码添加的 Attribute | 未标记 | 标记为代码添加 |

---

## 类型解析机制

### 解析优先级

1. **v1.1+ MonoBehaviour/ScriptableObject**：先通过 `.meta` GUID 定位脚本资产，再匹配类型名
2. **v1.0 或非 MonoBehaviour/ScriptableObject 类型**：仅通过 `FullTypeName, AssemblyName` 解析

### 对本项目的影响

`BilingualHeaderControl` 是普通 `[Serializable]` 类，**不是** MonoBehaviour 或 ScriptableObject，因此：

- v1.1 的 GUID 追踪**不适用**
- 类型解析完全依赖 `RunLab.AesirInspector.BilingualHeaderControl, RunLab.AesirInspector` 字符串
- **重命名类型或更改命名空间会导致 OVDF 文件失效**，需同步更新文件头

---

## 自定义包移动

### Odin 的文件发现机制

- Odin 扫描 `Assets/` 下所有 `.ovdf` 文件
- 文件可放在 Assets 内的任意位置，Odin 均可发现
- 默认保存路径在 `OdinVisualDesignerConfig.asset` 中配置

### 当前项目存在两份相同 OVDF

| 路径 | 说明 |
|------|------|
| `Assets/Plugins/Sirenix/Odin Inspector/Visual Designer/Saved/` | Odin 默认保存位置 |
| `Assets/aesir-inspector/Editor/Odin Integration/Visual Designer/Saved/` | 自定义包内副本 |

两份文件内容完全相同，但 meta GUID 不同（`4e037e03...` vs `e6ecbb9f...`）。

### 移动策略

| 场景 | 建议 |
|------|------|
| OVDF 随自定义包分发 | ✅ 可以。将文件放在包目录下，Odin 会自动发现 |
| 包内路径变更 | ✅ 安全。Odin 不依赖文件路径定位，只依赖类型名/GUID |
| 多项目复用 | ✅ 可以。OVDF 是纯文本，可随包复制 |
| Odin 默认路径下的副本 | ⚠️ 应删除。保留两份相同文件可能导致冲突，保留包内版本即可 |

### 保存路径配置

在 `OdinVisualDesignerConfig.asset` 中修改 `savePath`，将默认保存位置指向自定义包内：

```yaml
savePath: Assets/aesir-inspector/Editor/Odin Integration/Visual Designer/Saved
```

或在 Editor 中：**Tools → Odin → Inspector → Preferences → Visual Designer** 修改保存路径。

---

## 脚本名称修改处理

### 场景与影响

| 变更类型 | MonoBehaviour/ScriptableObject | 普通 Serializable 类 |
|----------|-------------------------------|---------------------|
| 重命名字段 | v1.1 支持 `FormerlySerializedAs` | v1.1 支持 `FormerlySerializedAs` |
| 重命名类型 | v1.1 通过 GUID 仍可解析 | ❌ 类型解析断开，需手动更新 OVDF 文件头 |
| 更改命名空间 | v1.1 通过 GUID 仍可解析 | ❌ 类型解析断开，需手动更新 OVDF 文件头 |
| 更改程序集 | v1.1 通过 GUID 仍可解析 | ❌ 类型解析断开，需手动更新 OVDF 文件头 |
| 移动脚本文件 | v1.1 通过 GUID 仍可解析 | ⚠️ 仅 `AssemblyName` 未变时可解析 |

### 修复方法

1. **字段重命名**：在 C# 代码中添加 `[FormerlySerializedAs("oldName")]`
2. **类型/命名空间/程序集变更**（普通类）：手动编辑 OVDF 文件第 2 行，更新 `FullTypeName, AssemblyName`
3. **OVDF 中的字段名**：若字段名变更且无 `FormerlySerializedAs`，需同步更新 OVDF 中 `# {fieldName}` 节点

### 安全变更流程

```
1. 添加 [FormerlySerializedAs] 到变更字段
2. 修改 OVDF 文件头的类型引用（如有命名空间/类型名变更）
3. 修改 OVDF 文件中的字段名节点（如有字段名变更）
4. 在 Designer Files Overview (Tools > Odin > Inspector > Designer Files Overview) 中检查错误
```

---

## 自定义 Attribute 注册

OVDF 中引用的 Attribute 必须在 Visual Designer 中注册，否则不会出现在 Attribute Editor 中。

### 注册方式

```csharp
[assembly: OdinVisualDesignerAttributeItem("Custom", typeof(SomeAttribute))]
```

### 序列化绑定

默认情况下 Visual Designer 不序列化 Attribute 的 property。需使用 `[OdinDesignerBinding]` 标记：

```csharp
public class SomeAttribute : Attribute
{
    public Color FieldColor; // public field 默认可序列化

    [OdinDesignerBinding(nameof(SomeProperty))]
    public string SomeProperty { get; set; } // 需要绑定才能序列化
}
```

- `public` 字段：默认可序列化
- Property：需 `[OdinDesignerBinding(nameof(backingField))]` 标记
- 复杂 Property：可绑定多个 backing field

---

## 相关工具

| 工具 | 路径 |
|------|------|
| Designer Files Overview | Tools → Odin → Inspector → Designer Files Overview |
| Visual Designer Preferences | Tools → Odin → Inspector → Preferences → Visual Designer |
| OdinVisualDesignerConfig | `Assets/Plugins/Sirenix/Odin Inspector/Config/Editor/OdinVisualDesignerConfig.asset` |

---

## 参考来源

- [Visual Designer - Getting Started](https://odininspector.com/visual-designer-getting-started)
- [Getting Started With The Visual Designer](https://odininspector.com/tutorials/visual-designer/getting-started-with-the-visual-designer)
- [Registering Custom Attributes](https://odininspector.com/tutorials/visual-designer/registering-custom-attributes)
- [Designer File Overview](https://odininspector.com/tutorials/visual-designer/getting-started-with-designer-file-overview)
- [Patch Notes v4.0.1.2](https://odininspector.com/patch-notes/4-0-1-2)（v1.1 变更）
- [Patch Notes v4.0.1](https://odininspector.com/patch-notes/4-0-1)（Visual Designer 正式发布）
