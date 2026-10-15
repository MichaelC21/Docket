using System;
using System.Data.Common;
using Docket.Persistence.Context;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Docket.Web.Health;

public class DatabaseHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        Env.TraversePath().Load();
        var connectionString = Environment.GetEnvironmentVariable("DOCKET_CONNECTION_STRING") ?? "";

        DocketDBContext db = new DocketDBContext(new DbContextOptionsBuilder<DocketDBContext>()
                .UseSqlServer(connectionString)
                .Options);
        
        bool healthy = db.Database.CanConnect();

        if (healthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("Database healthy"));
        }
        return Task.FromResult(HealthCheckResult.Unhealthy("Could not connect to database"));
    }

}
