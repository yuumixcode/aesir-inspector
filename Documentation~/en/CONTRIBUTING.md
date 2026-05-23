# Contributing Guide

[中文](../../CONTRIBUTING.md)

Thank you for your interest in Aesir Inspector! We welcome all forms of contribution, including but not limited to bug reports, feature suggestions, documentation improvements, and code contributions.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [How to Contribute](#how-to-contribute)
- [Development Environment Setup](#development-environment-setup)
- [Project Structure](#project-structure)
- [Coding Standards](#coding-standards)
- [Submitting a Pull Request](#submitting-a-pull-request)
- [Questions & Help](#questions--help)

## Code of Conduct

This project adopts the [Contributor Covenant](https://www.contributor-covenant.org/version/2/1/code_of_conduct/) Code of Conduct. By participating in this project, you agree to abide by its terms. Please treat every community member with respect and constructive engagement.

## How to Contribute

### Reporting Bugs

1. Search [Issues](https://github.com/yuumixcode/aesir-inspector/issues) to check if the problem has already been reported.
2. If not, [create a new Issue](https://github.com/yuumixcode/aesir-inspector/issues/new) with the following information:
   - **Steps to Reproduce**: Detailed description of how to trigger the bug.
   - **Expected Behavior**: What you expected to happen.
   - **Actual Behavior**: What actually happened.
   - **Environment**: Unity/Tuanjie version, Odin Inspector version (if applicable), operating system.
   - **Screenshots/Logs**: Attach screenshots or error logs if possible.

### Suggesting Features

1. Search [Issues](https://github.com/yuumixcode/aesir-inspector/issues) for similar suggestions.
2. If none exist, create a new Issue with the `Feature Request` label, including:
   - **Use Case**: Describe the specific problem you want to solve.
   - **Proposed Solution**: Describe your expected solution.
   - **Alternatives Considered**: Other approaches you have considered.

### Contributing Code

1. Fork this repository.
2. Develop based on the `Assets/aesir-inspector/` directory in the repository root.
3. Follow the [Coding Standards](#coding-standards) below.
4. Submit a Pull Request to the `main` branch.

## Development Environment Setup

### Prerequisites

- **Tuanjie Editor** 2022.3 or later (Unity 2022.3 fork)
- **Git**: For version control
- **Odin Inspector** 4.0.x or later (optional dependency, for developing OdinIntegration enhancement features. The project is continuously integrated against the latest stable release, currently baseline 4.0.1.x)

### Cloning the Project

```bash
git clone https://github.com/yuumixcode/aesir-inspector.git
```

Place the cloned repository in your Tuanjie project's `Assets/` directory, or reference it as a local package via Package Manager.

### Odin Inspector Integration

Odin Inspector is an optional dependency:

- **Without Odin**: Core features compile and run normally; the OdinIntegration assembly is automatically skipped.
- **With Odin**: Importing automatically adds the `ODIN_INSPECTOR` compilation symbol, enabling the OdinIntegration enhancement assembly.

## Project Structure

```
Aesir Inspector/
├── Runtime/
│   ├── Unity/                     # Core Runtime (RunLab.AesirInspector)
│   │   ├── Attributes/            # Custom attributes like [Summary]
│   │   ├── Core/                   # Version, Paths, WebLinks
│   │   ├── Inspector/              # Inspector display models
│   │   ├── Localization/           # Localization data & language settings
│   │   ├── Logging/               # Logging system
│   │   ├── OdinBridge/            # IOdinBridge bridge layer
│   │   ├── ScriptDocGenerator/    # Doc generator runtime models
│   │   └── Utilities/             # Safe editor utilities
│   └── OdinIntegration/           # Odin Runtime (ODIN_INSPECTOR)
│       ├── Attributes/            # Bilingual attributes
│       └── OdinCodeHighlighter.cs
├── Editor/
│   ├── Unity/                     # Core Editor (RunLab.AesirInspector.Editor)
│   │   ├── Core/                  # Install detection, menu management
│   │   ├── MiniTools/             # QuickCreateSO
│   │   └── SummaryTool/           # XML Summary sync
│   └── OdinIntegration/           # Odin Editor (ODIN_INSPECTOR)
│       ├── AttributeOverviewPro/  # Attribute overview window
│       ├── AttributeProcessors/   # OdinAttributeProcessor
│       ├── Bridge/                # OdinInspectorBridge
│       ├── Drawers/               # Bilingual Drawers
│       ├── ExtensionManager/      # Extension package manager
│       ├── MiniTools/             # MenuItem Viewer, Syntax Highlighter
│       ├── ScriptDocGenerator/    # Doc generator editor logic
│       └── Windows/               # Getting Started, Preferences
├── Tests/
│   ├── Editor/                    # Editor mode tests
│   └── Runtime/                   # Runtime mode tests
├── Samples~/                      # Usage examples
└── Documentation~/                # User docs & developer guides
```

### Assembly Descriptions

| Assembly | Odin Dependency | Description |
|----------|---------------|-------------|
| `RunLab.AesirInspector` | None | Core runtime; must not reference Odin APIs |
| `RunLab.AesirInspector.Editor` | None | Core editor |
| `RunLab.AesirInspector.OdinIntegration` | `ODIN_INSPECTOR` | Odin runtime bridge |
| `RunLab.AesirInspector.OdinIntegration.Editor` | `ODIN_INSPECTOR` | Odin editor enhancements |

## Coding Standards

Please read and follow these standards before submitting code. For detailed standards, see `Runtime/Unity/CodeStyle/AesirInspectorCodeStyle.cs` and `Documentation~/development.md`.

### Comment Standards

This project adopts **self-documenting code** and a **no-comment paradigm**:

- **No XML Comments**: Do not use `/// <summary>`, `/// <param>`, or other XML documentation comments.
- **Naming as Documentation**: Convey intent through clear naming without extra comments.
- **`[Summary]` Only for Complex Logic**: Use `[Summary("...")]` only when naming cannot fully express intent, explaining "why" rather than "what".

```csharp
// ✅ Self-documenting: clear naming, no comments needed
public int MaxRetryCount { get; }
public void ApplyDamage(float amount) { }

// ✅ [Summary] explains "why"
[Summary("Latter overrides former, used for priority merging of multiple config sources")]
public void MergeConfigSources(IReadOnlyList<ConfigSource> sources) { }

// ❌ XML comments are forbidden
/// <summary>
/// Applies damage amount
/// </summary>
public void ApplyDamage(float amount) { }
```

### Naming Standards

| Identifier | Rule | Example |
|-----------|------|---------|
| Classes, Interfaces | `PascalCase`, interfaces with `I` prefix | `PlayerManager`, `IDamageable` |
| Private non-serialized fields | `_camelCase` | `_health` |
| Serialized fields `[SerializeField]` | `camelCase` | `moveSpeed` |
| Constants / Static readonly | `PascalCase` | `MaxScore` |

### Unity/C# Key Rules

- **Never** use `?.` / `??` on `UnityEngine.Object` derived types.
- Add `Internal_` prefix to private methods corresponding to public methods.
- Wrap editor-only code with `#if UNITY_EDITOR`.
- Odin-dependent code **must** be placed in the `OdinIntegration/` subdirectory.
- Core assemblies **must not** directly reference Odin APIs — use the `IOdinBridge` bridge.

### Event Standards

| Role | Naming | Example |
|------|--------|---------|
| Event | No `On` prefix | `DoorOpened` |
| Subscription method | `On` + Event name | `OnDoorOpened` |
| Raise method | `Raise` + Event name | `RaiseDoorOpened` |

### Enum Standards

- Regular: Include `None = 0`, assign values explicitly.
- Flags: `[Flags]`, values as `1 << n`, compound values with `|`.

### Utility Naming

| Category | Naming Rule | Directory |
|----------|-------------|-----------|
| Runtime | `XxxUtility` | `Runtime/Unity/Utilities/` |
| Editor safe wrappers | `XxxSafeEditorUtility` | `Runtime/Unity/Utilities/` |
| Editor-Only | `XxxEditorUtility` | `Editor/Unity/` |

## Submitting a Pull Request

### Process

1. Ensure a corresponding Issue exists (for bug fixes or feature suggestions); create one first if not.
2. Fork the repository and create a feature branch from `main`:
   ```bash
   git checkout -b feature/your-feature-name
   # or
   git checkout -b fix/your-bug-fix-name
   ```
3. Write code following the coding standards.
4. Add necessary unit tests (tests are located in `Tests/Editor/` and `Tests/Runtime/`).
5. Ensure all existing tests pass.
6. Commit changes with present-tense, concise commit messages:
   ```
   Add bilingual attribute processor for Button
   Fix null reference in OdinBridgeLocator
   ```
7. Push to your fork and create a Pull Request.
8. Reference the related Issue in the PR description (e.g., `Closes #123`).

### Branch Naming

| Type | Format | Example |
|------|--------|---------|
| Feature | `feature/<name>` | `feature/bilingual-toggle` |
| Fix | `fix/<name>` | `fix/odin-bridge-null-ref` |
| Docs | `docs/<name>` | `docs/update-contributing-guide` |

### PR Checklist

Before submitting a PR, please confirm:

- [ ] Code follows project coding standards
- [ ] No XML documentation comments introduced (use `[Summary]` or self-documenting naming instead)
- [ ] No `?.` / `??` used on `UnityEngine.Object` derived types
- [ ] Editor-only code wrapped with `#if UNITY_EDITOR`
- [ ] Odin-dependent code placed in the `OdinIntegration/` subdirectory
- [ ] Core assemblies do not directly reference Odin APIs
- [ ] Necessary unit tests added
- [ ] All tests pass
- [ ] Commit messages are concise and use present tense

## Questions & Help

- **Bug Reports & Feature Suggestions**: [GitHub Issues](https://github.com/yuumixcode/aesir-inspector/issues)
- **Discussions & Questions**: [GitHub Discussions](https://github.com/yuumixcode/aesir-inspector/discussions)
- **Email**: zeriying@gmail.com

---

Thank you for contributing! Every submission makes Aesir Inspector better.
