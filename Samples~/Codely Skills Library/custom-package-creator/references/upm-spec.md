# UPM Package Specification Reference

本文件是 Unity / Tuanjie 自定义包（Custom Package）的规范参考文档，描述包结构的**是什么**和**为什么**。

执行流程（怎么做）见 [../SKILL.md](../SKILL.md)。

---

## 1. Package Name Convention

正式包名遵循反向域名格式：

```
<tld>.<company>.<identifier>
```

- 只允许小写字母、数字、连字符 `-`、下划线 `_` 和句点 `.`
- 示例：`com.runlab.aesir-inspector`

详细规则见 [package-name-rule.md](package-name-rule.md)。

---

## 2. Directory Structure

完整的包目录结构如下：

```
<package-root>/
  ├── package.json
  ├── README.md
  ├── CHANGELOG.md
  ├── LICENSE.md
  ├── Third Party Notices.md
  ├── Official Documentation Links.md
  ├── Editor/
  │   ├── RunLab.AesirInspector.Editor.asmdef
  │   └── EditorExample.cs
  ├── Runtime/
  │   ├── RunLab.AesirInspector.asmdef
  │   └── RuntimeExample.cs
  ├── Tests/
  │   ├── Editor/
  │   │   └── RunLab.AesirInspector.Editor.Tests.asmdef
  │   └── Runtime/
  │       └── RunLab.AesirInspector.Tests.asmdef
  ├── Samples~/
  │   └── HelloWorld/
  └── Documentation~/
      └── Aesir Inspector.md
```

> **开发阶段说明**：`Samples` 和 `Documentation` 在开发阶段不带 `~` 后缀，以便在 Unity 编辑器中直接可见；发布前再手动重命名加 `~`。

---

## 3. package.json Schema

```json
{
  "name": "com.runlab.aesir-inspector",
  "displayName": "Aesir Inspector",
  "version": "0.1.0",
  "unity": "2022.3",
  "description": "A lightweight inspector extension for Unity.",
  "keywords": [],
  "category": "Unity",
  "author": {
    "name": "RunLab - Yuumix",
    "email": "",
    "url": ""
  },
  "dependencies": {},
  "repository": {
    "type": "git",
    "url": ""
  },
  "license": "MIT",
  "licensesUrl": "",
  "changelogUrl": "",
  "documentationUrl": "",
  "samples": [
    {
      "displayName": "Hello World",
      "description": "A basic example.",
      "path": "Samples~/HelloWorld"
    }
  ]
}
```

**字段说明：**

| 字段 | 说明 |
|------|------|
| `name` | 正式包名，全小写反向域名格式 |
| `displayName` | Package Manager 中显示的名称 |
| `version` | 遵循 SemVer，开发阶段从 `0.1.0` 起 |
| `unity` | 最低支持的 Unity 版本，格式 `YYYY.x` |
| `samples[].path` | 必须指向 `Samples~/` 路径（带 `~`） |

版本规范详见 [package-version-control.md](package-version-control.md)。

---

## 4. Assembly Definition (asmdef) Schemas

### Namespace Convention

命名空间从正式包名派生，采用 PascalCase 点分隔格式：

| 包名 | 命名空间 |
|------|---------|
| `com.runlab.aesir-inspector` | `RunLab.AesirInspector` |
| `com.company.my-package` | `Company.MyPackage` |

asmdef 文件名与 `name` 字段保持一致。

### asmdef Type Overview

| 类型 | 文件路径 | `name` 字段 |
|------|---------|-------------|
| Runtime | `Runtime/RunLab.AesirInspector.asmdef` | `RunLab.AesirInspector` |
| Editor | `Editor/RunLab.AesirInspector.Editor.asmdef` | `RunLab.AesirInspector.Editor` |
| Runtime Tests | `Tests/Runtime/RunLab.AesirInspector.Tests.asmdef` | `RunLab.AesirInspector.Tests` |
| Editor Tests | `Tests/Editor/RunLab.AesirInspector.Editor.Tests.asmdef` | `RunLab.AesirInspector.Editor.Tests` |

### Runtime asmdef

```json
{
  "name": "RunLab.AesirInspector",
  "rootNamespace": "RunLab.AesirInspector",
  "references": [],
  "includePlatforms": [],
  "excludePlatforms": [],
  "allowUnsafeCode": false,
  "overrideReferences": false,
  "precompiledReferences": [],
  "autoReferenced": true,
  "defineConstraints": [],
  "versionDefines": [],
  "noEngineReferences": false
}
```

### Editor asmdef

Editor 程序集必须引用 Runtime 程序集，且只在编辑器平台生效。

```json
{
  "name": "RunLab.AesirInspector.Editor",
  "rootNamespace": "RunLab.AesirInspector.Editor",
  "references": ["RunLab.AesirInspector"],
  "includePlatforms": ["Editor"],
  "excludePlatforms": [],
  "allowUnsafeCode": false,
  "overrideReferences": false,
  "precompiledReferences": [],
  "autoReferenced": true,
  "defineConstraints": [],
  "versionDefines": [],
  "noEngineReferences": false
}
```

### Runtime Tests asmdef

测试程序集需要显式引用 `nunit.framework.dll`，因此 `overrideReferences` 为 `true`，`autoReferenced` 为 `false`。

```json
{
  "name": "RunLab.AesirInspector.Tests",
  "rootNamespace": "RunLab.AesirInspector.Tests",
  "references": [
    "RunLab.AesirInspector",
    "UnityEngine.TestRunner"
  ],
  "includePlatforms": [],
  "excludePlatforms": [],
  "allowUnsafeCode": false,
  "overrideReferences": true,
  "precompiledReferences": [
    "nunit.framework.dll"
  ],
  "autoReferenced": false,
  "defineConstraints": [
    "UNITY_INCLUDE_TESTS"
  ],
  "versionDefines": [],
  "noEngineReferences": false
}
```

### Editor Tests asmdef

```json
{
  "name": "RunLab.AesirInspector.Editor.Tests",
  "rootNamespace": "RunLab.AesirInspector.Editor.Tests",
  "references": [
    "RunLab.AesirInspector.Editor",
    "RunLab.AesirInspector",
    "UnityEngine.TestRunner",
    "UnityEditor.TestRunner"
  ],
  "includePlatforms": [
    "Editor"
  ],
  "excludePlatforms": [],
  "allowUnsafeCode": false,
  "overrideReferences": true,
  "precompiledReferences": [
    "nunit.framework.dll"
  ],
  "autoReferenced": false,
  "defineConstraints": [
    "UNITY_INCLUDE_TESTS"
  ],
  "versionDefines": [],
  "noEngineReferences": false
}
```

**程序集依赖关系：**

```
Editor.Tests ──► Editor ──► Runtime ◄── Runtime.Tests
```

单向依赖，不可逆向引用。

---

## 5. File Templates

### README.md

```markdown
# Aesir Inspector

[![license](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE.md)

A lightweight inspector extension for Unity.

## Installation

Open the Package Manager window in Unity, click the `+` button in the top-left corner,
and select `Add package from git URL...`. Enter:

```
<git-url>
```

## Usage

Brief usage instructions or code examples.

## License

This project is licensed under the MIT License — see the [LICENSE.md](LICENSE.md) file for details.
```

---

### CHANGELOG.md

格式基于 [Keep a Changelog](https://keepachangelog.com/en/1.1.0/)，版本遵循 [Semantic Versioning](https://semver.org/spec/v2.0.0.html)。

```markdown
# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

本项目所有重大变更都将记录在此文件中。

文件格式基于 Keep a Changelog，本项目遵循 语义化版本 规范。

## [0.1.0] - 2026-04-23

### Added

- Initial release.
```

---

### LICENSE.md

使用 MIT 协议，包含英文正文和中文译文。

```markdown
The MIT License

Copyright (c) 2026 RunLab - Yuumix

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

---

特此向获得本软件及相关文档文件（合称 "本软件"）副本的任何人免费授予不受限制地利用本软件的许可，包括但不限于使用、复制、修改、合并、发布、分发、分许可和 / 或销售本软件副本的权利，并允许向其提供本软件的人享有同等权利，但须遵守以下条件：

上述版权声明及本许可声明应包含在本软件的所有副本或主要部分中。

本软件按 "现状" 提供，不附带任何明示或暗示的保证，包括但不限于对适销性、特定用途适用性及非侵权性的保证。在任何情况下，作者或版权持有人均不对因本软件或本软件的使用或其他交易相关而产生、源于或与之有关的任何索赔、损害或其他责任承担责任，无论是合同诉讼、侵权行为还是其他形式。
```

---

### Third Party Notices.md

```markdown
This package contains third-party software components governed by the license(s) indicated below:

Component Name: Semver

License Type: "MIT"

[SemVer License](https://github.com/myusername/semver/blob/master/License.txt)

Component Name: MyComponent

License Type: "MyLicense"

[MyComponent License](https://www.mycompany.com/licenses/License.txt)
```

---

### Official Documentation Links.md

```markdown
# Official Documentation Links

[Creating Custom Packages](https://docs.unity.cn/cn/tuanjiemanual/Manual/CustomPackages.html)
```

---

### RuntimeExample.cs

```csharp
using UnityEngine;

namespace RunLab.AesirInspector
{
    /// <summary>
    /// Runtime example class for RunLab.AesirInspector
    /// </summary>
    public class RuntimeExample : MonoBehaviour { }
}
```

---

### EditorExample.cs

```csharp
using UnityEditor;

namespace RunLab.AesirInspector.Editor
{
    /// <summary>
    /// Editor example class for RunLab.AesirInspector
    /// </summary>
    public static class EditorExample
    {
        [MenuItem("Tools/RunLab.AesirInspector/Editor Example")]
        static void OpenExample() { }
    }
}
```

---

## 6. Validation Checklist

包创建完成后的核验清单：

### 目录结构

- [ ] 根目录包含：`package.json`、`README.md`、`CHANGELOG.md`、`LICENSE.md`、`Third Party Notices.md`、`Official Documentation Links.md`
- [ ] `Runtime/` 目录存在，包含对应 `.asmdef` 和 `RuntimeExample.cs`
- [ ] `Editor/` 目录存在，包含对应 `.asmdef` 和 `EditorExample.cs`
- [ ] `Tests/Runtime/` 目录存在，包含对应 `.asmdef`
- [ ] `Tests/Editor/` 目录存在，包含对应 `.asmdef`
- [ ] `Samples/HelloWorld/` 目录存在
- [ ] `Documentation/` 目录存在，包含以显示包名命名的 `.md` 文件

### package.json

- [ ] 是有效 JSON，无语法错误
- [ ] `name` 全小写，符合反向域名格式
- [ ] `version` 符合 SemVer
- [ ] `unity` 已填写
- [ ] `description` 非空
- [ ] `author.name` 已填写
- [ ] `samples[].path` 指向 `Samples~/HelloWorld`

### asmdef

- [ ] Runtime：`includePlatforms` 为空，`autoReferenced` 为 `true`
- [ ] Editor：`references` 包含 Runtime，`includePlatforms` = `["Editor"]`
- [ ] Runtime Tests：`overrideReferences` = `true`，`precompiledReferences` 含 `nunit.framework.dll`，`autoReferenced` = `false`，`defineConstraints` = `["UNITY_INCLUDE_TESTS"]`
- [ ] Editor Tests：同上，且 `references` 包含 Editor 和 `UnityEditor.TestRunner`，`includePlatforms` = `["Editor"]`
- [ ] 无循环程序集引用

### 文档与脚本

- [ ] `README.md` 含 `Installation` 和 `License` 章节
- [ ] `CHANGELOG.md` 含初始版本条目，日期格式为 `YYYY-MM-DD`
- [ ] `LICENSE.md` 含正确版权年份和作者，包含中文译文
- [ ] `Official Documentation Links.md` 含指向官方文档的链接
- [ ] `RuntimeExample.cs` 命名空间正确
- [ ] `EditorExample.cs` 命名空间正确，包含 `[MenuItem]` 特性
