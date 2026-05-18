# Aesir Inspector — Project Context

## Project Overview

Aesir Inspector (`cn.runlab.aesir-inspector`) 是一个 Unity/Tuanjie 编辑器扩展库，提供双语 Inspector UI、安全编辑器工具集、脚本文档生成器、XML Summary 同步工具等功能。**可选集成 Odin Inspector** 以获得增强的 Inspector 渲染和样式优化。当前版本 **0.4.0-pre.1**，MIT 协议开源。

- **构建版本**: Unity 2022.3 或更高版本
- **核心依赖**: 无硬依赖（核心程序集不依赖 Odin Inspector）
- **可选依赖**: Odin Inspector（导入后自动启用 OdinIntegration 增强程序集）
- **仓库**: https://github.com/yuumixcode/aesir-inspector

## Package Identity

| Field        | Value                       |
|--------------|-----------------------------|
| Package Name | `cn.runlab.aesir-inspector` |
| Display Name | Aesir Inspector             |
| Version      | 0.4.0-pre.1                 |
| Category     | Inspector                   |
| License      | MIT                         |

## Assembly Definitions

本项目使用 6 个程序集，采用**核心 + Odin Integration 分离架构**：

| Asmdef                                      | Namespace                                   | Platforms   | References                                  | defineConstraints        |
|---------------------------------------------|---------------------------------------------|-------------|---------------------------------------------|--------------------------|
| `RunLab.AesirInspector`                     | `RunLab.AesirInspector`                     | Any         | (none)                                      | (none)                   |
| `RunLab.AesirInspector.Editor`              | `RunLab.AesirInspector.Editor`              | Editor only | Runtime                                     | (none)                   |
| `RunLab.AesirInspector.OdinIntegration`         | `RunLab.AesirInspector.OdinIntegration`         | Any         | Runtime                                     | `ODIN_INSPECTOR`         |
| `RunLab.AesirInspector.OdinIntegration.Editor`  | `RunLab.AesirInspector.OdinIntegration.Editor`  | Editor only | Runtime + Editor + OdinIntegration Runtime      | `ODIN_INSPECTOR`         |
| `RunLab.AesirInspector.Tests`               | `RunLab.AesirInspector.Tests`               | Any         | Runtime                                     | `UNITY_INCLUDE_TESTS`    |
| `RunLab.AesirInspector.Editor.Tests`        | `RunLab.AesirInspector.Editor.Tests`        | Editor only | Runtime + Editor                            | `UNITY_INCLUDE_TESTS`    |

**架构说明**：
- `RunLab.AesirInspector`（核心运行时）和 `RunLab.AesirInspector.Editor`（核心编辑器）**不含** `ODIN_INSPECTOR` 编译约束，无 Odin 时正常编译
- `RunLab.AesirInspector.OdinIntegration` / `.OdinIntegration.Editor` 设有 `ODIN_INSPECTOR` 约束，仅在安装 Odin Inspector 后参与编译
- 测试程序集移除了 `ODIN_INSPECTOR` 约束，确保无 Odin 环境下测试可运行

## Directory Structure

```
Aesir Inspector/
├── Runtime/                               # 标准运行时根目录
│   ├── Unity/                             # 核心基础层 (RunLab.AesirInspector)
│   │   ├── Attributes/                    # 自定义特性 ([Summary])
│   │   ├── Bilingualism/                  # 双语数据与语言设置
│   │   ├── CodeStyle/                     # 代码风格示例
│   │   ├── Core/                          # 核心类 (Version, Paths, WebLinks, IAesirInspectorReset)
│   │   ├── InspectorControls/             # Inspector Control 实现
│   │   ├── Logger/                        # 日志系统
│   │   ├── OdinBridge/                    # Odin 桥接层 (IOdinBridge, DefaultOdinBridge, OdinBridgeLocator)
│   │   ├── ScriptDocGenerator/            # 文档生成器运行时模型
│   │   └── Utilities/                     # 安全编辑器工具类
│   └── Odin Integration/                       # Odin 集成层 (RunLab.AesirInspector.OdinIntegration, ODIN_INSPECTOR)
│       ├── Attributes/                    # 双语特性
│       └── OdinCodeHighlighter.cs
├── Editor/                                # 标准编辑器根目录
│   ├── Unity/                             # 核心基础层 (RunLab.AesirInspector.Editor)
│   │   ├── Core/                          # 编辑器核心 (安装检测、菜单管理)
│   │   ├── MiniTools/                     # QuickCreateSO
│   │   └── SummaryTool/                   # XML Summary 同步工具
│   └── Odin Integration/                       # Odin 集成层 (RunLab.AesirInspector.OdinIntegration.Editor, ODIN_INSPECTOR)
│       ├── AttributeOverviewPro/          # 特性总览窗口
│       ├── AttributeProcessors/           # OdinAttributeProcessor 实现
│       ├── Bridge/                        # OdinInspectorBridge 编辑器侧桥接
│       ├── Drawers/                       # Odin AttributeDrawer 实现
│       ├── ExtensionManager/              # 扩展包管理器
│       ├── MiniTools/                     # 迷你工具集
│       ├── ScriptDocGenerator/            # 文档生成器编辑器逻辑
│       └── Windows/                       # Getting Started, Preferences 窗口
├── Tests/
│   ├── Editor/                            # Edit-Mode 测试
│   └── Runtime/                           # Runtime 测试
├── Samples~/                              # 包管理器示例（Unity 忽略 tilde 目录）
│   ├── PluginConfigSolutions/             # ScriptableSingleton 配置示例
│   └── RuntimeInitializeLoadType/         # 初始化时机示例
├── Documentation~/                        # Unity 标准用户文档
├── Docs~/                                 # AI Agent 冷记忆层
│   ├── ARCHITECTURE.md                    # 系统架构 & C4 模型
│   ├── CONVENTIONS.md                     # 代码风格完整规范
│   ├── MODULES.md                         # 模块级 API 文档
│   ├── ADR/                               # 架构决策记录
│   └── SKILLS/                            # 任务专项技能指南
├── AGENTS.md                              # AI Agent 通用入口 (Hot Memory)
├── package.json
├── README.md / README_EN.md
├── CHANGELOG.md / CHANGELOG_EN.md
├── LICENSE.md
└── Third Party Notices.md
```

## Core Features (Modules)

| # | Module                    | Location                                                    | Description                                                                        |
|---|---------------------------|-------------------------------------------------------------|------------------------------------------------------------------------------------|
| 1 | Code Style & Standards    | `Runtime/Unity/CodeStyle/`                                  | 代码风格规范与示例，详见 `AESIR_INSPECTOR_CODE_STYLE.cs`                                   |
| 2 | Bilingualism              | `Runtime/Unity/Bilingualism/`, `Runtime/Odin Integration/Attributes/`, `Editor/Odin Integration/Drawers/`, `Editor/Odin Integration/AttributeProcessors/` | 双语数据、特性、Drawer、Processor，支持中英双语 Inspector 显示                                |
| 3 | OdinBridge                | `Runtime/Unity/OdinBridge/`, `Editor/Odin Integration/Bridge/`   | Odin 可选集成桥接层，无 Odin 时自动回退默认实现                                                |
| 4 | Inspector Controls        | `Runtime/Unity/InspectorControls/`                         | Inspector 控件实现 (BilingualDisplayAsStringControl, BilingualHeaderControl, HorizontalSeparateControl) |
| 5 | Custom Attributes         | `Runtime/Unity/Attributes/`                                | `[Summary]` (运行时可读注释)                                                            |
| 6 | Script Doc Generator      | `Runtime/Unity/ScriptDocGenerator/`, `Editor/Odin Integration/ScriptDocGenerator/` | 反射生成 API 文档，增量更新，AI 友好 Markdown 输出                                           |
| 7 | Summary Tool              | `Editor/Unity/SummaryTool/`                                 | 右键菜单 XML `<summary>` ↔ `[Summary]` 双向同步 (Sync/Replace/Remove)                   |
| 8 | Mini Tools                | `Editor/Unity/MiniTools/`, `Editor/Odin Integration/MiniTools/` | QuickCreate SO (核心), MenuItem Viewer & Syntax Highlighter (Odin Integration)           |
| 9 | Attribute Overview Pro    | `Editor/Odin Integration/AttributeOverviewPro/`                  | 可搜索树形菜单展示 Odin/Aesir 特性，实时预览                                                    |
| 10 | Extension Package Manager | `Editor/Odin Integration/ExtensionManager/`                      | 一键安装/移除推荐包 (Git URL)                                                            |

## OdinBridge Architecture

OdinBridge 是 Odin Inspector 可选集成的核心机制，使核心程序集不依赖 Odin，同时允许 OdinIntegration 程序集在 Odin 可用时提供增强功能：

```
┌─────────────────────────────────────────────┐
│ Runtime/Unity (RunLab.AesirInspector)       │
│                                              │
│  IOdinBridge ←── OdinBridgeLocator ──────┐  │
│       │                                  │  │
│  DefaultOdinBridge (无 Odin 时使用)      │  │
└──────────────────────────────────────────┼──┘
                                           │
┌──────────────────────────────────────────┼──┐
│ Editor/Odin Integration (ODIN_INSPECTOR)      │  │
│                                          │  │
│  OdinInspectorBridge ────────────────────┘  │  (implements IOdinBridge)
│  OdinAttributeProcessors                    │
│  Odin Drawers                               │
└─────────────────────────────────────────────┘
```

- `IOdinBridge`：定义 Odin 可用性查询接口
- `DefaultOdinBridge`：无 Odin 时的默认实现
- `OdinBridgeLocator`：运行时自动定位 Odin 桥接实现，无 Odin 时回退至 `DefaultOdinBridge`
- `OdinInspectorBridge`：Odin 可用时提供的编辑器侧增强桥接

## Development Conventions

### 代码风格

- **Unity Null 检查**: 严禁对 `UnityEngine.Object` 派生类使用 `?.` 或 `??`
- **私有方法**: 逻辑上对应公开方法的私有方法，增加 `Internal_` 前缀
- **条件编译**: `#if UNITY_EDITOR` 包裹编辑器专用代码
- **详尽规范**: 参见 `Runtime/Unity/CodeStyle/AESIR_INSPECTOR_CODE_STYLE.cs`

### Odin Inspector 集成规范

1. Odin 依赖代码**必须**放在 `Odin Integration/` 子目录下，核心程序集（`Runtime/Unity/`、`Editor/Unity/`）不允许直接引用 Odin API
2. 需要在运行时查询 Odin 可用性时，通过 `OdinBridgeLocator` 获取 `IOdinBridge` 实例
3. OdinAttributeProcessor 放在 `Editor/Odin Integration/AttributeProcessors/` 目录
4. OdinAttributeDrawer 放在 `Editor/Odin Integration/Drawers/` 目录
5. Processor 需访问私有成员时，定义为目标的嵌套类（internal 修饰符）
6. OdinAttributeProcessor 继承类**无需** XML 注释和 `[Summary]`

### 程序集依赖规则

- `RunLab.AesirInspector` → 无外部依赖
- `RunLab.AesirInspector.Editor` → Runtime
- `RunLab.AesirInspector.OdinIntegration` → Runtime（`ODIN_INSPECTOR` 约束）
- `RunLab.AesirInspector.OdinIntegration.Editor` → Runtime + Editor + OdinIntegration Runtime（`ODIN_INSPECTOR` 约束）

## Menu Paths

所有菜单入口统一由 `AesirInspectorMenuItems` 管理：

- `Aesir Inspector → Getting Started` — 欢迎窗口
- `Aesir Inspector → Attribute Overview Pro` — 特性总览
- `Aesir Inspector → Mini Tools` — 迷你工具集
- `Aesir Inspector → Extension Package Manager` — 扩展包管理器
- `Assets 上下文菜单` — Quick Create SO, Summary Tool, Script Doc Generator

## Installation Modes

`AesirInspectorInstallationChecker` 自动检测安装方式：

| Mode          | Property        | Description                                 |
|---------------|-----------------|---------------------------------------------|
| `Upm`         | `IsUpm`         | Package Manager / Git URL 安装                |
| `AssetFolder` | `IsAssetFolder` | Assets 目录直接放置 (Asset Store / Git submodule) |

## Testing

- **Framework**: NUnit (Unity Test Framework)
- **Edit-Mode Tests**: `Tests/Editor/` — ScriptDocGenerator + SummaryTool
- **Runtime Tests**: `Tests/Runtime/` — UnityEngine.Object 运算符重载
- **运行方式**: Unity Test Runner → Edit Mode / Play Mode
- 测试程序集无 `ODIN_INSPECTOR` 约束，确保无 Odin 环境下可运行

## Key Utility Classes

### Runtime/Unity/Utilities/

| Class                                             | Purpose                  |
|---------------------------------------------------|--------------------------|
| `ScriptableObjectSafeEditorUtility`               | SO 资产创建与管理               |
| `MonoScriptSafeEditorUtility`                     | 按名称查找/选择 MonoScript      |
| `PathUtility` / `PathSafeEditorUtility`           | 路径规范化、子路径、安全创建目录         |
| `HierarchyUtility` / `HierarchySafeEditorUtility`  | Transform/Hierarchy 路径操作 |
| `ProjectSafeEditorUtility`                        | Ping 并选中项目资源             |
| `UrlUtility`                                      | URL 打开与外部链接              |
| `ReflectionUtility`                               | 程序集/命名空间反射               |
| `PredefinedAssemblyUtility`                       | 预定义程序集识别                 |
| `PlayerLoopUtility`                               | PlayerLoop 子系统增删         |
| `RegexUtility`                                    | 命名空间/类名规范化、邮箱/URL 校验     |

### Runtime/Unity/Logger/

| Class                          | Purpose                                              |
|--------------------------------|------------------------------------------------------|
| `AesirInspectorLogger`         | 统一日志（彩色前缀、编译剔除、双击跳转）                               |
| `AesirInspectorLoggerSettings` | 日志级别配置（enableInfoLog / enableWarningLog）            |

### Runtime/Unity/OdinBridge/

| Class                | Purpose                                    |
|----------------------|--------------------------------------------|
| `IOdinBridge`        | Odin 可用性查询接口                            |
| `DefaultOdinBridge`  | 无 Odin 时的默认桥接实现                        |
| `OdinBridgeLocator`  | 自动定位 Odin 桥接，无 Odin 时回退 DefaultOdinBridge |

## Samples

| Sample                    | Path                                          | Description                       |
|---------------------------|-----------------------------------------------|-----------------------------------|
| Plugin Config Solutions   | `Samples~/PluginConfigSolutions/`            | ScriptableSingleton 配置持久化示例     |
| RuntimeInitializeLoadType | `Samples~/RuntimeInitializeLoadType/`          | 5 个初始化时机的执行顺序示例             |

## Version Control

`Library/`、`Temp/`、`obj/`、`Build/` 应在项目级 `.gitignore` 中排除。

## Important Notes

- **核心程序集不依赖 Odin Inspector** — `RunLab.AesirInspector` 和 `RunLab.AesirInspector.Editor` 可在无 Odin 环境下正常编译和运行。
- **OdinIntegration 程序集需要 `ODIN_INSPECTOR` 编译符号** — 安装 Odin Inspector 后自动启用，提供双语特性、Drawer、Processor 等增强功能。
- 版本号需在 `package.json` 和 `AesirInspectorVersion.cs` 两处同步维护。
- 本项目运行在 Tuanjie 引擎（Unity 2022.3 分支），场景文件扩展名为 `.scene`。
- **AI Agent 文档体系** — `AGENTS.md` 为 Hot Memory 入口，`Docs~/` 为 Cold Memory 冷记忆层，详见 `Docs~/ARCHITECTURE.md`。

## Codely Added Memories
- Aesir Inspector renamed OdinWrapper → Odin Integration (directory) / OdinIntegration (namespace & assembly). Directory: `Runtime/Odin Integration/`, `Editor/Odin Integration/`. Namespace: `RunLab.AesirInspector.OdinIntegration` / `.OdinIntegration.Editor`. Assembly: same as namespace.
