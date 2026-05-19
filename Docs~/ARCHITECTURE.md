# Architecture

> Aesir Inspector 系统架构文档。基于 C4 模型，面向 AI Agent 设计。

## C1 — System Context

```mermaid
graph TB
    Dev[开发者] --> AICode[AI Coding Agent]
    AICode --> AI[Aesir Inspector<br/>Agent Context Layer]
    Dev --> Editor[Tuanjie Editor]
    Editor --> PKG[Aesir Inspector Package]
    PKG --> Odin[Odin Inspector<br/>Optional Dependency]
    PKG --> Unity[Unity/Tuanjie API]
```

**外部系统**：

| 系统 | 关系 | 说明 |
|------|------|------|
| Tuanjie Editor | 运行平台 | Unity 2022.3 分支，场景文件 `.scene` |
| Odin Inspector | 可选依赖 | 安装后自动启用 OdinIntegration 程序集 |
| AI Coding Agents | 消费者 | 读取 AGENTS.md + Docs~/ 获取项目上下文 |

## C2 — Container

```mermaid
graph TB
    subgraph "Aesir Inspector Package"
        subgraph Runtime["Runtime/ (运行时)"]
            RTCore["Unity/<br/>核心运行时"]
            RTOdin["Odin Integration/<br/>Odin 运行时"]
        end
        subgraph Editor["Editor/ (编辑器)"]
            EdCore["Unity/<br/>核心编辑器"]
            EdOdin["Odin Integration/<br/>Odin 编辑器"]
        end
        Tests["Tests/"]
        Samples["Samples~/"]
    end

    RTCore -->|"引用"| RTOdin
    RTOdin -->|"ODIN_INSPECTOR 约束"| Odin
    EdCore -->|"引用"| RTCore
    EdOdin -->|"引用"| RTCore
    EdOdin -->|"引用"| EdCore
    EdOdin -->|"引用"| RTOdin
    EdOdin -->|"ODIN_INSPECTOR 约束"| Odin
```

**程序集依赖图**：

```mermaid
graph LR
    RT[RunLab.AesirInspector] -->|"无依赖"| None[ ]
    ED[RunLab.AesirInspector.Editor] -->|引用| RT
    OW[RunLab.AesirInspector.OdinIntegration] -->|引用| RT
    OWE[...Odin Integration.Editor] -->|引用| RT
    OWE -->|引用| ED
    OWE -->|引用| OW
    OW -.->|ODIN_INSPECTOR| Odin
    OWE -.->|ODIN_INSPECTOR| Odin
```

## C3 — Component

### Runtime/Unity/ 核心运行时

| 组件 | 目录 | 职责 |
|------|------|------|
| Attributes | `Attributes/` | 自定义特性 (`[Summary]`) |
| CodeStyle | `CodeStyle/` | 代码风格示例文件 |
| Core | `Core/` | 版本、路径、Web 链接、重置接口 |
| Inspector | `Inspector/` | Inspector 显示模型 |
| Localization | `Localization/` | 本地化数据与语言设置 |
| Logging | `Logging/` | 统一日志系统 |
| OdinBridge | `OdinBridge/` | Odin 可用性查询桥接 |
| ScriptDocGenerator | `ScriptDocGenerator/` | 文档生成器数据模型 |
| Utilities | `Utilities/` | 安全编辑器工具集 |

### Editor/Unity/ 核心编辑器

| 组件 | 目录 | 职责 |
|------|------|------|
| Core | `Core/` | 安装检测、菜单管理 |
| MiniTools | `MiniTools/` | QuickCreate SO 等迷你工具 |
| SummaryTool | `SummaryTool/` | XML Summary ↔ `[Summary]` 双向同步 |

### Runtime/Odin Integration/ Odin 运行时

| 组件 | 目录 | 职责 |
|------|------|------|
| Attributes | `Attributes/` | 双语 Odin 特性 |
| OdinCodeHighlighter | `OdinCodeHighlighter.cs` | 代码语法高亮 |

### Editor/Odin Integration/ Odin 编辑器

| 组件 | 目录 | 职责 |
|------|------|------|
| AttributeOverviewPro | `AttributeOverviewPro/` | 特性总览窗口 |
| AttributeProcessors | `AttributeProcessors/` | Odin 动态特性注入 |
| Bridge | `Bridge/` | OdinInspectorBridge 实现 |
| Drawers | `Drawers/` | 双语 Drawer 实现 |
| ExtensionManager | `ExtensionManager/` | 扩展包管理器 |
| MiniTools | `MiniTools/` | MenuItem Viewer, Syntax Highlighter |
| ScriptDocGenerator | `ScriptDocGenerator/` | 文档生成器编辑器逻辑 |
| Windows | `Windows/` | Getting Started, Preferences 窗口 |

## OdinBridge 数据流

```mermaid
sequenceDiagram
    participant Code as 核心代码
    participant Locator as OdinBridgeLocator
    participant Default as DefaultOdinBridge
    participant OdinBridge as OdinInspectorBridge

    Code->>Locator: OdinBridgeLocator.Bridge
    Locator->>Locator: 搜索 IOdinBridge 实现

    alt Odin 已安装
        Locator->>OdinBridge: 返回 OdinInspectorBridge 实例
        OdinBridge-->>Code: IsOdinInstalled = true
    else Odin 未安装
        Locator->>Default: 返回 DefaultOdinBridge 实例
        Default-->>Code: IsOdinInstalled = false
    end
```

## 双语系统数据流

```mermaid
sequenceDiagram
    participant SO as LanguageSettingsSO
    participant Data as BilingualData
    participant Attr as 双语 Attribute
    participant Drawer as 双语 Drawer
    participant UI as Inspector UI

    SO->>Data: 当前语言 (zh/en)
    Attr->>Data: 读取标签文本
    Attr->>Drawer: 特性参数
    Drawer->>UI: 渲染当前语言标签
    SO->>Drawer: OnLanguageChanged 事件
    Drawer->>UI: 重绘 UI
```
