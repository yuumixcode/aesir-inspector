using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("代码预览渲染器，负责绘制特性的示例代码及控制逻辑")]
    public class CodePreviewRenderer : IAttributeComponentRenderer
    {
        const int CodeAreaWidth = 750;
        const int IconTextureSize = 32;
        static readonly BilingualData CodePreviewLabel = new BilingualData("代码预览", "Code Preview");

        static readonly BilingualData ViewFullCodeButtonLabel =
            new BilingualData("查看完整代码", "View Full Code");

        static readonly BilingualData ViewShortenCodeButtonLabel =
            new BilingualData("查看简化代码", "View Shorten Code");

        static readonly BilingualData CopyCodeButtonLabel = new BilingualData("拷贝代码", "Copy Code");

        readonly AbstractAttributePanelSO _panel;
        Texture2D _copyCodeIconTexture;
        string _currentExampleSourceCode;
        bool _isShowShortenCodePreview = true;
        Vector2 _scrollPosition;

        Texture2D _viewFullCodeIconTexture;
        Texture2D _viewShortenCodeIconTexture;

        public CodePreviewRenderer(AbstractAttributePanelSO panel) => _panel = panel;

        public bool IsVisible => !string.IsNullOrEmpty(_currentExampleSourceCode);

        public void Draw()
        {
            if (!IsVisible)
            {
                return;
            }

            var contentRect =
                AbstractAttributePanelSO.BeginDrawContainerWithTitle(CodePreviewLabel,
                    out var headerToolBarRect);
            SirenixEditorGUI.DrawBorders(headerToolBarRect, 1, Color.clear);
            DrawCodePreviewContent();
            AbstractAttributePanelSO.EndDrawContainerWithTitle(contentRect);
            DrawCodePreviewHeader(headerToolBarRect);
        }

        public void OnLanguageChanged() { }

        public void Reset()
        {
            _isShowShortenCodePreview = true;
            if (_viewFullCodeIconTexture != null)
            {
                Object.DestroyImmediate(_viewFullCodeIconTexture);
            }

            if (_viewShortenCodeIconTexture != null)
            {
                Object.DestroyImmediate(_viewShortenCodeIconTexture);
            }

            if (_copyCodeIconTexture != null)
            {
                Object.DestroyImmediate(_copyCodeIconTexture);
            }

            _viewFullCodeIconTexture = null;
            _viewShortenCodeIconTexture = null;
            _copyCodeIconTexture = null;
        }

        public void SetData(string sourceCode)
        {
            _currentExampleSourceCode = sourceCode;
        }

        void DrawCodePreviewHeader(Rect headerToolBarRect)
        {
            var showSwitchButtonRect = headerToolBarRect.AlignLeft(140f).AddXMin(1f);
            if (_viewFullCodeIconTexture == null)
            {
                _viewFullCodeIconTexture = SdfIcons.CreateTransparentIconTexture(SdfIconType.Fullscreen,
                    Color.white, IconTextureSize, IconTextureSize, 0);
            }

            if (_viewShortenCodeIconTexture == null)
            {
                _viewShortenCodeIconTexture = SdfIcons.CreateTransparentIconTexture(
                    SdfIconType.FullscreenExit, Color.white, IconTextureSize, IconTextureSize, 0);
            }

            if (_isShowShortenCodePreview)
            {
                if (GUI.Button(showSwitchButtonRect,
                        GUIHelper.TempContent(" " + ViewFullCodeButtonLabel, _viewFullCodeIconTexture),
                        SirenixGUIStyles.ToolbarButton))
                {
                    _isShowShortenCodePreview = false;
                }
            }
            else
            {
                if (GUI.Button(showSwitchButtonRect,
                        GUIHelper.TempContent(" " + ViewShortenCodeButtonLabel, _viewShortenCodeIconTexture),
                        SirenixGUIStyles.ToolbarButton))
                {
                    _isShowShortenCodePreview = true;
                }
            }

            var copyButtonRect = headerToolBarRect.AlignRight(100f);
            if (_copyCodeIconTexture == null)
            {
                _copyCodeIconTexture = SdfIcons.CreateTransparentIconTexture(SdfIconType.Stack, Color.white,
                    IconTextureSize, IconTextureSize, 0);
            }

            if (GUI.Button(copyButtonRect,
                    GUIHelper.TempContent(" " + CopyCodeButtonLabel, _copyCodeIconTexture),
                    SirenixGUIStyles.ToolbarButton))
            {
                var codeToCopy = _isShowShortenCodePreview
                    ? AttributeOverviewEditorUtility.GetExampleShortenCode(_currentExampleSourceCode)
                    : _currentExampleSourceCode;
                EditorGUIUtility.systemCopyBuffer = codeToCopy;
            }
        }

        void DrawCodePreviewContent()
        {
            EditorGUILayout.BeginVertical();
            var highlighterCode = OdinCodeHighlighterUtility.ApplyHighlighting(_currentExampleSourceCode);
            if (_isShowShortenCodePreview)
            {
                highlighterCode = AttributeOverviewEditorUtility.GetExampleShortenCode(highlighterCode);
            }

            var calcHeight =
                AttributeOverviewEditorUtility.CodeTextEditorStyle.CalcHeight(
                    GUIHelper.TempContent(highlighterCode), CodeAreaWidth - 20f);
            const float maxScrollViewHeight = 600f;
            var actualHeight = Mathf.Min(calcHeight + 30f, maxScrollViewHeight);
            var scrollViewRect = EditorGUILayout.GetControlRect(false, actualHeight);
            SirenixEditorGUI.DrawSolidRect(scrollViewRect, OdinCodeHighlighterUtility.BackgroundColor);
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
