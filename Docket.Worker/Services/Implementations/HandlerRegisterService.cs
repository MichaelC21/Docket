using System;
using Docket.Worker.Handlers;
using Docket.Worker.Handlers.Data;
using Docket.Worker.Services.Interfaces;

namespace Docket.Worker.Services;

public class HandlerRegisterService : IHandlerRegisterService
{
    private readonly Dictionary<string, Func<string, IHandler>> _handlerFactories = new Dictionary<string, Func<string, IHandler>>
    {
        [nameof(SendEmailHandler<SendEmailHandlerData>)] = payload => new SendEmailHandler<SendEmailHandlerData>(payload),
        [nameof(GenerateReportHandler<GenerateReportHandlerData>)] = payload => new GenerateReportHandler<GenerateReportHandlerData>(payload),
        [nameof(CleanupTempFileHandler<CleanupTempFileHandlerData>)] = payload => new CleanupTempFileHandler<CleanupTempFileHandlerData>(payload),
        [nameof(ResizeImageHandler<ResizeImageHandlerData>)] = payload => new ResizeImageHandler<ResizeImageHandlerData>(payload),
    };

    public IHandler? GetHandler(string handlerName, string payload)
    {
        return _handlerFactories.TryGetValue(handlerName, out var factory) ? factory(payload) : null;
    }
}
