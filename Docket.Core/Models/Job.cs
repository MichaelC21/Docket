using System;
using System.ComponentModel.DataAnnotations;
using Docket.Core.Enums;

namespace Docket.Core.Models;

public class Job
{
    [Key]
    public required string JobID {get; set;}
    public required string Handler {get; set;}
    public required string Payload {get; set;}
    public JobStatus JobStatus {get; set;}
    public int Attempted {get; set;}
    public int MaxAttempts {get; set;}
    public DateTime NextRunUTC {get; set;}
    public DateTime CreatedAtUTC {get; set;}

}
