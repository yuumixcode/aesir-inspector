# Modules

> Aesir Inspector 模块级文档。每个模块的职责、关键类型和 API 签名。

---

## Runtime/Unity/ — 核心运行时

### Attributes

**目录**: `Runtime/Unity/Attributes/`

| 类型 | 职责 |
|------|------|
| `SummaryAttribute` | 运行时可读注释特性，与 XML `<summary>` 双向同步 |

### Bilingualism

**目录**: `Runtime/Unity/Bilingualism/`

| 类型 | 职责 |
|------|------|
| `BilingualData` | 中英双语数据容器，`zh` / `en` 字段 |
| `AesirInspectorLanguageSettingsSO` | 语言设置 ScriptableObject，提供 `OnLanguageChanged` 事件 |

### Core

**目录**: `Runtime/Unity/Core/`

| 类型 | 职责 |
|------|------|
| `AesirInspectorVersion` | 版本号常量，需与 `package.json` 同步 |
| `AesirInspectorPaths` | 编辑器资源路径常量 |
| `AesirInspectorWebLinks` | 外部链接常量 |
| `IAesirInspectorReset` | 重置接口，所有 SO 面板和案例必须实现 |

### InspectorControls

**目录**: `Runtime/Unity/InspectorControls/`

| 类型 | 职责 |
|------|------|
| `BilingualDisplayAsStringControl` | 双语只读显示控件 |
| `BilingualHeaderControl` | 双语标题头控件 |
| `HorizontalSeparateControl` | 水平分隔线控件 |

### Logger

**目录**: `Runtime/Unity/Logger/`

| 类型 | 职责 |
|------|------|
| `AesirInspectorLogger` | 统一日志（彩色前缀、编译剔除、双击跳转） |
| `AesirInspectorLoggerSettings` | 日志级别配置 |

### OdinBridge

**目录**: `Runtime/Unity/OdinBridge/`

| 类型 | 职责 |
|------|------|
| `IOdinBridge` | Odin 可用性查询接口 |
| `DefaultOdinBridge` | 无 Odin 时的默认实现 |
| `OdinBridgeLocator` | 运行时自动定位 Odin 桥接实现 |

### ScriptDocGenerator (数据模型)

**目录**: `Runtime/Unity/ScriptDocGenerator/`

| 类型 | 职责 |
|------|------|
| `ITypeData` | 类型数据接口 |
| `MemberData` | 成员数据基类 |
| `FieldData` | 字段数据 |
| `PropertyData` | 属性数据 |
| `MethodData` | 方法数据 |
| `ConstructorData` | 构造函数数据 |
| `EventData` | 事件数据 |
| `ParameterData` | 参数数据 |
| `TypeData` | 完整类型信息 |

### Utilities

**目录**: `Runtime/Unity/Utilities/`

| 类型 | 职责 |
|------|------|
| `ScriptableObjectSafeEditorUtility` | SO 资产创建与管理（单例模式） |
| `MonoScriptSafeEditorUtility` | 按名称查找/选择 MonoScript |
| `PathUtility` / `PathSafeEditorUtility` | 路径规范化、安全创建目录 |
| `HierarchyUtility` / `HierarchySafeEditorUtility` | Transform/Hierarchy 路径操作 |
| `ProjectSafeEditorUtility` | Ping 并选中项目资源 |
| `UrlUtility` | URL 打开与外部链接 |
| `ReflectionUtility` | 程序集/命名空间反射 |
| `PredefinedAssemblyUtility` | 预定义程序集识别 |
| `PlayerLoopUtility` | PlayerLoop 子系统增删 |
| `RegexUtility` | 命名空间/类名规范化、邮箱/URL 校验 |

---

## Editor/Unity/ — 核心编辑器

### Core

**目录**: `Editor/Unity/Core/`

| 类型 | 职责 |
|------|------|
| `AesirInspectorInstallationChecker` | 检测安装方式（UPM / AssetFolder） |
| `AesirInspectorMenuItems` | 所有菜单路径常量 |

### MiniTools

**目录**: `Editor/Unity/MiniTools/`

| 类型 | 职责 |
|------|------|
| `QuickCreateSOMenuItem` | Quick Create SO 右键菜单 |

### SummaryTool

**目录**: `Editor/Unity/SummaryTool/`

| 类型 | 职责 |
|------|------|
| `XmlSummaryTool` | XML `<summary>` ↔ `[Summary]` 双向同步核心逻辑 |
| `XmlCodePart` | XML 代码解析工具 |
| `SummaryToolMenuItems` | 右键菜单入口 |

---

## Runtime/Odin Integration/ — Odin 运行时

### Attributes

**目录**: `Runtime/Odin Integration/Attributes/`

| 类型 | 职责 |
|------|------|
| `BilingualTitleAttribute` | 双语标题 |
| `BilingualBoxGroupAttribute` | 双语分组 |
| `BilingualButtonAttribute` | 双语按钮 |
| （共 6 个双语特性） | — |

### OdinCodeHighlighter

**目录**: `Runtime/Odin Integration/OdinCodeHighlighter.cs`

语法高亮器运行时数据。

---

## Editor/Odin Integration/ — Odin 编辑器

### AttributeOverviewPro

**目录**: `Editor/Odin Integration/AttributeOverviewPro/`

可搜索树形菜单展示 Odin/Aesir 特性，实时预览。核心架构：Data-Panel-Example 三件套。

### AttributeProcessors

**目录**: `Editor/Odin Integration/AttributeProcessors/`

OdinAttributeProcessor 实现，动态注入特性到面板类。

### Bridge

**目录**: `Editor/Odin Integration/Bridge/`

| 类型 | 职责 |
|------|------|
| `OdinInspectorBridge` | `IOdinBridge` 的 Odin 增强实现 |

### Drawers

**目录**: `Editor/Odin Integration/Drawers/`

双语 OdinAttributeDrawer 实现（6 个 Drawer）。

### ExtensionManager

**目录**: `Editor/Odin Integration/ExtensionManager/`

一键安装/移除推荐包（Git URL）。

### MiniTools

**目录**: `Editor/Odin Integration/MiniTools/`

| 功能 | 职责 |
|------|------|
| MenuItem Viewer | 菜单项浏览器 |
| Syntax Highlighter | 代码语法高亮 |

### ScriptDocGenerator

**目录**: `Editor/Odin Integration/ScriptDocGenerator/`

文档生成器编辑器逻辑（面板、窗口、控制器、菜单）。

### Windows

**目录**: `Editor/Odin Integration/Windows/`

| 类型 | 职责 |
|------|------|
| Getting Started | 欢迎窗口 |
| Preferences | 偏好设置窗口 |
