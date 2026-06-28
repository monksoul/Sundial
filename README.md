# Sundial

[![license](https://img.shields.io/badge/license-MIT-orange?cacheSeconds=10800)](https://gitee.com/dotnetchina/Sundial/blob/master/LICENSE) [![nuget](https://img.shields.io/nuget/v/Sundial.svg?cacheSeconds=10800)](https://www.nuget.org/packages/Sundial) [![dotNET China](https://img.shields.io/badge/organization-dotNET%20China-yellow?cacheSeconds=10800)](https://gitee.com/dotnetchina)

A fully-featured open-source distributed job scheduling system for .NET, suitable for applications of all sizes – from small apps to large enterprise systems.

![Sundial.drawio](https://gitee.com/dotnetchina/Sundial/raw/master/drawio/Sundial.drawio.png "Sundial.drawio.png")

## Installation

```powershell
dotnet add package Sundial
```

## Quick Start

We have many examples on our [homepage](https://furion.net/docs/job/). Here’s one to get you started:

1. Define a job by implementing the `IJob` interface:

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

2. Register the `AddSchedule` service and the job in `Startup.cs`:

```cs
services.AddSchedule(options =>
{
    options.AddJob<MyJob>(Triggers.PeriodSeconds(5)); // Execute every 5 seconds
});
```

3. Run the project:

```diff
info: 2022-12-05 19:32:56.3835407 +08:00 Monday L System.Logging.ScheduleService[0] #1
      Schedule hosted service is running.
info: 2022-12-05 19:32:56.3913451 +08:00 Monday L System.Logging.ScheduleService[0] #1
      Schedule hosted service is preloading...
info: 2022-12-05 19:32:56.4322887 +08:00 Monday L System.Logging.ScheduleService[0] #1
      The <job1_trigger1> trigger for scheduler of <job1> successfully appended to the schedule.
info: 2022-12-05 19:32:56.4347959 +08:00 Monday L System.Logging.ScheduleService[0] #1
      The scheduler of <job1> successfully appended to the schedule.
warn: 2022-12-05 19:32:56.4504555 +08:00 Monday L System.Logging.ScheduleService[0] #1
      Schedule hosted service preload completed, and a total of <1> schedulers are appended.
info: 2022-12-05 19:33:01.5100177 +08:00 Monday L MyJob[0] #13
+     <job1> [C] <job1 job1_trigger1> 5s 1ts 2022-12-05 19:33:01.395 -> 2022-12-05 19:33:06.428
info: 2022-12-05 19:33:06.4676792 +08:00 Monday L MyJob[0] #13
+     <job1> [C] <job1 job1_trigger1> 5s 2ts 2022-12-05 19:33:06.428 -> 2022-12-05 19:33:11.435
info: 2022-12-05 19:33:11.4460946 +08:00 Monday L MyJob[0] #16
+     <job1> [C] <job1 job1_trigger1> 5s 3ts 2022-12-05 19:33:11.435 -> 2022-12-05 19:33:16.412
```

`JobExecutionContext` overrides the `ToString()` method and provides the following output formats:

```bash
# Running format
<jobId> job description [C: Concurrent / S: Serial] <jobId triggerId> trigger string trigger description fireCount ts fireTime -> nextFireTime

# Stopped/terminated format
<jobId> job description [C: Concurrent / S: Serial] <jobId triggerId> trigger string trigger description fireCount ts fireTime [trigger termination status]
```

[More documentation](https://furion.net/docs/job/)

## Documentation

You can find the Sundial documentation on the [homepage](https://furion.net/docs/job/).

## Contribution

The primary purpose of this repository is to continue evolving the Sundial core, making it faster and easier to use. Development of Sundial takes place openly on [Gitee](https://gitee.com/dotnetchina/Sundial). We greatly appreciate community contributions for bug fixes and improvements.

## License

Sundial is licensed under the [MIT](./LICENSE) license.

[![](./assets/baiqian.svg)](https://baiqian.com)