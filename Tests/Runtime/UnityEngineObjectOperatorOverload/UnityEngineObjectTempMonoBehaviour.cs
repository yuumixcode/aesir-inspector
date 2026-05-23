using UnityEngine;

namespace RunLab.AesirInspector.Tests
{
    /// <summary>
    /// 用于测试 UnityEngine.Object 的 MonoBehaviour
    /// </summary>
    [Summary("用于测试 UnityEngine.Object 的 MonoBehaviour")]
    public class UnityEngineObjectTempMonoBehaviour : MonoBehaviour
    {
        #region --- Serialized Fields ---

        [SerializeField]
        int id;

        #endregion
    }
}
