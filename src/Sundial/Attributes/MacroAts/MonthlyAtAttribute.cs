// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Sundial;

/// <summary>
/// 每月特定天（午夜）开始作业触发器特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class MonthlyAtAttribute : CronAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="field">字段值</param>
    /// <param name="fields">字段值</param>
    public MonthlyAtAttribute(object field, params object[] fields)
        : base("@monthly", new[] { field }.Concat(fields).ToArray())
    {
    }
}