using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    [HideNetworkBehaviourFields]
    public class HideNetworkBehaviourFieldsExampleSO : AttributeExampleSO<HideNetworkBehaviourFieldsExampleSO>
    {
        [Title("No Parameters")]
        [InfoBox(
            "HideNetworkBehaviourFields is a class-level attribute that prevents the special 'Network Channel' and 'Network Send Interval' properties from being shown in the inspector for a NetworkBehaviour. This example inherits from ScriptableObject, so the effect is not visible here.",
            InfoMessageType.Info)]
        public int exampleField;

        public override void AesirInspectorReset()
        {
            exampleField = 0;
        }
    }
}
