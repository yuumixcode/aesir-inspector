namespace RunLab.AesirInspector
{
    [Summary("Aesir Inspector 重置接口。用于 Preferences 等配置类在重置时将所有字段恢复默认值。")]
    public interface IAesirInspectorReset
    {
        void AesirInspectorReset();
    }
}
