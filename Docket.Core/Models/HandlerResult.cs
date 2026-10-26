using System;

namespace Docket.Core.Models;

public class HandlerResult
{
    public bool Success {get; set;}
    public DateTime CompletedAt {get; set;}
    public string Message {get; set;}
    public HandlerResult(bool success, DateTime completedAt, string message)
    {
        Success = success;
        CompletedAt = completedAt;
        Message = message;
    }
}
