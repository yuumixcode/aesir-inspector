using System;

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 无 Odin 环境下的默认 IOdinBridge 实现，返回合理的降级值。
    /// </summary>
    [Summary("无 Odin 环境下的默认 IOdinBridge 实现，返回合理的降级值。")]
    public class DefaultOdinBridge : IOdinBridge
    {
        [Summary("桥接是否可用")]
        public bool IsAvailable => false;

        [Summary("获取类型的友好名称")]
        public string GetFriendlyName(Type type) => type.Name;

        [Summary("获取类型的完整友好名称")]
        public string GetFriendlyFullName(Type type) => type.FullName;

        [Summary("获取泛型约束字符串")]
        public string GetGenericConstraintsString(Type type, bool useFullTypeNames) => string.Empty;
    }
}
