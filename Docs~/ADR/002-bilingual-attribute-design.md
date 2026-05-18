# ADR-002: 双语 Attribute + Drawer 分离设计

## Status

Accepted

## Context

Aesir Inspector 需要支持中英双语的 Inspector UI 显示。Odin Inspector 提供了强大的 Attribute + Drawer 机制。双语特性需要在 Attribute 中定义中英两套文本，在 Drawer 中根据当前语言设置选择渲染。

## Decision

采用 Attribute + Drawer 分离模式：

1. **Attribute**（在 `Runtime/Odin Integration/Attributes/`）：定义中英双语文本参数，如 `[BilingualTitle("标题", "Title")]`
2. **Drawer**（在 `Editor/Odin Integration/Drawers/`）：继承 `OdinAttributeDrawer<TAttribute>`，读取 `AesirInspectorLanguageSettingsSO` 当前语言，渲染对应文本
3. **Processor**（在 `Editor/Odin Integration/AttributeProcessors/`）：对不支持双语参数的 Odin 内置特性进行动态注入

Attribute 只承载数据，Drawer 负责渲染逻辑，Processor 负责动态注入。

## Consequences

- **优点**: 数据与渲染分离，符合 Unity Editor 扩展惯例
- **优点**: 新增双语特性只需创建 Attribute + Drawer 两个文件
- **缺点**: 每个双语特性需要三个文件（Attribute + Drawer + 可能的 Processor）
