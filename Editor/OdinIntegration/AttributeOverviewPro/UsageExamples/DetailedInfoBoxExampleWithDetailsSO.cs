using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class DetailedInfoBoxExampleWithDetailsSO : AttributeExampleSO<DetailedInfoBoxExampleWithDetailsSO>
    {
        [Title("Member Reference ($)")]
        public string detailsField = "Dynamic details from field";

        [DetailedInfoBox("Message text here.", "$detailsField")]
        public int referenceExample;

        [Title("Expression (@)")]
        [DetailedInfoBox("Message text here.",
            "@\"Details at: \" + System.DateTime.Now.ToString(\"HH:mm:ss\")")]
        public int expressionExample;

        public override void AesirInspectorReset()
        {
            detailsField = "Dynamic details from field";
            referenceExample = 0;
            expressionExample = 0;
        }
    }
}
