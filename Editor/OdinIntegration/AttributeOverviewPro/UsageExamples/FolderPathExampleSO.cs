using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class FolderPathExampleSO : AttributeExampleSO<FolderPathExampleSO>
    {
        [Title("No Parameters")]
        [FolderPath]
        public string path;

        [Title("Parameter: AbsolutePath")]
        [FolderPath(AbsolutePath = true)]
        public string absolutePath;

        [Title("Parameter: ParentFolder")]
        [FolderPath(ParentFolder = "Assets/RunLab")]
        public string relativePath;

        [Title("Parameter: RequireExistingPath")]
        [FolderPath(RequireExistingPath = true)]
        public string existingPath;

        public override void AesirInspectorReset()
        {
            path = "";
            absolutePath = "";
            relativePath = "";
            existingPath = "";
        }
    }
}
