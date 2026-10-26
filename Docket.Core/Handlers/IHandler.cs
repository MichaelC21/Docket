using System;

namespace Docket.Core.Handlers;

public interface IHandler
{
    public Task Execute();
}
