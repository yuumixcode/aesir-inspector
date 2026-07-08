using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RunLab.AesirInspector.OdinIntegration
{
    [Summary("水平分割线控件，用于在 Inspector 中绘制视觉分隔线")]
    [Serializable]
    public class HorizontalSeparateControl
    {
        [Summary("深色横线高度")]
        readonly int _darkLineHeight;

        [Summary("浅色横线高度，未设置时默认为深色横线高度 - 1")]
        readonly int _lightLineHeight;

        [Summary("分割线下方间距")]
        readonly float _spaceAfter;

        [Summary("分割线上方间距")]
        readonly float _spaceBefore;

        public HorizontalSeparateControl()
        {
            _darkLineHeight = 2;
            _lightLineHeight = _darkLineHeight - 1;
            _spaceBefore = 5;
            _spaceAfter = 5;
        }

        [Summary("浅色横线高度默认为深色横线高度 - 1")]
        public HorizontalSeparateControl(int darkLineHeight, float spaceBefore, float spaceAfter)
        {
            _darkLineHeight = darkLineHeight;
            _lightLineHeight = _darkLineHeight - 1;
            _spaceBefore = spaceBefore;
            _spaceAfter = spaceAfter;
        }

        public HorizontalSeparateControl(int darkLineHeight,
            int lightLineHeight,
            float spaceBefore,
            float spaceAfter)
        {
            _darkLineHeight = darkLineHeight;
            _lightLineHeight = lightLineHeight;
            _spaceBefore = spaceBefore;
            _spaceAfter = spaceAfter;
        }

#if UNITY_EDITOR

        [Summary("深色线条颜色，随编辑器主题自适应")]
        static Color DarkLineColor => EditorGUIUtility.isProSkin
            ? new Color(0.1f, 0.1f, 0.1f, 0.6f)
            : new Color(0f, 0f, 0f, 0.2f);

        [Summary("浅色线条颜色，随编辑器主题自适应")]
        static Color LightLineColor => EditorGUIUtility.isProSkin
            ? new Color(1f, 1f, 1f, 0.1f)
            : new Color(1f, 1f, 1f, 1f);

        public void Separate()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Space(_spaceBefore);
            float totalHeight = _darkLineHeight + _lightLineHeight;
            var rect = EditorGUILayout.GetControlRect(false, totalHeight);
            var darkRect = new Rect(rect.x, rect.y, rect.width, _darkLineHeight);
            var lightRect = new Rect(rect.x, rect.y + _darkLineHeight, rect.width, _lightLineHeight);
            EditorGUI.DrawRect(darkRect, DarkLineColor);
            EditorGUI.DrawRect(lightRect, LightLineColor);
            GUILayout.Space(_spaceAfter);
            EditorGUILayout.EndVertical();
        }

#endif
    }
}
