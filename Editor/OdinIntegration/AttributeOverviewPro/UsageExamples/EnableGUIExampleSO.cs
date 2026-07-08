using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class EnableGUIExampleSO : AttributeExampleSO<EnableGUIExampleSO>
    {
        [Title("No Parameters")]
        [ReadOnly]
        public string readOnlyField = "Read-only field (grayed out)";

        [ReadOnly]
        [EnableGUI]
        public string enabledReadOnlyField = "Read-only field with [EnableGUI]";

        [Title("Usage with ShowInInspector Properties")]
        [ShowInInspector]
        public int ReadOnlyProperty => 42;

        [ShowInInspector]
        [EnableGUI]
        public int EnabledReadOnlyProperty => 42;

        public override void AesirInspectorReset()
        {
            readOnlyField = "Read-only field (grayed out)";
            enabledReadOnlyField = "Read-only field with [EnableGUI]";
        }
    }
}
