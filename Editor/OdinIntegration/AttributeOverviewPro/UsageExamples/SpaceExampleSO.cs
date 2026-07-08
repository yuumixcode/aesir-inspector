using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class SpaceExampleSO : AttributeExampleSO<SpaceExampleSO>
    {
        [Title("No Parameters")]
        public int beforeSpace;

        [Space]
        public int afterSpace;

        [Title("Parameter: Height")]
        public int beforeHeightSpace;

        [Space(30)]
        public int afterHeightSpace;

        public override void AesirInspectorReset()
        {
            beforeSpace = 0;
            afterSpace = 0;
            beforeHeightSpace = 0;
            afterHeightSpace = 0;
        }
    }
}
