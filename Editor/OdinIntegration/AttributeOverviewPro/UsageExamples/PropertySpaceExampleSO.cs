using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// PropertySpace 特性的案例 SO。
    /// </summary>
    [AesirExample]
    internal class PropertySpaceExampleSO : AttributeExampleSO<PropertySpaceExampleSO>
    {
        [Title("No Parameters")]
        [PropertySpace]
        public int noParams;

        [Title("Parameter: SpaceBefore")]
        [PropertySpace(20)]
        public int spaceBefore;

        [Title("Parameter: SpaceBefore, SpaceAfter")]
        [PropertySpace(20, 20)]
        public int spaceBeforeAndAfter;

        public override void AesirInspectorReset()
        {
            noParams = 0;
            spaceBefore = 0;
            spaceBeforeAndAfter = 0;
        }
    }
}
