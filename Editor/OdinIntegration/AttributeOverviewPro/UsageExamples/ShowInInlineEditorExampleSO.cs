using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class ShowInInlineEditorExampleSO : AttributeExampleSO<ShowInInlineEditorExampleSO>
    {
        [Title("No Parameters")]
        [ShowInInlineEditors]
        public Material material;

        public Mesh mesh;

        public Material instanceMaterial;

        public override void AesirInspectorReset()
        {
            material = null;
            mesh = null;
            instanceMaterial = null;
        }
    }
}
