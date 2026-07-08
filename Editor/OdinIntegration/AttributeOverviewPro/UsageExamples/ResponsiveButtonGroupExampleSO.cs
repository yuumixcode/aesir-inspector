using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class ResponsiveButtonGroupExampleSO : AttributeExampleSO<ResponsiveButtonGroupExampleSO>
    {
        [Title("No Parameters")]
        [ResponsiveButtonGroup]
        public void Foo() { }

        [ResponsiveButtonGroup]
        public void Bar() { }

        [ResponsiveButtonGroup]
        public void Baz() { }

        [Title("Parameter: UniformLayout")]
        [ResponsiveButtonGroup("UniformGroup", UniformLayout = true)]
        public void Foo1() { }

        [ResponsiveButtonGroup("UniformGroup")]
        public void Foo2() { }

        [ResponsiveButtonGroup("UniformGroup")]
        public void LongestNameWins() { }

        [ResponsiveButtonGroup("UniformGroup")]
        public void Foo4() { }

        [ResponsiveButtonGroup("UniformGroup")]
        public void Foo5() { }

        [Title("Parameter: DefaultButtonSize")]
        [ResponsiveButtonGroup("DefaultButtonSize", DefaultButtonSize = ButtonSizes.Small)]
        public void Bar1() { }

        [ResponsiveButtonGroup("DefaultButtonSize")]
        public void Bar2() { }

        [ResponsiveButtonGroup("DefaultButtonSize")]
        public void Bar3() { }

        [Button(ButtonSizes.Large)]
        [ResponsiveButtonGroup("DefaultButtonSize")]
        public void Bar4() { }

        public override void AesirInspectorReset() { }
    }
}