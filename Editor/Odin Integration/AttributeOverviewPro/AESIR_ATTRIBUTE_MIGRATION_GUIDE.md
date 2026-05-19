# Aesir Inspector: AttributeOverviewPro 模块的 Example 移植规范

本指南旨在指导如何将旧版案例或其他模块中的案例迁移至 `Aesir Inspector` 内部的 `AttributeOverviewPro` 架构。

移植来源如下：

1. [OdinToolkits 的 AttributeOverviewPro] `Assets/Plugins/Yuumix/OdinToolkits/AttributeOverviewPro`
2. [Yuumix 的 ResolvedParametersOverview] `Assets/Plugins/Yuumix/Community/Modules/ResolvedParametersOverview`
3. [Odin Inspector 官方文档] `https://odininspector.com/attributes`

来源优先级为：优先依据 [OdinToolkits 的 AttributeOverviewPro]，然后依据 [Yuumix 的 ResolvedParametersOverview] 补充被解析字符串参数的信息，最后搜索 [Odin Inspector 官方文档] 官方文档进行验证补充，不因为官方文档而删除案例。

移植目标： [Aesir Inspector 的 Odin Integration 的 AttributeOverviewPro] `Assets/RunLab/Aesir Inspector/Editor/Odin Integration/AttributeOverviewPro`

## 1. 移植流程概览

1. **分析源案例**：识别特性的基本用法、构造函数参数以及支持的 Odin 表达式（Resolved Parameters），**确定是否需要 Odin 序列化**。
2. **创建 ExampleSO**：编写实际展示效果的 ScriptableObject，存放在 `UsageExamples/` 目录下。
3. **定义 AttributeData**：整理特性的元数据、参数说明和解析器信息，存放在 `Data/` 目录下。
4. **创建 PanelSO**：定义分类并关联数据，存放在 `AttributePanels/` 目录下。

## 2. 详细步骤规范

### 2.1 编写 ExampleSO (UsageExamples)
- **基类**: 
  - **Unity 序列化**: 继承 `AttributeExampleSO<T>`。适用于 Unity 原生支持序列化的类型。
  - **Odin 序列化**: 继承 `OdinAttributeExampleSO<T>`。
- **标记**: 类名上方添加 `[AesirExample]`。
- **重置逻辑**: 实现 `AesirInspectorReset()` 方法，用于清空/重置字段。**要求：方法内部严禁添加任何注释。**
- **结构精简**: 
  - 参考 `AssetsOnlyExampleSO.cs`，保持类结构极其精简。
  - 核心目标是让用户快速看到特性对参数的影响，而非复杂的业务逻辑。
  - **分组规范**:
  - **逻辑分组**: 并不是每个字段案例必须位于 `[Title]` 或 `[FoldoutGroup]` 之下。如果多个字段共同构成一个逻辑单元（例如：一个控制变量和受其影响的表达式案例），它们应当被视为一组，共享同一个标题。
  - **强制分组**: 每个字段案例或逻辑组必须位于 `[Title]` 或 `[FoldoutGroup]` 之下。
  - **显示参数**: 分组标签必须明确告知用户该字段正在展示特性的哪个参数（例如：`[Title("Parameter: Path")]`）。
  - **无参数案例**: 如果案例展示的是特性在没有任何参数的情况下的默认用法，分组标签必须使用 `No Parameters`，而**不是** `Default Usage`。
  - **方法签名**: 如果参数引用了方法（如 `CustomValueDrawer` 或 `CustomFilterMethod`），分组标签中还必须补充该方法的参数类型签名（例如：`[Title("Parameter: Action (float value, GUIContent label)")]`）。
  - **选择逻辑**:
    - **组数量 <= 5**: 使用 `[Title("...")]`。
    - **组数量 > 5**: 使用 `[FoldoutGroup("...")]`。
  - **取消双语**: 分组标签（Title/FoldoutGroup）直接使用硬编码字符串，**不再需要**通过 `BilingualData` 定义。
- **特殊案例**: 
  - 针对具有复杂逻辑或需要独立展示效果的特殊参数，应当创建独立的 `ExampleSO`（例如 `AssetListExampleWithCustomFilterMethodSO`）。
  - **字符串解析 (Resolved Parameters)**: 如果一个参数支持字符串解析（表达式），必须为其新增一个单独的 `ExampleSO`。
  - **命名规范**: 特殊案例脚本名称使用类似 `AttributeNameExampleWithParameterNameSO` 的命名。例如展示 `GUIColor` 的 `Color` 参数解析时，命名为 `GUIColorExampleWithColorSO`，而不是使用 `Expression` 等词汇。
  - **简化 UI**: 在这种支持字符串解析的特殊参数案例中，**不再需要** `[FoldoutGroup]`。因为此类案例都在展示同一个参数的不同解析方式，分组已失去区分不同参数的意义。直接使用 `[Title]` 即可。

### 2.2 定义 AttributeData (Data)
- **基类**: 继承 `AbstractAttributeData`。
- **BilingualHeaderControl**: 定义标题、简介和 `OdinInspectorDocumentationLinks` 中的链接。
- **UsageTips**: 提供核心使用建议。
- **AttributeParameters**: 定义特性的构造函数参数。
- **ResolvedStringParameters**: 
  - 识别特性中哪些参数支持 Odin 表达式。
  - 使用 `ResolvedStringParameterValue` 定义解析器类型、返回类型和特殊命名参数（Named Values）。
  - **注意**: `$property` 和 `$root` 已默认包含，无需重复添加。
- **ExamplePreviewItems**: 
  - 使用 `new AttributeExamplePreviewItem().InitializeUnitySerializedExample(...)` 关联 Unity 序列化案例。
  - 使用 `new AttributeExamplePreviewItem().InitializeOdinSerializedExample(...)` 关联 Odin 序列化案例。

### 2.3 创建 PanelSO (AttributePanels)
- **基类**: 继承 `AbstractAttributePanelSO`。
- **分类**: 标记 `[AttributeCategory(AesirAttributeCategory.XXX)]`。
- **初始化**: 在 `Initialize()` 方法中调用 `SetData(new MyAttributeData())`。

### 2.4 何时使用 Odin 序列化？
若特性涉及以下情况，必须使用 `OdinAttributeExampleSO<T>`：
- **多态字段**: 字段类型为接口（`interface`）或抽象类（`abstract class`）时（如 `PolymorphicDrawer` 案例）。
- **Unity 不支持的集合**: 如 `Dictionary<K, V>`。
- **特殊类型**: 如二维数组 `T[,]`。
- **属性序列化**: 需要通过 `[OdinSerialize]` 序列化 C# 属性（Property）而非字段时。

### 2.5 特殊注释规范 (Special Comment Conventions)

- **颜色参数 (Color Parameters)**: 如果参数涉及颜色定义（通常为 `string` 类型），**必须**使用以下统一描述：
  - **英文**: "Supports a variety of color formats, including named colors (e.g. \"red\", \"orange\", \"green\", \"blue\"), hex codes (e.g. \"#FF0000\" and \"#FF0000FF\"), and RGBA (e.g. \"RGBA(1,1,1,1)\") or RGB (e.g. \"RGB(1,1,1)\"), including Odin attribute expressions (e.g \"@this.MyColor\"). Here are the available named colors: black, blue, clear, cyan, gray, green, grey, magenta, orange, purple, red, transparent, transparentBlack, transparentWhite, white, yellow, lightblue, lightcyan, lightgray, lightgreen, lightgrey, lightmagenta, lightorange, lightpurple, lightred, lightyellow, darkblue, darkcyan, darkgray, darkgreen, darkgrey, darkmagenta, darkorange, darkpurple, darkred, darkyellow."
  - **中文**: "支持多种颜色格式，包括命名颜色（例如 \"red\"、\"orange\"、\"green\"、\"blue\"）、十六进制代码（例如 \"#FF0000\" 和 \"#FF0000FF\"）以及 RGBA（例如 \"RGBA(1,1,1,1)\"）或 RGB（例如 \"RGB(1,1,1)\"），包括 Odin 特性表达式（例如 \"@this.MyColor\"）。以下是可用的命名颜色：black, blue, clear, cyan, gray, green, grey, magenta, orange, purple, red, transparent, transparentBlack, transparentWhite, white, yellow, lightblue, lightcyan, lightgray, lightgreen, lightgrey, lightmagenta, lightorange, lightpurple, lightred, lightyellow, darkblue, darkcyan, darkgray, darkgreen, darkgrey, darkmagenta, darkorange, darkpurple, darkred, darkyellow。"

## 3. 命名与路径规范
- **ExampleSO**: `UsageExamples/AttributeNameExampleSO.cs`
- **Data**: `Data/AttributeNameAttributeData.cs`
- **PanelSO**: `AttributePanels/AttributeNamePanelSO.cs`
- **命名空间**: `RunLab.AesirInspector.OdinIntegration.Editor`
- **BilingualData 命名空间**: `RunLab.AesirInspector`

## 4. 联网参考要求

在移植或更新案例时，如果环境允许联网，**必须**访问 [Odin Inspector Attributes](https://odininspector.com/attributes) 官方文档进行交叉验证。
- **目的**: 确保参数名称、默认值和支持的表达式（Resolved Parameters）与官方一致。
- **补充**: 发现官方文档中有而本地案例缺失的重要用法时，应予以补充。
