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
    options.LoginConfig.DefaultUsername = "schedule";
});

app.Run();
