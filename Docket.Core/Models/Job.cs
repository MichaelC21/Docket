using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Docket.Core.Enums;

namespace Docket.Core.Models;

public class Job
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid JobID {get; private set;}
    public string Handler {get; private set;} = string.Empty;
    public string Payload {get; private set;}= string.Empty;
    public JobStatus Status {get; private set;}
    public int Attempted {get; private set;}
    public int MaxAttempts {get; private set;}
    public DateTimeOffset NextRunUTC {get; private set;}
    public DateTimeOffset CreatedAtUTC {get; private set;}

}
