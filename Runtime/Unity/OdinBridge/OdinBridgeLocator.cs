namespace RunLab.AesirInspector
{
    /// <summary>
    /// IOdinBridge 的静态定位器。OdinIntegration 程序集在加载时注入 OdinBridge 实现。
    /// </summary>
    [Summary("IOdinBridge 的静态定位器。OdinIntegration 程序集在加载时注入 OdinBridge 实现。")]
    public static class OdinBridgeLocator
    {
        [Summary("IOdinBridge 实例，默认为 DefaultOdinBridge")]
        public static IOdinBridge Bridge { get; set; } = new DefaultOdinBridge();
    }
}
