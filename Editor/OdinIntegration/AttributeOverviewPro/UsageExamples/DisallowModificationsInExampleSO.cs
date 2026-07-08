using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class DisallowModificationsInExampleSO : AttributeExampleSO<DisallowModificationsInExampleSO>
    {
        [Title("Parameter: PrefabKind")]
        [InfoBox("This attribute only takes effect on prefab instances. The fields below show different PrefabKind values.", InfoMessageType.Info)]
        [DisallowModificationsIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        public string prefabInstanceAndNonPrefabInstance = "Prefab Instances and Non-Prefab";

        [DisallowModificationsIn(PrefabKind.InstanceInScene)]
        public string instanceInScene = "Instance In Scene";

        [DisallowModificationsIn(PrefabKind.InstanceInPrefab)]
        public string instanceInPrefab = "Instance In Prefab";

        [DisallowModificationsIn(PrefabKind.Variant)]
        public string variant = "Variant";

        [DisallowModificationsIn(PrefabKind.NonPrefabInstance)]
        public string nonPrefabInstance = "Non Prefab Instance";

        [DisallowModificationsIn(PrefabKind.PrefabInstance)]
        public string prefabInstance = "Prefab Instance";

        public override void AesirInspectorReset()
        {
            prefabInstanceAndNonPrefabInstance = "Prefab Instances and Non-Prefab";
            instanceInScene = "Instance In Scene";
            instanceInPrefab = "Instance In Prefab";
            variant = "Variant";
            nonPrefabInstance = "Non Prefab Instance";
            prefabInstance = "Prefab Instance";
        }
    }
}