using Docket.Core.Models;
using Docket.Core.Services.Interfaces;

namespace Docket.Tests.Fakes;

public class FakeHandlerService : IHandlerService
{
    private readonly HandlerResult _result;
    public string? ReceivedHandler { get; private set; }
    public string? ReceivedPayload { get; private set; }

    public FakeHandlerService(HandlerResult result)
    {
        _result = result;
    }

    public Task<HandlerResult> Handle(string handler, string payload)
    {
        ReceivedHandler = handler;
        ReceivedPayload = payload;
        return Task.FromResult(_result);
    }
}
