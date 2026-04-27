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

using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.Editor
{
    /// <summary>
    /// 文档生成器设置抽象类
    /// </summary>
    [Summary("文档生成器设置抽象类")]
    public abstract class DocGeneratorSettingsSO : ScriptableObject, IAesirInspectorReset
    {
        /// <summary>
        /// 重置文档生成器设置
        /// </summary>
        [Summary("重置文档生成器设置")]
        public void AesirInspectorReset()
        {
            generateNamespaceFolder = true;
            customizeDocFileExtensionName = false;
            docFileExtensionName = ".md";
            generateIdentifier = true;
        }

        /// <summary>
        /// 通过 TypeData 实例对象，生成文档内容。注意：不要在此方法中添加增量生成标识符
        /// </summary>
        [Summary("通过 TypeData 实例对象，生成文档内容。注意：不要在此方法中添加增量生成标识符")]
        public abstract string GetGeneratedDoc(ITypeData data);

        #region Serialized Fields

        /// <summary>
        /// 是否按命名空间生成文件夹
        /// </summary>
        [Summary("是否按命名空间生成文件夹")]
        [BilingualText("按命名空间生成文件夹", "Generate Namespace Folder")]
        public bool generateNamespaceFolder = true;

        /// <summary>
        /// 是否自定义文档扩展名
        /// </summary>
        [Summary("是否自定义文档扩展名")]
        [BilingualText("自定义文档扩展名", "Customize Doc Extension Name")]
        public bool customizeDocFileExtensionName;

        /// <summary>
        /// 设置的文档扩展名
        /// </summary>
        [Summary("设置的文档扩展名")]
        [EnableIf("customizeDocFileExtensionName")]
        [BilingualText("文档扩展名", "Doc Extension Name")]
        public string docFileExtensionName = ".md";

        /// <summary>
        /// 是否生成增量标识符
        /// </summary>
        [Summary("是否生成增量标识符")]
        [BilingualText("是否生成增量标识符", "Generate Identifier")]
        public bool generateIdentifier = true;

        #endregion
    }
}
