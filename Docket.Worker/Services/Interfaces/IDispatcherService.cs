using System;
using Docket.Worker.Models;

namespace Docket.Worker.Services.Interfaces;

public interface IDispatcherService
{
    Task<HandlerResult> DispatchJob(string handler, string payload);
}
