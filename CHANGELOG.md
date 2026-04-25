# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

本项目所有重大变更都将记录在此文件中。

文件格式基于 Keep a Changelog，本项目遵循 语义化版本 规范。

---

## [0.3.0] - 2026-04-25

### Core

#### Added
- 新增菜单路径与优先级统一管理类 `AesirInspectorMenuItems`，统一管理 Tools 菜单和 Assets 上下文菜单 [`77f3b1b`](https://github.com/yuumixcode/aesir-inspector/commit/77f3b1b219eda197a67637c49c24c54ba4e8e0a5)
- 新增 Getting Started 窗口，展示版本号、功能列表和文档链接 [`77f3b1b`](https://github.com/yuumixcode/aesir-inspector/commit/77f3b1b219eda197a67637c49c24c54ba4e8e0a5)
- 新增 Preferences 偏好设置窗口，集成语言设置 [`77f3b1b`](https://github.com/yuumixcode/aesir-inspector/commit/77f3b1b219eda197a67637c49c24c54ba4e8e0a5)
- 新增 `AesirInspectorVersion` 版本信息静态类 [`77f3b1b`](https://github.com/yuumixcode/aesir-inspector/commit/77f3b1b219eda197a67637c49c24c54ba4e8e0a5)
- 新增 `IAesirInspectorReset` 重置接口及 `AesirInspectorResetAttributeProcessor`，为实现该接口的类自动添加右键重置菜单 [`77f3b1b`](https://github.com/yuumixcode/aesir-inspector/commit/77f3b1b219eda197a67637c49c24c54ba4e8e0a5)
- 新增代码语法高亮器 `AesirCodeHighlighter` [`77f3b1b`](https://github.com/yuumixcode/aesir-inspector/commit/77f3b1b219eda197a67637c49c24c54ba4e8e0a5)

#### Changed
- 静默安装检测日志输出（注释掉 `Debug.Log`） [`77f3b1b`](https://github.com/yuumixcode/aesir-inspector/commit/77f3b1b219eda197a67637c49c24c54ba4e8e0a5)
- 扩展 `AesirInspectorPaths` 路径常量，新增 AttributeOverview 和 MiniTools 路径 [`77f3b1b`](https://github.com/yuumixcode/aesir-inspector/commit/77f3b1b219eda197a67637c49c24c54ba4e8e0a5)
- 扩展 `AesirInspectorWebLinks` 链接常量，新增 GitHub 仓库、许可证、更新日志和 Odin Inspector 文档链接 [`77f3b1b`](https://github.com/yuumixcode/aesir-inspector/commit/77f3b1b219eda197a67637c49c24c54ba4e8e0a5)

### Bilingual

#### Added
- 新增 `ShowEnablePropertyAttribute` 复合特性 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 `HorizontalSeparateWidget` 水平分隔线 Inspector 组件 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)

#### Changed
- 重构 `HeaderBilingualWidget` [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)

### Utilities

#### Added
- 新增 `AesirInspectorLogger` 日志工具类 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 `PathUtility`、`PathSafeEditorUtility` 路径工具类 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 `ReflectionUtility` 反射工具类 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 `RegexUtility` 正则表达式工具类 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 `HierarchyUtility`、`HierarchySafeEditorUtility` Hierarchy 工具类 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 `MonoScriptSafeEditorUtility` MonoScript 工具类 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 `PlayerLoopUtility` PlayerLoop 工具类 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 `PredefinedAssemblyUtility` 预定义程序集工具类 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 `ProjectSafeEditorUtility` 项目安全编辑器工具类 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)

#### Changed
- 扩展 `ScriptableObjectSafeEditorUtility`，新增大量 ScriptableObject 编辑器操作方法 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 扩展 `OdinInspectorSafeEditorUtility` 和 `UrlUtility` [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)

### MiniTools

#### Added
- 新增 `AesirInspectorMiniToolsWindow` MiniTools 主窗口 [`b7068eb`](https://github.com/yuumixcode/aesir-inspector/commit/b7068eb0d1ab0a6cf7d7db9293ae874516a59d89)
- 新增 MenuItemViewer 菜单项检查器，支持 `IAssemblyFilter` 程序集过滤和 `ISearchFilterable` 搜索 [`b7068eb`](https://github.com/yuumixcode/aesir-inspector/commit/b7068eb0d1ab0a6cf7d7db9293ae874516a59d89)
- 新增 OdinSyntaxHighlighter 语法高亮处理器面板，委托 `AesirCodeHighlighter` 实现 [`b7068eb`](https://github.com/yuumixcode/aesir-inspector/commit/b7068eb0d1ab0a6cf7d7db9293ae874516a59d89)
- 新增 QuickCreateSO 右键快捷生成 ScriptableObject 工具，支持单选和多选批量创建 [`b7068eb`](https://github.com/yuumixcode/aesir-inspector/commit/b7068eb0d1ab0a6cf7d7db9293ae874516a59d89)

### ScriptDocGenerator

#### Added
- 新增文档生成器窗口和可视化面板 `ScriptableObject` 单例 [`c2f2e75`](https://github.com/yuumixcode/aesir-inspector/commit/c2f2e7503937cd2f4fae106938605464e512c332)
- 新增文档生成器逻辑控制类 `ScriptDocGeneratorController` [`c2f2e75`](https://github.com/yuumixcode/aesir-inspector/commit/c2f2e7503937cd2f4fae106938605464e512c332)
- 新增 Assets 上下文菜单项，支持添加脚本到 TargetType 或 TemporaryTypes [`c2f2e75`](https://github.com/yuumixcode/aesir-inspector/commit/c2f2e7503937cd2f4fae106938605464e512c332)
- 新增中文 Scripting API 配置和文档生成器设置 [`c2f2e75`](https://github.com/yuumixcode/aesir-inspector/commit/c2f2e7503937cd2f4fae106938605464e512c332)
- 新增完整的类型分析数据模型层：`MemberData`、`FieldData`、`PropertyData`、`MethodData`、`ConstructorData`、`EventData`、`ParameterData`、`TypeData` 及对应接口 [`c2f2e75`](https://github.com/yuumixcode/aesir-inspector/commit/c2f2e7503937cd2f4fae106938605464e512c332)
- 新增类型分析器静态扩展 `TypeAnalyzerStaticExtensions` 和工具类 `TypeAnalyzerUtility` [`c2f2e75`](https://github.com/yuumixcode/aesir-inspector/commit/c2f2e7503937cd2f4fae106938605464e512c332)
- 新增 `AccessModifierType`、`TypeCategory`、`ParameterDirection` 枚举 [`c2f2e75`](https://github.com/yuumixcode/aesir-inspector/commit/c2f2e7503937cd2f4fae106938605464e512c332)
- 新增 `DefaultAnalysisDataFactory`、`DefaultAttributeFilter`、`DerivedMemberDataComparer` 核心工具 [`c2f2e75`](https://github.com/yuumixcode/aesir-inspector/commit/c2f2e7503937cd2f4fae106938605464e512c332)
- 新增 `ReferenceLinkURLAttribute` 引用链接特性 [`c2f2e75`](https://github.com/yuumixcode/aesir-inspector/commit/c2f2e7503937cd2f4fae106938605464e512c332)

### AttributeOverview

#### Added
- 新增特性概览窗口 `AttributeOverviewWindow` 和数据库 `AttributeOverviewDatabaseSO` [`0e53a40`](https://github.com/yuumixcode/aesir-inspector/commit/0e53a40ef55c26dab037f3f6aaf11cc09fbc9dad)
- 新增面板抽象框架：泛型基类 `AttributeOverviewPanelSO<T>`、`AbstractAttributePanelSO` 及 Odin AttributeProcessor 自动配置 [`0e53a40`](https://github.com/yuumixcode/aesir-inspector/commit/0e53a40ef55c26dab037f3f6aaf11cc09fbc9dad)
- 新增 AssetList、AssetsOnly、CustomValueDrawer 三个内置特性面板 [`0e53a40`](https://github.com/yuumixcode/aesir-inspector/commit/0e53a40ef55c26dab037f3f6aaf11cc09fbc9dad)
- 新增 `AesirExampleAttribute`、`AttributeCategoryAttribute` 特性标记 [`0e53a40`](https://github.com/yuumixcode/aesir-inspector/commit/0e53a40ef55c26dab037f3f6aaf11cc09fbc9dad)
- 新增特性数据模型 `AbstractAttributeData`、`ParameterValue`、`ResolvedStringParameterValue`、`AttributeExamplePreviewItem` [`0e53a40`](https://github.com/yuumixcode/aesir-inspector/commit/0e53a40ef55c26dab037f3f6aaf11cc09fbc9dad)
- 新增 `AesirAttributeCategory` 分类枚举和 `OdinInspectorDocumentationLinks` 文档链接常量 [`0e53a40`](https://github.com/yuumixcode/aesir-inspector/commit/0e53a40ef55c26dab037f3f6aaf11cc09fbc9dad)
- 新增特性概览编辑器工具类和用法示例 [`0e53a40`](https://github.com/yuumixcode/aesir-inspector/commit/0e53a40ef55c26dab037f3f6aaf11cc09fbc9dad)

### SummaryTool

#### Added
- 新增 XML Summary 注释处理工具 `XmlSummaryTool`，支持 Sync/Replace/Remove 操作 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 `XmlCodePart` XML 代码段解析类 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 SummaryTool Assets 上下文菜单项 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)

### ExtensionManager

#### Added
- 新增扩展包管理器窗口 `ExtensionPackageManagerWindow`，支持 Git URL 安装 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 `ExtensionPackageCard` 扩展包卡片数据类 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 `PackageManagerEditorUtility` Package Manager 编辑器工具类 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)

### Samples

#### Added
- 新增 PluginConfigSolutions 示例模块，演示 ScriptableSingleton 在 Preferences 和 Project 中的使用方式 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)
- 新增 RuntimeInitializeLoadType 示例模块，演示五个初始化时机的执行顺序与最佳实践 [`2ac8573`](https://github.com/yuumixcode/aesir-inspector/commit/2ac85734743e115703d19fd5e548e85acf52c65a)

### Tests

#### Added
- 新增 ScriptDocGenerator 完整单元测试，覆盖构造方法、事件、字段、方法、属性、类型数据及成员继承 [`1cf6d6d`](https://github.com/yuumixcode/aesir-inspector/commit/1cf6d6ddd746628a03917783ca41cf378155f093)
- 新增 SummaryTool XML 注释处理测试 [`1cf6d6d`](https://github.com/yuumixcode/aesir-inspector/commit/1cf6d6ddd746628a03917783ca41cf378155f093)
- 新增 UnityEngine.Object 运算符重载 Runtime 测试 [`1cf6d6d`](https://github.com/yuumixcode/aesir-inspector/commit/1cf6d6ddd746628a03917783ca41cf378155f093)

#### Changed
- 为两个测试 asmdef 添加 `ODIN_INSPECTOR` 编译约束 [`1cf6d6d`](https://github.com/yuumixcode/aesir-inspector/commit/1cf6d6ddd746628a03917783ca41cf378155f093)

---

## [0.2.1] - 2026-04-23

### Added

- 新增 Aesir Inspector 安装方式检测功能 [`b7de538`](https://github.com/yuumixcode/aesir-inspector/commit/b7de538eaf24efdd5b96d2c26d7c2897a8e6a6b5)

---

## [0.2.0] - 2026-04-23

### Added

- 实现双语 Inspector 系统与核心基础设施 [`a2c750b`](https://github.com/yuumixcode/aesir-inspector/commit/a2c750b3e693f7a0d9d356e987a6fcf1ef8d59ea)
- 新增 Codely Skills Library 示例，包含 custom-package-creator 技能 [`9422695`](https://github.com/yuumixcode/aesir-inspector/commit/942269529be76eba7425dcbd1ffcd6fef4964ece)

---

## [0.1.0] - 2026-04-22

### Added

- Initial release.
