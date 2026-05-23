using UnityEditor;
using UnityEngine;

namespace RunLab.AesirInspector
{
    [Summary("项目级别配置。用于记录 Aesir Inspector 首次启动初始化状态，控制 Getting Started 等引导流程。")]
    public class AesirInspectorProjectSettingsSO : AesirInspectorSettings<AesirInspectorProjectSettingsSO>
    {
        [SerializeField]
        bool isInitialized;

        public bool IsInitialized
        {
            get => isInitialized;
            set
            {
                isInitialized = value;
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
#endif
            }
        }
    }
}
