using Sundial;
using Sundial.Samples;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSchedule(options =>
{
    options.AddJob<MyJob>(Triggers.Minutely()
     , Triggers.Period(5000)
     , Triggers.Hourly());
});

var app = builder.Build();

app.UseStaticFiles();
app.UseScheduleUI(options =>
{
    options.LoginConfig.AppSecret = "3f2d0ea0ef4df562719e70e41413658e";
    options.LoginConfig.DefaultUsername = "schedule";
});

app.Run();
