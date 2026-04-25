using RunLab.AesirInspector;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RunLab.AesirInspector.Samples.PluginConfig
{
    [Summary("在编辑器阶段使用且可以被 Runtime 程序集读取，构建后不使用的配置类案例")]
    public class PluginConfigRuntimeOnEditorSample : ScriptableObject
    {
        static PluginConfigRuntimeOnEditorSample _instance;

        [BilingualTitle("可配置数据", "Configurable Data")]
        public string runtimeName = "Runtime Name";

        static string ConfigName =>
            OdinInspectorSafeEditorUtility.GetNiceFullName(typeof(PluginConfigRuntimeOnEditorSample));

        public static PluginConfigRuntimeOnEditorSample Instance
        {
            get
            {
#if UNITY_EDITOR
                return ScriptableObjectSafeEditorUtility
                    .GetOrCreateEditorScriptableObject<PluginConfigRuntimeOnEditorSample>(
                        ConfigName,
                        AesirInspectorPaths.EditorDefaultResourcesPath + "/Samples",
                        nameof(PluginConfigRuntimeOnEditorSample));
#else
                return null;
#endif
            }
        }

        [BilingualButton("重置配置", "Reset Config", ButtonSizes.Medium)]
        public void ResetConfig()
        {
            runtimeName = "Runtime Name";
        }

        [BilingualTitle("调试", "Debug")]
        [BilingualButton("选择项目中的配置资产", "Select Config Asset In Project", ButtonSizes.Medium)]
        public void PingAsset()
        {
#if UNITY_EDITOR
            ProjectSafeEditorUtility.PingAndSelectAsset(
                AesirInspectorPaths.EditorDefaultResourcesPath + "/Samples/" +
                nameof(PluginConfigRuntimeOnEditorSample) + ".asset");
#endif
        }
    }
}
