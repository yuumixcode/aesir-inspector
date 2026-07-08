namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("特性面板组件渲染器接口，遵循 SRP 原则")]
    public interface IAttributeComponentRenderer
    {
        bool IsVisible { get; }

        void Draw();

        void OnLanguageChanged();

        void Reset();
    }
}
