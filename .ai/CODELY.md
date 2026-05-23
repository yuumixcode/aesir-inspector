# Aesir Inspector — AI Core Context

> 本文件是 AI Agent 的核心上下文文档，等同于 `AGENTS.md`。所有代码生成、重构、审查操作**必须**遵循本文档中的规范。

Aesir Inspector (`cn.runlab.aesir-inspector`) 是一个 Unity/Tuanjie 编辑器扩展库，提供双语 Inspector UI、安全编辑器工具集、脚本文档生成器、XML Summary 同步工具等功能。**可选集成 Odin Inspector**。

- **Version**: 0.4.0-pre.1 | **License**: MIT
- **Engine**: Tuanjie (Unity 2022.3 fork) | **Language**: C# / .NET Standard 2.1
- **Odin Inspector**: 基于**最新稳定版**持续集成，当前基线 **4.0.1.x**（含 Visual Designer + OVDF v1.1）
- **Repo**: https://github.com/yuumixcode/aesir-inspector

## Assemblies

| Assembly | Odin 依赖 |
|----------|-----------|
| `RunLab.AesirInspector` | 无 |
| `RunLab.AesirInspector.Editor` | 无 |
| `RunLab.AesirInspector.OdinIntegration` | `ODIN_INSPECTOR` |
| `RunLab.AesirInspector.OdinIntegration.Editor` | `ODIN_INSPECTOR` |
| `RunLab.AesirInspector.Tests` | 无 |
| `RunLab.AesirInspector.Editor.Tests` | 无 |

## Directory Structure

```
Aesir Inspector/
├── Runtime/
│   ├── Unity/                     # Core runtime (RunLab.AesirInspector)
│   │   ├── Attributes/            # [Summary]
│   │   ├── Core/                  # Version, Paths, WebLinks, IAesirInspectorReset
│   │   ├── Inspector/             # Inspector 显示模型
│   │   ├── Localization/          # 本地化数据与语言设置
│   │   ├── Logging/               # 日志系统
│   │   ├── OdinBridge/            # IOdinBridge, DefaultOdinBridge, OdinBridgeLocator
│   │   ├── ScriptDocGenerator/    # 文档生成器运行时模型
│   │   └── Utilities/             # SafeEditorUtility 工具集
│   └── Odin Integration/          # Odin runtime (ODIN_INSPECTOR)
│       ├── Attributes/            # 双语特性
│       └── OdinCodeHighlighter.cs
├── Editor/
│   ├── Unity/                     # Core editor (RunLab.AesirInspector.Editor)
│   │   ├── Core/                  # 安装检测、菜单管理
│   │   ├── MiniTools/             # QuickCreateSO
│   │   └── SummaryTool/           # XML Summary 同步
│   └── Odin Integration/          # Odin editor (ODIN_INSPECTOR)
│       ├── AttributeOverviewPro/  # 特性总览窗口
│       ├── AttributeProcessors/   # OdinAttributeProcessor
│       ├── Bridge/                # OdinInspectorBridge
│       ├── Drawers/               # 双语 Drawer
│       ├── ExtensionManager/      # 扩展包管理器
│       ├── MiniTools/             # MenuItem Viewer, Syntax Highlighter
│       ├── ScriptDocGenerator/    # 文档生成器编辑器逻辑
│       └── Windows/               # Getting Started, Preferences
├── Tests/Editor/ & Tests/Runtime/
├── Samples~/
└── Documentation~/               # aesir-inspector.md (用户) + development.md (开发者)
```

## SOLID Principles

遵循 SOLID 原则，确保用户可独立扩展而不必修改包内源码。**但每一层抽象都必须有明确的理由——过早抽象与缺乏抽象同样有害。**

> **核心判据**：引入抽象前，回答"谁会替换这个实现？"。没有替换者就不需要接口。

### 抽象判定

引入接口 / 抽象类 / Locator 前，按以下三层逐级判定：

| 层级 | 判定 | 示例 |
|------|------|------|
| **1. 不需要抽象** | 无替换者、无用户场景 → 直接写具体类 | `InstallationChecker`、`ILanguageProvider`（已移除——无用户替换场景） |
| **2. 插件内部替换** | 插件内部有多个可互换实现，运行时自动选择 | `IOdinBridge`：核心零 Odin 依赖，`DefaultOdinBridge` 降级；Integration 加载时 `OdinBridgeInitializer` 自动注入 `OdinInspectorBridge` |
| **3. 用户可替换** | 允许用户替换时，**必须提供不改源码的注入路径** | `IAssemblyFilter`：用户可注入自定义过滤规则；`ScriptDocGeneratorController`：`[SerializeField]` 工厂字段 |

**注入路径的两种形式**：

| 形式 | 适用场景 | 示例 |
|------|----------|------|
| **Inspector 可配字段** | 运行时 ScriptableObject / MonoBehaviour，用户在 Inspector 中指定实现 | `[SerializeField] DefaultAnalysisDataFactory analysisDataFactory` |
| **静态 Locator + `set` 访问器** | 静态服务，用户通过 `[InitializeOnLoad]` / 静态构造函数注入自定义实现 | `OdinBridgeLocator.Bridge` — `OdinBridgeInitializer` 通过 `[InitializeOnLoad]` 注入 |

### SRP — 单一职责

| 规则 | 说明 |
|------|------|
| **一类一职责** | 若 `[Summary]` 需用"和"连接多个职责，审视是否应拆分；但若各部分不会独立复用且职责相近，保持高内聚 |
| **数据与渲染分离** | Attribute / Data 与 Drawer / Renderer / Processor 各自独立（Odin 生态惯例） |
| **工具类单一领域** | 每个 `XxxUtility` 只服务一个领域 |
| **复杂类提取子组件** | 优先提取为内部子组件（参见 `IAttributeComponentRenderer`），而非拆分为继承链 |
| **内聚优先** | 步骤对外不可见且不被独立替换时，保持内聚比拆分更清晰 |

### OCP — 开闭原则

| 规则 | 说明 |
|------|------|
| **为已知扩展者开放** | 有明确替换实现或用户扩展场景时，提供扩展点 |
| **为内聚封闭** | 无扩展场景的内部逻辑直接写具体类 |
| **通过新增代码扩展** | 优先新增文件/类，而非修改稳定代码 |

### LSP — 里氏替换

| 规则 | 说明 |
|------|------|
| **契约一致** | 子类覆写行为须与基类契约一致 |
| **降级不是失败** | `DefaultOdinBridge.IsAvailable => false`，但 `GetFriendlyName` 仍返回语义正确的结果 |
| **禁止 NotImplementedException** | 抽象成员在具体子类中必须有完整实现 |

### ISP — 接口隔离

| 规则 | 说明 |
|------|------|
| **单一消费者 → 合并** | 只有一个消费者的接口，拆分无收益（如 `IAttributeComponentRenderer` 4 方法但 1 消费者） |
| **多消费者 → 按角色拆分** | 不同消费者只需部分方法时，按角色拆分小接口 |
| **5+ 方法审视** | 超过 5 个方法的接口应考虑按消费者拆分 |

### DIP — 依赖倒置

| 规则 | 说明 |
|------|------|
| **跨程序集边界必须** | 核心不能引用 Integration 程序集 → `IOdinBridge` + `OdinBridgeLocator` 逆转依赖 |
| **同程序集内不必** | 调用者和实现在同一程序集，直接引用具体类 |
| **引入第三方必须** | 核心不能依赖 Odin API → 通过 `IOdinBridge` 隔离 |

### 已落地模式

| 模式 | SOLID | 存在理由 | 位置 |
|------|-------|----------|------|
| `IOdinBridge` + `DefaultOdinBridge` / `OdinInspectorBridge` + `OdinBridgeLocator` | OCP / DIP | 核心程序集零 Odin 依赖，是架构红线 | `Runtime/Unity/OdinBridge/` |
| `AbstractAttributeData` | OCP / SRP | 50+ 面板共享渲染骨架，只替换数据绑定 | `Editor/Odin Integration/AttributeOverviewPro/Abstract/` |
| `AbstractAttributePanelSO` | OCP / SRP | 渲染流程固定，面板间只替换数据；提取子组件防止主类膨胀 | `Editor/Odin Integration/AttributeOverviewPro/Abstract/` |
| `IAttributeComponentRenderer` | SRP / ISP | 主面板类膨胀至 300+ 行，提取为子组件各司其职 | `Editor/Odin Integration/AttributeOverviewPro/Abstract/` |
| `IAssemblyFilter` | ISP / OCP | 用户需过滤自己项目的程序集，可注入自定义规则 | `Editor/Odin Integration/MiniTools/MenuItemViewer/` |
| `IAesirInspectorReset` | ISP | Preferences 等配置类只需一个重置能力 | `Runtime/Unity/Core/` |

## Key Rules

- `Internal_` 前缀：仅当私有/受保护/内部方法与公开方法重名时使用
- 版本号需在 `package.json` 和 `AesirInspectorVersion.cs` 两处同步
- `#if UNITY_EDITOR` 包裹编辑器专用代码

## Odin Inspector 集成规范

> 以下规范只针对 Aesir Inspector 插件内部。

- **核心程序集不允许直接引用 Odin API** — 通过 `IOdinBridge` 桥接（见"已落地模式"）
- Odin 依赖代码**必须**放在 `Odin Integration/` 子目录
- 优先使用 Odin Attribute 构建 UI，优先 `OdinAttributeProcessor` 动态添加特性
- 桥接方法使用 `OdinBridgeLocator.Bridge`；反射工具使用 `ReflectionUtility`
- Processor：无需 `[Summary]`，必须使用 `nameof` 获取字段名、属性名、方法名，禁止使用字符串硬编码。
- Attribute Drawer：继承 `OdinAttributeDrawer`，独立存于 `AttributeDrawers/` 目录，无需 `[Summary]`

### OVDF 规范

OVDF 是 Odin Visual Designer 的持久化格式，当前版本 **v1.1**。详细规范见 [.ai/ovdf-format.md](./ovdf-format.md)。

| 规则 | 说明 |
|------|------|
| **文件位置** | `Editor/Odin Integration/Visual Designer/`，Odin 扫描 Assets 下所有 `.ovdf` 自动发现 |
| **文件命名** | `{Namespace}_{TypeName}.ovdf` |
| **版本头** | 必须为 `OVDF v1.1` |
| **类型引用** | `{FullTypeName}, {AssemblyName}`，重命名时需同步更新文件头 |
| **字段引用** | 使用 C# 字段名（含 `[SerializeField]` 私有字段），重命名需 `[FormerlySerializedAs]` |
| **禁止手动编辑** | 仅用于类型/字段重命名后的同步修正 |
| **单例原则** | 同一类型只保留一份 OVDF |
| **变更后验证** | 在 Designer Files Overview (Tools → Odin → Inspector → Designer Files Overview) 中检查 |

## Coding Conventions

### 许可证前缀

MIT 许可证前缀仅放在 `Runtime/Unity/CodeStyle/AesirInspectorCodeStyle.cs`（包的唯一许可证锚点），其他脚本不含。

### 注释规范（自文档化 / 无注释范式）

| 规则 | 说明 |
|------|------|
| **禁止 XML 注释** | 不使用 `/// <summary>`、`/// <param>` 等 |
| **类必须 `[Summary]`** | 所有 class / struct / interface 必须具备，解释"为什么" |
| **复杂逻辑用 `[Summary]`** | 仅在命名无法充分表达意图时使用，解释"为什么"而非"做了什么" |
| **工具类 `[Summary]` 含场景** | 工具类的 `[Summary]` 需包含职责 + 典型使用场景 |
| **命名即文档** | 方法、字段、属性的命名应清晰传达意图，无需额外注释 |
| **唯一例外** | `AesirInspectorCodeStyle.cs` 作为风格示例文件，同时保留 XML 注释和 `[Summary]` |

**免除规范的模块**（展示/示例用途，用 `//` 补充即可）：
`Runtime/Unity/CodeStyle/`、`AttributeOverviewPro/Data/`、`AttributeOverviewPro/AttributePanels/`、`AttributeOverviewPro/UsageExamples/`

### 命名规范

| 标识符 | 规则 | 示例 |
|--------|------|------|
| 类、接口 | `PascalCase`，接口 `I` 前缀 | `PlayerManager`, `IDamageable` |
| 私有非序列化字段 | `_camelCase` | `_health` |
| 序列化字段 `[SerializeField]` | `camelCase` | `moveSpeed` |
| 常量 / 静态只读 | `PascalCase` | `MaxScore` |

### Property 与 GetXxx() 规范

| 场景 | 使用形式 |
|------|----------|
| 自身状态，无参数，O(1) | **Property** |
| 需要参数 / 有计算开销 / 依赖外部状态 | **GetXxx()** |

### Enum 规范

- 普通：含 `None = 0`，显式赋值
- Flags：`[Flags]`，值为 `1 << n`，复合用 `|`

### 事件规范

遵循 Unity 惯例：订阅/回调方法以 `On` 前缀命名（如 `OnTriggerEnter`、`OnEnable`）。术语使用 "raise"（.NET 官方建议，而非 "fire" / "trigger"）。

| 角色 | 命名 | 示例 |
|------|------|------|
| 事件 | 动词短语 / 过去式，无 `On` 前缀 | `DoorOpened` |
| 订阅方法 | `On` + 事件名 | `OnDoorOpened` |
| 触发方法 | `Raise` + 事件名 | `RaiseDoorOpened` |

### Utility 命名

| 类别 | 命名规则 | 目录 |
|------|----------|------|
| Runtime | `XxxUtility` | `Runtime/Unity/Utilities/` |
| Editor 安全封装 | `XxxSafeEditorUtility` | `Runtime/Unity/Utilities/` |
| Editor-Only | `XxxEditorUtility` | `Editor/Unity/` |

### #region 规范

仅在脚本**超过 100 行**时使用 `#region`，每个 region ≥ 5 行，命名需准确（如 `Internal`、`Serialization`，禁止 `Helper` 等泛化命名）。

## Menu Paths

- `Aesir Inspector → Getting Started` / `Attribute Overview Pro` / `Mini Tools` / `Extension Package Manager`
- `Assets 上下文菜单` — Quick Create SO, Summary Tool, Script Doc Generator

## Testing

Unity Test Runner → Edit Mode / Play Mode。测试程序集无 `ODIN_INSPECTOR` 约束。

## Documentation

- 用户文档 → `Documentation~/aesir-inspector.md`
- 开发者指南（架构、模块、ADR、任务指南）→ `.ai/development.md`
