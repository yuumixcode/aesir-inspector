using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class DetailedInfoBoxExampleWithMessageSO : AttributeExampleSO<DetailedInfoBoxExampleWithMessageSO>
    {
        [Title("Member Reference ($)")]
        public string messageField = "Dynamic message from field";

        [DetailedInfoBox("$messageField", "Details text here.")]
        public int referenceExample;

        [Title("Expression (@)")]
        [DetailedInfoBox(
            "@\"Current Time: \" + System.DateTime.Now.ToString(\"HH:mm:ss\")",
            "The message updates in real time.")]
        public int expressionExample;

        public override void AesirInspectorReset()
        {
            messageField = "Dynamic message from field";
            referenceExample = 0;
            expressionExample = 0;
        }
    }
}
