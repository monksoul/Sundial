// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Http;

namespace Sundial;

/// <summary>
/// Schedule UI 配置选项
/// </summary>
public sealed class ScheduleUIOptions
{
    /// <summary>
    /// UI 入口地址
    /// </summary>
    /// <remarks>需以 / 开头，结尾不包含 / </remarks>
    public string RequestPath { get; set; } = "/schedule";

    /// <summary>
    /// 启用目录浏览
    /// </summary>
    public bool EnableDirectoryBrowsing { get; set; } = false;

    /// <summary>
    /// 看板标题
    /// </summary>
    public string Title { get; set; } = "Schedule Dashboard";

    /// <summary>
    /// 生产环境关闭
    /// </summary>
    /// <remarks>默认 false</remarks>
    public bool DisableOnProduction { get; set; } = false;

    /// <summary>
    /// 二级虚拟目录
    /// </summary>
    /// <remarks>需以 / 开头，结尾不包含 / </remarks>
    public string VirtualPath { get; set; }

    /// <summary>
    /// 是否显示空触发器的作业信息
    /// </summary>
    public bool DisplayEmptyTriggerJobs { get; set; } = true;

    /// <summary>
    /// 是否显示页头
    /// </summary>
    public bool DisplayHead { get; set; } = true;

    /// <summary>
    /// 是否默认展开所有作业
    /// </summary>
    public bool DefaultExpandAllJobs { get; set; } = false;

    /// <summary>
    /// 登录逻辑
    /// </summary>
    public Func<string, string, HttpContext, Task<bool>> LoginHandle { get; set; } = (username, password, httpContext) =>
    {
        return Task.FromResult(username == "schedule" && string.IsNullOrWhiteSpace(password));
    };
}