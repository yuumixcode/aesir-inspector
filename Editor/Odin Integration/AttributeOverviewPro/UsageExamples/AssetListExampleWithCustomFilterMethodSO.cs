using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// AssetList 特性 CustomFilterMethod 参数使用案例，展示如何通过自定义方法过滤资源列表。
    /// </summary>
    [Summary("AssetList 特性 CustomFilterMethod 参数使用案例，展示如何通过自定义方法过滤资源列表")]
    [AesirExample]
    public class AssetListExampleWithCustomFilterMethodSO :
        AttributeExampleSO<AssetListExampleWithCustomFilterMethodSO>
    {
        static readonly BilingualData CustomFilterMethodTitleLabel =
            new BilingualData("参数: Custom Filter Method", "Parameter: Custom Filter Method");

        static readonly BilingualData LogButtonLabel = new BilingualData("输出信息", "Output Info");

        [Title("$CustomFilterMethodTitleLabel")]
        [AssetList(CustomFilterMethod = "$HasRigidbodyComponent")]
        [InlineButton("LogRigidbodyPrefabs", "$LogButtonLabel")]
        public List<GameObject> rigidbodyPrefabs;

        /// <summary>
        /// 重置所有字段到默认值。
        /// </summary>
        [Summary("重置所有字段到默认值")]
        public override void AesirInspectorReset()
        {
            rigidbodyPrefabs = null;
        }

        bool HasRigidbodyComponent(GameObject obj) => obj.GetComponent<Rigidbody>() != null;

        void LogRigidbodyPrefabs()
        {
            if (rigidbodyPrefabs != null)
            {
                foreach (var prefab in rigidbodyPrefabs)
                {
                    if (prefab != null)
                    {
                        Debug.Log("Rigidbody Prefab: " + prefab.name);
                    }
                }
            }
        }
    }
}

#endif
