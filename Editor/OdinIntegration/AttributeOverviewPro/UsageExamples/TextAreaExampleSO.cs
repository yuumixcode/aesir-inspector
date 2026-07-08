using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class TextAreaExampleSO : AttributeExampleSO<TextAreaExampleSO>
    {
        [Title("Parameter: MinLines, MaxLines")]
        [TextArea(4, 10)]
        public string textAreaField;

        public override void AesirInspectorReset()
        {
            textAreaField = "";
        }
    }
}
