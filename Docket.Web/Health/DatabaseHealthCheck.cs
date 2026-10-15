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
    private DocketDBContext _db;
    public DatabaseHealthCheck(DocketDBContext dbContext)
    {
        _db = dbContext;
    }
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
      
        bool healthy = _db.Database.CanConnect();

        if (healthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("Database healthy"));
        }
        return Task.FromResult(HealthCheckResult.Unhealthy("Could not connect to database"));
    }

}
