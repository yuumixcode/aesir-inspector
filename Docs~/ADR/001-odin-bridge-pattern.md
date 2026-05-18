# ADR-001: OdinBridge 分离模式

## Status

Accepted

## Context

Aesir Inspector 需要在 Odin Inspector 可用时提供增强功能（双语特性、Drawer、Processor 等），但在无 Odin 环境下也必须正常编译和运行。核心程序集不能直接引用 Odin API，否则会在无 Odin 的项目中导致编译失败。

## Decision

采用 OdinBridge 分离模式：

1. 核心运行时程序集 (`RunLab.AesirInspector`) 定义 `IOdinBridge` 接口
2. 无 Odin 时，`DefaultOdinBridge` 提供默认实现
3. 有 Odin 时，`OdinInspectorBridge`（在 Odin Integration 编辑器程序集中）提供增强实现
4. `OdinBridgeLocator` 在运行时自动定位可用实现

核心代码通过 `IOdinBridge` 接口查询 Odin 可用性，不直接引用任何 Odin 类型。

## Consequences

- **优点**: 核心程序集零 Odin 依赖，无 Odin 时正常编译运行
- **优点**: OdinIntegration 程序集通过 `ODIN_INSPECTOR` 编译约束自动启用/禁用
- **缺点**: 新增一层间接调用，运行时查询有微小开销
- **缺点**: 需要维护两套实现的接口一致性
