using System;
using Docket.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Docket.Core.Services;

public class JobService(DocketDBContext db)
{
    public async Task AquireJob()
    {
       var job = db.Jobs.FromSql(@"
        BEGIN TRANSACTION 
            BEGIN
                WITH job AS (
                    SELECT TOP (1) *
                    FROM dbo.Jobs j WITH (UPDLOCK, READPAST, ROWLOCK)
                    WHERE j.Status = 0 AND j.Attempted < j.MaxAttempts AND j.NextRunUTC <= GETUTCDATE()
                    ORDER BY j.CreatedAtUTC
                )
                UPDATE job
                SET Status = 1,
                    Attempted = Attempted + 1  

                OUTPUT inserted.*;
            END
        COMMIT TRANSACTION;
        ");
    }
}
