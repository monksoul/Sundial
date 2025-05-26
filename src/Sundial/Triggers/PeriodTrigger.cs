// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Sundial;

/// <summary>
/// 毫秒周期（间隔）作业触发器
/// </summary>
public class PeriodTrigger : Trigger
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="interval">间隔（毫秒）</param>
    public PeriodTrigger(long interval)
    {
        // 最低运行毫秒数为 100ms
        if (interval < 100) throw new InvalidOperationException($"The interval cannot be less than 100ms, but the value is <{interval}ms>.");

        Interval = interval;
    }

    /// <summary>
    /// 间隔（毫秒）
    /// </summary>
    protected long Interval { get; }

    /// <summary>
    /// 计算下一个触发时间
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="DateTime"/></returns>
    public override DateTime GetNextOccurrence(DateTime startAt)
    {
        // 获取间隔触发器周期计算基准时间
        var baseTime = StartTime == null ? startAt : StartTime.Value;

        // 处理基准时间大于当前时间
        if (baseTime > startAt)
        {
            return baseTime;
        }

        // 获取从基准时间开始到现在经过了多少个完整周期
        var elapsedMilliseconds = (startAt - baseTime).Ticks / TimeSpan.TicksPerMillisecond;
        var fullPeriods = elapsedMilliseconds / Interval;

        // 获取下一次执行时间
        var nextRunTime = baseTime.AddMilliseconds(fullPeriods * Interval);

        // 确保下一次执行时间是在当前时间之后
        if (startAt >= nextRunTime)
        {
            nextRunTime = nextRunTime.AddMilliseconds(Interval);
        }

        return nextRunTime;
    }

    /// <summary>
    /// 作业触发器转字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return $"<{JobId} {TriggerId}> {FormatDuration(Interval)}{(string.IsNullOrWhiteSpace(Description) ? string.Empty : $" {Description.GetMaxLengthString()}")} {NumberOfRuns}ts";
    }

    /// <summary>
    /// 将毫秒数格式化为更直观的时间单位字符串（如 ms, s, min, h, d, y）
    /// </summary>
    /// <param name="ms">毫秒</param>
    /// <returns><see cref="string"/></returns>
    private static string FormatDuration(long ms)
    {
        if (ms < 1000)
        {
            return $"{ms}ms";
        }

        var seconds = ms / 1000.0;
        if (seconds < 60)
        {
            var val = Math.Round(seconds * 10) / 10;
            if (val >= 60)
                return FormatDuration((long)(val * 1000));
            return val % 1 == 0 ? $"{val:F0}s" : $"{val:F1}s";
        }

        var minutes = seconds / 60;
        if (minutes < 60)
        {
            var val = Math.Round(minutes * 10) / 10;
            if (val >= 60)
                return FormatDuration((long)(val * 60 * 1000));
            return val % 1 == 0 ? $"{val:F0}min" : $"{val:F1}min";
        }

        var hours = minutes / 60;
        if (hours < 24)
        {
            var val = Math.Round(hours * 10) / 10;
            if (val >= 24)
                return FormatDuration((long)(val * 60 * 60 * 1000));
            return val % 1 == 0 ? $"{val:F0}h" : $"{val:F1}h";
        }

        var days = hours / 24;
        if (days < 365)
        {
            var val = Math.Round(days * 10) / 10;
            if (val >= 365)
                return FormatDuration((long)(val * 24 * 60 * 60 * 1000));
            return val % 1 == 0 ? $"{val:F0}d" : $"{val:F1}d";
        }

        var years = days / 365;
        var finalVal = Math.Round(years * 10) / 10;
        return finalVal % 1 == 0 ? $"{finalVal:F0}y" : $"{finalVal:F1}y";
    }
}