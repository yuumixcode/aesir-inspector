# Skill: 添加新的 InspectorControl

> 当需要新增一个自定义 Inspector 控件时，按此指南操作。

## 步骤

### 1. 创建控件文件

**文件**: `Runtime/Unity/InspectorControls/{Name}Control.cs`

```csharp
namespace RunLab.AesirInspector
{
    /// <summary>
    /// {Description}。
    /// </summary>
    [Summary("{Description}。")]
    public class {Name}Control
    {
        // 控件实现...
    }
}
```

### 2. 规范

- 命名：`{Purpose}Control`，如 `BilingualHeaderControl`
- 放置于 `Runtime/Unity/InspectorControls/` 目录
- 公共类型必须有 `/// <summary>` + `[Summary]`
- 如有 Editor 功能需求，通过 `SafeEditorUtility` 模式桥接

### 3. 双语支持

如需双语标签，使用 `BilingualData`：

```csharp
static readonly BilingualData Label = new BilingualData("中文标签", "English Label");
```

读取当前语言：

```csharp
var text = AesirInspectorLanguageSettingsSO.CurrentLanguage == Language.Zh
    ? Label.Zh : Label.En;
```

## 验证

1. 无 Odin 环境下编译通过
2. 有 Odin 环境下编译通过
3. Inspector 中控件渲染正常
