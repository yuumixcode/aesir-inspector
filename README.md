# Aesir Inspector

[![license](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE.md)

`Aesir Inspector` 是一个基于 **Odin Inspector** 的 Unity 编辑器扩展库，旨在提供更强大的 Inspector 自定义功能、多语言 UI 支持以及更安全的编辑器工具集。

> **💡 无 Odin 亦可使用**：本项目已针对未导入 Odin Inspector 的用户做了完整适配——即使没有 Odin Inspector 插件，项目也能正常编译导入，不会触发任何报错或编译失败。只是 Odin 相关的核心增强功能（双语属性装饰器、安全编辑器桥接等）将不可用，其余部分不受影响。

## 适用人群

- **编辑器工具开发者**：正在开发自定义 Inspector 工具，需要双语（中/英）UI 显示支持的开发者。
- **跨国/跨地区协作团队**：团队成员语言背景不同，需要在 Inspector 面板中同时展示中英文信息以降低沟通成本。
- **Odin Inspector 用户**：已有 Odin Inspector 并希望获得更丰富的属性装饰器与安全编辑器工具的开发者。
- **代码规范倡导者**：希望团队遵循统一的代码风格与注释标准，提升项目可维护性。

## 核心功能

### 1. 代码风格与规范

本项目将代码风格视为与功能同等重要的组成部分。内置严格的代码编写标准与示例，确保团队协作中的代码一致性与可维护性：

- **强制规范**：公共方法必须包含 XML 注释与 `[Summary]` 特性。
- **风格指南**：详情请参阅 `Runtime/CodeStyle/AESIR_INSPECTOR_CODE_STYLE.cs`。
- **设计理念**：良好的代码风格不是可选项，而是项目质量的基石。所有贡献者均需遵循本规范。

### 2. 双语 UI 特性 (Bilingual Attributes)

提供了一套完整的双语属性装饰器，支持在 Inspector 面板中同时显示中文和英文信息。主要面向以下场景：

- **编辑器工具开发**：当你在开发其他编辑器工具时，希望 Inspector 界面支持中英双语显示，让不同语言背景的用户都能直观理解各项参数与操作。
- **团队协作**：跨地区、跨语言的团队在共享项目时，双语显示可有效降低沟通成本，避免因语言差异导致的误操作。

可用装饰器：
- `[BilingualTitle]`, `[BilingualTitleGroup]`
- `[BilingualBoxGroup]`
- `[BilingualButton]`
- `[BilingualInfoBox]`
- `[BilingualText]`
- `[ShowIfChinese]`, `[ShowIfEnglish]` 条件显示支持。

### 3. 安全编辑器工具 (Safe Editor Utilities)

针对 Odin Inspector 进行了桥接封装，确保在未安装 Odin 的环境下代码依然可以编译通过：
- **`OdinInspectorSafeEditorUtility`**: 安全调用 Odin API 的桥梁工具。
- **`ScriptableObjectSafeEditorUtility`**: 提供更可靠的 ScriptableObject 资产创建与管理。
- **`UrlUtility`**: 便捷的 URL 打开与外部链接处理。

## 安装说明

### 通过 Git URL 安装
1. 打开 Unity Package Manager 窗口。
2. 点击左上角的 `+` 按钮，选择 `Add package from git URL...`。
3. 输入以下地址：
   ```
   https://github.com/yuumixcode/aesir-inspector.git
   ```

### 通过 manifest.json 安装
在项目的 `Packages/manifest.json` 文件中添加：
```json
{
  "dependencies": {
    "com.runlab.aesir-inspector": "https://github.com/yuumixcode/aesir-inspector.git"
  }
}
```

## 环境依赖

- **Unity**: 2022.3.2t3 (Tuanjie) 或更高版本。
- **Odin Inspector**: 3.3.x 或更高版本（核心功能依赖，但非必需——见上方提示）。

## 使用示例

```csharp
using RunLab.AesirInspector;
using Sirenix.OdinInspector;
using UnityEngine;

public class ExampleMonoBehaviour : MonoBehaviour
{
    [BilingualTitle("玩家属性", "Player Stats")]
    [SerializeField]
    private int health;

    [BilingualButton("重置属性", "Reset Stats")]
    private void ResetStats()
    {
        health = 100;
    }
}
```

## 许可协议

本项目采用 MIT 协议开源。详情请参阅 [LICENSE.md](LICENSE.md)。
