using System;
using System.Runtime.CompilerServices;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("标记一个类为 Aesir Inspector 特性示例类，编译时自动捕获源文件绝对路径")]
    [AttributeUsage(AttributeTargets.Class)]
    public class AesirExampleAttribute : Attribute
    {
        public AesirExampleAttribute([CallerFilePath] string filePath = "unknown") =>
            FilePath = filePath;

        [Summary("示例类的源文件绝对路径")]
        public string FilePath { get; }
    }
}
