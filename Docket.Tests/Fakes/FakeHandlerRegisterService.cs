using Docket.Worker.Handlers;
using Docket.Worker.Services.Interfaces;

namespace Docket.Tests.Fakes;

public class FakeHandlerRegisterService : IHandlerRegisterService
{
    private readonly IHandler? _handler;
    private readonly string _registeredHandlerName;

    public FakeHandlerRegisterService(IHandler? handler, string registeredHandlerName = "SendEmailHandler")
    {
        _handler = handler;
        _registeredHandlerName = registeredHandlerName;
    }

    public IHandler? GetHandler(string handlerName, string payload) => handlerName == _registeredHandlerName ? _handler : null;
}
