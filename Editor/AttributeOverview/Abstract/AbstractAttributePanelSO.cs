// ----------------------------------------------------------------------------
// MIT License
//
// Copyright (c) 2026 RunLab - Yuumix
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// ----------------------------------------------------------------------------

#if UNITY_EDITOR && ODIN_INSPECTOR_3_3

namespace RunLab.AesirInspector
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Sirenix.OdinInspector;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    /// 特性面板 SO 泛型单例基类，继承自 SerializedScriptableObject。
    /// </summary>
    [Summary("特性面板 SO 泛型单例基类，继承自 SerializedScriptableObject")]
    public abstract class AttributeOverviewPanelSO<T> : SerializedScriptableObject, IAesirInspectorReset
        where T : AttributeOverviewPanelSO<T>
    {
        static T _asset;

        /// <summary>
        /// 获取单例实例，若不存在则自动创建。
        /// </summary>
        [Summary("获取单例实例，若不存在则自动创建")]
        public static T Instance
        {
            get
            {
                if (_asset)
                {
                    return _asset;
                }

                _asset = ScriptableObjectSafeEditorUtility
                    .GetSingletonAssetAndDeleteOther<T>(AesirInspectorPaths.AttributePanelsPath);
                return _asset;
            }
        }

        /// <summary>
        /// 重置面板状态。
        /// </summary>
        [Summary("重置面板状态")]
        public abstract void AesirInspectorReset();
    }

    /// <summary>
    /// 特性介绍面板抽象基类，负责渲染顶部控件、使用提示、参数表、案例预览与代码预览。
    /// </summary>
    [Summary("特性介绍面板抽象基类，负责渲染顶部控件、使用提示、参数表、案例预览与代码预览")]
    public abstract class AbstractAttributePanelSO : AttributeOverviewPanelSO<AbstractAttributePanelSO>
    {
        const float AfterSpace = 20f;
        static BilingualData _guiTableNumberLabel = new BilingualData("序号", "Number");

        [SerializeField]
        HeaderBilingualWidget headerWidget;

        AbstractAttributeData _data;

        /// <summary>
        /// 顶部说明控件引用。
        /// </summary>
        [Summary("顶部说明控件引用")]
        public HeaderBilingualWidget HeaderWidget => headerWidget;

        #region --- Public Methods ---

        /// <summary>
        /// 初始化面板，子类中调用 SetData 完成数据绑定。
        /// </summary>
        [Summary("初始化面板，子类中调用 SetData 完成数据绑定")]
        public abstract void Initialize();

        /// <summary>
        /// 重置面板至初始状态。
        /// </summary>
        [Summary("重置面板至初始状态")]
        public override void AesirInspectorReset()
        {
            _isShowShortenCodePreview = false;
            _usageTipsTextHeightCache = new Dictionary<string, float>();
            _attributeParameterTextHeightCache = new Dictionary<string, float>();
            if (_data != null)
            {
                currentSelectedExample = _data.GetInitialExample();
            }

            if (_examplePreviewItems is { Length: > 0 })
            {
                foreach (var item in _examplePreviewItems)
                {
                    item.Reset();
                }
            }
        }

        #endregion

        #region Internal

        void OnDestroy()
        {
            AesirInspectorLanguageSettings.LanguageChanged -= Internal_OnLanguageChanged;
        }

        void Internal_SetData(AbstractAttributeData attributeData)
        {
            _data = attributeData;
            headerWidget = _data.HeaderWidget;
            _usageTips = _data.UsageTips;
            if (_usageTips != null)
            {
                Internal_CreateUsageTipsTable();
                if (Event.current != null)
                {
                    Internal_ResizeUsageTipsTable();
                }
            }

            _attributeParameters = _data.AttributeParameters;
            if (_attributeParameters != null)
            {
                Internal_CreateAttributeParametersTable();
                if (Event.current != null)
                {
                    Internal_ResizeAttributeParameterTable();
                }
            }

            _resolvedStringParameters = _data.ResolvedStringParameters;
            _examplePreviewItems = _data.ExamplePreviewItems;
            if (_examplePreviewItems != null)
            {
                currentSelectedExample = _data.GetInitialExample();
                _currentExampleSourceCode =
                    AttributeOverviewEditorUtility.GetExampleSourceCodeWithoutNamespace(
                        Internal_MarkExampleAttribute);
            }

            AesirInspectorLanguageSettings.LanguageChanged -= Internal_OnLanguageChanged;
            AesirInspectorLanguageSettings.LanguageChanged += Internal_OnLanguageChanged;
        }

        void Internal_OnLanguageChanged()
        {
            if (_usageTips != null)
            {
                Internal_CreateUsageTipsTable();
                if (Event.current != null)
                {
                    Internal_ResizeUsageTipsTable();
                }
            }

            if (_attributeParameters != null)
            {
                Internal_CreateAttributeParametersTable();
                if (Event.current != null)
                {
                    Internal_ResizeAttributeParameterTable();
                }
            }

            if (_resolvedStringParameters != null)
            {
                foreach (var rValue in _resolvedStringParameters)
                {
                    rValue.CreateResolverInfoTable();
                    rValue.CreateNamedValueTable();
                    if (Event.current != null)
                    {
                        rValue.ResizeAllTables();
                    }
                }
            }
        }

        #endregion

        #region Usage Tips

        static BilingualData _usageTipsLabel = new BilingualData("使用提示", "Usage Tips");
        Rect _usageTipContentRect;
        BilingualData[] _usageTips;
        GUITable _usageTipsTable;
        Dictionary<string, float> _usageTipsTextHeightCache = new Dictionary<string, float>();

        bool UsageTipIsEmpty => _usageTips == null || _usageTips.Length == 0;

        void DrawUsageTips()
        {
            _usageTipContentRect = Internal_BeginDrawContainerWithTitle(_usageTipsLabel, out _);
            _usageTipsTable.DrawTable();
            Internal_ResizeUsageTipsTable();
            Internal_EndDrawContainerWithTitle(_usageTipContentRect);
        }

        void Internal_CreateUsageTipsTable()
        {
            _usageTipsTable = GUITable.Create(_usageTips, null, new GUITableColumn
            {
                ColumnTitle = _guiTableNumberLabel,
                Width = 60,
                OnGUI = (rect, index) =>
                {
                    EditorGUI.LabelField(rect, (index + 1).ToString(),
                        AttributeOverviewEditorUtility.TableCellTextStyle);
                }
            }, new GUITableColumn
            {
                ColumnTitle = _usageTipsLabel,
                MinWidth = 200,
                OnGUI = (rect, index) =>
                {
                    EditorGUI.LabelField(rect, _usageTips[index],
                        AttributeOverviewEditorUtility.TableCellTextStyle);
                }
            });
        }

        void Internal_ResizeUsageTipsTable()
        {
            var tableRowHeight = new int[_usageTipsTable.RowCount];
            for (var row = 0; row < _usageTipsTable.RowCount; row++)
            {
                for (var col = 0; col < _usageTipsTable.ColumnCount; col++)
                {
                    var width = _usageTipsTable[col, row].Rect.width;
                    if (row == 0)
                    {
                        tableRowHeight[0] = (int)Internal_GetCachedTextHeight(_usageTipsLabel, width,
                            _usageTipsTextHeightCache);
                    }
                    else
                    {
                        tableRowHeight[row] = (int)Internal_GetCachedTextHeight(_usageTips[row - 1], width,
                            _usageTipsTextHeightCache);
                    }
                }

                _usageTipsTable[0, row].Height = tableRowHeight[row] + 10f;
            }

            _usageTipsTable.ReCalculateSizes();
        }

        #endregion

        #region Attribute Parameters

        static BilingualData _attributeParametersTitleLabel =
            new BilingualData("特性参数", "Attribute Parameters");

        static BilingualData _attributeParameterReturnTypeLabel =
            new BilingualData("返回值类型", "Return Type");

        static BilingualData _attributeParameterParamNameLabel =
            new BilingualData("参数名", "Parameter Name");

        static BilingualData _attributeParameterParamDescriptionLabel =
            new BilingualData("参数描述", "Parameter Description");

        ParameterValue[] _attributeParameters;
        Rect _attributeParametersContentRect;
        GUITable _attributeParametersTable;
        Dictionary<string, float> _attributeParameterTextHeightCache = new Dictionary<string, float>();

        bool AttributeParameterIsEmpty => _attributeParameters == null || _attributeParameters.Length == 0;

        void DrawAttributeParameters()
        {
            _attributeParametersContentRect =
                Internal_BeginDrawContainerWithTitle(_attributeParametersTitleLabel, out _);
            _attributeParametersTable.DrawTable();
            Internal_ResizeAttributeParameterTable();
            Internal_EndDrawContainerWithTitle(_attributeParametersContentRect);
        }

        void Internal_CreateAttributeParametersTable()
        {
            _attributeParametersTable = GUITable.Create(_attributeParameters, null, new GUITableColumn
            {
                ColumnTitle = _guiTableNumberLabel,
                Width = 60,
                OnGUI = (rect, index) =>
                {
                    EditorGUI.LabelField(rect, (index + 1).ToString(),
                        AttributeOverviewEditorUtility.TableCellTextStyle);
                }
            }, new GUITableColumn
            {
                ColumnTitle = _attributeParameterReturnTypeLabel,
                Width = 140,
                OnGUI = (rect, index) =>
                {
                    EditorGUI.LabelField(rect, _attributeParameters[index].ReturnType,
                        AttributeOverviewEditorUtility.TableCellTextStyle);
                }
            }, new GUITableColumn
            {
                ColumnTitle = _attributeParameterParamNameLabel,
                MinWidth = 140,
                OnGUI = (rect, index) =>
                {
                    EditorGUI.LabelField(rect, _attributeParameters[index].ParameterName,
                        AttributeOverviewEditorUtility.TableCellTextStyle);
                }
            }, new GUITableColumn
            {
                ColumnTitle = _attributeParameterParamDescriptionLabel,
                MinWidth = 200,
                OnGUI = (rect, index) =>
                {
                    EditorGUI.LabelField(rect, _attributeParameters[index].GetDescription(),
                        AttributeOverviewEditorUtility.TableCellTextStyle);
                }
            });
        }

        void Internal_ResizeAttributeParameterTable()
        {
            var tableRowHeight = new int[_attributeParametersTable.RowCount];
            for (var row = 0; row < _attributeParametersTable.RowCount; row++)
            {
                for (var col = 0; col < _attributeParametersTable.ColumnCount; col++)
                {
                    var width = _attributeParametersTable[col, row].Rect.width;
                    if (row == 0)
                    {
                        tableRowHeight[0] = (int)Mathf.Max(
                            Internal_GetCachedTextHeight(_attributeParameterReturnTypeLabel, width,
                                _attributeParameterTextHeightCache),
                            Internal_GetCachedTextHeight(_attributeParameterParamNameLabel, width,
                                _attributeParameterTextHeightCache),
                            Internal_GetCachedTextHeight(_attributeParameterParamDescriptionLabel, width,
                                _attributeParameterTextHeightCache));
                    }
                    else
                    {
                        tableRowHeight[row] = (int)Mathf.Max(
                            Internal_GetCachedTextHeight(_attributeParameters[row - 1].ReturnType, width,
                                _attributeParameterTextHeightCache),
                            Internal_GetCachedTextHeight(_attributeParameters[row - 1].ParameterName, width,
                                _attributeParameterTextHeightCache),
                            Internal_GetCachedTextHeight(_attributeParameters[row - 1].GetDescription(),
                                width, _attributeParameterTextHeightCache));
                    }

                    if (Event.current != null)
                    {
                        SirenixEditorGUI.DrawBorders(_attributeParametersTable[col, row].Rect, 1,
                            Color.clear);
                    }
                }

                _attributeParametersTable[0, row].Height = tableRowHeight[row] + 10f;
            }

            _attributeParametersTable.ReCalculateSizes();
        }

        #endregion

        #region Resolved String Parameters

        static BilingualData _resolvedStringParameterLabel =
            new BilingualData("被解析的字符串参数", "Resolved String Parameters");

        ResolvedStringParameterValue[] _resolvedStringParameters;
        Rect _resolvedStringParametersContentRect;

        bool ResolvedStringParametersIsEmpty =>
            _resolvedStringParameters == null || _resolvedStringParameters.Length == 0;

        void DrawResolvedStringParameters()
        {
            _resolvedStringParametersContentRect =
                Internal_BeginDrawContainerWithTitle(_resolvedStringParameterLabel, out _);
            SirenixEditorGUI.BeginVerticalList(false);
            foreach (var resolvedString in _resolvedStringParameters)
            {
                SirenixEditorGUI.BeginListItem(false);
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(8);
                EditorGUILayout.BeginVertical();
                GUILayout.Space(5);
                GUILayout.Label(resolvedString.ParameterName,
                    AttributeOverviewEditorUtility.ResolvedStringParameterValueTitleStyle);
                GUILayout.Space(5);
                SirenixEditorGUI.HorizontalLineSeparator(new Color(1, 1, 1, 0.4f));
                GUILayout.Space(10);
                resolvedString.ResolverInfoTable.DrawTable();
                GUILayout.Space(10);
                resolvedString.NamedValueTable.DrawTable();
                resolvedString.ResizeAllTables();
                GUILayout.Space(8);
                EditorGUILayout.EndVertical();
                GUILayout.Space(5);
                EditorGUILayout.EndHorizontal();
                SirenixEditorGUI.EndListItem();
            }

            SirenixEditorGUI.EndVerticalList();
            Internal_EndDrawContainerWithTitle(_resolvedStringParametersContentRect);
        }

        #endregion

        #region Usage Example

        static BilingualData _usageExampleLabel = new BilingualData("使用案例预览", "Usage Examples");

        static BilingualData _pingMonoScriptButtonLabel =
            new BilingualData("Ping 脚本文件", "Ping Script File");

        static BilingualData _resetExampleButtonLabel = new BilingualData("重置案例", "Reset Example");

        AttributeExamplePreviewItem[] _examplePreviewItems;
        Rect _usageExampleContentRect;
        Rect _usageHeaderToolbarRect;
        const int ExampleNumberOneRow = 3;

        bool UsageExampleItemsIsEmpty => _examplePreviewItems == null || _examplePreviewItems.Length == 0;

        [SerializeField]
        ScriptableObject currentSelectedExample;

        void DrawUsageExamplePreview()
        {
            _usageExampleContentRect =
                Internal_BeginDrawContainerWithTitle(_usageExampleLabel, out var headerToolbarRect);
            _usageHeaderToolbarRect = headerToolbarRect;
            Internal_DrawExamplePreviewItems();
        }

        void EndDrawUsageExampleContainer()
        {
            Internal_EndDrawContainerWithTitle(_usageExampleContentRect);
            Internal_DrawUsageExampleTitleButton();
        }

        AesirExampleAttribute Internal_MarkExampleAttribute => !currentSelectedExample
            ? null
            : AttributeOverviewEditorUtility.GetAttributeInExampleType(currentSelectedExample.GetType());

        void Internal_DrawUsageExampleTitleButton()
        {
            var headerButtonRect = _usageHeaderToolbarRect.AlignCenterY(_usageHeaderToolbarRect.height)
                .AlignRight(240);
            var leftButtonRect = headerButtonRect.Split(0, 2);
            var pingTexture =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.HandIndexFill, Color.white, 20, 20, 0);
            if (GUI.Button(leftButtonRect,
                    GUIHelper.TempContent(" " + _pingMonoScriptButtonLabel, pingTexture),
                    SirenixGUIStyles.ToolbarButton))
            {
                EditorGUIUtility.PingObject(Internal_GetCurrentExampleMonoScript());
            }

            var rightButtonRect = headerButtonRect.Split(1, 2);
            var resetTexture =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.ArrowClockwise, Color.white, 20, 20, 0);
            if (GUI.Button(rightButtonRect,
                    GUIHelper.TempContent(" " + _resetExampleButtonLabel, resetTexture),
                    SirenixGUIStyles.ToolbarButton))
            {
                if (currentSelectedExample is IAesirInspectorReset canReset)
                {
                    canReset.AesirInspectorReset();
                    AttributeOverviewEditorUtility.LogEditorResetSuccess(
                        currentSelectedExample.GetType().Name);
                }
                else
                {
                    AttributeOverviewEditorUtility.LogEditorResetWarning(
                        currentSelectedExample.GetType().Name);
                }
            }
        }

        Object Internal_GetCurrentExampleMonoScript()
        {
            if (!currentSelectedExample)
            {
                return null;
            }

            var monoScriptAbsolutePath = Internal_MarkExampleAttribute.FilePath;
            var assetRelativePath =
                "Assets/" + PathUtilities.MakeRelative(Application.dataPath, monoScriptAbsolutePath);
            return AssetDatabase.LoadAssetAtPath<Object>(assetRelativePath);
        }

        void Internal_DrawExamplePreviewItems()
        {
            if (_examplePreviewItems is not { Length: > 1 })
            {
                return;
            }

            EditorGUILayout.BeginVertical();
            for (var i = 0; i < _examplePreviewItems.Length; i += 3)
            {
                EditorGUILayout.BeginHorizontal();
                for (var j = 0; j < ExampleNumberOneRow && i + j < _examplePreviewItems.Length; j++)
                {
                    Internal_DrawExampleTabButton(_examplePreviewItems[i + j]);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            GUILayout.Space(10f);
        }

        void Internal_DrawExampleTabButton(AttributeExamplePreviewItem item)
        {
            var content = GUIHelper.TempContent(" " + item.ItemName,
                GUIHelper.GetAssetThumbnail(null, typeof(MonoBehaviour), false));
            var iconSizeBackup = EditorGUIUtility.GetIconSize();
            EditorGUIUtility.SetIconSize(new Vector2(16f, 16f));
            var rect = GUILayoutUtility.GetRect(content,
                AttributeOverviewEditorUtility.TabButtonCellTextStyle, GUILayoutOptions.Height(26));
            SirenixEditorGUI.DrawBorders(rect, 1);
            var selectExample = item.ExampleType == AttributeExampleType.OdinSerialized
                ? item.OdinSerializedExample
                : item.UnitySerializedExample;
            if (selectExample == currentSelectedExample)
            {
                var color = EditorGUIUtility.isProSkin
                    ? new Color(0.25f, 0.4f, 0.6f, 1f)
                    : new Color(0.7f, 0.8f, 0.9f, 1f);
                var innerRect = new Rect(rect.x + 1f, rect.y + 1f, rect.width - 2f, rect.height - 2f);
                EditorGUI.DrawRect(innerRect, color);
            }

            if (GUI.Button(rect, GUIContent.none, GUIStyle.none))
            {
                currentSelectedExample = selectExample;
                _currentExampleSourceCode =
                    AttributeOverviewEditorUtility.GetExampleSourceCodeWithoutNamespace(
                        Internal_MarkExampleAttribute);
                _currentExampleShortenCode =
                    AttributeOverviewEditorUtility.GetExampleShortenCode(_currentExampleSourceCode);
            }

            if (currentSelectedExample != selectExample && rect.Contains(Event.current.mousePosition))
            {
                GUIHelper.PushColor(new Color(1f, 1f, 1f, 0.4f));
                var hoverInnerRect = new Rect(rect.x + 1f, rect.y + 1f, rect.width - 2f,
                    rect.height - 2f);
                EditorGUI.DrawRect(hoverInnerRect, SirenixGUIStyles.DarkEditorBackground);
                GUIHelper.PopColor();
            }

            var labelStyle = EditorGUIUtility.isProSkin
                ? SirenixGUIStyles.WhiteLabelCentered
                : SirenixGUIStyles.LabelCentered;
            GUI.Label(rect, content, labelStyle);
            EditorGUIUtility.SetIconSize(iconSizeBackup);
        }

        #endregion

        #region Code Preview

        static BilingualData _codePreviewLabel = new BilingualData("代码预览", "Code Preview");

        static BilingualData _viewFullCodeButtonLabel =
            new BilingualData("查看完整代码", "View Full Code");

        static BilingualData _viewShortenCodeButtonLabel =
            new BilingualData("查看简化代码", "View Shorten Code");

        static BilingualData _copyCodeButtonLabel = new BilingualData("拷贝代码", "Copy Code");

        bool CurrentExampleIsNull => !currentSelectedExample;
        bool _isShowShortenCodePreview;
        Vector2 _scrollPosition;
        string _currentExampleSourceCode;
        string _currentExampleShortenCode;
        const int CodeAreaWidth = 750;

        void DrawCurrentExampleCodePreview()
        {
            Internal_DrawContainerWithTitle(_codePreviewLabel, Internal_DrawCodePreview,
                out var headerToolBarRect);
            SirenixEditorGUI.DrawBorders(headerToolBarRect, 1, Color.clear);
            var showSwitchButtonRect = headerToolBarRect.AlignLeft(140f).AddXMin(1f);
            var viewFullCodeTexture =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.Fullscreen, Color.white, 20, 20, 0);
            var viewShortenCodeTexture =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.FullscreenExit, Color.white, 20, 20, 0);
            if (_isShowShortenCodePreview)
            {
                if (GUI.Button(showSwitchButtonRect,
                        GUIHelper.TempContent(" " + _viewFullCodeButtonLabel, viewFullCodeTexture),
                        SirenixGUIStyles.ToolbarButton))
                {
                    _isShowShortenCodePreview = false;
                }
            }
            else
            {
                if (GUI.Button(showSwitchButtonRect,
                        GUIHelper.TempContent(" " + _viewShortenCodeButtonLabel, viewShortenCodeTexture),
                        SirenixGUIStyles.ToolbarButton))
                {
                    _isShowShortenCodePreview = true;
                }
            }

            var copyButtonRect = headerToolBarRect.AlignRight(100f);
            var copyCodeTexture =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.Stack, Color.white, 20, 20, 0);
            if (GUI.Button(copyButtonRect,
                    GUIHelper.TempContent(" " + _copyCodeButtonLabel, copyCodeTexture),
                    SirenixGUIStyles.ToolbarButton))
            {
                EditorGUIUtility.systemCopyBuffer = _isShowShortenCodePreview
                    ? _currentExampleShortenCode
                    : _currentExampleSourceCode;
            }
        }

        void Internal_DrawCodePreview()
        {
            EditorGUILayout.BeginVertical();
            var highlighterCode = AesirCodeHighlighter.ApplyHighlighting(_currentExampleSourceCode);
            if (_isShowShortenCodePreview)
            {
                highlighterCode =
                    AttributeOverviewEditorUtility.GetExampleShortenCode(highlighterCode);
            }

            var calcHeight =
                AttributeOverviewEditorUtility.CodeTextEditorStyle.CalcHeight(
                    GUIHelper.TempContent(highlighterCode), CodeAreaWidth - 20f);
            const float MaxScrollViewHeight = 600f;
            var actualHeight = Mathf.Min(calcHeight + 30f, MaxScrollViewHeight);
            var scrollViewRect = EditorGUILayout.GetControlRect(false, actualHeight);
            SirenixEditorGUI.DrawSolidRect(scrollViewRect, AesirCodeHighlighter.BackgroundColor);
            SirenixEditorGUI.DrawBorders(scrollViewRect, 1, Color.clear);
            _scrollPosition = GUI.BeginScrollView(scrollViewRect, _scrollPosition,
                new Rect(0, 0, CodeAreaWidth - 20f, calcHeight + 20f), false, false);
            var contentRect = new Rect(10f, 10f, CodeAreaWidth - 30f, calcHeight);
            EditorGUI.SelectableLabel(contentRect, highlighterCode,
                AttributeOverviewEditorUtility.CodeTextEditorStyle);
            GUI.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        #endregion

        #region Draw Container Helpers

        static Rect Internal_BeginDrawContainerWithTitle(string title, out Rect headerToolBarRect)
        {
            var titleStyle = AttributeOverviewEditorUtility.ContainerTitleStyle;
            var titleWidth = titleStyle.CalcSize(GUIHelper.TempContent(title)).x;
            var titleHeight = titleStyle.CalcSize(GUIHelper.TempContent(title)).y;
            headerToolBarRect = SirenixEditorGUI.BeginHorizontalToolbar(titleHeight + 12f);
            var titleRect = headerToolBarRect.AlignCenter(titleWidth);
            EditorGUI.LabelField(titleRect, title, titleStyle);
            GUILayout.FlexibleSpace();
            SirenixEditorGUI.EndHorizontalToolbar();
            GUILayout.Space(-2);
            return EditorGUILayout.BeginVertical(AttributeOverviewEditorUtility.ContainerContentStyle);
        }

        static void Internal_EndDrawContainerWithTitle(Rect contentRect)
        {
            EditorGUILayout.EndVertical();
            SirenixEditorGUI.DrawBorders(contentRect, 1);
        }

        static void Internal_DrawContainerWithTitle(string title, Action drawContent,
            out Rect headerToolBarRect)
        {
            var contentRect = Internal_BeginDrawContainerWithTitle(title, out headerToolBarRect);
            drawContent();
            Internal_EndDrawContainerWithTitle(contentRect);
        }

        #endregion

        #region Performance Cache

        static float Internal_GetCachedTextHeight(string text, float width,
            Dictionary<string, float> cache)
        {
            var key = text + "_" + width;
            if (cache.TryGetValue(key, out var height))
            {
                return height;
            }

            height = AttributeOverviewEditorUtility.TableCellTextStyle.CalcHeight(GUIHelper.TempContent(text), width);
            cache[key] = height;
            return height;
        }

        #endregion

        // 供子类调用
        protected void SetData(AbstractAttributeData attributeData) => Internal_SetData(attributeData);
    }

    internal sealed class AbstractAttributePanelSOProcessor<T> : OdinAttributeProcessor<T>
        where T : AbstractAttributePanelSO
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty,
            MemberInfo member,
            List<Attribute> attributes)
        {
            switch (member.Name)
            {
                case nameof(AbstractAttributePanelSO.Initialize):
                    attributes.Add(new OnInspectorInitAttribute());
                    attributes.Add(new PropertyOrderAttribute(-1000));
                    break;
                case "headerWidget":
                    attributes.Add(new PropertyOrderAttribute(-100));
                    attributes.Add(new PropertySpaceAttribute(0, AbstractAttributePanelSO_Consts.AfterSpace));
                    break;
                case "DrawUsageTips":
                    attributes.Add(new HideIfAttribute("UsageTipIsEmpty"));
                    attributes.Add(new PropertyOrderAttribute(-60));
                    attributes.Add(new OnInspectorGUIAttribute());
                    attributes.Add(new PropertySpaceAttribute(0, AbstractAttributePanelSO_Consts.AfterSpace));
                    break;
                case "DrawAttributeParameters":
                    attributes.Add(new HideIfAttribute("AttributeParameterIsEmpty"));
                    attributes.Add(new PropertyOrderAttribute(-20));
                    attributes.Add(new OnInspectorGUIAttribute());
                    attributes.Add(new PropertySpaceAttribute(0, AbstractAttributePanelSO_Consts.AfterSpace));
                    break;
                case "DrawResolvedStringParameters":
                    attributes.Add(new HideIfAttribute("ResolvedStringParametersIsEmpty"));
                    attributes.Add(new PropertyOrderAttribute(-10));
                    attributes.Add(new OnInspectorGUIAttribute());
                    attributes.Add(new PropertySpaceAttribute(0, AbstractAttributePanelSO_Consts.AfterSpace));
                    break;
                case "DrawUsageExamplePreview":
                    attributes.Add(new HideIfAttribute("UsageExampleItemsIsEmpty"));
                    attributes.Add(new OnInspectorGUIAttribute());
                    attributes.Add(new PropertyOrderAttribute(-1));
                    break;
                case "currentSelectedExample":
                    attributes.Add(new HideIfAttribute("UsageExampleItemsIsEmpty"));
                    attributes.Add(new InlineEditorAttribute(InlineEditorObjectFieldModes.Hidden));
                    attributes.Add(new PropertyOrderAttribute(0));
                    break;
                case "EndDrawUsageExampleContainer":
                    attributes.Add(new HideIfAttribute("UsageExampleItemsIsEmpty"));
                    attributes.Add(new PropertySpaceAttribute(0, AbstractAttributePanelSO_Consts.AfterSpace));
                    attributes.Add(new PropertyOrderAttribute(100));
                    attributes.Add(new OnInspectorGUIAttribute());
                    break;
                case "DrawCurrentExampleCodePreview":
                    attributes.Add(new HideIfAttribute("CurrentExampleIsNull"));
                    attributes.Add(new OnInspectorGUIAttribute());
                    attributes.Add(new PropertyOrderAttribute(150));
                    attributes.Add(new PropertySpaceAttribute(0, AbstractAttributePanelSO_Consts.AfterSpace));
                    break;
            }
        }
    }

    internal static class AbstractAttributePanelSO_Consts
    {
        public const float AfterSpace = 20f;
    }
}

#endif
