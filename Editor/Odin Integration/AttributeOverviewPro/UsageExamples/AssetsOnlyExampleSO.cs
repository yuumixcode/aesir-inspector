using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
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

        public override void AesirInspectorReset()
        {
            somePrefab = null;
            materialAsset = null;
            someMeshRendererOnPrefab = null;
        }
    }
}
