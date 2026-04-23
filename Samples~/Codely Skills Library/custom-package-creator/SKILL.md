---
name: custom-package-creator
description: Scaffold complete, publishable Unity/Tuanjie UPM packages with standard structure. Use when creating a new UPM package, generating package scaffolding, or setting up a Unity plugin package with package.json, asmdef, README, CHANGELOG, LICENSE, and Runtime/Editor/Tests directories. Triggers: 创建UPM包, 创建自定义包, 生成包结构, Unity插件脚手架, 初始化包目录, 新建Package, scaffold package, create unity package.
---

# Custom Package Creator

创建符合 Unity / Tuanjie 标准的 Unity Custom Package 目录结构，开发者可以在此基础上立刻着手填充内容。

## Workflow

### Gather Package Metadata

**在执行任何写入操作之前，必须严格按照以下步骤完成信息收集。**

#### Step 1 — 展示参考示例

在提出任何问题之前，**必须**先向用户展示以下完整的参考示例，让用户对所有字段的填写格式一目了然：

---

> 📦 **参考示例 — 以下是一个完整包的元数据填写样例：**
>
> | 字段 | 示例值 |
> |------|--------|
> | 正式包名 (Package Name) | `com.runlab.aesir-inspector` |
> | 显示包名 (Display Name) | `Aesir Inspector` |
> | 初始版本 | `0.1.0` |
> | Unity 最低版本 | `2022.3` |
> | 描述 | `A lightweight inspector extension for Unity.` |
> | 作者 | `RunLab - Yuumix` |
> | 输出路径 | `Assets/RunLab/Aesir Inspector/` |
>
> 📌 **推荐值说明**：部分字段提供了推荐值，**直接按 Enter 即表示接受推荐值**，无需重新输入。如不适用，请直接输入你的内容。

---

#### Step 2 — 逐项收集元数据

展示完示例后，**依次询问**以下每一项。每一项都必须等待用户回复后再询问下一项。

---

**① 正式包名 (Package Name)**

参考 [references/package-name-rule.md](references/package-name-rule.md) 中的命名规则，正式包名使用反向域名格式：

- 格式：`<域名扩展>.<公司名称>.<包标识符>`，只允许小写字母、数字、连字符 `-`、下划线 `_` 和句点 `.`
- 示例：`com.runlab.aesir-inspector`

> 请输入正式包名：

---

**② 显示包名 (Display Name)**

显示名称是在 Unity Package Manager 中展示给用户的名称，应简短且能说明包的内容：

- 格式：单词首字母大写，不同单词间用空格分隔
- 示例：根据正式包名 `com.runlab.aesir-inspector` 派生为 `Aesir Inspector`

> 请输入显示包名（或直接 Enter 接受自动派生值）：

派生规则：取正式包名最后一段，将连字符替换为空格，每个单词首字母大写。

---

**③ 初始版本号**

参考 [references/package-version-control.md](references/package-version-control.md)，包开发初始阶段版本号从 `0.1.0` 开始，MAJOR 为 0 表示尚处于开发阶段。

> 推荐值：`0.1.0`，直接按 Enter 接受，或输入其他版本号：

---

**④ Unity 最低版本**

指定此包支持的最低 Unity 版本，格式为 `YYYY.x`（主版本.次版本）：

- 示例：`2022.3`、`6000.0`

> 请输入 Unity 最低版本：

---

**⑤ 描述 (Description)**

对应 `package.json` 中的 `description` 字段，应简洁地说明此包的功能：

- 示例：`A lightweight inspector extension for Unity.`

> 请输入包的简短描述：

---

**⑥ 作者 (Author)**

对应 `package.json` 中的 `author.name` 字段：

- 示例：`RunLab - Yuumix`

> 示例：`RunLab - Yuumix`，输入作者名称，直接按 Enter 接受示例值。

---

**⑦ 输出路径 (Output Path)**

包文件将生成到此路径，路径中包名的不同单词使用空格分隔：

- 默认推导规则：`Assets/<公司名称>/<显示包名>/`
- 示例：`Assets/RunLab/Aesir Inspector/`
- 备选：`Packages/com.runlab.aesir-inspector/`（嵌入式包）

> 推荐值：根据以上信息自动推导，展示推导结果后询问用户是否接受（直接 Enter 接受，或输入新路径）：

---

#### Step 3 — 汇总确认

收集完所有字段后，**必须**展示以下格式的汇总表，等待用户确认后再执行任何文件写入操作：

```
📋 以下是你的包配置，请确认后按 Enter 继续（输入 N 重新填写）：

  正式包名：    com.runlab.aesir-inspector
  显示包名：    Aesir Inspector
  初始版本：    0.1.0
  Unity 版本：  2022.3
  描述：        A lightweight inspector extension for Unity.
  作者：        RunLab - Yuumix
  命名空间：    RunLab.AesirInspector（自动派生）
  输出路径：    Assets/RunLab/Aesir Inspector/
```

- 用户输入 **Enter** → 进入下一步，开始创建目录结构
- 用户输入 **N** → 重新询问所有字段（返回 Step 2 第①项）
- 用户指出某字段有误 → 仅重新询问该字段，其余字段保留

---

#### 命名空间派生规则

在汇总确认阶段，自动从正式包名派生 PascalCase 命名空间：

- `com.runlab.aesir-inspector` → `RunLab.AesirInspector`
- `com.company.my-package` → `Company.MyPackage`

---

### Create Directory Structure

根据用户提供的元数据，在 `<输出路径>/` 下**严格**创建以下结构。

**文件名替换规则：**
- `{Namespace}` = 从正式包名派生的 PascalCase 命名空间，例如 `RunLab.AesirInspector`
- `{DisplayName}` = 显示包名，例如 `Aesir Inspector`
- 所有目录和文件**必须按此结构一一创建**，不可省略、不可增减

```
<输出路径>/
  ├── package.json
  ├── README.md
  ├── CHANGELOG.md
  ├── LICENSE.md
  ├── Third Party Notices.md
  ├── Official Documentation Links.md          ← 链接文档（英文文件名）
  ├── Editor/
  │   ├── {Namespace}.Editor.asmdef
  │   └── EditorExample.cs
  ├── Runtime/
  │   ├── {Namespace}.asmdef
  │   └── RuntimeExample.cs
  ├── Tests/
  │   ├── Editor/
  │   │   └── {Namespace}.Editor.Tests.asmdef
  │   └── Runtime/
  │       └── {Namespace}.Tests.asmdef
  ├── Samples/                                  ← 首次开发时不带 ~ 后缀
  │   └── HelloWorld/                           ← 默认示例文件夹
  └── Documentation/                            ← 首次开发时不带 ~ 后缀
      └── {DisplayName}.md
```

**注意事项：**
- `Samples` 和 `Documentation` 目录生成时**不带 `~` 后缀**，因为 Unity 编辑器首次开发阶段需要能看见这两个目录；发布时再手动重命名加 `~`
- `Samples/HelloWorld/` 为默认示例子目录，后续按需添加内容
- 创建完目录结构后，立即进入 Generate Files 阶段逐一生成文件内容

### Generate Files

**按以下顺序逐一生成每个文件，每个文件都必须严格按照指定模板填写内容，不得省略任何字段。**

模板中的占位符统一替换规则：
- `{PackageName}` → 正式包名，如 `com.runlab.aesir-inspector`
- `{DisplayName}` → 显示包名，如 `Aesir Inspector`
- `{Version}` → 初始版本号，如 `0.1.0`
- `{UnityVersion}` → Unity 最低版本，如 `2022.3`
- `{Description}` → 包描述
- `{AuthorName}` → 作者名称，如 `RunLab - Yuumix`
- `{Namespace}` → PascalCase 命名空间，如 `RunLab.AesirInspector`
- `{YYYY}` → 当前年份（执行时取实际年份）
- `{TodayDate}` → 今天日期，格式 `YYYY-MM-DD`

---

#### ① package.json

必须是有效的 JSON，所有字段从已收集的元数据填入，未知字段使用模板默认值：

```json
{
  "name": "{PackageName}",
  "displayName": "{DisplayName}",
  "version": "{Version}",
  "unity": "{UnityVersion}",
  "description": "{Description}",
  "keywords": [],
  "category": "Unity",
  "author": {
    "name": "{AuthorName}",
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

---

#### ② README.md

````markdown
# {DisplayName}

[![license](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE.md)

{Description}

## Installation

Open the Package Manager window in Unity, click the `+` button in the top-left corner, and select `Add package from git URL...`. Enter:

```
<git-url>
```

## Usage

Brief usage instructions or code examples.

## License

This project is licensed under the MIT License — see the [LICENSE.md](LICENSE.md) file for details.
````

---

#### ③ CHANGELOG.md

```markdown
# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

本项目所有重大变更都将记录在此文件中。

文件格式基于 Keep a Changelog，本项目遵循 语义化版本 规范。

## [{Version}] - {TodayDate}

### Added

- Initial release.
```

---

#### ④ LICENSE.md

```markdown
The MIT License

Copyright (c) {YYYY} {AuthorName}

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

#### ⑤ Third Party Notices.md

```markdown
This package contains third-party software components governed by the license(s) indicated below:

Component Name: Semver

License Type: "MIT"

[SemVer License](https://github.com/myusername/semver/blob/master/License.txt)
```

---

#### ⑥ Official Documentation Links.md

此文件放在包根目录，文件名为英文 `Official Documentation Links.md`：

```markdown
# Official Documentation Links

[Creating Custom Packages](https://docs.unity.cn/cn/tuanjiemanual/Manual/CustomPackages.html)
```

---

#### ⑦ Runtime/{Namespace}.asmdef

```json
{
  "name": "{Namespace}",
  "rootNamespace": "{Namespace}",
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

---

#### ⑧ Editor/{Namespace}.Editor.asmdef

`references` 必须包含 Runtime asmdef 的 `name` 字段值，`includePlatforms` 必须为 `["Editor"]`：

```json
{
  "name": "{Namespace}.Editor",
  "rootNamespace": "{Namespace}.Editor",
  "references": ["{Namespace}"],
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

---

#### ⑨ Tests/Runtime/{Namespace}.Tests.asmdef

`overrideReferences` 必须为 `true`，`precompiledReferences` 必须包含 `"nunit.framework.dll"`，`autoReferenced` 必须为 `false`，`defineConstraints` 必须为 `["UNITY_INCLUDE_TESTS"]`：

```json
{
  "name": "{Namespace}.Tests",
  "rootNamespace": "{Namespace}.Tests",
  "references": [
    "{Namespace}",
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

---

#### ⑩ Tests/Editor/{Namespace}.Editor.Tests.asmdef

`overrideReferences` 必须为 `true`，`precompiledReferences` 必须包含 `"nunit.framework.dll"`，`autoReferenced` 必须为 `false`，`includePlatforms` 为 `["Editor"]`，`defineConstraints` 为 `["UNITY_INCLUDE_TESTS"]`：

```json
{
  "name": "{Namespace}.Editor.Tests",
  "rootNamespace": "{Namespace}.Editor.Tests",
  "references": [
    "{Namespace}.Editor",
    "{Namespace}",
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

---

#### ⑪ Runtime/RuntimeExample.cs

```csharp
using UnityEngine;

namespace {Namespace}
{
    /// <summary>
    /// Runtime example class for {Namespace}
    /// </summary>
    public class RuntimeExample : MonoBehaviour { }
}
```

---

#### ⑫ Editor/EditorExample.cs

```csharp
using UnityEditor;

namespace {Namespace}.Editor
{
    /// <summary>
    /// Editor example class for {Namespace}
    /// </summary>
    public static class EditorExample
    {
        [MenuItem("Tools/{DisplayName}/Editor Example")]
        static void OpenExample() { }
    }
}
```

---

#### ⑬ Documentation/{DisplayName}.md

```markdown
# {DisplayName}

{Description}
```

---

#### ⑭ Samples/HelloWorld/

创建空的 `HelloWorld/` 子目录，用于后续添加示例内容。无需在其中生成任何文件。

### Validate

生成所有文件后，**逐项**执行以下校验清单。每项必须通过，否则立即修复再继续。

#### 目录结构校验

- [ ] 根目录下存在以下全部文件：`package.json`、`README.md`、`CHANGELOG.md`、`LICENSE.md`、`Third Party Notices.md`、`Official Documentation Links.md`
- [ ] 存在 `Runtime/` 目录，其中包含 `{Namespace}.asmdef` 和 `RuntimeExample.cs`
- [ ] 存在 `Editor/` 目录，其中包含 `{Namespace}.Editor.asmdef` 和 `EditorExample.cs`
- [ ] 存在 `Tests/Runtime/` 目录，其中包含 `{Namespace}.Tests.asmdef`
- [ ] 存在 `Tests/Editor/` 目录，其中包含 `{Namespace}.Editor.Tests.asmdef`
- [ ] 存在 `Samples/HelloWorld/` 目录
- [ ] 存在 `Documentation/{DisplayName}.md` 文件

#### package.json 校验

- [ ] 是有效 JSON（可解析，无语法错误）
- [ ] `name` 字段值为全小写、使用连字符的正式包名，符合反向域名格式
- [ ] `displayName` 字段已填写
- [ ] `version` 字段符合 SemVer 格式（如 `0.1.0`）
- [ ] `unity` 字段已填写（如 `2022.3`）
- [ ] `description` 字段已填写且非空
- [ ] `author.name` 字段已填写
- [ ] `samples` 数组中至少包含一项，`path` 指向 `Samples~/HelloWorld`

#### asmdef 文件校验

- [ ] `Runtime/{Namespace}.asmdef`：`name` = `{Namespace}`，`rootNamespace` = `{Namespace}`，`includePlatforms` = `[]`
- [ ] `Editor/{Namespace}.Editor.asmdef`：`name` = `{Namespace}.Editor`，`references` 包含 `{Namespace}`，`includePlatforms` = `["Editor"]`
- [ ] `Tests/Runtime/{Namespace}.Tests.asmdef`：`name` = `{Namespace}.Tests`，`references` 包含 `{Namespace}` 和 `UnityEngine.TestRunner`，`overrideReferences` = `true`，`precompiledReferences` 包含 `"nunit.framework.dll"`，`autoReferenced` = `false`，`defineConstraints` = `["UNITY_INCLUDE_TESTS"]`
- [ ] `Tests/Editor/{Namespace}.Editor.Tests.asmdef`：`name` = `{Namespace}.Editor.Tests`，`references` 包含 `{Namespace}.Editor`、`{Namespace}`、`UnityEngine.TestRunner`、`UnityEditor.TestRunner`，`includePlatforms` = `["Editor"]`，`overrideReferences` = `true`，`precompiledReferences` 包含 `"nunit.framework.dll"`，`autoReferenced` = `false`，`defineConstraints` = `["UNITY_INCLUDE_TESTS"]`
- [ ] 无循环程序集引用（Editor → Runtime，不可反向）

#### 文档文件校验

- [ ] `README.md`：包含 `# {DisplayName}` 标题、`## Installation` 章节和 `## License` 章节
- [ ] `CHANGELOG.md`：包含 `## [{Version}]` 条目，日期格式为 `YYYY-MM-DD`
- [ ] `LICENSE.md`：包含 `Copyright (c) {YYYY} {AuthorName}` 版权行，内容为完整 MIT 协议（含中文部分）
- [ ] `Third Party Notices.md`：文件存在且包含模板内容
- [ ] `Official Documentation Links.md`：文件存在，包含指向 `https://docs.unity.cn/cn/tuanjiemanual/Manual/CustomPackages.html` 的链接

#### 脚本文件校验

- [ ] `Runtime/RuntimeExample.cs`：命名空间为 `{Namespace}`
- [ ] `Editor/EditorExample.cs`：命名空间为 `{Namespace}.Editor`，包含 `[MenuItem]` 特性

#### 全部校验通过后

向用户输出一份摘要，列出所有已创建的文件路径，并提示下一步建议（如填写 `repository.url`、添加示例代码、重命名 `Samples/` 为 `Samples~/` 等）。

如果 Unity Editor 已连接，编译项目并检查是否有错误。
