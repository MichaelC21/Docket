using System;

namespace Docket.Worker.Handlers;

public interface IHandler
{
    public Task Execute();
}
