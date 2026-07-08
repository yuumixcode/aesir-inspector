using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class CustomValueDrawerExampleSO : AttributeExampleSO<CustomValueDrawerExampleSO>
    {
        [Title("Controls")]
        public float min;

        public float max = 10f;

        [Title("Action(float value)")]
        [BilingualInfoBox("范围编写在代码中", "Range is hardcoded in the drawer method")]
        [CustomValueDrawer("DrawStaticSlider")]
        public float customStaticSlider;

        [Title("Action(float value) - 数组元素")]
        [BilingualInfoBox("对集合类型使用，实际作用于集合中的每个元素",
            "When applied to collections, the drawer acts on each element individually")]
        [CustomValueDrawer("DrawArrayElementSlider")]
        public float[] customArraySliders = { 3f, 5f, 6f };

        [Title("Action(float value, GUIContent label)")]
        [BilingualInfoBox("绘制方法内部可以引用其他字段，动态设置范围",
            "The drawer method can reference other fields to set the range dynamically")]
        [CustomValueDrawer("DrawSlider")]
        public float customSlider = 5f;

        [Title("Action(Color value, GUIContent label)")]
        [CustomValueDrawer("DrawColorBox")]
        public Color customColor = Color.red;

        [Title("Action(float, GUIContent, Func<GUIContent, bool>)")]
        [BilingualInfoBox("接入 Odin 绘制链，调用 callNextDrawer 进入下一层绘制",
            "Integrates with Odin's drawer chain by calling callNextDrawer")]
        [CustomValueDrawer("DrawAppendRange")]
        public float appendRange;

        [Title("Action(float, GUIContent, Func, InspectorProperty)")]
        [BilingualInfoBox("获取 InspectorProperty，绿色边框代表 Property 的范围",
            "Accesses InspectorProperty; the green border indicates the Property's range")]
        [CustomValueDrawer("DrawWithInspectorProperty")]
        public float specialFloat;

        public override void AesirInspectorReset()
        {
            customStaticSlider = 0f;
            customArraySliders = new float[] { 3f, 5f, 6f };
            customSlider = 5f;
            customColor = Color.red;
            appendRange = 0f;
            specialFloat = 0f;
            min = 0f;
            max = 10f;
        }

#if UNITY_EDITOR
        // 1. Action(float value) — 最简签名
        float DrawStaticSlider(float value) => EditorGUILayout.Slider(value, 0, 10);

        // Action(float value) 应用于数组元素
        float DrawArrayElementSlider(float value) => EditorGUILayout.Slider(value, min, max);

        // 2. Action(float value, GUIContent label) — 可使用 label
        float DrawSlider(float value, GUIContent label) =>
            EditorGUILayout.Slider(label, value, min, max);

        // Action(Color value, GUIContent label) — 不同类型示例
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

        // 3. Action(float, GUIContent, Func<GUIContent, bool>) — 接入绘制链
        float DrawAppendRange(float value, GUIContent label, Func<GUIContent, bool> callNextDrawer)
        {
            SirenixEditorGUI.BeginBox();
            callNextDrawer(label);
            var result = EditorGUILayout.Slider(label, value, min, max);
            SirenixEditorGUI.EndBox();
            return result;
        }

        // 4. Action(float, GUIContent, Func, InspectorProperty) — 完整签名
        float DrawWithInspectorProperty(float value, GUIContent label,
            Func<GUIContent, bool> callNextDrawer, InspectorProperty property)
        {
            var rect = EditorGUILayout.GetControlRect();
            SirenixEditorGUI.DrawHorizontalLineSeperator(rect.x, rect.center.y, rect.width, 1);
            SirenixEditorGUI.BeginBox(label);
            EditorGUILayout.LabelField("Property Odin 路径: " + property.Path);
            EditorGUILayout.LabelField("Property Unity 路径: " + property.UnityPropertyPath);
            EditorGUILayout.LabelField("Property State Enabled: " + property.State.Enabled);
            SirenixEditorGUI.EndBox();
            SirenixEditorGUI.BeginBox();
            EditorGUILayout.LabelField("Property Attributes 特性列表:");
            foreach (var attr in property.Attributes)
            {
                EditorGUILayout.LabelField(attr.GetType().Name);
            }

            SirenixEditorGUI.EndBox();
            SirenixEditorGUI.BeginBox();
            callNextDrawer(label);
            var result = EditorGUILayout.Slider(label, value, min, max);
            SirenixEditorGUI.EndBox();
            SirenixEditorGUI.DrawBorders(new Rect(property.LastDrawnValueRect)
            {
                x = property.LastDrawnValueRect.x - 1,
                y = property.LastDrawnValueRect.y - 1,
                width = property.LastDrawnValueRect.width + 2,
                height = property.LastDrawnValueRect.height + 2
            }, 1, Color.green);
            return result;
        }
#endif
    }
}
