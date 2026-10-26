using System;
using Docket.Core.Enums;
using Docket.Core.Models;
using Docket.Core.Services.Interfaces;

namespace Docket.Core.Services;

public class HandlerService : IHandlerService
{
    private readonly IHandlerRegisterService _handlerRegisterService;
    public HandlerService(IHandlerRegisterService handlerRegisterService)
    {
        _handlerRegisterService = handlerRegisterService;
    }
    public async Task<HandlerResult> Handle(string handlerName, string payload)
    {
        HandlerResult result = new HandlerResult(false, DateTime.Now, "No handler found"); 

        var handlerEnum = ParseHandlerType(handlerName);
        
        if (handlerEnum == null)
        {
            result.Message = "Handler Enum not set";
            return result;
        }

        var handler = _handlerRegisterService.GetHandler(handlerEnum.Value, payload);

        if (handler == null)
        {
            return result;
        }
        try
        {
            await handler.Execute();
            result.Success = true;
            result.Message = "Handler found";
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }

    private Handler? ParseHandlerType(string handlerName)
    {
        if (Enum.TryParse<Handler>(handlerName, out var result))
        {
            return result;
        }

        return null;
    }
}
