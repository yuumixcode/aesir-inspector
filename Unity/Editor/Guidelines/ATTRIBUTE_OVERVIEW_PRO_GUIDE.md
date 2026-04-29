# AttributeOverviewPro 编码指南

> **本指南适用于 Aesir Inspector AttributeOverviewPro 模块，与 [Aesir Inspector 编码指南](AESIR_INSPECTOR_CODE_STYLE_GUIDE.md)、[Utilities 编码指南](UTILITIES_GUIDE.md) 互补。**

---

## 目录结构

```
AttributeOverviewPro/
├── Abstract/       抽象基类，定义面板、数据、案例的通用契约
├── Attributes/     自定义特性（AesirExampleAttribute、AttributeCategoryAttribute）
├── AttributePanels/具体特性面板 SO，每个 Odin 特性一个文件
├── Core/           核心数据类型与主窗口
├── Data/           面板数据类，每个 Odin 特性一个文件，描述标题、参数、案例
├── Definitions/    枚举定义与常量链接
├── UsageExamples/  使用案例 SO，展示 Odin 特性的实际用法
└── Utilities/      编辑器工具类
```

---

## Data-Panel-Example 三件套

新增一个 Odin 特性的介绍页面，**必须同时创建三个文件**：

| 层 | 目录 | 文件命名 | 职责 |
|---|---|---|---|
| 数据 | `Data/` | `{AttributeName}AttributeData.cs` | 纯数据，声明标题、参数表、解析字符串参数、案例预览项 |
| 面板 | `AttributePanels/` | `{AttributeName}AttributePanelSO.cs` | 继承 `AbstractAttributePanelSO`，仅 `Initialize()` 中调用 `SetData` |
| 案例 | `UsageExamples/` | `{AttributeName}ExampleSO.cs` | 继承 `AttributeExampleSO<T>` 或 `OdinAttributeExampleSO<T>`，展示特性用法 |

### 数据类

```csharp
internal class AssetListAttributeData : AbstractAttributeData
{
    public override HeaderBilingualWidget HeaderWidget { get; set; } = new HeaderBilingualWidget(...);
    public override BilingualData[] UsageTips { get; set; } = null;           // 无则 null
    public override ParameterValue[] AttributeParameters { get; set; } = ...;  // 无则 null
    public override ResolvedStringParameterValue[] ResolvedStringParameters { get; set; } = ...; // 无则 null
    public override AttributeExamplePreviewItem[] ExamplePreviewItems { get; set; } = ...;
}
```

- 数据类**必须**为 `internal`，不需要 `[Summary]` / `/// <summary>`。
- 不需要的部分返回 `null`，不要返回空数组。

### 面板类

```csharp
[AttributeCategory(AesirAttributeCategory.TypeSpecifics)] // 必须标注分类
public class AssetListAttributePanelSO : AbstractAttributePanelSO
{
    public override void Initialize()
    {
        SetData(new AssetListAttributeData());
    }
}
```

- **必须**标注 `[AttributeCategory]`，用于菜单分类。
- 面板类**仅负责绑定数据**，所有渲染逻辑由 `AbstractAttributePanelSO` 基类 + `OdinAttributeProcessor` 完成。
- **禁止**在面板类中添加 GUI 绘制代码。

### 案例类

```csharp
[AesirExample] // 必须，用于源码预览捕获文件路径
public class AssetListExampleSO : AttributeExampleSO<AssetListExampleSO>
{
    // 使用 Odin 特性的字段...

    public override void AesirInspectorReset() { /* 重置字段 */ }
}
```

- **必须**标注 `[AesirExample]`，编译时自动捕获源文件路径供代码预览使用。
- **必须**实现 `AesirInspectorReset()`，将所有序列化字段恢复默认值。
- Unity 原生序列化继承 `AttributeExampleSO<T>`；Odin 序列化继承 `OdinAttributeExampleSO<T>`。
- 多个案例时，每个案例一个 SO 类。

---

## 单例 ScriptableObject 模式

面板 SO 和案例 SO 均采用泛型单例模式：

```csharp
public static T Instance
{
    get
    {
        if (_asset) return _asset;
        _asset = ScriptableObjectSafeEditorUtility.GetSingletonAssetAndDeleteOther<T>(
            AesirInspectorPaths.AttributePanelsPath);
        return _asset;
    }
}
```

- 使用 `ScriptableObjectSafeEditorUtility.GetSingletonAssetAndDeleteOther<T>` 自动创建资产并清理重复项。
- 静态字段 `_asset` 为私有，命名统一为 `_asset`。
- **禁止**直接使用 `CreateInstance` + `AssetDatabase.CreateAsset` 创建面板/案例 SO，统一走单例入口。

---

## IAesirInspectorReset

所有面板 SO 和案例 SO **必须**实现 `IAesirInspectorReset`：

```csharp
public override void AesirInspectorReset()
{
    // 将所有序列化字段恢复为默认值
    // 清空缓存（如高度缓存 Dictionary）
    // 恢复 currentSelectedExample 为初始案例
}
```

- 重置方法中**必须清空性能缓存**（如 `_heightCache = new Dictionary<string, float>()`）。
- `AbstractAttributePanelSO.AesirInspectorReset()` 已包含基类缓存清理，子类调用 `base` 后补充自身字段。

---

## OdinAttributeProcessor 动态注入

`AbstractAttributePanelSO` 使用 `OdinAttributeProcessor<T>` 动态注入 Odin 特性，**禁止**在面板类上手动添加以下特性：

| 成员 | 自动注入的特性 | 作用 |
|---|---|---|
| `Initialize()` | `[OnInspectorInit]` `[PropertyOrder(-1000)]` | 面板选中时自动初始化 |
| `headerWidget` | `[PropertyOrder(-100)]` `[PropertySpace]` | 顶部控件置顶 |
| `DrawUsageTips` | `[HideIf("UsageTipIsEmpty")]` `[OnInspectorGUI]` `[PropertyOrder(-60)]` | 条件显示使用提示 |
| `DrawAttributeParameters` | `[HideIf("AttributeParameterIsEmpty")]` `[OnInspectorGUI]` `[PropertyOrder(-20)]` | 条件显示参数表 |
| `DrawResolvedStringParameters` | `[HideIf("ResolvedStringParametersIsEmpty")]` `[OnInspectorGUI]` `[PropertyOrder(-10)]` | 条件显示解析参数 |
| `DrawUsageExamplePreview` | `[HideIf("UsageExampleItemsIsEmpty")]` `[OnInspectorGUI]` `[PropertyOrder(-1)]` | 条件显示案例 |
| `currentSelectedExample` | `[HideIf]` `[InlineEditor]` `[PropertyOrder(0)]` | 内联编辑当前案例 |
| `EndDrawUsageExampleContainer` | `[OnInspectorGUI]` `[PropertyOrder(100)]` | 案例区域尾部 |
| `DrawCurrentExampleCodePreview` | `[HideIf("CurrentExampleIsNull")]` `[OnInspectorGUI]` `[PropertyOrder(150)]` | 代码预览 |

- Processor 类**必须**为 `internal sealed`，与被处理类定义在**同一脚本文件**中。
- 无需 XML / `[Summary]` 注释。

### 隐藏条件属性命名

用于 `[HideIf]` 判断的布尔属性，命名规则：

- **集合为空**：`{FieldName}IsEmpty`，如 `UsageTipIsEmpty`、`AttributeParameterIsEmpty`。
- **对象为空**：`{Target}IsNull`，如 `CurrentExampleIsNull`。

---

## 双语系统

### 静态标签

所有 UI 标签使用 `static readonly BilingualData` 声明，放在使用它的 `#region` 块顶部：

```csharp
static readonly BilingualData _usageTipsLabel = new BilingualData("使用提示", "Usage Tips");
static readonly BilingualData _codePreviewLabel = new BilingualData("代码预览", "Code Preview");
```

- 私有标签以下划线开头：`_usageTipsLabel`。
- 公共/跨类标签使用 PascalCase：`ResolverTypeLabel`。
- 标签声明紧跟在所属 `#region` 的第一行。

### HeaderBilingualWidget

面板顶部说明控件，构造参数顺序：中文名、英文名、中文描述、英文描述、文档链接。

```csharp
new HeaderBilingualWidget(
    "AssetList", "AssetList",
    "AssetList 特性可以用于...",    // 中文描述
    "AssetList is used on...",       // 英文描述
    OdinInspectorDocumentationLinks.AssetListUrl);
```

### 语言变更响应

数据绑定后**必须**订阅语言变更事件，重建所有 GUITable：

```csharp
AesirInspectorLanguageSettingsSO.OnLanguageChanged -= Internal_OnLanguageChanged;
AesirInspectorLanguageSettingsSO.OnLanguageChanged += Internal_OnLanguageChanged;
```

在 `OnDestroy` 中**必须**取消订阅。

---

## GUITable 与性能缓存

### 高度缓存

GUITable 行高基于文本内容动态计算，**必须**使用缓存避免每帧重复计算：

```csharp
Dictionary<string, float> _textHeightCache = new Dictionary<string, float>();

static float Internal_GetCachedTextHeight(string text, float width, Dictionary<string, float> cache)
{
    var key = text + "_" + width;
    if (cache.TryGetValue(key, out var height)) return height;
    height = style.CalcHeight(GUIHelper.TempContent(text), width);
    cache[key] = height;
    return height;
}
```

- 缓存键格式：`{text}_{width}`。
- `AesirInspectorReset()` 中**必须**重建缓存实例。

### 表格创建与调整

```csharp
void Internal_CreateTable()  // 创建 GUITable 实例
void Internal_ResizeTable()   // 根据缓存高度设置行高并 ReCalculateSizes
```

- 创建与调整**分离**，语言变更时只需重建，不需要重新调整。
- 行高 = `Mathf.Max(各列高度) + 10f`（留出内边距）。

### 懒加载 GUIStyle

所有自定义 GUIStyle 使用 `??=` 懒加载，并在进入 Play Mode 时重置：

```csharp
public static GUIStyle ContainerTitleStyle
{
    get { _containerTitleStyle ??= new GUIStyle(SirenixGUIStyles.TitleCentered) { fontSize = 16 }; }
}

[InitializeOnEnterPlayMode]
static void Internal_ResetStyles()
{
    _containerTitleStyle = null;
    // ... 重置所有静态 GUIStyle
}
```

---

## 特性分类

每个面板 SO **必须**使用 `[AttributeCategory]` 标注分类：

```csharp
[AttributeCategory(AesirAttributeCategory.TypeSpecifics)]
// 多分类：
[AttributeCategory(AesirAttributeCategory.Essentials | AesirAttributeCategory.Validation)]
```

| 分类 | 含义 |
|---|---|
| `Essentials` | 基础常用 |
| `Buttons` | 按钮相关 |
| `Collections` | 集合/列表 |
| `Groups` | 分组布局 |
| `Conditionals` | 条件显隐 |
| `Numbers` | 数值滑条 |
| `TypeSpecifics` | 类型特定 |
| `Validation` | 验证约束 |
| `Misc` | 其他 |
| `Meta` | 元特性 |
| `Unity` | Unity 特定 |
| `Debug` | 调试相关 |

分类判断使用 `HasFlagFast` 而非 `HasFlag`，避免装箱：

```csharp
attr.Category.HasFlagFast(AesirAttributeCategory.Essentials)
```

---

## 文档链接

所有 Odin 特性文档链接**必须**集中在 `OdinInspectorDocumentationLinks` 类中，定义为 `public const string`：

```csharp
public static class OdinInspectorDocumentationLinks
{
    public const string AssetListUrl = "https://odininspector.com/attributes/asset-list-attribute";
}
```

**禁止**在 Data 类中硬编码 URL。

---

## 代码预览

`[AesirExample]` 特性使用 `CallerFilePath` 在编译时捕获源文件路径：

```csharp
[AttributeUsage(AttributeTargets.Class)]
public class AesirExampleAttribute : Attribute
{
    public AesirExampleAttribute([CallerFilePath] string filePath = "unknown") =>
        FilePath = filePath;
}
```

- **禁止**手动传递 `filePath` 参数。
- 代码预览读取源文件后，自动移除 `namespace` 包裹与 `[AesirExample]` 标注行。

---

## 数据库

`AttributeOverviewDatabaseSO` 负责发现所有面板并构建菜单映射：

- 使用 `TypeCache.GetTypesDerivedFrom<AbstractAttributePanelSO>()` 发现面板类型。
- 使用 `AssetDatabase.FindAssets` 查找已有资产，缺失时自动创建。
- `AttributePanelArrayMap`：按分类名分组的面板数组，用于菜单构建。
- `AttributePanelMap`：`"分类/中文名"` → 面板实例，用于 `OdinMenuTree`。

**禁止**手动注册面板到数据库，新面板只需继承 `AbstractAttributePanelSO` 并标注 `[AttributeCategory]`，数据库会自动发现。

---

## 案例预览项

```csharp
new AttributeExamplePreviewItem().InitializeUnitySerializedExample("名称", XxxExampleSO.Instance)
new AttributeExamplePreviewItem().InitializeOdinSerializedExample("名称", XxxExampleSO.Instance)
```

- 使用链式初始化，不要直接构造后赋值。
- Unity 序列化用 `InitializeUnitySerializedExample`，Odin 序列化用 `InitializeOdinSerializedExample`。
- 多个案例时，首个案例为默认选中项。

---

## 事件订阅规范

```csharp
// 订阅前先取消订阅，防止重复
AesirInspectorLanguageSettingsSO.OnLanguageChanged -= Internal_OnLanguageChanged;
AesirInspectorLanguageSettingsSO.OnLanguageChanged += Internal_OnLanguageChanged;

// OnDestroy 中取消订阅
void OnDestroy()
{
    AesirInspectorLanguageSettingsSO.OnLanguageChanged -= Internal_OnLanguageChanged;
}
```

- **必须**先 `-=` 再 `+=`，确保不重复订阅。
- **必须**在 `OnDestroy` / `OnDisable` 中取消订阅。

---

## 命名速查

| 标识符 | 规则 | 示例 |
|---|---|---|
| 面板 SO | `{AttributeName}AttributePanelSO` | `AssetListAttributePanelSO` |
| 数据类 | `{AttributeName}AttributeData` | `AssetListAttributeData` |
| 案例 SO | `{AttributeName}ExampleSO` | `AssetListExampleSO` |
| 双语标签（私有） | `_camelCase` + `Label` 后缀 | `_usageTipsLabel` |
| 双语标签（公共） | `PascalCase` + `Label` 后缀 | `ResolverTypeLabel` |
| 隐藏条件 | `{Target}IsEmpty` / `{Target}IsNull` | `UsageTipIsEmpty`, `CurrentExampleIsNull` |
| 内部方法 | `Internal_{Purpose}` | `Internal_CreateUsageTipsTable` |
| 绘制方法 | `Draw{Component}` | `DrawUsageTips`, `DrawAttributeParameters` |
| 缓存字段 | `_camelCase` + `Cache` 后缀 | `_textHeightCache` |
