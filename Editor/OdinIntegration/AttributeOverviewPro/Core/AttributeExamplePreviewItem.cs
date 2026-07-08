using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("特性案例序列化类型")]
    public enum AttributeExampleType
    {
        UnitySerialized = 0,
        OdinSerialized = 1
    }

    [Summary("单个特性使用案例预览项，持有案例名称与对应的 ScriptableObject 引用")]
    public class AttributeExamplePreviewItem
    {
        SerializedScriptableObject _odinSerializedExample;

        ScriptableObject _unitySerializedExample;

        [Summary("案例序列化类型")]
        public AttributeExampleType ExampleType { get; private set; }

        [Summary("案例显示名称")]
        public string ItemName { get; private set; }

        [Summary("Unity 原生序列化的案例 ScriptableObject")]
        public ScriptableObject UnitySerializedExample
        {
            get
            {
                if (ExampleType == AttributeExampleType.UnitySerialized)
                {
                    return _unitySerializedExample;
                }

                AesirInspectorLogger.Warning(nameof(AttributeExamplePreviewItem),
                    "Odin 序列化的案例应该获取 " + nameof(OdinSerializedExample));
                return null;
            }
        }

        [Summary("Odin 序列化的案例 ScriptableObject")]
        public SerializedScriptableObject OdinSerializedExample
        {
            get
            {
                if (ExampleType == AttributeExampleType.OdinSerialized)
                {
                    return _odinSerializedExample;
                }

                AesirInspectorLogger.Warning(nameof(AttributeExamplePreviewItem),
                    "Unity 原生序列化的案例应该获取 " + nameof(UnitySerializedExample));
                return null;
            }
        }

        [Summary("初始化为 Unity 序列化案例")]
        public AttributeExamplePreviewItem InitializeUnitySerializedExample(string itemName,
            ScriptableObject unitySerializedExample)
        {
            ExampleType = AttributeExampleType.UnitySerialized;
            ItemName = itemName;
            _unitySerializedExample = unitySerializedExample;
            return this;
        }

        [Summary("初始化为 Odin 序列化案例")]
        public AttributeExamplePreviewItem InitializeOdinSerializedExample(string itemName,
            SerializedScriptableObject odinSerializedExample)
        {
            ExampleType = AttributeExampleType.OdinSerialized;
            ItemName = itemName;
            _odinSerializedExample = odinSerializedExample;
            return this;
        }

        [Summary("重置当前案例到初始状态")]
        public void Reset()
        {
            var exampleName = ExampleType == AttributeExampleType.OdinSerialized
                ? _odinSerializedExample?.GetType().Name
                : _unitySerializedExample?.GetType().Name;

            if (exampleName == null) return;

            switch (ExampleType)
            {
                case AttributeExampleType.OdinSerialized:
                    if (_odinSerializedExample is IAesirInspectorReset canResetOdin)
                    {
                        canResetOdin.AesirInspectorReset();
                        AttributeOverviewEditorUtility.LogEditorResetSuccess(exampleName);
                    }
                    else
                    {
                        AttributeOverviewEditorUtility.LogEditorResetWarning(exampleName);
                    }

                    break;

                case AttributeExampleType.UnitySerialized:
                    if (_unitySerializedExample is IAesirInspectorReset canResetUnity)
                    {
                        canResetUnity.AesirInspectorReset();
                        AttributeOverviewEditorUtility.LogEditorResetSuccess(exampleName);
                    }
                    else
                    {
                        AttributeOverviewEditorUtility.LogEditorResetWarning(exampleName);
                    }

                    break;
            }
        }
    }
}
