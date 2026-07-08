using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class DrawWithUnityExampleSO : AttributeExampleSO<DrawWithUnityExampleSO>
    {
        [Title("Odin Drawn")]
        [InfoBox("This field is drawn by Odin's drawing system.")]
        public GameObject objectDrawnWithOdin;

        [Title("No Parameters")]
        [InfoBox("This field is drawn using Unity's old drawing system.")]
        [DrawWithUnity]
        public GameObject objectDrawnWithUnity;

        public override void AesirInspectorReset()
        {
            objectDrawnWithOdin = null;
            objectDrawnWithUnity = null;
        }
    }
}