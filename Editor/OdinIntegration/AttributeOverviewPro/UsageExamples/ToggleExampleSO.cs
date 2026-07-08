using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class ToggleExampleSO : AttributeExampleSO<ToggleExampleSO>
    {
        [Title("Parameter: MemberName")]
        [Toggle("Enabled")]
        public MyToggleable toggler = new MyToggleable();

        [Title("On Class")]
        public ToggleableClass toggleableClass = new ToggleableClass();

        public override void AesirInspectorReset()
        {
            toggler = new MyToggleable();
            toggleableClass = new ToggleableClass();
        }

        [Serializable]
        public class MyToggleable
        {
            public bool Enabled;
            public int MyValue;
        }

        [Serializable, Toggle("Enabled")]
        public class ToggleableClass
        {
            public bool Enabled;
            public string Text;
        }
    }
}