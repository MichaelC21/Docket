using System;
using Docket.Core.Models;
using Docket.Core.Services.Interfaces;
using Docket.Worker.Services.Interfaces;

namespace Docket.Worker.Services;

public class DispatcherService : IDispatcherService
{
    private readonly IHandlerService _handlerService;
    public DispatcherService(IHandlerService handlerService)
    {
        _handlerService = handlerService;
    }
    public async Task<HandlerResult> DispatchJob(string handler, string payload)
    {
        return await _handlerService.Handle(handler, payload);
    }
}
