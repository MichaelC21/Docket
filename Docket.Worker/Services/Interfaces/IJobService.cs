using Docket.Core.Entities;

namespace Docket.Worker.Services;

public interface IJobService
{
    Task<Job?> AcquireJob(string workerIdentity, CancellationToken token = default);
}
