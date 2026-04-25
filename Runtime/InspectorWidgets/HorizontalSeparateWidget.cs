// ----------------------------------------------------------------------------
// MIT License
// 
// Copyright (c) 2026 RunLab - Yuumix
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// ----------------------------------------------------------------------------

using System;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
#endif

namespace RunLab.AesirInspector
{
    /// <summary>
    /// 水平横向分割线控件，用于在 Inspector 中绘制视觉分隔线。
    /// </summary>
    [Summary("水平横向分割线控件")]
    [Serializable]
    [HideLabel]
    [InlineProperty]
    public class HorizontalSeparateWidget
    {
        #region --- Non-Serialized Fields ---

        [Summary("深色横线高度")]
        int _darkLineHeight;

        [Summary("浅色横线高度，构造函数中未设置则默认为深色横线高度 - 1")]
        int _lightLineHeight;

        [Summary("分割线下方高度")]
        float _spaceAfter;

        [Summary("分割线上方高度")]
        float _spaceBefore;

        #endregion

        #region --- Constructors ---

        /// <summary>
        /// 创建默认水平分割线控件。
        /// </summary>
        public HorizontalSeparateWidget()
        {
            _darkLineHeight = 2;
            _lightLineHeight = _darkLineHeight - 1;
            _spaceBefore = 5;
            _spaceAfter = 5;
        }

        /// <summary>
        /// 创建自定义水平分割线控件，浅色横线高度默认为深色横线高度 - 1。
        /// </summary>
        public HorizontalSeparateWidget(int darkLineHeight, float spaceBefore, float spaceAfter)
        {
            _darkLineHeight = darkLineHeight;
            _lightLineHeight = _darkLineHeight - 1;
            _spaceBefore = spaceBefore;
            _spaceAfter = spaceAfter;
        }

        /// <summary>
        /// 创建自定义水平分割线控件，指定所有参数。
        /// </summary>
        public HorizontalSeparateWidget(int darkLineHeight,
            int lightLineHeight,
            float spaceAfter,
            float spaceBefore)
        {
            _darkLineHeight = darkLineHeight;
            _lightLineHeight = lightLineHeight;
            _spaceAfter = spaceAfter;
            _spaceBefore = spaceBefore;
        }

        #endregion

        #region --- Odin Inspector ---

#if UNITY_EDITOR

        /// <summary>
        /// 深色线条颜色
        /// </summary>
        [Summary("深色线条颜色")]
        Color DarkLineColor => EditorGUIUtility.isProSkin
            ? SirenixGUIStyles.BorderColor
            : new Color(0f, 0f, 0f, 0.2f);

        /// <summary>
        /// 浅色线条颜色
        /// </summary>
        [Summary("浅色线条颜色")]
        Color LightLineColor => EditorGUIUtility.isProSkin
            ? new Color(1f, 1f, 1f, 0.1f)
            : new Color(1f, 1f, 1f, 1f);

        /// <summary>
        /// 绘制分割线
        /// </summary>
        [Summary("绘制分割线")]
        [OnInspectorGUI]
        public void Separate()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Space(_spaceBefore);
            SirenixEditorGUI.HorizontalLineSeparator(DarkLineColor, _darkLineHeight);
            SirenixEditorGUI.HorizontalLineSeparator(LightLineColor, _lightLineHeight);
            GUILayout.Space(_spaceAfter);
            EditorGUILayout.EndVertical();
        }

#endif

        #endregion
    }
}
