using System.Text;
using System.Text.RegularExpressions;

namespace RunLab.AesirInspector
{
    [Summary("正则表达式工具类，提供命名空间、类名及常用数据格式的正则校验和规范化方法")]
    public static class RegexUtility
    {
        [Summary("匹配 XML 的 Summary，直到下一个分号、大括号")]
        public static readonly Regex XmlToDeclarationEndRegex =
            new Regex(@"(\s*///\s*<summary>(.*?)</summary>\s*)(.*?)((?=;|\{))",
                RegexOptions.Singleline | RegexOptions.Multiline);

        [Summary("不合法的命名空间正则表达式")]
        public static readonly Regex InvalidNamespaceRegex =
            new Regex(@"([^a-zA-Z0-9._]|[\s]|::|\b(using)\b|\.{2,})");

        [Summary("常用的邮箱匹配正则表达式")]
        public static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        [Summary("常用的 URL 匹配正则表达式")]
        public static readonly Regex WebUrlRegex = new Regex(@"^(http|https)://[^\s/$.?#].[^\s]*$");

        [Summary("规范化命名空间")]
        public static string CanonicalNamespace(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var foundFirstLetter = false;
            var firstValidator = new StringBuilder();

            foreach (var c in input)
            {
                if (char.IsLetter(c))
                {
                    foundFirstLetter = true;
                    firstValidator.Append(c);
                }
                else if (foundFirstLetter)
                {
                    firstValidator.Append(c);
                }
            }

            var firstResult = firstValidator.ToString();
            return InvalidNamespaceRegex.Replace(firstResult, "");
        }

        [Summary("规范化脚本类名")]
        public static string CanonicalScriptClassName(string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                return string.Empty;
            }

            const string canonicalScriptClassNameRegex = @"([^\w\u4e00-\u9fa5])";
            className = Regex.Replace(className, canonicalScriptClassNameRegex, "");

            if (className.Length > 0)
            {
                className = char.ToUpper(className[0]) + className[1..];
            }

            return className;
        }

        [Summary("检查字符串是否匹配指定的正则表达式")]
        public static bool IsMatch(string input, string pattern) =>
            !string.IsNullOrEmpty(input) && Regex.IsMatch(input, pattern);

        [Summary("检查字符串是否为有效的电子邮箱格式")]
        public static bool IsEmail(string input) =>
            !string.IsNullOrEmpty(input) && EmailRegex.IsMatch(input);
    }
}
