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

namespace RunLab.AesirInspector.OdinWrapper.Editor
{
    /// <summary>
    /// 特性参数数据类，包含参数的返回类型、名称及描述。
    /// </summary>
    [Summary("特性参数数据类，包含参数的返回类型、名称及描述")]
    public class ParameterValue
    {
        readonly BilingualData _parameterDescriptionData;

        public ParameterValue(string returnType, string parameterName, string parameterDescription)
        {
            ReturnType = returnType;
            ParameterName = parameterName;
            ParameterDescription = parameterDescription;
            _parameterDescriptionData = BilingualData.Empty;
        }

        public ParameterValue(string returnType, string parameterName, BilingualData parameterDescriptionData)
        {
            ReturnType = returnType;
            ParameterName = parameterName;
            _parameterDescriptionData = parameterDescriptionData;
            ParameterDescription = string.Empty;
        }

        public ParameterValue() { }

        /// <summary>
        /// 参数返回类型。
        /// </summary>
        [Summary("参数返回类型")]
        public string ReturnType { get; set; }

        /// <summary>
        /// 参数名称。
        /// </summary>
        [Summary("参数名称")]
        public string ParameterName { get; set; }

        /// <summary>
        /// 参数描述（字符串形式）。
        /// </summary>
        [Summary("参数描述（字符串形式）")]
        public string ParameterDescription { get; set; }

        #region --- Public Methods ---

        /// <summary>
        /// 获取当前语言的参数描述。
        /// </summary>
        [Summary("获取当前语言的参数描述")]
        public string GetDescription() =>
            _parameterDescriptionData != BilingualData.Empty
                ? _parameterDescriptionData.GetCurrentOrFallback()
                : ParameterDescription;

        #endregion
    }
}
