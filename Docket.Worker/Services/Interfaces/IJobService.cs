using System;
using Docket.Core.Entities;
using Docket.Persistence.Context;

namespace Docket.Worker.Services;

public interface IJobService
{
    Task<Job?> AcquireJob(DocketDBContext context, CancellationToken token = default);
}
