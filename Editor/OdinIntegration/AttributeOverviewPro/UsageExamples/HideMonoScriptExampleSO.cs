using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    [HideMonoScript]
    public class HideMonoScriptExampleSO : AttributeExampleSO<HideMonoScriptExampleSO>
    {
        [Title("No Parameters")]
        public string sampleField;

        public override void AesirInspectorReset()
        {
            sampleField = string.Empty;
        }
    }
}
