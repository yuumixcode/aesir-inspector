using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [Summary("存储 Type 的资源文件，提供给脚本文档生成工具复用，用户无需每次重新选择 Type")]
    public class TypesCacheSO : SerializedScriptableObject
    {
        [Summary("存储 Type 的列表")]
        public List<Type> Types;
    }
}
