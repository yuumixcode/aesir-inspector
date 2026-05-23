using System;

namespace RunLab.AesirInspector
{
    [Summary("只读双语数据结构体，存放中文和英文自动属性")]
    [Serializable]
    public readonly struct BilingualData : IEquatable<BilingualData>
    {
        [Summary("类似于 string.Empty，中文和英文均为空字符串")]
        public static BilingualData Empty => new BilingualData(string.Empty, string.Empty);

        public BilingualData(string chinese, string english)
        {
            Chinese = chinese;
            English = english;
        }

        public string Chinese { get; }
        public string English { get; }
        public bool Equals(BilingualData other) => Chinese == other.Chinese && English == other.English;

        [Summary("返回当前编辑器语言的文本，英文为空时回退到中文")]
        public string GetCurrentOrFallback()
        {
            if (AesirInspectorLanguageSettingsSO.CurrentIsEnglish && !string.IsNullOrWhiteSpace(English))
            {
                return English;
            }

            return Chinese;
        }

        public override string ToString() => GetCurrentOrFallback();

        [Summary("BilingualData 可直接隐式转换为 string")]
        public static implicit operator string(BilingualData data) => data.GetCurrentOrFallback();
    }
}
