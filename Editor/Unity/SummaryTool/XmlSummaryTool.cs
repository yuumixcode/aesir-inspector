using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace RunLab.AesirInspector.Editor
{
    [Summary("C# 脚本 XML 文档注释处理器，在 Sync / Replace / Remove 三种模式下生成 [Summary] 特性或清理注释，供 SummaryToolMenuItems 右键菜单调用")]
    [Serializable]
    public class XmlSummaryTool
    {
        static readonly Regex NamespaceRegex = new(@"namespace\s+([\w.]+)");
        static readonly Regex BlankLineRegex = new(@"(?:^\s*$\r?\n)", RegexOptions.Multiline);
        static readonly string[] LineSeparators = { "\r\n", "\r", "\n" };

        public enum ProcessMode
        {
            None = 0,
            SyncSummary = 1,
            ReplaceSummary = 2,
            RemoveSummary = 3
        }

        readonly string _sourceScriptText;
        List<string> _sourceScriptLines;
        List<string> _headerLines;

        public string SourceScriptText => _sourceScriptText;
        public List<string> SourceScriptLines => _sourceScriptLines;
        public List<string> HeaderLines => _headerLines;

        public int FirstXmlCommentLineIndex { get; private set; } = -1;

        [Summary("XML 文档注释与代码块的组合列表，代码块可能包含多个成员")]
        public List<XmlCodePart> XmlCodeParts { get; private set; } = new();

        public XmlSummaryTool(string sourceScript)
        {
            _sourceScriptText = sourceScript ?? throw new ArgumentNullException(nameof(sourceScript));
            InitializeSourceLines();
        }

        public string GetHeaderScript() => string.Join("\n", _headerLines);

        [Summary("解析源脚本，分解为头部部分和 XML 文档注释与代码块的组合列表")]
        public XmlSummaryTool ParseSourceScript()
        {
            ExtractHeaderLines();
            CreateXmlCodeParts();
            return this;
        }

        public string GetProcessedSourceScript(ProcessMode processMode)
        {
            var sb = new StringBuilder(GetProcessedHeaderScript()).Append('\n');
            foreach (var xmlCodePart in XmlCodeParts)
            {
                switch (processMode)
                {
                    case ProcessMode.SyncSummary:
                        sb.Append(xmlCodePart.GetSyncOutput());
                        break;
                    case ProcessMode.ReplaceSummary:
                        sb.Append(xmlCodePart.GetReplaceOutput());
                        break;
                    case ProcessMode.RemoveSummary:
                        sb.Append(xmlCodePart.GetReplaceAllOutput());
                        break;
                }
            }

            return BlankLineRegex.Replace(sb.ToString(), "\n");
        }

        void InitializeSourceLines()
        {
            _sourceScriptLines = _sourceScriptText.Split(LineSeparators, StringSplitOptions.None).ToList();
        }

        string GetProcessedHeaderScript()
        {
            var headerScript = GetHeaderScript();
            var match = NamespaceRegex.Match(headerScript);
            if (match.Success)
            {
                var namespaceName = match.Groups[1].Value;
                if (namespaceName == typeof(SummaryAttribute).Namespace)
                {
                    return headerScript;
                }
            }

            var usingDirective = "using " + typeof(SummaryAttribute).Namespace + ";";
            if (!headerScript.Contains(usingDirective))
            {
                headerScript = usingDirective + "\n" + headerScript;
            }

            return headerScript;
        }

        void ExtractHeaderLines()
        {
            _headerLines = new List<string>();
            for (var i = 0; i < _sourceScriptLines.Count; i++)
            {
                var line = _sourceScriptLines[i];
                if (!IsXmlDocumentationLine(line))
                {
                    _headerLines.Add(line);
                }
                else
                {
                    FirstXmlCommentLineIndex = i;
                    break;
                }
            }
        }

        void CreateXmlCodeParts()
        {
            if (FirstXmlCommentLineIndex == -1)
            {
                Debug.LogWarning("未在代码中发现 XML 文档注释（以 /// 开头的注释）");
                return;
            }

            XmlCodeParts = new List<XmlCodePart>();
            var currentXmlStartLine = FirstXmlCommentLineIndex;
            while (currentXmlStartLine < _sourceScriptLines.Count)
            {
                var (xmlComment, nextStartLine) = ExtractXmlCommentBlock(currentXmlStartLine);
                if (string.IsNullOrEmpty(xmlComment))
                {
                    break;
                }

                var (codeBlock, newXmlStartLine) = ExtractCodeBlock(nextStartLine);
                XmlCodeParts.Add(new XmlCodePart(xmlComment, codeBlock));
                currentXmlStartLine = newXmlStartLine;
            }
        }

        (string xmlComment, int nextStartLine) ExtractXmlCommentBlock(int startLine)
        {
            var sb = new StringBuilder();
            var currentLine = startLine;
            for (var i = startLine; i < _sourceScriptLines.Count; i++)
            {
                var line = _sourceScriptLines[i];
                if (IsXmlDocumentationLine(line))
                {
                    sb.AppendLine(line);
                    currentLine = i + 1;
                }
                else
                {
                    break;
                }
            }

            return (sb.ToString(), currentLine);
        }

        (string codeBlock, int nextXmlStartLine) ExtractCodeBlock(int startLine)
        {
            var sb = new StringBuilder();
            var nextXmlStartLine = startLine;
            for (var i = startLine; i < _sourceScriptLines.Count; i++)
            {
                var line = _sourceScriptLines[i];
                if (i == _sourceScriptLines.Count - 1)
                {
                    sb.Append(line);
                    nextXmlStartLine = i + 1;
                    break;
                }

                if (!IsXmlDocumentationLine(line))
                {
                    sb.AppendLine(line);
                }
                else
                {
                    nextXmlStartLine = i;
                    break;
                }
            }

            return (sb.ToString(), nextXmlStartLine);
        }

        static bool IsXmlDocumentationLine(string line) => line.Trim().StartsWith("///");
    }
}
