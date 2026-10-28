using System;
using Docket.Worker.Models;

namespace Docket.Worker.Services.Interfaces;

public interface IHandlerService
{
    Task<HandlerResult> Handle(string handler, string payload);
}
