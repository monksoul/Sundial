# Sundial

[![license](https://img.shields.io/badge/license-MIT-orange?cacheSeconds=10800)](https://gitee.com/dotnetchina/Sundial/blob/master/LICENSE) [![nuget](https://img.shields.io/nuget/v/Sundial.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Sundial) [![dotNET China](https://img.shields.io/badge/organization-dotNET%20China-yellow?cacheSeconds=10800)](https://gitee.com/dotnetchina)

.NET 功能齐全的开源分布式作业调度系统，可从最小的应用程序到大型企业系统使用。

![Sundial.drawio](https://gitee.com/dotnetchina/Sundial/raw/master/drawio/Sundial.drawio.png "Sundial.drawio.png")

## 安装

```powershell
dotnet add package Sundial
```

## 快速入门

我们在[主页](https://furion.net/docs/job/)上有不少例子，这是让您入门的第一个：

1. 定义作业，并实现 `IJob` 接口：

```cs
public class MyJob : IJob
{
    private readonly ILogger<MyJob> _logger;
    public MyJob(ILogger<MyJob> logger)
    {
        _logger = logger;
    }

    public Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        _logger.LogInformation(context.ToString());
        return Task.CompletedTask;
    }
}
```

2. 在 `Startup.cs` 注册 `AddSchedule` 服务和作业：

```cs
services.AddSchedule(options =>
{
    options.AddJob<MyJob>(Triggers.PeriodSeconds(5)); // 5s 执行一次
});
```

3. 运行项目：

```diff
info: 2022-12-05 19:32:56.3835407 +08:00 星期一 L System.Logging.ScheduleService[0] #1
      Schedule hosted service is running.
info: 2022-12-05 19:32:56.3913451 +08:00 星期一 L System.Logging.ScheduleService[0] #1
      Schedule hosted service is preloading...
info: 2022-12-05 19:32:56.4322887 +08:00 星期一 L System.Logging.ScheduleService[0] #1
      The <job1_trigger1> trigger for scheduler of <job1> successfully appended to the schedule.
info: 2022-12-05 19:32:56.4347959 +08:00 星期一 L System.Logging.ScheduleService[0] #1
      The scheduler of <job1> successfully appended to the schedule.
warn: 2022-12-05 19:32:56.4504555 +08:00 星期一 L System.Logging.ScheduleService[0] #1
      Schedule hosted service preload completed, and a total of <1> schedulers are appended.
info: 2022-12-05 19:33:01.5100177 +08:00 星期一 L MyJob[0] #13
+     <job1> [C] <job1 job1_trigger1> 5s 1ts 2022-12-05 19:33:01.395 -> 2022-12-05 19:33:06.428
info: 2022-12-05 19:33:06.4676792 +08:00 星期一 L MyJob[0] #13
+     <job1> [C] <job1 job1_trigger1> 5s 2ts 2022-12-05 19:33:06.428 -> 2022-12-05 19:33:11.435
info: 2022-12-05 19:33:11.4460946 +08:00 星期一 L MyJob[0] #16
+     <job1> [C] <job1 job1_trigger1> 5s 3ts 2022-12-05 19:33:11.435 -> 2022-12-05 19:33:16.412
```

`JobExecutionContext` 重写了 `ToString()` 方法并提供以下几种格式：

```bash
# 持续运行格式
<作业Id> 作业描述 [并行C/串行S] <作业Id 触发器Id> 触发器字符串 触发器描述 触发次数ts 触发时间 -> 下一次触发时间

# 触发停止格式
<作业Id> 作业描述 [并行C/串行S] <作业Id 触发器Id> 触发器字符串 触发器描述 触发次数ts 触发时间 [触发器终止状态]
```

[更多文档](https://furion.net/docs/job/)

## 文档

您可以在[主页](https://furion.net/docs/job/)找到 Sundial 文档。

## 贡献

该存储库的主要目的是继续发展 Sundial 核心，使其更快、更易于使用。Sundial 的开发在 [Gitee](https://gitee.com/dotnetchina/Sundial) 上公开进行，我们感谢社区贡献错误修复和改进。

## 许可证

Sundial 采用 [MIT](./LICENSE) 开源许可证。

[![](./assets/baiqian.svg)](https://baiqian.com)