using Docket.Persistence.Context;
using Docket.Worker.Services;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

Env.NoClobber().TraversePath().Load();
var connectionString = Environment.GetEnvironmentVariable("DOCKET_CONNECTION_STRING");

if (String.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string is not set");
}

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddNLog();

builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddDbContext<DocketDBContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddHostedService<WorkerService>();

var app = builder.Build();

await app.RunAsync();