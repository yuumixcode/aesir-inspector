# Skill: 添加新的双语 Attribute

> 当需要新增一个支持中英双语的 Odin Inspector 特性时，按此指南操作。

## 前置条件

- 确认目标 Odin 内置特性（如 `[Title]`、`[BoxGroup]`、`[Button]`）
- 确认该特性需要哪些双语参数

## 步骤

### 1. 创建 Attribute

**文件**: `Runtime/Odin Integration/Attributes/{Name}Attribute.cs`

```csharp
namespace RunLab.AesirInspector.OdinIntegration
{
    /// <summary>
    /// 双语 {Purpose} 特性。
    /// </summary>
    [Summary("双语 {Purpose} 特性。")]
    [DontApplyToListElements]
    public class Bilingual{Name}Attribute : Attribute
    {
        public string ZhText { get; }
        public string EnText { get; }

        public Bilingual{Name}Attribute(string zhText, string enText)
        {
            ZhText = zhText;
            EnText = enText;
        }
    }
}
```

- 命名规则：`Bilingual{OdinOriginalName}Attribute`
- 必须 `[DontApplyToListElements]`（除非需要应用在列表元素上）
- 公共类型必须有 `/// <summary>` + `[Summary]`

### 2. 创建 Drawer

**文件**: `Editor/Odin Integration/Drawers/Bilingual{Name}AttributeDrawer.cs`

```csharp
namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    public class Bilingual{Name}AttributeDrawer : OdinAttributeDrawer<Bilingual{Name}Attribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var isZh = AesirInspectorLanguageSettingsSO.CurrentLanguage == Language.Zh;
            var text = isZh ? Attribute.ZhText : Attribute.EnText;
            // 调用 Odin 原生特性逻辑...
        }
    }
}
```

- 读取 `AesirInspectorLanguageSettingsSO.CurrentLanguage` 确定当前语言
- Drawer 类无需 XML / `[Summary]`

### 3. 如需 Processor

**文件**: 与被处理类同一脚本文件（嵌套类）

```csharp
internal sealed class Bilingual{Name}AttributeProcessor : OdinAttributeProcessor<Bilingual{Name}Attribute>
{
    public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
    {
        // 动态注入 Odin 特性
    }
}
```

- Processor 必须 `internal sealed`，与目标类同文件
- 无需 XML / `[Summary]`

### 4. 注册到 AttributeOverviewPro

如果特性需要在 AttributeOverviewPro 中展示，还需创建 Data-Panel-Example 三件套。参见 `Docs~/SKILLS/add-new-odin-drawer.md`。

## 验证

1. 在无 Odin 环境下编译，确认核心程序集无报错
2. 在有 Odin 环境下编译，确认 OdinIntegration 程序集正常
3. 在 Inspector 中测试特性，切换中英文验证双语显示
