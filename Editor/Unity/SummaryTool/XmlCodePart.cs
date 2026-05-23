using System;
using System.Text.RegularExpressions;

namespace RunLab.AesirInspector.Editor
{
    [Summary("XML 注释部分和代码块的组合")]
    [Serializable]
    public class XmlCodePart
    {
        static readonly Regex SummaryTagRegex = new(@"/// <summary>(.*?)</summary>", RegexOptions.Singleline);
        static readonly Regex XmlSubTagRegex = new(@"<[^>]+>");
        static readonly Regex CommentPrefixRegex = new(@"^\s*///\s*", RegexOptions.Multiline);
        static readonly Regex BlankLineRegex = new(@"^\s*$\r?\n", RegexOptions.Multiline);
        static readonly Regex WhitespaceRegex = new(@"\s+");
        static readonly Regex LeadingIndentRegex = new(@"^\s*");
        static readonly Regex SummaryAttributeRegex = new(
            @"(?m)(?:^|\s)\s*\[Summary\(\""(?<content>[\s\S]*?)""\)\]", RegexOptions.Multiline);

        readonly string _xml;
        readonly string _code;

        public string Xml => _xml;
        public string Code => _code;

        public XmlCodePart(string xml, string code)
        {
            _xml = xml;
            _code = code;
        }

        [Summary("code 开头的连续预处理指令行，确保添加 [Summary] 时位于条件编译块内部")]
        public string LeadingPreprocessorLines
        {
            get
            {
                if (string.IsNullOrEmpty(_code))
                {
                    return string.Empty;
                }

                var lines = _code.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                var count = 0;
                while (count < lines.Length && IsPreprocessorDirective(lines[count]))
                {
                    count++;
                }

                return count > 0 ? string.Join("\n", lines, 0, count) + "\n" : string.Empty;
            }
        }

        public string CodeAfterLeadingPreprocessor
        {
            get
            {
                var leading = LeadingPreprocessorLines;
                return leading.Length > 0 ? _code.Substring(leading.Length) : _code;
            }
        }

        public string SummaryValue
        {
            get
            {
                var match = SummaryTagRegex.Match(_xml);
                if (!match.Success)
                {
                    return string.Empty;
                }

                var summaryContent = match.Groups[1].Value.Trim();
                var cleaned = XmlSubTagRegex.Replace(summaryContent, "");
                cleaned = CommentPrefixRegex.Replace(cleaned, "");
                cleaned = BlankLineRegex.Replace(cleaned, "");
                cleaned = WhitespaceRegex.Replace(cleaned, " ").Trim();
                return cleaned;
            }
        }

        public string SummaryAttributeText
        {
            get
            {
                var value = SummaryValue;
                if (string.IsNullOrEmpty(value))
                {
                    return string.Empty;
                }

                var indent = LeadingIndentRegex.Match(_xml).Value;
                return $"{indent}[Summary(\"{value}\")]\n";
            }
        }

        [Summary("删除了 summary 标签部分后的 xml")]
        public string RemovedSummaryXml
        {
            get
            {
                var match = SummaryTagRegex.Match(_xml);
                if (!match.Success)
                {
                    return _xml;
                }

                var processed = _xml.Replace(match.Value, "");
                return BlankLineRegex.Replace(processed, "");
            }
        }

        [Summary("删除了第一个 [Summary()] 后的代码块（不含开头预处理指令行）")]
        public string RemovedFirstSummaryAttributeCode
        {
            get
            {
                var targetCode = CodeAfterLeadingPreprocessor;
                var match = SummaryAttributeRegex.Match(targetCode);
                if (match.Success)
                {
                    targetCode = targetCode.Replace(match.Value, "");
                    targetCode = BlankLineRegex.Replace(targetCode, "");
                }

                return targetCode;
            }
        }

        [Summary("删除了所有 [Summary()] 后的代码块（不含开头预处理指令行）")]
        public string RemoveAllSummaryAttributeCode
        {
            get
            {
                var targetCode = CodeAfterLeadingPreprocessor;
                targetCode = SummaryAttributeRegex.Replace(targetCode, "");
                return BlankLineRegex.Replace(targetCode, "");
            }
        }

        public string GetReplaceAllOutput() =>
            _xml + LeadingPreprocessorLines + RemoveAllSummaryAttributeCode;

        public string GetSyncOutput() =>
            _xml + LeadingPreprocessorLines + SummaryAttributeText + RemovedFirstSummaryAttributeCode;

        public string GetReplaceOutput() =>
            RemovedSummaryXml + LeadingPreprocessorLines + SummaryAttributeText +
            RemovedFirstSummaryAttributeCode;

        static bool IsPreprocessorDirective(string line) => line.TrimStart().StartsWith("#");
    }
}
