using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// 代码预览渲染器，负责绘制特性的示例代码及控制逻辑。
    /// </summary>
    [Summary("代码预览渲染器，负责绘制特性的示例代码及控制逻辑")]
    public class CodePreviewRenderer : IAttributeComponentRenderer
    {
        static readonly BilingualData _codePreviewLabel = new BilingualData("代码预览", "Code Preview");

        static readonly BilingualData _viewFullCodeButtonLabel =
            new BilingualData("查看完整代码", "View Full Code");

        static readonly BilingualData _viewShortenCodeButtonLabel =
            new BilingualData("查看简化代码", "View Shorten Code");

        static readonly BilingualData _copyCodeButtonLabel = new BilingualData("拷贝代码", "Copy Code");

        const int CodeAreaWidth = 750;

        readonly AbstractAttributePanelSO _panel;
        bool _isShowShortenCodePreview = true;
        Vector2 _scrollPosition;
        string _currentExampleSourceCode;

        public CodePreviewRenderer(AbstractAttributePanelSO panel) => _panel = panel;

        public bool IsVisible => !string.IsNullOrEmpty(_currentExampleSourceCode);

        public void Draw()
        {
            if (!IsVisible)
            {
                return;
            }

            var contentRect =
                AbstractAttributePanelSO.Internal_BeginDrawContainerWithTitle(_codePreviewLabel,
                    out var headerToolBarRect);
            SirenixEditorGUI.DrawBorders(headerToolBarRect, 1, Color.clear);
            Internal_DrawCodePreviewHeader(headerToolBarRect);
            Internal_DrawCodePreviewContent();

            AbstractAttributePanelSO.Internal_EndDrawContainerWithTitle(contentRect);
        }

        public void OnLanguageChanged()
        {
        }

        public void Reset()
        {
            _isShowShortenCodePreview = true;
        }

        public void SetData(string sourceCode)
        {
            _currentExampleSourceCode = sourceCode;
        }

        void Internal_DrawCodePreviewHeader(Rect headerToolBarRect)
        {
            var showSwitchButtonRect = headerToolBarRect.AlignLeft(140f).AddXMin(1f);
            var viewFullCodeTexture =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.Fullscreen, Color.white, 24, 24, 0);
            var viewShortenCodeTexture =
                SdfIcons.CreateTransparentIconTexture(SdfIconType.FullscreenExit, Color.white, 24, 24, 0);

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
                SdfIcons.CreateTransparentIconTexture(SdfIconType.Stack, Color.white, 24, 24, 0);
            if (GUI.Button(copyButtonRect, GUIHelper.TempContent(" " + _copyCodeButtonLabel, copyCodeTexture),
                    SirenixGUIStyles.ToolbarButton))
            {
                var codeToCopy = _isShowShortenCodePreview
                    ? AttributeOverviewEditorUtility.GetExampleShortenCode(_currentExampleSourceCode)
                    : _currentExampleSourceCode;
                EditorGUIUtility.systemCopyBuffer = codeToCopy;
            }
        }

        void Internal_DrawCodePreviewContent()
        {
            EditorGUILayout.BeginVertical();
            var highlighterCode = OdinCodeHighlighter.ApplyHighlighting(_currentExampleSourceCode);
            if (_isShowShortenCodePreview)
            {
                highlighterCode = AttributeOverviewEditorUtility.GetExampleShortenCode(highlighterCode);
            }

            var calcHeight =
                AttributeOverviewEditorUtility.CodeTextEditorStyle.CalcHeight(
                    GUIHelper.TempContent(highlighterCode), CodeAreaWidth - 20f);
            const float MaxScrollViewHeight = 600f;
            var actualHeight = Mathf.Min(calcHeight + 30f, MaxScrollViewHeight);
            var scrollViewRect = EditorGUILayout.GetControlRect(false, actualHeight);
            SirenixEditorGUI.DrawSolidRect(scrollViewRect, OdinCodeHighlighter.BackgroundColor);
            SirenixEditorGUI.DrawBorders(scrollViewRect, 1, Color.clear);
            _scrollPosition = GUI.BeginScrollView(scrollViewRect, _scrollPosition,
                new Rect(0, 0, CodeAreaWidth - 20f, calcHeight + 20f), false, false);
            var contentRect = new Rect(10f, 10f, CodeAreaWidth - 30f, calcHeight);
            EditorGUI.SelectableLabel(contentRect, highlighterCode,
                AttributeOverviewEditorUtility.CodeTextEditorStyle);
            GUI.EndScrollView();
            EditorGUILayout.EndVertical();
        }
    }
}
