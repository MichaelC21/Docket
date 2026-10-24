using Docket.Core.Entities;
using Docket.Core.Enums;
using Docket.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Docket.Worker.Services;

public class JobService : IJobService
{
    private readonly DocketDBContext _context;

    public JobService(DocketDBContext context)
    {
        _context = context;
    }

    public async Task<Job?> AcquireJob(string workerIdentity, CancellationToken token = default)
    {
        var jobs = await _context.Jobs.FromSql($"""
                WITH job AS (
                    SELECT TOP (1) *
                    FROM dbo.Jobs j WITH (UPDLOCK, READPAST, ROWLOCK)
                    WHERE j.Status = {JobStatus.Queued} AND j.Attempted < j.MaxAttempts AND j.NextRunUTC <= SYSUTCDATETIME()
                    ORDER BY j.CreatedAtUTC
                )
                UPDATE job
                SET Status = {JobStatus.Running},
                    Attempted = Attempted + 1,
                    ClaimedBy = {workerIdentity},
                    ClaimedAt = SYSDATETIMEOFFSET()
                OUTPUT inserted.*;
        """).ToListAsync(token);

        return jobs.FirstOrDefault();
    }
}
