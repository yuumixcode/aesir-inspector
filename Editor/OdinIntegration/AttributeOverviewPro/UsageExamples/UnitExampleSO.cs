using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class UnitExampleSO : AttributeExampleSO<UnitExampleSO>
    {
        [Title("Parameter: ActualUnit")]
        [Unit(Units.Kilogram)]
        public float weight;

        [Title("Parameter: ActualUnit, DisplayUnit")]
        [Unit(Units.MetersPerSecond, Units.KilometersPerHour)]
        public float speed;

        [Unit(Units.Meter, Units.Centimeter)]
        public float distance;

        [Title("Parameter: DisplayAsString, ForceDisplayUnit")]
        [ShowInInspector]
        [Unit(Units.MetersPerSecond, Units.MilesPerHour, DisplayAsString = true, ForceDisplayUnit = true)]
        public float SpeedMilesPerHour => speed;

        public override void AesirInspectorReset()
        {
            weight = 0f;
            speed = 0f;
            distance = 0f;
        }
    }
}