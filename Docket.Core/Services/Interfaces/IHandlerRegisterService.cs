using System;
using Docket.Core.Enums;
using Docket.Core.Handlers;

namespace Docket.Core.Services.Interfaces;

public interface IHandlerRegisterService
{
    IHandler? GetHandler(Handler handlerType, string payload);
}
