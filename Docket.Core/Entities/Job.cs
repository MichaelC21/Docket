using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Docket.Core.Enums;

namespace Docket.Core.Entities;

public class Job
{
    public Guid JobID {get; private set;}
    public string Handler {get; private set;} = string.Empty;
    public string Payload {get; private set;} = string.Empty;
    public JobStatus Status {get; private set;}
    public int Attempted {get; private set;}
    public int MaxAttempts {get; private set;}
    public DateTimeOffset NextRunUTC {get; private set;}
    public DateTimeOffset CreatedAtUTC {get; private set;}
    
    public Job(string handler, string payload, int maxAttempts = 3)
    {
        if (String.IsNullOrEmpty(handler) || String.IsNullOrEmpty(payload))
        {
            throw new NoNullAllowedException("Both handler and payload must be intialized");
        }

        if (maxAttempts <= 0)
        {
            throw new ArgumentOutOfRangeException("Max attempts must be greater than 0");
        }
        var now = DateTimeOffset.UtcNow;
        Handler = handler;
        Payload = payload;
        MaxAttempts = maxAttempts;
        NextRunUTC = now;
        CreatedAtUTC = now;
    }
}
