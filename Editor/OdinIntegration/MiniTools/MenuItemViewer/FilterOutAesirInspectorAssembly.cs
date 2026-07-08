using System.Reflection;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("示例实现 IAssemblyFilter 接口，用于过滤名称以 RunLab.AesirInspector 开头的程序集。")]
    public class FilterOutAesirInspectorAssembly : IAssemblyFilter
    {
        [Summary("判断指定程序集是否应该被过滤掉")]
        public bool ShouldFilterOut(Assembly assembly) =>
            assembly.FullName.StartsWith("RunLab.AesirInspector");
    }
}
