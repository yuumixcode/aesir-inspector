using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class MultilineExampleSO : AttributeExampleSO<MultilineExampleSO>
    {
        [Title("Parameter: Lines")]
        [Multiline(10)]
        public string multilineField;

        public override void AesirInspectorReset()
        {
            multilineField = "";
        }
    }
}
