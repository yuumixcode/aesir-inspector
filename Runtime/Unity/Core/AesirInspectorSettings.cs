using UnityEngine;

namespace RunLab.AesirInspector
{
    [Summary("编辑器单例设置基类，自动获取或创建 Preferences 资产。用于 ProjectSettings 等配置类的懒加载访问。")]
    public abstract class AesirInspectorSettings<T> : ScriptableObject where T : AesirInspectorSettings<T>
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                var type = typeof(T);
                var configName = OdinBridgeLocator.Bridge.GetFriendlyFullName(type);
                var assetName = type.Name;

                _instance = ScriptableObjectSafeEditorUtility.GetOrCreateEditorScriptableObject<T>(configName,
                    AesirInspectorPaths.PreferencesAssetsFolderPath, assetName);

                return _instance;
            }
        }
    }
}
