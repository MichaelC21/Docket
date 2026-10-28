using System;
using Docket.Worker.Models;
using Docket.Worker.Services.Interfaces;

namespace Docket.Worker.Services;

public class HandlerService : IHandlerService
{
    private readonly IHandlerRegisterService _handlerRegisterService;
    public HandlerService(IHandlerRegisterService handlerRegisterService)
    {
        _handlerRegisterService = handlerRegisterService;
    }
    public async Task<HandlerResult> Handle(string handlerName, string payload)
    {
        HandlerResult result = new HandlerResult(false, DateTimeOffset.UtcNow, "No handler found");
        try
        {
            var handler = _handlerRegisterService.GetHandler(handlerName, payload);

            if (handler == null)
            {
                return result;
            }

            await handler.Execute();
            result.Success = true;
            result.Message = "Handler executed successfully";
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        result.CompletedAt = DateTimeOffset.UtcNow;
        return result;
    }
}
