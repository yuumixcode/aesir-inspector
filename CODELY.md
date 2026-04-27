# Aesir Inspector — Project Context

## Project Overview

Aesir Inspector (`cn.runlab.aesir-inspector`) 是一个基于 **Odin Inspector** 的 Unity/Tuanjie 编辑器扩展库，提供双语
Inspector UI、安全编辑器工具集、脚本文档生成器、XML Summary 同步工具等功能。当前版本 **0.3.1**，MIT 协议开源。

- **构建版本**: Unity 2022.3 或更高版本
- **核心依赖**: Odin Inspector (最新版)（通过 `defineConstraints: ODIN_INSPECTOR` 编译约束实现，非硬依赖 —
  缺少时程序集不编译但不报错）
- **仓库**: https://github.com/yuumixcode/aesir-inspector

## Package Identity

| Field        | Value                       |
|--------------|-----------------------------|
| Package Name | `cn.runlab.aesir-inspector` |
| Display Name | Aesir Inspector             |
| Version      | 0.3.1                       |
| Category     | Inspector                   |
| License      | MIT                         |

## Assembly Definitions

本项目使用 4 个程序集，**全部**设有 `defineConstraints: ["ODIN_INSPECTOR"]`，即 Odin Inspector 未安装时不会参与编译：

| Asmdef                               | Namespace                            | Platforms   | References       |
|--------------------------------------|--------------------------------------|-------------|------------------|
| `RunLab.AesirInspector`              | `RunLab.AesirInspector`              | Any         | (none)           |
| `RunLab.AesirInspector.Editor`       | `RunLab.AesirInspector.Editor`       | Editor only | Runtime          |
| `RunLab.AesirInspector.Tests`        | `RunLab.AesirInspector.Tests`        | Any         | Runtime          |
| `RunLab.AesirInspector.Editor.Tests` | `RunLab.AesirInspector.Editor.Tests` | Editor only | Runtime + Editor |

测试程序集额外约束 `UNITY_INCLUDE_TESTS`。

## Directory Structure

```
Aesir Inspector/
├── Runtime/                          # 运行时代码 (RunLab.AesirInspector)
│   ├── Attributes/                   # 自定义特性 ([Summary], [ShowEnableProperty])
│   ├── Bilingual/                    # 双语特性与 Widget
│   ├── CodeStyle/                    # 代码风格示例 AESIR_INSPECTOR_CODE_STYLE.cs
│   ├── Core/                         # 核心类 (Version, Paths, WebLinks, Logger, IAesirInspectorReset)
│   ├── InspectorWidgets/             # Inspector Widget 实现
│   ├── ScriptDocGenerator/           # 文档生成器运行时模型 (ITypeData, MemberData 等)
│   └── Utilities/                    # 安全编辑器工具类
├── Editor/                           # 编辑器代码 (RunLab.AesirInspector.Editor)
│   ├── AttributeOverviewPro/         # 特性总览窗口
│   ├── Core/                         # 编辑器核心 (安装检测、菜单管理、高亮器、窗口)
│   ├── Drawers/                      # Odin AttributeDrawer 实现
│   ├── ExtensionManager/             # 扩展包管理器
│   ├── Guidelines/                   # 编辑器指引/规范
│   ├── MiniTools/                    # 迷你工具集 (MenuItem Viewer, Syntax Highlighter, Quick Create SO)
│   ├── ScriptDocGenerator/           # 文档生成器编辑器逻辑 (Controller, SettingsSO)
│   └── SummaryTool/                  # XML Summary 同步工具
├── Tests/
│   ├── Editor/                       # Edit-Mode 测试 (ScriptDocGenerator 153 个 + SummaryTool)
│   └── Runtime/                      # Runtime 测试 (UnityEngine.Object 运算符重载)
├── Samples~/                         # 包管理器示例 (隐藏目录)
│   ├── Codely Skills Library/        # Codely CLI Skills 案例库
│   ├── Plugin Config Solutions/      # ScriptableSingleton 配置示例
│   └── RuntimeInitializeLoadType/    # 初始化时机示例
├── package.json
├── README.md / README_EN.md
├── CHANGELOG.md / CHANGELOG_EN.md
├── LICENSE.md
└── Third Party Notices.md
```

## Core Features (Modules)

| # | Module                    | Location                                                    | Description                                                                        |
|---|---------------------------|-------------------------------------------------------------|------------------------------------------------------------------------------------|
| 1 | Code Style & Standards | `Runtime/CodeStyle/` | 代码风格规范与示例，详见 `AESIR_INSPECTOR_CODE_STYLE.cs` |
| 2 | Bilingual Attributes      | `Runtime/Bilingual/`, `Editor/Drawers/`                     | `[BilingualTitle]`, `[BilingualBoxGroup]`, `[BilingualButton]` 等中英双语 Inspector 装饰器 |
| 3 | Safe Editor Utilities     | `Runtime/Utilities/`                                        | Odin API 安全桥接，确保无 Odin 时编译通过、打包自动剔除                                                |
| 4 | Custom Attributes         | `Runtime/Attributes/`                                       | `[Summary]` (运行时可读注释), `[ShowEnableProperty]`                                      |
| 5 | Script Doc Generator      | `Runtime/ScriptDocGenerator/`, `Editor/ScriptDocGenerator/` | 反射生成 API 文档，增量更新，AI 友好 Markdown 输出                                                 |
| 6 | Summary Tool              | `Editor/SummaryTool/`                                       | 右键菜单 XML `<summary>` ↔ `[Summary]` 双向同步 (Sync/Replace/Remove)                      |
| 7 | Mini Tools                | `Editor/MiniTools/`                                         | MenuItem Viewer, Syntax Highlighter, Quick Create SO                               |
| 8 | Attribute Overview Pro    | `Editor/AttributeOverviewPro/`                              | 可搜索树形菜单展示 Odin/Aesir 特性，实时预览                                                       |
| 9 | Extension Package Manager | `Editor/ExtensionManager/`                                  | 一键安装/移除推荐包 (Git URL)                                                               |

## Development Conventions

### 代码风格

- **Unity Null 检查**: 严禁对 `UnityEngine.Object` 派生类使用 `?.` 或 `??`
- **私有方法**: 逻辑上对应公开方法的私有方法，增加 `Internal_` 前缀
- **条件编译**: `#if UNITY_EDITOR` 包裹编辑器专用代码；`ODIN_INSPECTOR` 是核心编译约束
- **详尽规范**: 参见 `Runtime/CodeStyle/AESIR_INSPECTOR_CODE_STYLE.cs`

### Odin Inspector 集成规范

1. 优先使用 Odin Attribute 构建 UI，而非原始 Editor 代码；优先使用 OdinAttributeProcessor 动态注入特性
2. `OdinInspectorSafeEditorUtility` 是 Odin API 的安全桥接类，保留宏定义约束
3. **Processor 必须与对应 Attribute/类在同一脚本文件中**；Drawer 仍在 `Editor/Drawers/` 目录
4. Processor 需访问私有成员时，定义为目标的嵌套类（internal 修饰符）
5. OdinAttributeProcessor 继承类**无需** XML 注释和 `[Summary]`

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
- **Edit-Mode Tests**: `Tests/Editor/` — ScriptDocGenerator (153 个) + SummaryTool
- **Runtime Tests**: `Tests/Runtime/` — UnityEngine.Object 运算符重载
- **运行方式**: Unity Test Runner → Edit Mode / Play Mode

## Key Utility Classes (Runtime/Utilities/)

| Class                                             | Purpose                  |
|---------------------------------------------------|--------------------------|
| `OdinInspectorSafeEditorUtility`                  | Odin API 安全调用桥接          |
| `ScriptableObjectSafeEditorUtility`               | SO 资产创建与管理               |
| `MonoScriptSafeEditorUtility`                     | 按名称查找/选择 MonoScript      |
| `PathUtility` / `PathSafeEditorUtility`           | 路径规范化、子路径、安全创建目录         |
| `HierarchyUtility` / `HierarchySafeEditorUtility` | Transform/Hierarchy 路径操作 |
| `ProjectSafeEditorUtility`                        | Ping 并选中项目资源             |
| `UrlUtility`                                      | URL 打开与外部链接              |
| `ReflectionUtility`                               | 程序集/命名空间反射               |
| `PredefinedAssemblyUtility`                       | 预定义程序集识别                 |
| `PlayerLoopUtility`                               | PlayerLoop 子系统增删         |
| `RegexUtility`                                    | 命名空间/类名规范化、邮箱/URL 校验     |
| `AesirInspectorLogger`                            | 统一日志（彩色前缀、编译剔除、双击跳转）     |

## Samples

| Sample                    | Path                                  | Description                                   |
|---------------------------|---------------------------------------|-----------------------------------------------|
| Codely Skills Library     | `Samples~/Codely Skills Library/`     | Codely CLI Skills 案例 (custom-package-creator) |
| Plugin Config Solutions   | `Samples~/PluginConfigSolutions/`     | ScriptableSingleton 配置持久化示例                   |
| RuntimeInitializeLoadType | `Samples~/RuntimeInitializeLoadType/` | 5 个初始化时机的执行顺序示例                               |

## Version Control

`Library/`、`Temp/`、`obj/`、`Build/` 应在项目级 `.gitignore` 中排除。

## Important Notes

- **ODIN_INSPECTOR 编译符号是硬性前提** — 没有它所有 4 个程序集都不会编译。Odin Inspector 导入后自动添加此符号。
- 版本号需在 `package.json` 和 `AesirInspectorVersion.cs` 两处同步维护。
- 本项目运行在 Tuanjie 引擎（Unity 2022.3 分支），场景文件扩展名为 `.scene`。
