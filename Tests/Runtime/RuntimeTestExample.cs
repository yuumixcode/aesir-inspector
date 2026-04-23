
using NUnit.Framework;
using UnityEngine;

namespace RunLab.AesirInspector.Tests
{
    public class RuntimeTestExample
    {
        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityEngine.TestTools.UnityTest]
        public System.Collections.IEnumerator RuntimeTestExampleWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            var runtimeExample = new GameObject("RuntimeExample").AddComponent<RuntimeExample>();
            yield return null;
            Assert.NotNull(runtimeExample);
        }
    }
}
