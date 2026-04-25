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

#if UNITY_EDITOR && ODIN_INSPECTOR_3_3
using Sirenix.Utilities;

namespace RunLab.AesirInspector.Editor
{
    using Sirenix.OdinInspector;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities.Editor;
    using UnityEditor;
    using UnityEngine;

    public class ScriptDocGeneratorWindow : OdinEditorWindow
    {
        const string ScriptDocGenWindowName = "Script Doc Generator";

        [SerializeField]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        ScriptDocGeneratorSO asset;

        [MenuItem(AesirInspectorMenuItems.ScriptDocGenerator, false, AesirInspectorMenuItems.ScriptDocGeneratorOrder)]
        public static void OpenWindow()
        {
            var window = GetWindow<ScriptDocGeneratorWindow>();
            window.titleContent = new GUIContent(ScriptDocGenWindowName);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 800);
            window.Show();
        }

        #region Event Functions

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
            asset = ScriptDocGeneratorSO.Instance;
            ScriptDocGeneratorSO.ToastRequested -= ShowToast;
            ScriptDocGeneratorSO.ToastRequested += ShowToast;
        }

        protected override void OnDestroy()
        {
            ScriptDocGeneratorSO.ToastRequested -= ShowToast;
            base.OnDestroy();
        }

        #endregion
    }
}

#endif
