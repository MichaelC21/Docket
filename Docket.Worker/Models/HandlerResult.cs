using System;

namespace Docket.Worker.Models;

public class HandlerResult
{
    public bool Success {get; set;}
    public DateTimeOffset CompletedAt {get; set;}
    public string Message {get; set;}
    public HandlerResult(bool success, DateTimeOffset completedAt, string message)
    {
        Success = success;
        CompletedAt = completedAt;
        Message = message;
    }
}
