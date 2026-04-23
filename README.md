# Aesir Inspector

[![license](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE.md)

`Aesir Inspector` 是一个基于 **Odin Inspector** 的 Unity 编辑器扩展库，旨在提供更强大的 Inspector 自定义功能、多语言 UI 支持以及更安全的编辑器工具集。

## 核心功能

### 1. 双语 UI 特性 (Bilingual Attributes)
提供了一套完整的双语属性装饰器，支持在 Inspector 面板中同时显示中文和英文信息，方便跨团队协作：
- `[BilingualTitle]`, `[BilingualTitleGroup]`
- `[BilingualBoxGroup]`
- `[BilingualButton]`
- `[BilingualInfoBox]`
- `[BilingualText]`
- `[ShowIfChinese]`, `[ShowIfEnglish]` 条件显示支持。

### 2. 安全编辑器工具 (Safe Editor Utilities)
针对 Odin Inspector 进行了桥接封装，确保在未安装 Odin 的环境下代码依然可以编译通过：
- **`OdinInspectorSafeEditorUtility`**: 安全调用 Odin API 的桥梁工具。
- **`ScriptableObjectSafeEditorUtility`**: 提供更可靠的 ScriptableObject 资产创建与管理。
- **`UrlUtility`**: 便捷的 URL 打开与外部链接处理。

### 3. 代码风格与规范
本项目遵循严格的代码编写标准，内置了代码风格示例与指南：
- 详情请参阅：`Runtime/CodeStyle/AESIR_INSPECTOR_CODE_STYLE.cs`。
- 强制要求公共方法包含 XML 注释与 `[Summary]` 特性。

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
- **Odin Inspector**: 3.3.x 或更高版本（核心功能依赖）。

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
