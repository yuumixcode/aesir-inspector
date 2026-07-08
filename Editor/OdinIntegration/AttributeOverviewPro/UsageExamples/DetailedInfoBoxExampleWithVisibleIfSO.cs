using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class DetailedInfoBoxExampleWithVisibleIfSO : AttributeExampleSO<DetailedInfoBoxExampleWithVisibleIfSO>
    {
        [Title("Member Reference ($)")]
        public bool toggleInfoBox;

        [DetailedInfoBox("This box is only visible when toggleInfoBox is true.",
            "Here are the details that are also conditionally visible.",
            VisibleIf = "toggleInfoBox")]
        public int referenceExample;

        [Title("Expression (@)")]
        [DetailedInfoBox("Visible when current second is even.",
            "The visibility updates in real time.",
            VisibleIf = "@DateTime.Now.Second % 2 == 0")]
        public int expressionExample;

        public override void AesirInspectorReset()
        {
            toggleInfoBox = false;
            referenceExample = 0;
            expressionExample = 0;
        }
    }
}
