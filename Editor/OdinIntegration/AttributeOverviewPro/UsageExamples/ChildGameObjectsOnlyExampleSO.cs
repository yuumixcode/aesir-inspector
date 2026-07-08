using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class ChildGameObjectsOnlyExampleSO : AttributeExampleSO<ChildGameObjectsOnlyExampleSO>
    {
        [Title("No Parameters")]
        [ChildGameObjectsOnly]
        public Transform childObject;

        public override void AesirInspectorReset()
        {
            childObject = null;
        }
    }
}
