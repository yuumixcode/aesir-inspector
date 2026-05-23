using System.Reflection;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// Assembly 过滤器接口，判断 Assembly 是否应该剔除
    /// </summary>
    [Summary("Assembly 过滤器接口，判断 Assembly 是否应该剔除")]
    public interface IAssemblyFilter
    {
        /// <summary>
        /// 判断指定程序集是否应该被过滤掉
        /// </summary>
        [Summary("判断指定程序集是否应该被过滤掉")]
        bool ShouldFilterOut(Assembly assembly);
    }
}
