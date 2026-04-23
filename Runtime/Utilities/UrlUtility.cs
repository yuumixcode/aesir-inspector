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

namespace RunLab.AesirInspector
{
    /// <summary>
    /// URL 工具类
    /// </summary>
    [Summary("URL 工具类")]
    public static class UrlUtility
    {
        #region --- Public Methods ---

        /// <summary>
        /// 验证并规范化 URL，如果输入无效则返回回退 URL。
        /// </summary>
        [Summary("验证并规范化 URL，如果输入无效则返回回退 URL")]
        public static string ValidateAndNormalizeUrl(string inputUrl, string fallbackUrl)
        {
            inputUrl = inputUrl?.Trim() ?? "";

            if (string.IsNullOrEmpty(inputUrl))
            {
                return fallbackUrl;
            }

            if (Uri.TryCreate(inputUrl, UriKind.Absolute, out var uriResult) &&
                IsValidWebProtocol(uriResult.Scheme))
            {
                return uriResult.ToString();
            }

            return fallbackUrl;
        }

        #endregion

        #region Internal

        static bool IsValidWebProtocol(string scheme) =>
            string.Equals(scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);

        #endregion
    }
}
