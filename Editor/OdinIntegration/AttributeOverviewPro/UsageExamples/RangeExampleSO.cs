using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class RangeExampleSO : AttributeExampleSO<RangeExampleSO>
    {
        [Title("Parameter: Min, Max")]
        [Range(0, 10)]
        public int field = 2;

        public override void AesirInspectorReset()
        {
            field = 2;
        }
    }
}
