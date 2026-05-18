# ADR-003: 核心/集成层程序集分离

## Status

Accepted

## Context

Aesir Inspector 包含两类代码：
1. **核心代码**：不依赖 Odin Inspector，在所有环境下运行
2. **集成代码**：依赖 Odin Inspector，仅在安装 Odin 后运行

如果将所有代码放在一个程序集中，核心代码将无法在无 Odin 环境下编译。

## Decision

将代码分为 4 个程序集，按目录和编译约束分离：

| 程序集 | 目录 | Odin 约束 |
|--------|------|-----------|
| `RunLab.AesirInspector` | `Runtime/Unity/` | 无 |
| `RunLab.AesirInspector.Editor` | `Editor/Unity/` | 无 |
| `RunLab.AesirInspector.OdinIntegration` | `Runtime/Odin Integration/` | `ODIN_INSPECTOR` |
| `RunLab.AesirInspector.OdinIntegration.Editor` | `Editor/Odin Integration/` | `ODIN_INSPECTOR` |

依赖关系：核心 Editor → 核心 Runtime；Odin Integration Editor → 核心 Runtime + 核心 Editor + Odin Integration Runtime。

## Consequences

- **优点**: 核心程序集零 Odin 依赖，任何环境可编译
- **优点**: OdinIntegration 程序集通过编译约束自动启用/禁用
- **缺点**: 4 个程序集增加了构建复杂度
- **缺点**: 跨程序集的 internal 可见性需要 `InternalsVisibleTo` 或嵌套类方案
