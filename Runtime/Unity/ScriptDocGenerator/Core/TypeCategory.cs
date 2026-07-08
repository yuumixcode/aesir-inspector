namespace RunLab.AesirInspector
{
    [Summary("类型种类枚举")]
    public enum TypeCategory
    {
        [Summary("类")]
        Class,

        [Summary("结构体")]
        Struct,

        [Summary("接口")]
        Interface,

        [Summary("枚举")]
        Enum,

        [Summary("委托")]
        Delegate,

        [Summary("记录类型")]
        Record,

        [Summary("未知类型")]
        Unknown
    }
}
