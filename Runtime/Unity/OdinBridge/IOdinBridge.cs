using System;

namespace RunLab.AesirInspector
{
    [Summary("Odin 包装层的核心桥接接口。由 OdinIntegration 程序集实现，在无 Odin 环境下使用 DefaultOdinBridge。")]
    public interface IOdinBridge
    {
        [Summary("桥接是否可用")]
        bool IsAvailable { get; }

        [Summary("获取类型的友好名称")]
        string GetFriendlyName(Type type);

        [Summary("获取类型的完整友好名称")]
        string GetFriendlyFullName(Type type);

        [Summary("获取泛型约束字符串")]
        string GetGenericConstraintsString(Type type, bool useFullTypeNames = false);
    }
}
