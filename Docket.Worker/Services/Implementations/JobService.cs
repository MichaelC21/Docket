using System;
using Docket.Core.Entities;
using Docket.Core.Enums;
using Docket.Persistence.Context;
using Docket.Worker.Services;
using Microsoft.EntityFrameworkCore;

namespace Docket.Worker.Services;

public class JobService : IJobService
{
    public async Task<Job?> AcquireJob(DocketDBContext context, CancellationToken token = default)
    {
       var job = await context.Jobs.FromSql($"""
                WITH job AS (
                    SELECT TOP (1) *
                    FROM dbo.Jobs j WITH (UPDLOCK, READPAST, ROWLOCK)
                    WHERE j.Status = {JobStatus.Queued} AND j.Attempted < j.MaxAttempts AND j.NextRunUTC <= SYSUTCDATETIME()
                    ORDER BY j.CreatedAtUTC
                )
                UPDATE job
                SET Status = {JobStatus.Running},
                    Attempted = Attempted + 1  

                OUTPUT inserted.*;
        """).ToListAsync(token);

        return job.FirstOrDefault();
    }
}
