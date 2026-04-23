using NUnit.Framework;
using UnityEngine;

namespace RunLab.AesirInspector.Editor.Tests
{
    public class EditorTestExample
    {
        [Test]
        public void EditorTestExampleSimplePasses()
        {
            var runtimeExample = new GameObject("RuntimeExample").AddComponent<RuntimeExample>();
            Assert.AreEqual("RuntimeExample", runtimeExample.name);
            Object.DestroyImmediate(runtimeExample.gameObject);
        }
    }
}
