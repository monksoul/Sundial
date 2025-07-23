// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Sundial;

/// <summary>
/// 作业执行上下文基类
/// </summary>
public abstract class JobExecutionContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="trigger">作业触发器</param>
    /// <param name="occurrenceTime">作业计划触发时间</param>
    /// <param name="runId">作业触发器触发的唯一标识</param>
    /// <param name="serviceProvider">服务提供器</param>
    internal JobExecutionContext(JobDetail jobDetail
        , Trigger trigger
        , DateTime occurrenceTime
        , string runId
        , IServiceProvider serviceProvider)
    {
        JobId = jobDetail.JobId;
        TriggerId = trigger.TriggerId;
        JobDetail = jobDetail;
        Trigger = trigger;
        OccurrenceTime = occurrenceTime;
        RunId = runId;
        ServiceProvider = serviceProvider;
    }

    /// <summary>
    /// 服务提供器
    /// </summary>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// 作业 Id
    /// </summary>
    public string JobId { get; }

    /// <summary>
    /// 作业触发器 Id
    /// </summary>
    public string TriggerId { get; }

    /// <summary>
    /// 作业信息
    /// </summary>
    public JobDetail JobDetail { get; }

    /// <summary>
    /// 作业触发器
    /// </summary>
    public Trigger Trigger { get; }

    /// <summary>
    /// 作业计划触发时间
    /// </summary>
    public DateTime OccurrenceTime { get; }

    /// <summary>
    /// 作业触发器触发的唯一标识
    /// </summary>
    public string RunId { get; }

    /// <summary>
    /// 本次执行结果
    /// </summary>
    public string Result { get; set; }

    /// <summary>
    /// 触发模式
    /// </summary>
    /// <remarks>默认为定时触发，0:定时，1:手动</remarks>
    public int Mode { get; internal set; }

    /// <summary>
    /// 存储作业执行过程中需要传递的数据
    /// </summary>
    public IDictionary<string, object> Items { get; internal set; }

    /// <summary>
    /// 获取作业执行过程中传递的数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns><see cref="object"/></returns>
    public object GetItem(string key)
    {
        return Items[key];
    }

    /// <summary>
    /// 获取作业执行过程中传递的数据
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="key">键</param>
    /// <returns><typeparamref name="T"/></returns>
    public T GetItem<T>(string key)
    {
        return Items.TryGetValue(key, out var value) ? (T)value : default;
    }

    /// <summary>
    /// 获取作业执行过程中传递的数据
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <returns><see cref="IEnumerable{T}"/></returns>
    public IEnumerable<T> GetItems<T>()
    {
        return Items.Values.OfType<T>();
    }

    /// <summary>
    /// 获取作业执行过程中传递的数据
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <returns><typeparamref name="T"/></returns>
    public T GetItem<T>()
    {
        return GetItems<T>().FirstOrDefault();
    }

    /// <summary>
    /// 转换成 JSON 字符串
    /// </summary>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string ConvertToJSON(NamingConventions naming = NamingConventions.CamelCase)
    {
        return Penetrates.Write(writer =>
        {
            writer.WriteStartObject();

            // 输出 JobDetail
            writer.WritePropertyName(Penetrates.GetNaming(nameof(JobDetail), naming));
            writer.WriteRawValue(JobDetail.ConvertToJSON(naming));

            // 输出 Trigger
            writer.WritePropertyName(Penetrates.GetNaming(nameof(Trigger), naming));
            writer.WriteRawValue(Trigger.ConvertToJSON(naming));

            writer.WriteEndObject();
        });
    }

    /// <summary>
    /// 作业执行上下文转字符串输出输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return $"{JobDetail} {Trigger}{(Mode == 1 ? " Manual" : string.Empty)} {OccurrenceTime.ToFormatString()}{(Trigger.NextRunTime == null ? $" [{Trigger.Status}]" : $" -> {Trigger.NextRunTime.ToFormatString()}")}";
    }
}