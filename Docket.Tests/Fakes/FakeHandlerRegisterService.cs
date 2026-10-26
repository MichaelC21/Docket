using Docket.Core.Enums;
using Docket.Core.Handlers;
using Docket.Core.Services.Interfaces;

namespace Docket.Tests.Fakes;

public class FakeHandlerRegisterService : IHandlerRegisterService
{
    private readonly IHandler? _handler;

    public FakeHandlerRegisterService(IHandler? handler)
    {
        _handler = handler;
    }

    public IHandler? GetHandler(Handler handlerType, string payload) => _handler;
}
