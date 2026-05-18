using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    /// <summary>
    /// CustomValueDrawer 特性使用案例，展示自定义 Slider 和 Color 绘制器。
    /// </summary>
    [Summary("CustomValueDrawer 特性使用案例，展示自定义 Slider 和 Color 绘制器")]
    [AesirExample]
    public class CustomValueDrawerExampleSO : AttributeExampleSO<CustomValueDrawerExampleSO>
    {
        [CustomValueDrawer("DrawSlider")]
        public float customSlider = 5f;

        [CustomValueDrawer("DrawColorBox")]
        public Color customColor = Color.red;

        public float min;
        public float max = 10f;

        /// <summary>
        /// 重置所有字段到默认值。
        /// </summary>
        [Summary("重置所有字段到默认值")]
        public override void AesirInspectorReset()
        {
            customSlider = 5f;
            customColor = Color.red;
            min = 0f;
            max = 10f;
        }

        float DrawSlider(float value, GUIContent label) => EditorGUILayout.Slider(label, value, min, max);

        Color DrawColorBox(Color value, GUIContent label)
        {
            var rect = EditorGUILayout.GetControlRect();
            if (label != null)
            {
                rect = EditorGUI.PrefixLabel(rect, label);
            }

            var newColor = EditorGUI.ColorField(rect, value);
            return newColor;
        }
    }
}
