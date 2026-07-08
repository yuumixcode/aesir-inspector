using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class DetailedInfoBoxExampleSO : AttributeExampleSO<DetailedInfoBoxExampleSO>
    {
        [Title("No Parameters")]
        [DetailedInfoBox("Click the DetailedInfoBox...",
            "... to reveal more information! This allows you to reduce unnecessary clutter in your editors.")]
        public int basicExample;

        [Title("Parameter: InfoMessageType (Warning)")]
        [DetailedInfoBox("This is a warning message.", "Here are the warning details.",
            InfoMessageType.Warning)]
        public int warningExample;

        [Title("Parameter: InfoMessageType (Error)")]
        [DetailedInfoBox("This is an error message.", "Here are the error details.", InfoMessageType.Error)]
        public int errorExample;

        [Title("Parameter: InfoMessageType (None)")]
        [DetailedInfoBox("This message has no icon.", "Here are the details with no icon.",
            InfoMessageType.None)]
        public int noneExample;

        public override void AesirInspectorReset()
        {
            basicExample = 0;
            warningExample = 0;
            errorExample = 0;
            noneExample = 0;
        }
    }
}
