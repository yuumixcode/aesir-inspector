using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// AssetsOnly 特性使用案例，展示对 GameObject、Material、MeshRenderer 等类型的资源约束。
    /// </summary>
    [Summary("AssetsOnly 特性使用案例，展示对 GameObject、Material、MeshRenderer 等类型的资源约束")]
    [AesirExample]
    public class AssetsOnlyExampleSO : AttributeExampleSO<AssetsOnlyExampleSO>
    {
        [Title("No Parameters")]
        [AssetsOnly]
        public GameObject somePrefab;

        [AssetsOnly]
        public Material materialAsset;

        [AssetsOnly]
        public MeshRenderer someMeshRendererOnPrefab;

        /// <summary>
        /// 重置所有字段到默认值。
        /// </summary>
        [Summary("重置所有字段到默认值")]
        public override void AesirInspectorReset()
        {
            somePrefab = null;
            materialAsset = null;
            someMeshRendererOnPrefab = null;
        }
    }
}
