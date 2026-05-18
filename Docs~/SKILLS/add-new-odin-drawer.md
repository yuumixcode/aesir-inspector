# Skill: 添加新的 Odin Drawer

> 当需要新增一个 Odin AttributeDrawer 时，按此指南操作。

## 步骤

### 1. 创建 Drawer 文件

**文件**: `Editor/Odin Integration/Drawers/{Name}Drawer.cs`

```csharp
namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    public class {Name}Drawer : OdinAttributeDrawer<{TargetAttribute}>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // Drawer 实现
        }
    }
}
```

### 2. Drawer 规范

- 继承 `OdinAttributeDrawer<TAttribute>` 或 `OdinAttributeDrawer<TAttribute, TValue>`
- 命名：`{Purpose}Drawer`，如 `BilingualTitleDrawer`
- 放置于 `Editor/Odin Integration/Drawers/` 目录
- 无需 XML / `[Summary]`

### 3. 双语支持

如需双语支持，通过 `AesirInspectorLanguageSettingsSO` 获取当前语言：

```csharp
var isZh = AesirInspectorLanguageSettingsSO.CurrentLanguage == Language.Zh;
```

订阅语言变更事件实现动态切换：

```csharp
AesirInspectorLanguageSettingsSO.OnLanguageChanged -= Internal_OnLanguageChanged;
AesirInspectorLanguageSettingsSO.OnLanguageChanged += Internal_OnLanguageChanged;
```

## 验证

1. 编译通过（有 Odin 环境）
2. 在 Inspector 中测试 Drawer 渲染效果
