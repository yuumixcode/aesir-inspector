using System.Reflection;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("Assembly 过滤器接口，判断 Assembly 是否应该剔除")]
    public interface IAssemblyFilter
    {
        [Summary("判断指定程序集是否应该被过滤掉")]
        bool ShouldFilterOut(Assembly assembly);
    }
}
