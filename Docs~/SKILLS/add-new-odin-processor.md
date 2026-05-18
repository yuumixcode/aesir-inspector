# Skill: 添加新的 OdinAttributeProcessor

> 当需要通过 OdinAttributeProcessor 动态注入特性时，按此指南操作。

## 步骤

### 1. 确定放置位置

Processor 必须 `internal sealed`，与被处理类定义在**同一脚本文件**中：

```csharp
// MyPanelSO.cs 中
public class MyPanelSO : AbstractAttributePanelSO
{
    // 面板类实现...
}

internal sealed class MyPanelSOProcessor : OdinAttributeProcessor<MyPanelSO>
{
    public override void ProcessChildMemberAttributes(
        InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
    {
        // 动态注入特性
    }
}
```

### 2. Processor 规范

- 必须 `internal sealed`
- 与被处理类**同文件**定义
- 无需 XML / `[Summary]`
- 如需 `nameof` 引用私有成员，定义为**嵌套类**（仍 `internal`）

### 3. 常用注入模式

**注入 `[OnInspectorInit]`**：
```csharp
attributes.Add(new OnInspectorInitAttribute("@$property.Parent.Update()"));
```

**注入 `[HideIf]`**：
```csharp
attributes.Add(new HideIfAttribute("FieldNameIsEmpty"));
```

**注入 `[PropertyOrder]`**：
```csharp
attributes.Add(new PropertyOrderAttribute(-100));
```

### 4. 隐藏条件属性命名

- 集合为空：`{FieldName}IsEmpty`
- 对象为空：`{Target}IsNull`

## 验证

1. 编译通过
2. 在 Inspector 中确认特性被正确注入
3. 条件显隐逻辑正常
