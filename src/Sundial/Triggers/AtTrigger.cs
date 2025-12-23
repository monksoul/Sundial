// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Sundial;

/// <summary>
/// 指定具体时间触发的一次性作业触发器
/// </summary>
public class AtTrigger : Trigger
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="triggerTime">触发时间</param>
    public AtTrigger(string triggerTime)
    {
        TriggerTime = Convert.ToDateTime(triggerTime);
    }

    /// <summary>
    /// 触发时间
    /// </summary>
    public DateTime TriggerTime { get; }

    /// <summary>
    /// 计算下一个触发时间
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="DateTime"/></returns>
    public override DateTime? GetNextOccurrence(DateTime startAt)
    {
        // 获取配置的触发时间基准时间
        var nextRunTime = Penetrates.GetStandardDateTime(TriggerTime);

        // 处理当前时间大于触发时间基准时间
        if (startAt > nextRunTime)
        {
            // 归档
            Status = TriggerStatus.Archived;
            return null;
        }

        return nextRunTime;
    }

    /// <summary>
    /// 作业触发器转字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return $"<{JobId} {TriggerId}> {TriggerTime:yyyy-MM-dd HH:mm:ss} {(string.IsNullOrWhiteSpace(Description) ? string.Empty : $" {Description.GetMaxLengthString()}")} {NumberOfRuns}ts";
    }
}