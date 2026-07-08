# Aesir Inspector: 未完成移植的特性清单

> 基于 [Odin Inspector 官方特性列表](https://odininspector.com/attributes) 与 `AttributePanels/` 目录已有 PanelSO 对比生成。
> 移植规范参见同目录下的 `AESIR_ATTRIBUTE_MIGRATION_GUIDE.md`。

## 统计

| 指标 | 数量 |
|------|------|
| Odin 官方特性（去重） | 108 |
| 已移植 | 108 |
| 未移植 | 0 |
| 完成率 | 100% 🎉 |

---

## 全部 108 项 Odin Inspector 特性已移植完成

所有特性均已按照 `AESIR_ATTRIBUTE_MIGRATION_GUIDE.md` 移植规范完成迁移，每个特性包含完整的三文件结构：

- **ExampleSO** (`UsageExamples/`) — 实际展示效果的 ScriptableObject
- **AttributeData** (`Data/`) — 特性元数据、参数说明和解析器信息
- **PanelSO** (`AttributePanels/`) — 面板定义，关联数据

## 已移植特性完整列表（108 项）

<details>
<summary>点击展开完整列表</summary>

| # | 特性名 | 分类 | PanelSO 文件 |
|---|--------|------|-------------|
| 1 | Asset List | TypeSpecifics | AssetListAttributePanelSO |
| 2 | Asset Selector | TypeSpecifics | AssetSelectorAttributePanelSO |
| 3 | Assets Only | Essentials | AssetsOnlyAttributePanelSO |
| 4 | Box Group | Groups | BoxGroupAttributePanelSO |
| 5 | Button | Buttons | ButtonAttributePanelSO |
| 6 | Button Group | Groups | ButtonGroupAttributePanelSO |
| 7 | Child Game Objects Only | TypeSpecifics | ChildGameObjectsOnlyAttributePanelSO |
| 8 | Color Palette | TypeSpecifics | ColorPaletteAttributePanelSO |
| 9 | Custom Context Menu | Misc | CustomContextMenuAttributePanelSO |
| 10 | Custom Value Drawer | Essentials | CustomValueDrawerAttributePanelSO |
| 11 | Delayed Property | Essentials | DelayedPropertyAttributePanelSO |
| 12 | Detailed Info Box | Essentials | DetailInfoBoxAttributePanelSO |
| 13 | Dictionary Drawer Settings | Collections | DictionaryDrawerSettingsAttributePanelSO |
| 14 | Disable Context Menu | Misc | DisableContextMenuAttributePanelSO |
| 15 | Disable If | Conditionals | DisableIfAttributePanelSO |
| 16 | Disable In | Conditionals | DisableInAttributePanelSO |
| 17 | Disable In Editor Mode | Conditionals | DisableInEditorModeAttributePanelSO |
| 18 | Disable In Inline Editors | Conditionals | DisableInInlineEditorsAttributePanelSO |
| 19 | Disable In Play Mode | Conditionals | DisableInPlayModeAttributePanelSO |
| 20 | Disallow Modifications In | Validation | DisallowModificationsInAttributePanelSO |
| 21 | Display As String | TypeSpecifics | DisplayAsStringAttributePanelSO |
| 22 | Draw With Unity | Misc | DrawWithUnityAttributePanelSO |
| 23 | Enable GUI | Essentials | EnableGUIAttributePanelSO |
| 24 | Enable If | Conditionals | EnableIfAttributePanelSO |
| 25 | Enable In | Conditionals | EnableInAttributePanelSO |
| 26 | Enum Paging | TypeSpecifics | EnumPagingAttributePanelSO |
| 27 | Enum Toggle Buttons | TypeSpecifics | EnumToggleButtonsAttributePanelSO |
| 28 | File Path | TypeSpecifics | FilePathAttributePanelSO |
| 29 | Folder Path | TypeSpecifics | FolderPathAttributePanelSO |
| 30 | Foldout Group | Groups | FoldoutGroupAttributePanelSO |
| 31 | GUIColor | Essentials | GUIColorAttributePanelSO |
| 32 | Hide Duplicate Reference Box | Misc | HideDuplicateReferenceBoxAttributePanelSO |
| 33 | Hide If | Conditionals | HideIfAttributePanelSO |
| 34 | Hide If Group | Conditionals | HideIfGroupAttributePanelSO |
| 35 | Hide In | Conditionals | HideInAttributePanelSO |
| 36 | Hide In Editor Mode | Conditionals | HideInEditorModeAttributePanelSO |
| 37 | Hide In Inline Editors | Conditionals | HideInInlineEditorsAttributePanelSO |
| 38 | Hide In Play Mode | Conditionals | HideInPlayModeAttributePanelSO |
| 39 | Hide In Tables | TypeSpecifics | HideInTablesAttributePanelSO |
| 40 | Hide Label | Essentials | HideLabelAttributePanelSO |
| 41 | Hide Mono Script | TypeSpecifics | HideMonoScriptAttributePanelSO |
| 42 | Hide Network Behaviour Fields | TypeSpecifics | HideNetworkBehaviourFieldsAttributePanelSO |
| 43 | Hide Reference Object Picker | TypeSpecifics | HideReferenceObjectPickerAttributePanelSO |
| 44 | Horizontal Group | Groups | HorizontalGroupAttributePanelSO |
| 45 | Indent | Misc | IndentAttributePanelSO |
| 46 | Info Box | Essentials | InfoBoxAttributePanelSO |
| 47 | Inline Button | Buttons | InlineButtonAttributePanelSO |
| 48 | Inline Editor | TypeSpecifics | InlineEditorAttributePanelSO |
| 49 | Inline Property | Misc | InlinePropertyAttributePanelSO |
| 50 | Label Text | Essentials | LabelTextAttributePanelSO |
| 51 | Label Width | Essentials | LabelWidthAttributePanelSO |
| 52 | List Drawer Settings | Collections | ListDrawerSettingsAttributePanelSO |
| 53 | Max Value | Numbers | MaxValueAttributePanelSO |
| 54 | Min Max Slider | Numbers | MinMaxSliderAttributePanelSO |
| 55 | Min Value | Numbers | MinValueAttributePanelSO |
| 56 | Multi Line Property | TypeSpecifics | MultiLinePropertyAttributePanelSO |
| 57 | Multiline | Unity | MultilineAttributePanelSO |
| 58 | On Collection Changed | Misc | OnCollectionChangedAttributePanelSO |
| 59 | On Inspector Dispose | Misc | OnInspectorDisposeAttributePanelSO |
| 60 | On Inspector GUI | Misc | OnInspectorGUIAttributePanelSO |
| 61 | On Inspector Init | Misc | OnInspectorInitAttributePanelSO |
| 62 | On State Update | Misc | OnStateUpdateAttributePanelSO |
| 63 | On Value Changed | Misc | OnValueChangedAttributePanelSO |
| 64 | Polymorphic Drawer Settings | TypeSpecifics | PolymorphicDrawerSettingsAttributePanelSO |
| 65 | Preview Field | TypeSpecifics | PreviewFieldAttributePanelSO |
| 66 | Progress Bar | Numbers | ProgressBarAttributePanelSO |
| 67 | Property Order | Essentials | PropertyOrderAttributePanelSO |
| 68 | Property Range | Numbers | PropertyRangeAttributePanelSO |
| 69 | Property Space | Essentials | PropertySpaceAttributePanelSO |
| 70 | Property Tooltip | Essentials | PropertyTooltipAttributePanelSO |
| 71 | Range | Unity | RangeAttributePanelSO |
| 72 | Read Only | Essentials | ReadOnlyAttributePanelSO |
| 73 | Required | Validation | RequiredAttributePanelSO |
| 74 | Required In | Validation | RequiredInAttributePanelSO |
| 75 | Required List Length | Validation | RequiredListLengthAttributePanelSO |
| 76 | Responsive Button Group | Groups | ResponsiveButtonGroupAttributePanelSO |
| 77 | Scene Objects Only | TypeSpecifics | SceneObjectsOnlyAttributePanelSO |
| 78 | Searchable | Collections | SearchableAttributePanelSO |
| 79 | Show Drawer Chain | Debug | ShowDrawerChainAttributePanelSO |
| 80 | Show If | Conditionals | ShowIfAttributePanelSO |
| 81 | Show If Group | Conditionals | ShowIfGroupAttributePanelSO |
| 82 | Show In | Conditionals | ShowInAttributePanelSO |
| 83 | Show In Inline Editors | Conditionals | ShowInInlineEditorsAttributePanelSO |
| 84 | Show In Inspector | Essentials | ShowInInspectorAttributePanelSO |
| 85 | Show Property Resolver | Debug | ShowPropertyResolverAttributePanelSO |
| 86 | Space | Unity | SpaceAttributePanelSO |
| 87 | Suppress Invalid Attribute Error | Meta | SuppressInvalidAttributeErrorAttributePanelSO |
| 88 | Suffix Label | Essentials | SuffixLabelAttributePanelSO |
| 89 | Tab Group | Groups | TabGroupAttributePanelSO |
| 90 | Table Column Width | Collections | TableColumnWidthAttributePanelSO |
| 91 | Table List | Collections | TableListAttributePanelSO |
| 92 | Table Matrix | TypeSpecifics | TableMatrixAttributePanelSO |
| 93 | Text Area | Unity | TextAreaAttributePanelSO |
| 94 | Title | Essentials | TitleAttributePanelSO |
| 95 | Title Group | Groups | TitleGroupAttributePanelSO |
| 96 | Toggle | TypeSpecifics | ToggleAttributePanelSO |
| 97 | Toggle Group | Groups | ToggleGroupAttributePanelSO |
| 98 | Toggle Left | TypeSpecifics | ToggleLeftAttributePanelSO |
| 99 | Type Drawer Settings | TypeSpecifics | TypeDrawerSettingsAttributePanelSO |
| 100 | Type Filter | Essentials | TypeFilterAttributePanelSO |
| 101 | Type Info Box | Essentials | TypeInfoBoxAttributePanelSO |
| 102 | Type Registry Item | Misc | TypeRegistryItemAttributePanelSO |
| 103 | Type Selector Settings | Misc | TypeSelectorSettingsAttributePanelSO |
| 104 | Unit | Numbers | UnitAttributePanelSO |
| 105 | Validate Input | Validation | ValidateInputAttributePanelSO |
| 106 | Value Dropdown | Collections | ValueDropdownAttributePanelSO |
| 107 | Vertical Group | Groups | VerticalGroupAttributePanelSO |
| 108 | Wrap | Numbers | WrapAttributePanelSO |

</details>
