using System;
using Docket.Core.Enums;
using Docket.Core.Handlers;
using Docket.Core.Handlers.Data;
using Docket.Core.Services.Interfaces;

namespace Docket.Core.Services;

public class HandlerRegisterService : IHandlerRegisterService
{
    private readonly Dictionary<Handler, Func<string, IHandler>> _handlerFactories = new Dictionary<Handler, Func<string, IHandler>>
    {
        [Handler.SendEmailHandler] = payload => new SendEmailHandler<SendEmailHandlerData>(payload),
        [Handler.GenerateReportHandler] = payload => new GenerateRepoertHandler<GenerateRepoertHandlerData>(payload),
        [Handler.CleanupTempFilesHandler] = payload => new CleanupTempFileHandler<CleanupTempFileHandlerData>(payload),
        [Handler.ResizeImageHandler] = payload => new ResizeImageHandler<ResizeImageHandlerData>(payload),
    };

    public IHandler? GetHandler(Handler handlerType, string payload)
    {
        return _handlerFactories.TryGetValue(handlerType, out var factory) ? factory(payload) : null;
    }
}
