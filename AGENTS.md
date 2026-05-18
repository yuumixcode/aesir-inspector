# Aesir Inspector — Agent Context

## Project Identity

- **Package**: `cn.runlab.aesir-inspector` | **Version**: 0.4.0-pre.1
- **Engine**: Tuanjie (Unity 2022.3 fork)
- **Language**: C# | .NET Standard 2.1
- **License**: MIT

## Commands

- **Test**: Unity Test Runner → Edit Mode / Play Mode (no CLI)
- **Build**: Tuanjie Editor → Build (no CLI build)
- **Doc Generate**: `Aesir Inspector → Script Doc Generator` context menu

## Project Structure

```
Aesir Inspector/
├── Runtime/
│   ├── Unity/             # Core runtime (RunLab.AesirInspector)
│   └── Odin Integration/       # Odin runtime (RunLab.AesirInspector.OdinIntegration, ODIN_INSPECTOR)
├── Editor/
│   ├── Unity/             # Core editor (RunLab.AesirInspector.Editor)
│   └── Odin Integration/       # Odin editor (RunLab.AesirInspector.OdinIntegration.Editor, ODIN_INSPECTOR)
├── Tests/Editor/          # Edit-mode tests
├── Tests/Runtime/         # Runtime tests
├── Samples~/              # Package Manager samples
├── Documentation~/        # Unity user docs
└── Docs~/                 # AI agent deep context (Cold Memory)
```

## Key Rules

- **严禁**对 `UnityEngine.Object` 派生类使用 `?.` 或 `??`
- 私有方法对应公开方法时，增加 `Internal_` 前缀
- `#if UNITY_EDITOR` 包裹编辑器专用代码
- Odin 依赖代码**必须**放在 `Odin Integration/` 子目录
- 核心程序集**不允许**直接引用 Odin API — 通过 `IOdinBridge` 桥接
- 公共类型**必须同时**具备 `/// <summary>` 和 `[Summary("...")]`
- 版本号需在 `package.json` 和 `AesirInspectorVersion.cs` 两处同步

## Deep Context

For architecture, conventions, and task guides:

- Architecture & C4 model → `Docs~/ARCHITECTURE.md`
- Code style & naming → `Docs~/CONVENTIONS.md`
- Module API docs → `Docs~/MODULES.md`
- Design decisions → `Docs~/ADR/`
- Task-specific guides → `Docs~/SKILLS/`
