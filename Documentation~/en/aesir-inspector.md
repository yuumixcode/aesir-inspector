# Aesir Inspector

[中文](../aesir-inspector.md)

Aesir Inspector is a Unity/Tuanjie editor extension library that provides bilingual Inspector UI, safe editor tooling, script documentation generation, XML Summary sync tools, and more. Optional integration with Odin Inspector for enhanced Inspector rendering and styling.

## Installation

### Via Package Manager (Recommended)

1. Open the Package Manager
2. Click `+` → `Add package from git URL...`
3. Enter: `https://github.com/yuumixcode/aesir-inspector.git`

### Via Assets Directory

Place the `Aesir Inspector` folder anywhere under the `Assets/` directory.

## Quick Start

After installation, access all features via the `Aesir Inspector` menu bar:

| Menu | Feature |
|------|---------|
| Getting Started | Welcome window |
| Attribute Overview Pro | Attribute overview (requires Odin Inspector) |
| Mini Tools | Mini toolset |
| Extension Package Manager | Extension package manager (requires Odin Inspector) |

## Core Features

### Bilingual Inspector

Supports Chinese and English bilingual Inspector display, with language switching via `AesirInspectorLanguageSettingsSO`. Provides bilingual Odin attributes (e.g., `[BilingualTitle]`, `[BilingualButton]`, `[BilingualText]`).

### Safe Editor Utilities

All Runtime utility classes use the `SafeEditorUtility` pattern, ensuring editor code is automatically stripped in builds.

### Script Doc Generator

Generates API documentation via reflection, with incremental updates and AI-friendly Markdown output. Supports custom document format strategies.

### Summary Tool

Right-click menu for bidirectional XML `<summary>` ↔ `[Summary]` synchronization, keeping code comments and XML documentation consistent.

### Odin Inspector Integration

Enhanced features are automatically enabled when Odin Inspector is installed: bilingual attributes, Drawers, AttributeProcessors, Attribute Overview Pro, Extension Package Manager, and more.

## Assemblies

| Assembly | Description | Odin Dependency |
|----------|-------------|----------------|
| RunLab.AesirInspector | Core runtime | None |
| RunLab.AesirInspector.Editor | Core editor | None |
| RunLab.AesirInspector.OdinIntegration | Odin runtime | Required |
| RunLab.AesirInspector.OdinIntegration.Editor | Odin editor | Required |

## System Requirements

- Unity 2022.3 or Tuanjie Engine
- Optional: Odin Inspector 4.0.x or later (project is continuously integrated against the latest stable release)

## License

MIT License — See [LICENSE.md](../LICENSE.md) for details.
