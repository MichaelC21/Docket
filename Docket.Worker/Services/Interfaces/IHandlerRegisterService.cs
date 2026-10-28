using System;
using Docket.Worker.Handlers;

namespace Docket.Worker.Services.Interfaces;

public interface IHandlerRegisterService
{
    IHandler? GetHandler(string handlerName, string payload);
}
