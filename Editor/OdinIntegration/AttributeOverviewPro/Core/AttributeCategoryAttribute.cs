using System;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("为特性面板 SO 标记所属的 Odin 特性分类")]
    [AttributeUsage(AttributeTargets.Class)]
    public class AttributeCategoryAttribute : Attribute
    {
        public AttributeCategoryAttribute(AesirAttributeCategory category) => Category = category;

        [Summary("所属特性分类")]
        public AesirAttributeCategory Category { get; }
    }
}
