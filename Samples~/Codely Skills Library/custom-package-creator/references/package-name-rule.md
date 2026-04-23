# 包名规则

一个包有两个名称：Package Name 和 Display Name，前者用于注册包，后者是用户在 Editor 中看到的面向用户的名称。

Package Name = 正式包名。

Display Name = 显示名称。

显示名称使用首字母大写，不同单词之间使用空格分隔。

显示名称应简短，但应在一定程度上表明包中的内容。除此以外，Unity Package Manager 对显示名称没有任何限制。

正式名称必须遵循 Unity Package Manager 命名约定，也就是使用反向域名表示法。名称必须满足以下条件：

以 <域名扩展>.<公司名称>（例如，com.example 或 net.example）开头，即使公司或网站名称以数字开头也是如此。

如果您希望正式名称显示在编辑器中，则长度不能超过 50 个字符。如果包名称不需要出现在编辑器中，则 Unity Package Manager 会将名称长度限制为不超过 214 个字符。

只能包含小写字母、数字、连字符 (-)、下划线 (_) 和句点 (.)

要指示嵌套的命名空间，请为命名空间添加一个句点作为后缀。例如，“com.unity.2d.animation”和“com.unity.2d.ik”。

For example, “com.unity.2d.animation” and “com.unity.2d.ik” are the names of two Unity 2D packages, but a custom package developer at https://example.net might create a package named “net.example.physics”. Use your own company name in your package names. Do not use the “unity” prefix in your own package names.

注意：这些命名限制仅适用于包名本身，不需要与代码中的命名空间相匹配。例如，您可以使用 Project3dBase 作为名为 net.example.3d.base 的包中的命名空间。

官网原文网页：https://docs.unity.cn/cn/tuanjiemanual/Manual/cus-naming.html