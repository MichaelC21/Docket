using Docket.Worker.Handlers;

namespace Docket.Tests.Fakes;

public class FakeHandler : IHandler
{
    private readonly Exception? _exceptionToThrow;
    public bool Executed { get; private set; }

    public FakeHandler(Exception? exceptionToThrow = null)
    {
        _exceptionToThrow = exceptionToThrow;
    }

    public Task Execute()
    {
        Executed = true;
        if (_exceptionToThrow != null)
        {
            throw _exceptionToThrow;
        }
        return Task.CompletedTask;
    }
}
