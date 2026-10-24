using Docket.Core.Enums;

namespace Docket.Core.Entities;

public class Job
{
    public Guid JobID {get; private set;}
    public string Handler {get; private set;}
    public string Payload {get; private set;}
    public JobStatus Status {get; private set;}
    public int Attempted {get; private set;}
    public int MaxAttempts {get; private set;}
    public DateTimeOffset NextRunUTC {get; private set;}
    public DateTimeOffset CreatedAtUTC {get; private set;}
    public string? ClaimedBy {get; private set;}
    public DateTimeOffset? ClaimedAt {get; private set;}

    public Job(string handler, string payload, int maxAttempts = 3)
    {
        ArgumentException.ThrowIfNullOrEmpty(handler);
        ArgumentNullException.ThrowIfNull(payload);

        if (maxAttempts <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxAttempts), "Max attempts must be greater than 0");
        }

        var now = DateTimeOffset.UtcNow;

        JobID = Guid.NewGuid();
        Handler = handler;
        Payload = payload;
        MaxAttempts = maxAttempts;
        NextRunUTC = now;
        CreatedAtUTC = now;
    }
}
