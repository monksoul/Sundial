// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Sundial;

/// <summary>
/// 作业执行记录持久上下文
/// </summary>
public sealed class PersistenceExecutionRecordContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="trigger">作业触发器</param>
    /// <param name="mode">触发模式</param>
    /// <param name="timeline">作业触发器运行记录</param>
    internal PersistenceExecutionRecordContext(JobDetail jobDetail
        , Trigger trigger
        , int mode
        , TriggerTimeline timeline)
    {
        JobId = jobDetail.JobId;
        JobDetail = jobDetail;
        TriggerId = trigger.TriggerId;
        Trigger = trigger;
        Mode = mode;

        Timeline = timeline;
    }

    /// <summary>
    /// 作业 Id
    /// </summary>
    public string JobId { get; }

    /// <summary>
    /// 作业信息
    /// </summary>
    public JobDetail JobDetail { get; }

    /// <summary>
    /// 作业触发器 Id
    /// </summary>
    public string TriggerId { get; }

    /// <summary>
    /// 作业触发器
    /// </summary>
    public Trigger Trigger { get; }

    /// <summary>
    /// 触发模式
    /// </summary>
    /// <remarks>默认为定时触发，0:定时，1:手动</remarks>
    public int Mode { get; }

    /// <summary>
    /// 作业触发器运行记录
    /// </summary>
    public TriggerTimeline Timeline { get; }

    /// <summary>
    /// 作业执行记录持久上下文转字符串输出
    /// </summary>
    /// <returns><see cref="String"/></returns>
    public override string ToString()
    {
        return $"{JobDetail} {Trigger}{(Mode == 1 ? " Manual" : string.Empty)} {Timeline.LastRunTime.ToFormatString()}{(Timeline.NextRunTime == null ? $" [{Timeline.Status}]" : $" -> {Timeline.NextRunTime.ToFormatString()}")} {Timeline.ElapsedTime}ms";
    }
}